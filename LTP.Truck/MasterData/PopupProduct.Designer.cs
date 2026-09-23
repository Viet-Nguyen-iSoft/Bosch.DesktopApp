namespace LTP.Truck.MasterData
{
  partial class PopupProduct
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupProduct));
      tableLayoutPanel3 = new TableLayoutPanel();
      tableLayoutPanel5 = new TableLayoutPanel();
      label2 = new Label();
      txtCode = new Common.Custom.RJTextBox();
      label1 = new Label();
      txtDescription = new Common.Custom.RJTextBox();
      label3 = new Label();
      label4 = new Label();
      txtName = new Common.Custom.RJTextBox();
      lbTitle = new Label();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      cbbProductGroup = new ComboBox();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel5.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 0, 1);
      tableLayoutPanel3.Controls.Add(lbTitle, 0, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 2);
      tableLayoutPanel3.Dock = DockStyle.Fill;
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(5);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.Padding = new Padding(5);
      tableLayoutPanel3.RowCount = 4;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel3.Size = new Size(624, 420);
      tableLayoutPanel3.TabIndex = 6;
      // 
      // tableLayoutPanel5
      // 
      tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel5.ColumnCount = 2;
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Controls.Add(label2, 0, 2);
      tableLayoutPanel5.Controls.Add(txtCode, 1, 0);
      tableLayoutPanel5.Controls.Add(label1, 0, 0);
      tableLayoutPanel5.Controls.Add(txtDescription, 1, 3);
      tableLayoutPanel5.Controls.Add(label3, 0, 1);
      tableLayoutPanel5.Controls.Add(label4, 0, 3);
      tableLayoutPanel5.Controls.Add(txtName, 1, 1);
      tableLayoutPanel5.Controls.Add(cbbProductGroup, 1, 2);
      tableLayoutPanel5.Location = new Point(8, 68);
      tableLayoutPanel5.Name = "tableLayoutPanel5";
      tableLayoutPanel5.RowCount = 4;
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
      tableLayoutPanel5.Size = new Size(608, 279);
      tableLayoutPanel5.TabIndex = 8;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.BackColor = Color.Transparent;
      label2.Font = new Font("Roboto", 14F);
      label2.Location = new Point(0, 138);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(200, 69);
      label2.TabIndex = 7;
      label2.Text = "Nhóm phế phẩm";
      label2.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtCode
      // 
      txtCode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtCode.BackColor = SystemColors.Window;
      txtCode.BorderColor = Color.Black;
      txtCode.BorderFocusColor = Color.HotPink;
      txtCode.BorderRadius = 5;
      txtCode.BorderSize = 2;
      txtCode.Font = new Font("Roboto", 14F);
      txtCode.ForeColor = Color.FromArgb(64, 64, 64);
      txtCode.Location = new Point(204, 15);
      txtCode.Margin = new Padding(4);
      txtCode.Multiline = false;
      txtCode.Name = "txtCode";
      txtCode.Padding = new Padding(10, 7, 10, 7);
      txtCode.PasswordChar = false;
      txtCode.PlaceholderColor = Color.DarkGray;
      txtCode.PlaceholderText = "";
      txtCode.Size = new Size(400, 38);
      txtCode.TabIndex = 6;
      txtCode.Texts = "";
      txtCode.UnderlinedStyle = false;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.Transparent;
      label1.Font = new Font("Roboto", 14F);
      label1.Location = new Point(0, 0);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(200, 69);
      label1.TabIndex = 5;
      label1.Text = "Mã";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtDescription
      // 
      txtDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtDescription.BackColor = SystemColors.Window;
      txtDescription.BorderColor = Color.Black;
      txtDescription.BorderFocusColor = Color.HotPink;
      txtDescription.BorderRadius = 5;
      txtDescription.BorderSize = 2;
      txtDescription.Font = new Font("Roboto", 14F);
      txtDescription.ForeColor = Color.FromArgb(64, 64, 64);
      txtDescription.Location = new Point(204, 224);
      txtDescription.Margin = new Padding(4);
      txtDescription.Multiline = false;
      txtDescription.Name = "txtDescription";
      txtDescription.Padding = new Padding(10, 7, 10, 7);
      txtDescription.PasswordChar = false;
      txtDescription.PlaceholderColor = Color.DarkGray;
      txtDescription.PlaceholderText = "";
      txtDescription.Size = new Size(400, 38);
      txtDescription.TabIndex = 4;
      txtDescription.Texts = "";
      txtDescription.UnderlinedStyle = false;
      // 
      // label3
      // 
      label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label3.AutoSize = true;
      label3.BackColor = Color.Transparent;
      label3.Font = new Font("Roboto", 14F);
      label3.Location = new Point(0, 69);
      label3.Margin = new Padding(0);
      label3.Name = "label3";
      label3.Size = new Size(200, 69);
      label3.TabIndex = 1;
      label3.Text = "Tên";
      label3.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label4
      // 
      label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label4.AutoSize = true;
      label4.BackColor = Color.Transparent;
      label4.Font = new Font("Roboto", 14F);
      label4.Location = new Point(0, 207);
      label4.Margin = new Padding(0);
      label4.Name = "label4";
      label4.Size = new Size(200, 72);
      label4.TabIndex = 2;
      label4.Text = "Mô tả";
      label4.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtName
      // 
      txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtName.BackColor = SystemColors.Window;
      txtName.BorderColor = Color.Black;
      txtName.BorderFocusColor = Color.HotPink;
      txtName.BorderRadius = 5;
      txtName.BorderSize = 2;
      txtName.Font = new Font("Roboto", 14F);
      txtName.ForeColor = Color.FromArgb(64, 64, 64);
      txtName.Location = new Point(204, 84);
      txtName.Margin = new Padding(4);
      txtName.Multiline = false;
      txtName.Name = "txtName";
      txtName.Padding = new Padding(10, 7, 10, 7);
      txtName.PasswordChar = false;
      txtName.PlaceholderColor = Color.DarkGray;
      txtName.PlaceholderText = "";
      txtName.Size = new Size(400, 38);
      txtName.TabIndex = 3;
      txtName.Texts = "";
      txtName.UnderlinedStyle = false;
      // 
      // lbTitle
      // 
      lbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbTitle.AutoSize = true;
      lbTitle.BackColor = Color.FromArgb(199, 199, 199);
      lbTitle.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbTitle.Location = new Point(5, 5);
      lbTitle.Margin = new Padding(0);
      lbTitle.Name = "lbTitle";
      lbTitle.Size = new Size(614, 60);
      lbTitle.TabIndex = 0;
      lbTitle.Text = "Phế phẩm";
      lbTitle.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.ColumnCount = 3;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel2.Controls.Add(btnConfirm, 1, 0);
      tableLayoutPanel2.Controls.Add(btnClose, 2, 0);
      tableLayoutPanel2.Location = new Point(5, 350);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(614, 60);
      tableLayoutPanel2.TabIndex = 5;
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
      btnConfirm.Location = new Point(257, 3);
      btnConfirm.Name = "btnConfirm";
      btnConfirm.Padding = new Padding(10, 0, 0, 0);
      btnConfirm.Size = new Size(174, 54);
      btnConfirm.TabIndex = 0;
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
      btnClose.Location = new Point(437, 3);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(174, 54);
      btnClose.TabIndex = 1;
      btnClose.Text = "       Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      // 
      // cbbProductGroup
      // 
      cbbProductGroup.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      cbbProductGroup.Font = new Font("Roboto", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      cbbProductGroup.FormattingEnabled = true;
      cbbProductGroup.Location = new Point(203, 161);
      cbbProductGroup.Name = "cbbProductGroup";
      cbbProductGroup.Size = new Size(402, 31);
      cbbProductGroup.TabIndex = 8;
      // 
      // PopupProduct
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(624, 420);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupProduct";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel5.ResumeLayout(false);
      tableLayoutPanel5.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private TableLayoutPanel tableLayoutPanel5;
    private Label label2;
    private Common.Custom.RJTextBox txtCode;
    private Label label1;
    private Common.Custom.RJTextBox txtDescription;
    private Label label3;
    private Label label4;
    private Common.Custom.RJTextBox txtName;
    private Label lbTitle;
    private TableLayoutPanel tableLayoutPanel2;
    private Common.Custom.RJButton btnConfirm;
    private Common.Custom.RJButton btnClose;
    private ComboBox cbbProductGroup;
  }
}