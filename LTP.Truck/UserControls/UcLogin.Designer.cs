namespace LTP.Truck.UserControls
{
  partial class UcLogin
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
      lbAccount = new Label();
      tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = Color.LightGray;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 59F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
      tableLayoutPanel1.Controls.Add(lbAccount, 1, 0);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Size = new Size(275, 61);
      tableLayoutPanel1.TabIndex = 1;
      // 
      // pictureBox1
      // 
      pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      pictureBox1.Image = Properties.Resources.icon_btn_user;
      pictureBox1.InitialImage = Properties.Resources.icon_btn_user;
      pictureBox1.Location = new Point(5, 5);
      pictureBox1.Margin = new Padding(5);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(49, 51);
      pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      pictureBox1.TabIndex = 0;
      pictureBox1.TabStop = false;
      // 
      // lbAccount
      // 
      lbAccount.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbAccount.AutoSize = true;
      lbAccount.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbAccount.ForeColor = Color.Black;
      lbAccount.Location = new Point(62, 0);
      lbAccount.Name = "lbAccount";
      lbAccount.Size = new Size(210, 61);
      lbAccount.TabIndex = 1;
      lbAccount.Text = "Login";
      lbAccount.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // UcLogin
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(tableLayoutPanel1);
      Name = "UcLogin";
      Size = new Size(275, 61);
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private PictureBox pictureBox1;
    private Label lbAccount;
  }
}
