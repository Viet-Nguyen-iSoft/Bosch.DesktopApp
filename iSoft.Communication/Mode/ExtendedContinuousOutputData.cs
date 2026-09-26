using System.Globalization;
using System.Text;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Mode
{
  /// <summary>
  /// Giải mã frame Continuous Extended Output của Mettler Toledo IND570.
  /// Frame đầy đủ gồm 24 byte, hoặc 25 byte khi có checksum.
  /// </summary>
  public sealed class ExtendedContinuousOutputData
  {
    private const byte StartOfHeader = 0x01;
    private const byte CarriageReturn = 0x0D;
    private const int FrameLength = 24;
    private const int FrameLengthWithChecksum = 25;

    public double IndicatedWeight { get; private set; }
    public double TareWeight { get; private set; }
    public UnitOfWeight Unit { get; private set; }
    public ActiveWeighingStatus ActiveWeighingStatus { get; private set; }
    public bool IsNetWeight { get; private set; }
    public bool IsDataValid { get; private set; }

    public static bool TryDecode(
      byte[]? receivedBytes,
      out ExtendedContinuousOutputData? output)
    {
      output = null;
      if (receivedBytes == null || receivedBytes.Length == 0)
        return false;

      int startIndex = Array.IndexOf(receivedBytes, StartOfHeader);
      if (startIndex < 0)
        return false;

      int availableLength = receivedBytes.Length - startIndex;
      bool hasCarriageReturn =
        availableLength >= FrameLength &&
        receivedBytes[startIndex + FrameLength - 1] == CarriageReturn;

      // Một số connection dùng ReadTo("\r"), vì vậy CR đã bị loại khỏi dữ liệu.
      int frameLength = hasCarriageReturn ? FrameLength : FrameLength - 1;
      if (availableLength < frameLength)
        return false;

      var frame = receivedBytes
        .Skip(startIndex)
        .Take(Math.Min(availableLength, FrameLengthWithChecksum))
        .ToArray();

      if (hasCarriageReturn && frame.Length >= FrameLengthWithChecksum &&
          !HasValidChecksum(frame))
      {
        return false;
      }

      byte status1 = frame[2];
      byte status2 = frame[3];
      byte status3 = frame[4];

      bool isDataValid = (status3 & 0x01) == 0;
      if (!TryParseWeight(frame, 6, 9, out double indicatedWeight) ||
          !TryParseWeight(frame, 15, 8, out double tareWeight))
      {
        return false;
      }

      // Status Byte 2, bit 6: dữ liệu trọng lượng được mở rộng x10.
      if ((status2 & 0x40) != 0)
      {
        indicatedWeight *= 10;
        tareWeight *= 10;
      }

      output = new ExtendedContinuousOutputData
      {
        IndicatedWeight = indicatedWeight,
        TareWeight = tareWeight,
        Unit = DecodeUnit(status1),
        ActiveWeighingStatus = DecodeStatus(status1, status3),
        IsNetWeight = (status2 & 0x01) != 0,
        IsDataValid = isDataValid,
      };

      return isDataValid;
    }

    private static bool TryParseWeight(
      byte[] frame,
      int startIndex,
      int length,
      out double value)
    {
      value = 0;
      if (frame.Length < startIndex + length)
        return false;

      string text = Encoding.ASCII
        .GetString(frame, startIndex, length)
        .Trim();

      return double.TryParse(
        text,
        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
        CultureInfo.InvariantCulture,
        out value);
    }

    private static UnitOfWeight DecodeUnit(byte status1)
    {
      return (status1 & 0x0F) switch
      {
        1 => UnitOfWeight.Pounds,
        2 => UnitOfWeight.Kilograms,
        3 => UnitOfWeight.Grams,
        8 => UnitOfWeight.Ounces,
        _ => UnitOfWeight.None,
      };
    }

    private static ActiveWeighingStatus DecodeStatus(byte status1, byte status3)
    {
      if ((status3 & 0x04) != 0)
        return ActiveWeighingStatus.Overload;

      if ((status3 & 0x02) != 0)
        return ActiveWeighingStatus.Underload;

      if ((status3 & 0x01) != 0)
        return ActiveWeighingStatus.Default;

      return (status1 & 0x40) != 0
        ? ActiveWeighingStatus.Motion
        : ActiveWeighingStatus.Stable;
    }

    private static bool HasValidChecksum(byte[] frame)
    {
      int sum = 0;
      for (int i = 0; i < FrameLength; i++)
        sum += frame[i];

      byte expected = (byte)((-(sum & 0x7F)) & 0x7F);
      return expected == frame[FrameLength];
    }
  }
}
