using System;
using System.Drawing;
using System.Linq;
using System.Drawing.Printing;
using System.Windows.Forms;
using LTP.Truck.UserControls;

namespace LTP.Truck.Popup
{
  public partial class PopupListPrinter : Form
  {
    private readonly string? _preferredPrinterName;
    private UcPrinter? _selectedPrinter;

    public string? SelectedPrinterName => _selectedPrinter?.Title;
    public int PrintCopies => Decimal.ToInt32(numPrintCopies.Value);

    public PopupListPrinter(string? preferredPrinterName = null)
    {
      _preferredPrinterName = preferredPrinterName;
      InitializeComponent();

      flowLayoutPanelPrinter.AutoScroll = true;
      flowLayoutPanelPrinter.FlowDirection = FlowDirection.LeftToRight;
      flowLayoutPanelPrinter.WrapContents = true;
      flowLayoutPanelPrinter.Padding = new Padding(5);
      btnConfirm.Enabled = false;

      Load += PopupListPrinter_Load;
      flowLayoutPanelPrinter.ClientSizeChanged += (_, _) => ResizePrinterItems();
      btnConfirm.Click += btnConfirm_Click;
      btnClose.Click += btnClose_Click;
    }

    private void PopupListPrinter_Load(object? sender, EventArgs e)
    {
      LoadInstalledPrinters();
    }

    private void LoadInstalledPrinters()
    {
      flowLayoutPanelPrinter.SuspendLayout();
      try
      {
        flowLayoutPanelPrinter.Controls.Clear();

        var printerNames = PrinterSettings.InstalledPrinters
          .Cast<string>()
          .OrderBy(name => name, StringComparer.CurrentCultureIgnoreCase)
          .ToList();

        foreach (string printerName in printerNames)
        {
          var printer = new UcPrinter
          {
            Title = printerName,
            Height = 88,
            Margin = new Padding(5)
          };
          printer.PrinterSelected += Printer_PrinterSelected;
          flowLayoutPanelPrinter.Controls.Add(printer);

          if (!string.IsNullOrWhiteSpace(_preferredPrinterName) &&
              string.Equals(printerName, _preferredPrinterName,
                StringComparison.OrdinalIgnoreCase))
          {
            SelectPrinter(printer);
          }
        }

        if (_selectedPrinter == null)
        {
          var firstPrinter = flowLayoutPanelPrinter.Controls
            .OfType<UcPrinter>()
            .FirstOrDefault();

          if (firstPrinter != null)
            SelectPrinter(firstPrinter);
        }

        if (printerNames.Count == 0)
        {
          flowLayoutPanelPrinter.Controls.Add(new Label
          {
            AutoSize = false,
            Font = new Font("Roboto", 14F),
            ForeColor = Color.DimGray,
            Height = 80,
            Text = "Không tìm thấy máy in trên máy tính.",
            TextAlign = ContentAlignment.MiddleCenter,
            Width = Math.Max(100, flowLayoutPanelPrinter.ClientSize.Width - 20)
          });
        }

        ResizePrinterItems();
      }
      finally
      {
        flowLayoutPanelPrinter.ResumeLayout();
      }
    }

    private void Printer_PrinterSelected(object? sender, EventArgs e)
    {
      if (sender is UcPrinter printer)
        SelectPrinter(printer);
    }

    private void SelectPrinter(UcPrinter printer)
    {
      if (_selectedPrinter != null)
        _selectedPrinter.IsSelected = false;

      _selectedPrinter = printer;
      _selectedPrinter.IsSelected = true;
      btnConfirm.Enabled = true;
    }

    private void ResizePrinterItems()
    {
      const int columnCount = 2;
      int horizontalSpace = flowLayoutPanelPrinter.Padding.Horizontal + 20;
      int itemWidth = Math.Max(100,
        (flowLayoutPanelPrinter.ClientSize.Width - horizontalSpace) / columnCount);

      foreach (UcPrinter printer in flowLayoutPanelPrinter.Controls.OfType<UcPrinter>())
        printer.Width = itemWidth;
    }

    private void btnConfirm_Click(object? sender, EventArgs e)
    {
      if (_selectedPrinter == null)
        return;

      DialogResult = DialogResult.OK;
      Close();
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }
  }
}
