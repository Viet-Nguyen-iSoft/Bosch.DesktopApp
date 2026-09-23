using LTP.Truck.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTP.Truck.UserControls
{
  public partial class UcItem : UserControl
  {
    public UcItem()
    {
      InitializeComponent();
      CustomUI();
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

    public string Title
    {
      set
      {
        lbTitle.Text = value;
      }
    }
    public string Value
    {
      set
      {
        lbValue.Text = value;
      }
    }
    public string Time
    {
      set
      {
        lbTime.Text = value;
      }
    }

    public bool VisibleTime
    {
      set
      {
        lbTime.Visible = value;
      }
    }

  }
}
