namespace LTP.Truck.UserControls
{
  partial class UcItem
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      tableLayoutPanel1 = new TableLayoutPanel();
      lbTime = new Label();
      lbValue = new Label();
      lbTitle = new Label();
      tableLayoutPanel1.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = Color.PeachPuff;
      tableLayoutPanel1.ColumnCount = 1;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(lbTime, 0, 2);
      tableLayoutPanel1.Controls.Add(lbValue, 0, 1);
      tableLayoutPanel1.Controls.Add(lbTitle, 0, 0);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.Padding = new Padding(5);
      tableLayoutPanel1.RowCount = 4;
      tableLayoutPanel1.RowStyles.Add(new RowStyle());
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle());
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.Size = new Size(257, 144);
      tableLayoutPanel1.TabIndex = 0;
      // 
      // lbTime
      // 
      lbTime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTime.AutoSize = true;
      lbTime.Font = new Font("Roboto", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lbTime.Location = new Point(8, 104);
      lbTime.Name = "lbTime";
      lbTime.Size = new Size(241, 25);
      lbTime.TabIndex = 5;
      lbTime.Text = "...";
      lbTime.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // lbValue
      // 
      lbValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbValue.AutoSize = true;
      lbValue.Font = new Font("Roboto", 24.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbValue.Location = new Point(8, 28);
      lbValue.Name = "lbValue";
      lbValue.Size = new Size(241, 76);
      lbValue.TabIndex = 4;
      lbValue.Text = "0.000";
      lbValue.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // lbTitle
      // 
      lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitle.AutoSize = true;
      lbTitle.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lbTitle.Location = new Point(8, 5);
      lbTitle.Name = "lbTitle";
      lbTitle.Size = new Size(241, 23);
      lbTitle.TabIndex = 3;
      lbTitle.Text = "Label";
      lbTitle.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // UcItem
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.FromArgb(223, 239, 255);
      Controls.Add(tableLayoutPanel1);
      Name = "UcItem";
      Size = new Size(257, 144);
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private Label lbValue;
    private Label lbTitle;
    private Label lbTime;
  }
}
