namespace LTP.Truck.Forms
{
  partial class FrmWaiting
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWaiting));
      tableLayoutPanel1 = new TableLayoutPanel();
      tableLayoutPanel2 = new TableLayoutPanel();
      lbVersion = new Label();
      lbStatusHID = new Label();
      tableLayoutPanel3 = new TableLayoutPanel();
      tableLayoutPanel4 = new TableLayoutPanel();
      ucPanelLogin1 = new LTP.Truck.UserControls.UcPanelLogin();
      tableLayoutPanel5 = new TableLayoutPanel();
      pictureBox1 = new PictureBox();
      label2 = new Label();
      lbTitle = new Label();
      btnMenu = new PictureBox();
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel4.SuspendLayout();
      tableLayoutPanel5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      ((System.ComponentModel.ISupportInitialize)btnMenu).BeginInit();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.ColumnCount = 1;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 2;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel1.Size = new Size(1886, 1041);
      tableLayoutPanel1.TabIndex = 1;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.BackColor = Color.Red;
      tableLayoutPanel2.ColumnCount = 3;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel2.Controls.Add(lbVersion, 0, 0);
      tableLayoutPanel2.Controls.Add(lbStatusHID, 2, 0);
      tableLayoutPanel2.Location = new Point(0, 991);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(1886, 50);
      tableLayoutPanel2.TabIndex = 21;
      // 
      // lbVersion
      // 
      lbVersion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbVersion.AutoSize = true;
      lbVersion.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold);
      lbVersion.ForeColor = Color.White;
      lbVersion.Location = new Point(0, 0);
      lbVersion.Margin = new Padding(0);
      lbVersion.Name = "lbVersion";
      lbVersion.Padding = new Padding(10, 0, 0, 0);
      lbVersion.Size = new Size(866, 50);
      lbVersion.TabIndex = 14;
      lbVersion.Text = "Copyright @ 2026 i-Soft JSC. All rights reserved.  | Version 1.0.0";
      lbVersion.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // lbStatusHID
      // 
      lbStatusHID.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbStatusHID.AutoSize = true;
      lbStatusHID.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Italic);
      lbStatusHID.ForeColor = Color.Red;
      lbStatusHID.Location = new Point(1556, 0);
      lbStatusHID.Name = "lbStatusHID";
      lbStatusHID.Padding = new Padding(0, 0, 30, 0);
      lbStatusHID.Size = new Size(327, 50);
      lbStatusHID.TabIndex = 21;
      lbStatusHID.Text = "Mất kết nối đọc thẻ HID";
      lbStatusHID.TextAlign = ContentAlignment.MiddleRight;
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel3.ColumnCount = 5;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
      tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 1, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 3, 0);
      tableLayoutPanel3.Controls.Add(btnMenu, 4, 0);
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 1;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Size = new Size(1886, 991);
      tableLayoutPanel3.TabIndex = 1;
      // 
      // tableLayoutPanel4
      // 
      tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel4.ColumnCount = 1;
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.Controls.Add(ucPanelLogin1, 0, 1);
      tableLayoutPanel4.Location = new Point(153, 3);
      tableLayoutPanel4.Name = "tableLayoutPanel4";
      tableLayoutPanel4.RowCount = 3;
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 49.99999F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle());
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50.0000076F));
      tableLayoutPanel4.Size = new Size(568, 985);
      tableLayoutPanel4.TabIndex = 0;
      // 
      // ucPanelLogin1
      // 
      ucPanelLogin1.Account = "admin";
      ucPanelLogin1.Location = new Point(3, 247);
      ucPanelLogin1.Name = "ucPanelLogin1";
      ucPanelLogin1.Password = "admin";
      ucPanelLogin1.Size = new Size(562, 490);
      ucPanelLogin1.TabIndex = 0;
      // 
      // tableLayoutPanel5
      // 
      tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel5.ColumnCount = 1;
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Controls.Add(pictureBox1, 0, 1);
      tableLayoutPanel5.Controls.Add(label2, 0, 3);
      tableLayoutPanel5.Controls.Add(lbTitle, 0, 2);
      tableLayoutPanel5.Location = new Point(877, 3);
      tableLayoutPanel5.Name = "tableLayoutPanel5";
      tableLayoutPanel5.RowCount = 5;
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 49.99998F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle());
      tableLayoutPanel5.RowStyles.Add(new RowStyle());
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50.0000153F));
      tableLayoutPanel5.Size = new Size(855, 985);
      tableLayoutPanel5.TabIndex = 2;
      // 
      // pictureBox1
      // 
      pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
      pictureBox1.Location = new Point(200, 373);
      pictureBox1.Margin = new Padding(200, 10, 200, 10);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(455, 110);
      pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      pictureBox1.TabIndex = 0;
      pictureBox1.TabStop = false;
      pictureBox1.Click += pictureBox1_Click;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.Font = new Font("Segoe UI", 24.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label2.Location = new Point(3, 575);
      label2.Name = "label2";
      label2.Size = new Size(849, 45);
      label2.TabIndex = 2;
      label2.Text = "Weight Logging System";
      label2.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // lbTitle
      // 
      lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitle.AutoSize = true;
      lbTitle.Font = new Font("Segoe UI", 45.25F, FontStyle.Bold);
      lbTitle.Location = new Point(3, 493);
      lbTitle.Name = "lbTitle";
      lbTitle.Size = new Size(849, 82);
      lbTitle.TabIndex = 1;
      lbTitle.Text = "HỆ THỐNG CÂN XE TẢI";
      lbTitle.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // btnMenu
      // 
      btnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      btnMenu.Image = (Image)resources.GetObject("btnMenu.Image");
      btnMenu.Location = new Point(1798, 10);
      btnMenu.Margin = new Padding(3, 10, 10, 3);
      btnMenu.Name = "btnMenu";
      btnMenu.Size = new Size(78, 72);
      btnMenu.SizeMode = PictureBoxSizeMode.StretchImage;
      btnMenu.TabIndex = 20;
      btnMenu.TabStop = false;
      btnMenu.Click += btnMenu_Click;
      // 
      // FrmWaiting
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1886, 1041);
      Controls.Add(tableLayoutPanel1);
      Name = "FrmWaiting";
      Text = "FrmWaiting";
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel2.PerformLayout();
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel4.ResumeLayout(false);
      tableLayoutPanel5.ResumeLayout(false);
      tableLayoutPanel5.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      ((System.ComponentModel.ISupportInitialize)btnMenu).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private Label lbVersion;
    private Label lbStatusHID;
    private TableLayoutPanel tableLayoutPanel3;
    private TableLayoutPanel tableLayoutPanel4;
    private TableLayoutPanel tableLayoutPanel5;
    private PictureBox pictureBox1;
    private Label label2;
    private Label lbTitle;
    private PictureBox btnMenu;
    private UserControls.UcPanelLogin ucPanelLogin1;
  }
}