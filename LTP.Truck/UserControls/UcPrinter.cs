using LTP.Truck.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTP.Truck.UserControls
{
  public partial class UcPrinter : UserControl
  {
    private bool _isSelected;

    public event EventHandler? PrinterSelected;

    public UcPrinter()
    {
      InitializeComponent();
      CustomUI();

      Cursor = Cursors.Hand;
      Click += SelectPrinter;
      tableLayoutPanel1.Click += SelectPrinter;
      lbTitle.Click += SelectPrinter;
    }

    public string Title
    {
      get
      {
        return lbTitle.Text;
      }
      set
      {
        lbTitle.Text = value;
      }
    }

    public bool IsSelected
    {
      get => _isSelected;
      set
      {
        _isSelected = value;
        tableLayoutPanel1.BackColor = value
          ? Color.FromArgb(51, 108, 181)
          : SystemColors.GradientActiveCaption;
        lbTitle.ForeColor = value ? Color.White : SystemColors.ControlText;
      }
    }

    private void SelectPrinter(object? sender, EventArgs e)
    {
      PrinterSelected?.Invoke(this, EventArgs.Empty);
    }

    private void CustomUI()
    {
      ElipseControl elipseControl = new ElipseControl();
      elipseControl.TargetControl = this;
      elipseControl.CornerRadius = 20;

      ElipseControl elipseControl01 = new ElipseControl();
      elipseControl01.TargetControl = tableLayoutPanel1;
      elipseControl01.CornerRadius = 20;
    }
  }
}
