using LTP.Truck.Custom;

namespace LTP.Truck.Controls
{
  /// <summary>Shared palette for the blue industrial-dashboard visual language.</summary>
  internal static class AppTheme
  {
    public static readonly Color Navy = Color.FromArgb(42, 67, 105);
    public static readonly Color Primary = Color.FromArgb(48, 108, 177);
    public static readonly Color PrimaryHover = Color.FromArgb(39, 89, 148);
    public static readonly Color PrimaryPressed = Color.FromArgb(31, 72, 121);
    public static readonly Color Surface = Color.FromArgb(232, 238, 245);
    public static readonly Color SoftBlue = Color.FromArgb(216, 230, 244);
    public static readonly Color Border = Color.FromArgb(190, 205, 221);
    public static readonly Color Text = Color.FromArgb(35, 48, 65);
    public static readonly Color Success = Color.FromArgb(55, 174, 99);
    public static readonly Color Danger = Color.FromArgb(169, 32, 16);

    public static void Apply(Control root)
    {
      ApplyControl(root);
      foreach (Control child in root.Controls)
        Apply(child);
    }

    private static void ApplyControl(Control control)
    {
      Color originalBackColor = control.BackColor;

      if (originalBackColor == Color.FromArgb(236, 236, 236))
        control.BackColor = Surface;
      else if (originalBackColor == Color.FromArgb(223, 239, 255))
        control.BackColor = SoftBlue;
      else if (originalBackColor == Color.FromArgb(199, 199, 199))
      {
        control.BackColor = Navy;
        control.ForeColor = Color.White;
      }
      else if (originalBackColor == Color.FromArgb(64, 107, 177)
        || originalBackColor == Color.FromArgb(51, 108, 181))
      {
        control.BackColor = Primary;
        control.ForeColor = Color.White;
      }

      if (control is RJButton button && button.BackColor == Primary)
      {
        button.BackgroundColor = Primary;
        button.TextColor = Color.White;
        button.BorderColor = Primary;
        button.FlatAppearance.MouseOverBackColor = PrimaryHover;
        button.FlatAppearance.MouseDownBackColor = PrimaryPressed;
      }

      if (control is RJTextBox textBox)
      {
        textBox.BorderColor = Border;
        textBox.BorderFocusColor = Primary;
      }

      if (control is DataGridView grid)
        ApplyGrid(grid);
    }

    private static void ApplyGrid(DataGridView grid)
    {
      grid.BackgroundColor = Surface;
      grid.BorderStyle = BorderStyle.None;
      grid.GridColor = Color.White;
      grid.EnableHeadersVisualStyles = false;
      grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
      grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
      grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = PrimaryHover;
      grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
      grid.DefaultCellStyle.BackColor = Color.White;
      grid.DefaultCellStyle.ForeColor = Text;
      grid.DefaultCellStyle.SelectionBackColor = SoftBlue;
      grid.DefaultCellStyle.SelectionForeColor = Text;
      grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 244, 249);
      grid.RowHeadersDefaultCellStyle.BackColor = Navy;
      grid.RowHeadersDefaultCellStyle.ForeColor = Color.White;
    }
  }
}
