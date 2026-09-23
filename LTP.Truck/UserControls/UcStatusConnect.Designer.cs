namespace LTP.Truck.UserControls
{
  partial class UcStatusConnect
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
      pictureBox1 = new PictureBox();
      lbTitle = new Label();
      tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = Color.Red;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
      tableLayoutPanel1.Controls.Add(lbTitle, 1, 0);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Size = new Size(270, 50);
      tableLayoutPanel1.TabIndex = 0;
      // 
      // pictureBox1
      // 
      pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      pictureBox1.Image = Properties.Resources.icon_status_connect;
      pictureBox1.Location = new Point(5, 5);
      pictureBox1.Margin = new Padding(5);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(40, 40);
      pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      pictureBox1.TabIndex = 0;
      pictureBox1.TabStop = false;
      // 
      // lbTitle
      // 
      lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitle.AutoSize = true;
      lbTitle.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTitle.ForeColor = Color.White;
      lbTitle.Location = new Point(53, 0);
      lbTitle.Name = "lbTitle";
      lbTitle.Size = new Size(214, 50);
      lbTitle.TabIndex = 1;
      lbTitle.Text = "Mất kết nối";
      lbTitle.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // UcStatusConnect
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(tableLayoutPanel1);
      Name = "UcStatusConnect";
      Size = new Size(270, 50);
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private PictureBox pictureBox1;
    private Label lbTitle;
  }
}
