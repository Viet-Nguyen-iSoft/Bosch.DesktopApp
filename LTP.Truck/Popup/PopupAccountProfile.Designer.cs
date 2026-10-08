namespace LTP.Truck.Popup
{
  partial class PopupAccountProfile
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      tableLayoutPanel3 = new TableLayoutPanel();
      rjButton1 = new Common.Custom.RJButton();
      rjButton2 = new Common.Custom.RJButton();
      tableLayoutPanel3.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(rjButton2, 0, 1);
      tableLayoutPanel3.Controls.Add(rjButton1, 0, 0);
      tableLayoutPanel3.Dock = DockStyle.Fill;
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(5);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.Padding = new Padding(5);
      tableLayoutPanel3.RowCount = 2;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel3.Size = new Size(337, 159);
      tableLayoutPanel3.TabIndex = 7;
      // 
      // rjButton1
      // 
      rjButton1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      rjButton1.BackColor = Color.FromArgb(51, 108, 181);
      rjButton1.BackgroundColor = Color.FromArgb(51, 108, 181);
      rjButton1.BorderColor = Color.PaleVioletRed;
      rjButton1.BorderRadius = 4;
      rjButton1.BorderSize = 0;
      rjButton1.FlatAppearance.BorderSize = 0;
      rjButton1.FlatStyle = FlatStyle.Flat;
      rjButton1.Font = new Font("Roboto", 14F, FontStyle.Bold);
      rjButton1.ForeColor = Color.White;
      rjButton1.ImageAlign = ContentAlignment.MiddleLeft;
      rjButton1.Location = new Point(8, 8);
      rjButton1.Name = "rjButton1";
      rjButton1.Padding = new Padding(10, 0, 0, 0);
      rjButton1.Size = new Size(321, 68);
      rjButton1.TabIndex = 6;
      rjButton1.Text = "Thông tin";
      rjButton1.TextAlign = ContentAlignment.MiddleLeft;
      rjButton1.TextColor = Color.White;
      rjButton1.UseVisualStyleBackColor = false;
      // 
      // rjButton2
      // 
      rjButton2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      rjButton2.BackColor = Color.FromArgb(51, 108, 181);
      rjButton2.BackgroundColor = Color.FromArgb(51, 108, 181);
      rjButton2.BorderColor = Color.PaleVioletRed;
      rjButton2.BorderRadius = 4;
      rjButton2.BorderSize = 0;
      rjButton2.FlatAppearance.BorderSize = 0;
      rjButton2.FlatStyle = FlatStyle.Flat;
      rjButton2.Font = new Font("Roboto", 14F, FontStyle.Bold);
      rjButton2.ForeColor = Color.White;
      rjButton2.ImageAlign = ContentAlignment.MiddleLeft;
      rjButton2.Location = new Point(8, 82);
      rjButton2.Name = "rjButton2";
      rjButton2.Padding = new Padding(10, 0, 0, 0);
      rjButton2.Size = new Size(321, 69);
      rjButton2.TabIndex = 7;
      rjButton2.Text = "Đổi mật khẩu";
      rjButton2.TextAlign = ContentAlignment.MiddleLeft;
      rjButton2.TextColor = Color.White;
      rjButton2.UseVisualStyleBackColor = false;
      // 
      // PopupAccountProfile
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(337, 159);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupAccountProfile";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel3.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private Common.Custom.RJButton rjButton2;
    private Common.Custom.RJButton rjButton1;
  }
}