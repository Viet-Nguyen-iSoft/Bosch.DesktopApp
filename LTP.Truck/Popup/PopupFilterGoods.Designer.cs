namespace LTP.Truck.Popup
{
  partial class PopupFilterGoods
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupFilterGoods));
      label1 = new Label();
      label21 = new Label();
      cbbGroup = new ComboBox();
      label2 = new Label();
      cbbType = new ComboBox();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      SuspendLayout();
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.Transparent;
      label1.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label1.Location = new Point(17, 15);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(105, 23);
      label1.TabIndex = 39;
      label1.Text = "Lọc dữ liệu";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label21
      // 
      label21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label21.AutoSize = true;
      label21.BackColor = Color.Transparent;
      label21.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label21.Location = new Point(17, 86);
      label21.Margin = new Padding(0);
      label21.Name = "label21";
      label21.Size = new Size(62, 23);
      label21.TabIndex = 37;
      label21.Text = "Nhóm";
      label21.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // cbbGroup
      // 
      cbbGroup.Anchor = AnchorStyles.Left;
      cbbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbGroup.Font = new Font("Roboto", 20F);
      cbbGroup.FormattingEnabled = true;
      cbbGroup.Items.AddRange(new object[] { "Tất cả", "Chưa hoàn thành", "Hoàn thành", "Đã xóa" });
      cbbGroup.Location = new Point(135, 75);
      cbbGroup.Name = "cbbGroup";
      cbbGroup.Size = new Size(334, 41);
      cbbGroup.TabIndex = 38;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.BackColor = Color.Transparent;
      label2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label2.Location = new Point(17, 157);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(46, 23);
      label2.TabIndex = 42;
      label2.Text = "Loại";
      label2.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // cbbType
      // 
      cbbType.Anchor = AnchorStyles.Left;
      cbbType.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbType.Font = new Font("Roboto", 20F);
      cbbType.FormattingEnabled = true;
      cbbType.Items.AddRange(new object[] { "Tất cả", "Chưa hoàn thành", "Hoàn thành", "Đã xóa" });
      cbbType.Location = new Point(135, 146);
      cbbType.Name = "cbbType";
      cbbType.Size = new Size(334, 41);
      cbbType.TabIndex = 43;
      // 
      // btnConfirm
      // 
      btnConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnConfirm.BackColor = Color.FromArgb(51, 108, 181);
      btnConfirm.BackgroundColor = Color.FromArgb(51, 108, 181);
      btnConfirm.BorderColor = Color.PaleVioletRed;
      btnConfirm.BorderRadius = 5;
      btnConfirm.BorderSize = 0;
      btnConfirm.FlatAppearance.BorderSize = 0;
      btnConfirm.FlatStyle = FlatStyle.Flat;
      btnConfirm.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnConfirm.ForeColor = Color.White;
      btnConfirm.Image = (Image)resources.GetObject("btnConfirm.Image");
      btnConfirm.ImageAlign = ContentAlignment.MiddleLeft;
      btnConfirm.Location = new Point(143, 236);
      btnConfirm.Name = "btnConfirm";
      btnConfirm.Padding = new Padding(10, 0, 0, 0);
      btnConfirm.Size = new Size(158, 55);
      btnConfirm.TabIndex = 44;
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
      btnClose.BorderRadius = 5;
      btnClose.BorderSize = 0;
      btnClose.FlatAppearance.BorderSize = 0;
      btnClose.FlatStyle = FlatStyle.Flat;
      btnClose.Font = new Font("Roboto", 14F, FontStyle.Bold);
      btnClose.ForeColor = Color.White;
      btnClose.Image = (Image)resources.GetObject("btnClose.Image");
      btnClose.ImageAlign = ContentAlignment.MiddleLeft;
      btnClose.Location = new Point(311, 236);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(158, 55);
      btnClose.TabIndex = 45;
      btnClose.Text = "       Đóng";
      btnClose.TextAlign = ContentAlignment.MiddleLeft;
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      // 
      // PopupFilterGoods
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(481, 303);
      ControlBox = false;
      Controls.Add(btnConfirm);
      Controls.Add(btnClose);
      Controls.Add(label2);
      Controls.Add(cbbType);
      Controls.Add(label1);
      Controls.Add(label21);
      Controls.Add(cbbGroup);
      Name = "PopupFilterGoods";
      StartPosition = FormStartPosition.CenterParent;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label21;
    private ComboBox cbbGroup;
    private Label label2;
    private ComboBox cbbType;
    private Common.Custom.RJButton btnConfirm;
    private Common.Custom.RJButton btnClose;
  }
}