using System.Globalization;
using System.Text;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Mode
{
  /// <summary>
  /// Standard Continuous Output của Mettler Toledo IND570.
  /// Frame gồm 17 byte, hoặc 18 byte khi bật checksum.
  /// </summary>
  public class StandardContinuousOutputData
  {
    private const int FrameLength = 17;
    private const int FrameLengthWithChecksum = 18;

    public byte[] DataBytes = Array.Empty<byte>();
    public readonly byte StartChar = 0x02;
    public StatusA StatusA = new();
    public StatusB StatusB = new();
    public StatusC StatusC = new();
    public double? IndicatedWeight;
    public double? TareWeight;
    public UnitOfWeight Unit;
    public readonly byte CarriageReturnChar = 0x0D;
    public bool CheckSumEnabled;

    public ActiveWeighingStatus ActiveWeighingStatus =>
      StatusB.OutOfRange == OutOfRange.True
        ? ActiveWeighingStatus.Overload
        : StatusB.ActiveWeighingStatus;

    public static StandardContinuousOutputData Decode(
      byte[] dataBytes,
      bool validateChecksum = true)
    {
      if (!TryDecode(dataBytes, out var output, validateChecksum) || output == null)
      {
        throw new FormatException(
          "Dữ liệu Standard Continuous phải là frame IND570 hợp lệ gồm 17 hoặc 18 byte.");
      }

      return output;
    }

    /// <summary>
    /// Giải mã dữ liệu Standard Continuous Output của IND570.
    /// Frame hợp lệ gồm 17 byte, hoặc 18 byte khi có checksum.
    /// </summary>
    /// <returns>
    /// Dữ liệu cân đã giải mã; trả về null nếu frame không hợp lệ.
    /// </returns>
    public static StandardContinuousOutputData? DecodeCTN(
      byte[]? dataBytes,
      bool validateChecksum = true)
    {
      return TryDecode(dataBytes, out var output, validateChecksum)
        ? output
        : null;
    }

    public static bool TryDecode(
      byte[]? dataBytes,
      out StandardContinuousOutputData? output,
      bool validateChecksum = true)
    {
      output = null;
      if (dataBytes == null ||
          (dataBytes.Length != FrameLength &&
           dataBytes.Length != FrameLengthWithChecksum) ||
          dataBytes[0] != 0x02 ||
          dataBytes[16] != 0x0D)
      {
        return false;
      }

      bool hasChecksum = dataBytes.Length == FrameLengthWithChecksum;
      if (hasChecksum && validateChecksum && !HasValidChecksum(dataBytes))
        return false;

      var statusA = StatusA.Decode(dataBytes[1]);
      var statusB = StatusB.Decode(dataBytes[2]);
      var statusC = StatusC.Decode(dataBytes[3]);

      if (!TryDecodeWeight(dataBytes, 4, statusA, statusB.Sign,
            out double indicatedWeight) ||
          !TryDecodeWeight(dataBytes, 10, statusA, Sign.Positive,
            out double tareWeight))
      {
        return false;
      }

      if (statusC.ExpandDataOrNormal == ExpandData.x10)
      {
        indicatedWeight *= 10;
        tareWeight *= 10;
      }

      output = new StandardContinuousOutputData
      {
        DataBytes = dataBytes.ToArray(),
        StatusA = statusA,
        StatusB = statusB,
        StatusC = statusC,
        IndicatedWeight = indicatedWeight,
        TareWeight = tareWeight,
        Unit = DecodeUnit(statusB, statusC),
        CheckSumEnabled = hasChecksum,
      };

      return true;
    }

    public bool CheckSumOK()
    {
      return DataBytes.Length == FrameLengthWithChecksum &&
             HasValidChecksum(DataBytes);
    }

    private static bool TryDecodeWeight(
      byte[] dataBytes,
      int startIndex,
      StatusA statusA,
      Sign sign,
      out double weight)
    {
      weight = 0;
      string rawValue = Encoding.ASCII
        .GetString(dataBytes, startIndex, 6)
        .Trim();

      if (rawValue.Length == 0 ||
          !rawValue.All(char.IsDigit) ||
          !double.TryParse(rawValue, NumberStyles.None,
            CultureInfo.InvariantCulture, out weight))
      {
        return false;
      }

      weight *= statusA.DecimalPointLocation switch
      {
        DecimalPointLocation.XXXXX00 => 100d,
        DecimalPointLocation.XXXXX0 => 10d,
        DecimalPointLocation.XXXXXX => 1d,
        DecimalPointLocation.XXXXX_X => 0.1d,
        DecimalPointLocation.XXXX_XX => 0.01d,
        DecimalPointLocation.XXX_XXX => 0.001d,
        DecimalPointLocation.XX_XXXX => 0.0001d,
        DecimalPointLocation.X_XXXXX => 0.00001d,
        _ => 1d,
      };

      if (sign == Sign.Negative)
        weight = -weight;

      return true;
    }

    private static UnitOfWeight DecodeUnit(StatusB statusB, StatusC statusC)
    {
      return statusC.WeightDescription switch
      {
        WeightDescription.SelectedByStatusByteB => statusB.UnitOfWeight,
        WeightDescription.Grams => UnitOfWeight.Grams,
        WeightDescription.Ounces => UnitOfWeight.Ounces,
        _ => UnitOfWeight.None,
      };
    }

    private static bool HasValidChecksum(byte[] dataBytes)
    {
      return CalculateChecksum(dataBytes.AsSpan(0, FrameLength)) ==
             dataBytes[FrameLength];
    }

    private static byte CalculateChecksum(ReadOnlySpan<byte> data)
    {
      int sum = 0;
      foreach (byte value in data)
        sum += value;

      return (byte)((-(sum & 0x7F)) & 0x7F);
    }

    public static byte[] Encode(StandardContinuousOutputData data)
    {
      ArgumentNullException.ThrowIfNull(data);

      var result = new byte[FrameLengthWithChecksum];
      result[0] = data.StartChar;
      result[1] = StatusA.Encode(data.StatusA);
      result[2] = StatusB.Encode(data.StatusB);
      result[3] = StatusC.Encode(data.StatusC);
      EncodeWeight(result, 4, data.IndicatedWeight ?? 0, data.StatusA);
      EncodeWeight(result, 10, data.TareWeight ?? 0, data.StatusA);
      result[16] = data.CarriageReturnChar;
      result[17] = CalculateChecksum(result.AsSpan(0, FrameLength));
      return result;
    }

    private static void EncodeWeight(
      byte[] destination,
      int startIndex,
      double weight,
      StatusA statusA)
    {
      double divisor = statusA.DecimalPointLocation switch
      {
        DecimalPointLocation.XXXXX00 => 100d,
        DecimalPointLocation.XXXXX0 => 10d,
        DecimalPointLocation.XXXXXX => 1d,
        DecimalPointLocation.XXXXX_X => 0.1d,
        DecimalPointLocation.XXXX_XX => 0.01d,
        DecimalPointLocation.XXX_XXX => 0.001d,
        DecimalPointLocation.XX_XXXX => 0.0001d,
        DecimalPointLocation.X_XXXXX => 0.00001d,
        _ => 1d,
      };

      string rawValue = Math.Abs(Math.Round(weight / divisor))
        .ToString("0", CultureInfo.InvariantCulture)
        .PadLeft(6, ' ');

      if (rawValue.Length > 6)
        throw new ArgumentOutOfRangeException(nameof(weight));

      Encoding.ASCII.GetBytes(rawValue, 0, 6, destination, startIndex);
    }
  }
}
