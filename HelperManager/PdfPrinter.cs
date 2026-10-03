using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HelperManager
{
  public static class PdfPrinter
  {
    /// <summary>
    /// In file PDF đến máy in chỉ định.
    /// </summary>
    /// <param name="pdfPath">Đường dẫn file PDF</param>
    /// <param name="printerName">Tên máy in</param>
    public static void PrintPdf(string pdfPath, string printerName)
    {
      if (string.IsNullOrWhiteSpace(pdfPath))
        throw new ArgumentException("Đường dẫn file PDF không hợp lệ.", nameof(pdfPath));

      if (!File.Exists(pdfPath))
        throw new FileNotFoundException("Không tìm thấy file PDF cần in.", pdfPath);

      if (string.IsNullOrWhiteSpace(printerName))
        throw new ArgumentException("Tên máy in không hợp lệ.", nameof(printerName));

      var printerSettings = new PrinterSettings
      {
        PrinterName = printerName
      };

      if (!printerSettings.IsValid)
        throw new InvalidPrinterException(printerSettings);

      // Render và gửi PDF trực tiếp tới Windows print queue. Không dùng shell verb
      // "printto", vì verb này chỉ tồn tại khi máy đã cài và đăng ký PDF viewer.
      var printer = new global::PdfiumPrinter.PdfPrinter(printerName);
      printer.Print(pdfPath, documentName: Path.GetFileName(pdfPath));
    }

    public static void MergePdf(List<string> pathsPdf, string pathFilePdfOut)
    {
      try
      {
        if (pathsPdf == null || pathsPdf.Count == 0)
          throw new ArgumentException("Danh sách PDF rỗng.");

        // Tạo thư mục nếu chưa có
        var dir = Path.GetDirectoryName(pathFilePdfOut);
        if (!string.IsNullOrWhiteSpace(dir))
          Directory.CreateDirectory(dir);

        using (PdfDocument outputDocument = new PdfDocument())
        {
          foreach (string pdf in pathsPdf)
          {
            if (!File.Exists(pdf))
              continue;

            using (PdfDocument inputDocument = PdfReader.Open(pdf, PdfDocumentOpenMode.Import))
            {
              for (int i = 0; i < inputDocument.PageCount; i++)
              {
                outputDocument.AddPage(inputDocument.Pages[i]);
              }
            }
          }

          outputDocument.Save(pathFilePdfOut);
        }
      }
      catch (Exception)
      {
        throw;
      }
    }



  }
}
