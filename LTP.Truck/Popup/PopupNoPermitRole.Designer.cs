namespace LTP.Truck.Popup
{
  partial class PopupNoPermitRole
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupNoPermitRole));
      pictureBox1 = new PictureBox();
      label27 = new Label();
      label1 = new Label();
      btnClose = new Common.Custom.RJButton();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      SuspendLayout();
      // 
      // pictureBox1
      // 
      pictureBox1.Image = Properties.Resources.iconRole;
      pictureBox1.Location = new Point(185, 31);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(154, 143);
      pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      pictureBox1.TabIndex = 0;
      pictureBox1.TabStop = false;
      // 
      // label27
      // 
      label27.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label27.AutoSize = true;
      label27.BackColor = Color.Transparent;
      label27.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label27.Location = new Point(137, 180);
      label27.Margin = new Padding(0);
      label27.Name = "label27";
      label27.Size = new Size(261, 23);
      label27.TabIndex = 1;
      label27.Text = "Bạn không có quyền truy cập";
      label27.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.Transparent;
      label1.Font = new Font("Roboto Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label1.Location = new Point(41, 221);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(450, 46);
      label1.TabIndex = 2;
      label1.Text = "Bạn không có quyền truy cập vào trang này\r\nVui lòng liên hệ quản trị hệ thống để được cấp quyền.\r\n";
      label1.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // btnClose
      // 
      btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnClose.BackColor = Color.Red;
      btnClose.BackgroundColor = Color.Red;
      btnClose.BorderColor = Color.PaleVioletRed;
      btnClose.BorderRadius = 4;
      btnClose.BorderSize = 0;
      btnClose.FlatAppearance.BorderSize = 0;
      btnClose.FlatStyle = FlatStyle.Flat;
      btnClose.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnClose.ForeColor = Color.White;
      btnClose.Image = (Image)resources.GetObject("btnClose.Image");
      btnClose.ImageAlign = ContentAlignment.MiddleLeft;
      btnClose.Location = new Point(185, 292);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(154, 59);
      btnClose.TabIndex = 3;
      btnClose.Text = "    Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      btnClose.Click += btnClose_Click;
      // 
      // PopupNoPermitRole
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.White;
      ClientSize = new Size(528, 363);
      ControlBox = false;
      Controls.Add(btnClose);
      Controls.Add(label1);
      Controls.Add(label27);
      Controls.Add(pictureBox1);
      Name = "PopupNoPermitRole";
      StartPosition = FormStartPosition.CenterParent;
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private Label label27;
    private Label label1;
    private Common.Custom.RJButton btnClose;
  }
}