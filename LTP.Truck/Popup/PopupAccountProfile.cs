using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LTP.Truck.Controls;

namespace LTP.Truck.Popup
{
  public partial class PopupAccountProfile : Form
  {
    public PopupAccountProfile()
    {
      InitializeComponent();
      btnChangePass.Click += BtnChangePass_Click;
    }

    private void BtnChangePass_Click(object? sender, EventArgs e)
    {
      var currentUser = AppCore.Ins._userCurrent;
      if (currentUser == null)
        return;

      var popupOwner = Owner;
      Close();

      using var popup = new PopupChangePasswork(currentUser);
      popup.ShowDialog(popupOwner);
    }
  }
}
