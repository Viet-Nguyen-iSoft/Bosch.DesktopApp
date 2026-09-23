namespace LTP.Truck.Forms
{
  partial class PopupFilter
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupFilter));
      label21 = new Label();
      cbbStatus = new ComboBox();
      labelType = new Label();
      cbbType = new ComboBox();
      label1 = new Label();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      SuspendLayout();
      // 
      // label21
      // 
      label21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label21.AutoSize = true;
      label21.BackColor = Color.Transparent;
      label21.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label21.Location = new Point(9, 76);
      label21.Margin = new Padding(0);
      label21.Name = "label21";
      label21.Size = new Size(108, 27);
      label21.TabIndex = 30;
      label21.Text = "Trạng thái";
      label21.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // cbbStatus
      // 
      cbbStatus.Anchor = AnchorStyles.Left;
      cbbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbStatus.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      cbbStatus.FormattingEnabled = true;
      cbbStatus.Items.AddRange(new object[] { "Tất cả", "Chưa hoàn thành", "Hoàn thành" });
      cbbStatus.Location = new Point(126, 72);
      cbbStatus.Name = "cbbStatus";
      cbbStatus.Size = new Size(314, 33);
      cbbStatus.TabIndex = 31;
      // 
      // labelType
      // 
      labelType.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      labelType.AutoSize = true;
      labelType.BackColor = Color.Transparent;
      labelType.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      labelType.Location = new Point(9, 141);
      labelType.Margin = new Padding(0);
      labelType.Name = "labelType";
      labelType.Size = new Size(79, 27);
      labelType.TabIndex = 32;
      labelType.Text = "Dữ liệu";
      labelType.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // cbbType
      // 
      cbbType.Anchor = AnchorStyles.Left;
      cbbType.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbType.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      cbbType.FormattingEnabled = true;
      cbbType.Items.AddRange(new object[] { "Tất cả", "Dữ liệu hiện hữu", "Dữ liệu xóa" });
      cbbType.Location = new Point(126, 140);
      cbbType.Name = "cbbType";
      cbbType.Size = new Size(314, 33);
      cbbType.TabIndex = 33;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.Transparent;
      label1.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label1.Location = new Point(9, 9);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(118, 27);
      label1.TabIndex = 34;
      label1.Text = "Lọc dữ liệu";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // btnConfirm
      // 
      btnConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnConfirm.BackColor = Color.FromArgb(51, 108, 181);
      btnConfirm.BackgroundColor = Color.FromArgb(51, 108, 181);
      btnConfirm.BorderColor = Color.PaleVioletRed;
      btnConfirm.BorderRadius = 4;
      btnConfirm.BorderSize = 0;
      btnConfirm.FlatAppearance.BorderSize = 0;
      btnConfirm.FlatStyle = FlatStyle.Flat;
      btnConfirm.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnConfirm.ForeColor = Color.White;
      btnConfirm.Image = (Image)resources.GetObject("btnConfirm.Image");
      btnConfirm.ImageAlign = ContentAlignment.MiddleLeft;
      btnConfirm.Location = new Point(118, 227);
      btnConfirm.Name = "btnConfirm";
      btnConfirm.Padding = new Padding(10, 0, 0, 0);
      btnConfirm.Size = new Size(158, 54);
      btnConfirm.TabIndex = 35;
      btnConfirm.Text = "       Xác nhận";
      btnConfirm.TextColor = Color.White;
      btnConfirm.UseVisualStyleBackColor = false;
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
      btnClose.Location = new Point(282, 227);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(158, 54);
      btnClose.TabIndex = 36;
      btnClose.Text = "       Đóng";
      btnClose.TextAlign = ContentAlignment.MiddleLeft;
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      // 
      // PopupFilter
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(456, 293);
      ControlBox = false;
      Controls.Add(btnConfirm);
      Controls.Add(btnClose);
      Controls.Add(label1);
      Controls.Add(label21);
      Controls.Add(cbbStatus);
      Controls.Add(labelType);
      Controls.Add(cbbType);
      Name = "PopupFilter";
      StartPosition = FormStartPosition.CenterScreen;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Label label21;
    private ComboBox cbbStatus;
    private Label labelType;
    private ComboBox cbbType;
    private Label label1;
    private Common.Custom.RJButton btnConfirm;
    private Common.Custom.RJButton btnClose;
  }
}