namespace LTP.Truck.Forms
{
  partial class PopupApplyVersionNew
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupApplyVersionNew));
      tableLayoutPanel1 = new TableLayoutPanel();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnDownload = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      btnApply = new Common.Custom.RJButton();
      tableLayoutPanel3 = new TableLayoutPanel();
      lbTitle = new Label();
      picLoading = new PictureBox();
      tableLayoutPanel4 = new TableLayoutPanel();
      lbVersionCurrent = new Label();
      label1 = new Label();
      pictureBox2 = new PictureBox();
      txtCommit = new TextBox();
      progressBar1 = new ProgressBar();
      tableLayoutPanel5 = new TableLayoutPanel();
      lbVersion = new Label();
      label2 = new Label();
      pictureBox3 = new PictureBox();
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)picLoading).BeginInit();
      tableLayoutPanel4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
      tableLayoutPanel5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = Color.White;
      tableLayoutPanel1.ColumnCount = 3;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 10);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 2);
      tableLayoutPanel1.Controls.Add(txtCommit, 1, 6);
      tableLayoutPanel1.Controls.Add(progressBar1, 1, 8);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 1, 4);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 12;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel1.Size = new Size(635, 590);
      tableLayoutPanel1.TabIndex = 0;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.ColumnCount = 4;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel2.Controls.Add(btnDownload, 2, 0);
      tableLayoutPanel2.Controls.Add(btnClose, 3, 0);
      tableLayoutPanel2.Controls.Add(btnApply, 1, 0);
      tableLayoutPanel2.Location = new Point(10, 515);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(615, 65);
      tableLayoutPanel2.TabIndex = 6;
      // 
      // btnDownload
      // 
      btnDownload.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnDownload.BackColor = Color.Green;
      btnDownload.BackgroundColor = Color.Green;
      btnDownload.BorderColor = Color.PaleVioletRed;
      btnDownload.BorderRadius = 4;
      btnDownload.BorderSize = 0;
      btnDownload.FlatAppearance.BorderSize = 0;
      btnDownload.FlatStyle = FlatStyle.Flat;
      btnDownload.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnDownload.ForeColor = Color.White;
      btnDownload.Image = (Image)resources.GetObject("btnDownload.Image");
      btnDownload.ImageAlign = ContentAlignment.MiddleLeft;
      btnDownload.Location = new Point(292, 3);
      btnDownload.Name = "btnDownload";
      btnDownload.Padding = new Padding(10, 0, 0, 0);
      btnDownload.Size = new Size(170, 59);
      btnDownload.TabIndex = 0;
      btnDownload.Text = "       Tải xuống";
      btnDownload.TextColor = Color.White;
      btnDownload.UseVisualStyleBackColor = false;
      btnDownload.Click += btnDownload_Click;
      // 
      // btnClose
      // 
      btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnClose.BackColor = Color.Tomato;
      btnClose.BackgroundColor = Color.Tomato;
      btnClose.BorderColor = Color.PaleVioletRed;
      btnClose.BorderRadius = 4;
      btnClose.BorderSize = 0;
      btnClose.FlatAppearance.BorderSize = 0;
      btnClose.FlatStyle = FlatStyle.Flat;
      btnClose.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnClose.ForeColor = Color.White;
      btnClose.Image = (Image)resources.GetObject("btnClose.Image");
      btnClose.ImageAlign = ContentAlignment.MiddleLeft;
      btnClose.Location = new Point(468, 3);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(144, 59);
      btnClose.TabIndex = 1;
      btnClose.Text = "   Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      btnClose.Click += btnClose_Click;
      // 
      // btnApply
      // 
      btnApply.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnApply.BackColor = Color.FromArgb(51, 108, 181);
      btnApply.BackgroundColor = Color.FromArgb(51, 108, 181);
      btnApply.BorderColor = Color.PaleVioletRed;
      btnApply.BorderRadius = 4;
      btnApply.BorderSize = 0;
      btnApply.FlatAppearance.BorderSize = 0;
      btnApply.FlatStyle = FlatStyle.Flat;
      btnApply.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnApply.ForeColor = Color.White;
      btnApply.Image = (Image)resources.GetObject("btnApply.Image");
      btnApply.ImageAlign = ContentAlignment.MiddleLeft;
      btnApply.Location = new Point(78, 3);
      btnApply.Name = "btnApply";
      btnApply.Padding = new Padding(10, 0, 0, 0);
      btnApply.Size = new Size(208, 59);
      btnApply.TabIndex = 2;
      btnApply.Text = "     Cập nhật ngay";
      btnApply.TextColor = Color.White;
      btnApply.UseVisualStyleBackColor = false;
      btnApply.Visible = false;
      btnApply.Click += btnApply_Click;
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel3.BackColor = Color.White;
      tableLayoutPanel3.ColumnCount = 2;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.52459F));
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.4754095F));
      tableLayoutPanel3.Controls.Add(lbTitle, 0, 0);
      tableLayoutPanel3.Controls.Add(picLoading, 1, 0);
      tableLayoutPanel3.Location = new Point(10, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 1;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel3.Size = new Size(615, 60);
      tableLayoutPanel3.TabIndex = 8;
      // 
      // lbTitle
      // 
      lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitle.AutoSize = true;
      lbTitle.BackColor = Color.White;
      lbTitle.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTitle.Location = new Point(0, 0);
      lbTitle.Margin = new Padding(0);
      lbTitle.Name = "lbTitle";
      lbTitle.Size = new Size(544, 60);
      lbTitle.TabIndex = 7;
      lbTitle.Text = "Cập nhật phiên bản phần mềm";
      lbTitle.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // picLoading
      // 
      picLoading.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      picLoading.Image = Properties.Resources.Loading_icon;
      picLoading.Location = new Point(547, 3);
      picLoading.Name = "picLoading";
      picLoading.Size = new Size(65, 54);
      picLoading.SizeMode = PictureBoxSizeMode.StretchImage;
      picLoading.TabIndex = 8;
      picLoading.TabStop = false;
      // 
      // tableLayoutPanel4
      // 
      tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel4.BackColor = Color.White;
      tableLayoutPanel4.ColumnCount = 3;
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.Controls.Add(lbVersionCurrent, 2, 0);
      tableLayoutPanel4.Controls.Add(label1, 1, 0);
      tableLayoutPanel4.Controls.Add(pictureBox2, 0, 0);
      tableLayoutPanel4.Location = new Point(10, 70);
      tableLayoutPanel4.Margin = new Padding(0);
      tableLayoutPanel4.Name = "tableLayoutPanel4";
      tableLayoutPanel4.RowCount = 1;
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.Size = new Size(615, 60);
      tableLayoutPanel4.TabIndex = 9;
      // 
      // lbVersionCurrent
      // 
      lbVersionCurrent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbVersionCurrent.AutoSize = true;
      lbVersionCurrent.BackColor = Color.White;
      lbVersionCurrent.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbVersionCurrent.Location = new Point(280, 0);
      lbVersionCurrent.Margin = new Padding(0);
      lbVersionCurrent.Name = "lbVersionCurrent";
      lbVersionCurrent.Size = new Size(335, 60);
      lbVersionCurrent.TabIndex = 9;
      lbVersionCurrent.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.White;
      label1.Font = new Font("Roboto", 14F);
      label1.Location = new Point(60, 0);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(220, 60);
      label1.TabIndex = 7;
      label1.Text = "   Phiên bản hiện tại:";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // pictureBox2
      // 
      pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      pictureBox2.Image = Properties.Resources.icon_ver_old;
      pictureBox2.Location = new Point(3, 3);
      pictureBox2.Name = "pictureBox2";
      pictureBox2.Size = new Size(54, 54);
      pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
      pictureBox2.TabIndex = 8;
      pictureBox2.TabStop = false;
      // 
      // txtCommit
      // 
      txtCommit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      txtCommit.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtCommit.Location = new Point(13, 213);
      txtCommit.Multiline = true;
      txtCommit.Name = "txtCommit";
      txtCommit.Size = new Size(609, 259);
      txtCommit.TabIndex = 11;
      // 
      // progressBar1
      // 
      progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      progressBar1.Location = new Point(13, 488);
      progressBar1.Name = "progressBar1";
      progressBar1.Size = new Size(609, 14);
      progressBar1.TabIndex = 12;
      // 
      // tableLayoutPanel5
      // 
      tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel5.BackColor = Color.White;
      tableLayoutPanel5.ColumnCount = 3;
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Controls.Add(lbVersion, 2, 0);
      tableLayoutPanel5.Controls.Add(label2, 1, 0);
      tableLayoutPanel5.Controls.Add(pictureBox3, 0, 0);
      tableLayoutPanel5.Location = new Point(10, 140);
      tableLayoutPanel5.Margin = new Padding(0);
      tableLayoutPanel5.Name = "tableLayoutPanel5";
      tableLayoutPanel5.RowCount = 1;
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Size = new Size(615, 60);
      tableLayoutPanel5.TabIndex = 10;
      // 
      // lbVersion
      // 
      lbVersion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbVersion.AutoSize = true;
      lbVersion.BackColor = Color.White;
      lbVersion.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbVersion.Location = new Point(280, 0);
      lbVersion.Margin = new Padding(0);
      lbVersion.Name = "lbVersion";
      lbVersion.Size = new Size(335, 60);
      lbVersion.TabIndex = 9;
      lbVersion.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.BackColor = Color.White;
      label2.Font = new Font("Roboto", 14F);
      label2.Location = new Point(60, 0);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(220, 60);
      label2.TabIndex = 7;
      label2.Text = "   Phiên bản mới nhất:";
      label2.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // pictureBox3
      // 
      pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      pictureBox3.Image = Properties.Resources.icon_ver_new;
      pictureBox3.Location = new Point(3, 3);
      pictureBox3.Name = "pictureBox3";
      pictureBox3.Size = new Size(54, 54);
      pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
      pictureBox3.TabIndex = 8;
      pictureBox3.TabStop = false;
      // 
      // PopupApplyVersionNew
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(635, 590);
      ControlBox = false;
      Controls.Add(tableLayoutPanel1);
      Name = "PopupApplyVersionNew";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)picLoading).EndInit();
      tableLayoutPanel4.ResumeLayout(false);
      tableLayoutPanel4.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
      tableLayoutPanel5.ResumeLayout(false);
      tableLayoutPanel5.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private Common.Custom.RJButton btnDownload;
    private Common.Custom.RJButton btnClose;
    private TableLayoutPanel tableLayoutPanel3;
    private Label lbTitle;
    private PictureBox picLoading;
    private Common.Custom.RJButton btnApply;
    private TableLayoutPanel tableLayoutPanel4;
    private Label label1;
    private PictureBox pictureBox2;
    private TableLayoutPanel tableLayoutPanel5;
    private Label label2;
    private PictureBox pictureBox3;
    private TextBox txtCommit;
    private Label lbVersionCurrent;
    private Label lbVersion;
    private ProgressBar progressBar1;
  }
}