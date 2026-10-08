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
  public partial class UcLogin : UserControl
  {
    public string Account
    {
      get => lbAccount.Text;
      set => lbAccount.Text = string.IsNullOrWhiteSpace(value) ? "Login" : value;
    }

    public UcLogin()
    {
      InitializeComponent();
      CustomUI();

      Cursor = Cursors.Hand;
      tableLayoutPanel1.Cursor = Cursors.Hand;
      pictureBox1.Cursor = Cursors.Hand;
      lbAccount.Cursor = Cursors.Hand;

      tableLayoutPanel1.Click += ForwardClick;
      pictureBox1.Click += ForwardClick;
      lbAccount.Click += ForwardClick;
    }

    private void ForwardClick(object? sender, EventArgs e)
    {
      OnClick(e);
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
