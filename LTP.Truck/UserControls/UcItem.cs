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
    private Font? _titleFont;
    private Font? _valueFont;
    private Font? _timeFont;

    public UcItem()
    {
      InitializeComponent();
      CustomUI();
      Disposed += (_, _) =>
      {
        _titleFont?.Dispose();
        _valueFont?.Dispose();
        _timeFont?.Dispose();
      };
    }

    [Category("Appearance")]
    [Description("Cỡ chữ tiêu đề, tính bằng point.")]
    [DefaultValue(14.25F)]
    public float TitleFontSize
    {
      get => lbTitle.Font.SizeInPoints;
      set => SetLabelFontSize(lbTitle, value, ref _titleFont);
    }

    [Category("Appearance")]
    [Description("Cỡ chữ giá trị, tính bằng point.")]
    [DefaultValue(18F)]
    public float ValueFontSize
    {
      get => lbValue.Font.SizeInPoints;
      set => SetLabelFontSize(lbValue, value, ref _valueFont);
    }

    [Category("Appearance")]
    [Description("Cỡ chữ thời gian, tính bằng point.")]
    [DefaultValue(12F)]
    public float TimeFontSize
    {
      get => lbTime.Font.SizeInPoints;
      set => SetLabelFontSize(lbTime, value, ref _timeFont);
    }

    private static void SetLabelFontSize(Label label, float size, ref Font? ownedFont)
    {
      if (!float.IsFinite(size) || size <= 0)
        throw new ArgumentOutOfRangeException(nameof(size), "Cỡ chữ phải lớn hơn 0.");

      if (label.Font.SizeInPoints == size)
        return;

      var currentFont = label.Font;
      var newFont = new Font(
        currentFont.FontFamily, size, currentFont.Style, GraphicsUnit.Point,
        currentFont.GdiCharSet, currentFont.GdiVerticalFont);
      var previousFont = ownedFont;
      label.Font = newFont;
      ownedFont = newFont;
      previousFont?.Dispose();
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
