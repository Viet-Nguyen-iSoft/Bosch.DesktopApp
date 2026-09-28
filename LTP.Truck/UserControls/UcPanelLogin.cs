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
  public partial class UcPanelLogin : UserControl
  {
    public event EventHandler? OnSendLogin;
    public UcPanelLogin()
    {
      InitializeComponent();
      CustomUI();
    }

    public string Account
    {
      get
      {
        return txtAccount.Texts;
      }
      set
      {
        txtAccount.Texts = value;
      }
    }

    public string Password
    {
      get
      {
        return txtPass.Texts;
      }
      set
      {
        txtPass.Texts = value;
      }
    }

    private void CustomUI()
    {
      ElipseControl elipseControl = new ElipseControl();
      elipseControl.TargetControl = this;
      elipseControl.CornerRadius = 50;

      ElipseControl elipseControl01 = new ElipseControl();
      elipseControl01.TargetControl = tableLayoutPanel1;
      elipseControl01.CornerRadius = 50;
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
      OnSendLogin?.Invoke(this, e);
    }

    private bool _hide { get; set; } = true;
    private void btnHide_Click(object sender, EventArgs e)
    {
      _hide = !_hide;
      btnHide.Image = _hide ? Properties.Resources.icon_hide : Properties.Resources.icon_unhide;
      txtPass.PasswordChar = _hide;
    }
  }
}
