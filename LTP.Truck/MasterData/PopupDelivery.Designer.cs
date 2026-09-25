namespace LTP.Truck.MasterData
{
  partial class PopupDelivery
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupDelivery));
      tableLayoutPanel3 = new TableLayoutPanel();
      tableLayoutPanel5 = new TableLayoutPanel();
      label1 = new Label();
      txtOfficeAddress = new Common.Custom.RJTextBox();
      txtAgentAddressForOfficeAddress = new Common.Custom.RJTextBox();
      label6 = new Label();
      label5 = new Label();
      label7 = new Label();
      label8 = new Label();
      txtPhoneForOfficeAddress = new Common.Custom.RJTextBox();
      txtDescription = new Common.Custom.RJTextBox();
      label4 = new Label();
      txtAgentAddress = new Common.Custom.RJTextBox();
      txtCompanyName = new Common.Custom.RJTextBox();
      lbTitle = new Label();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
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
      tableLayoutPanel3.Size = new Size(975, 538);
      tableLayoutPanel3.TabIndex = 7;
      // 
      // tableLayoutPanel5
      // 
      tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel5.ColumnCount = 2;
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Controls.Add(label1, 0, 0);
      tableLayoutPanel5.Controls.Add(txtOfficeAddress, 1, 1);
      tableLayoutPanel5.Controls.Add(txtAgentAddressForOfficeAddress, 1, 4);
      tableLayoutPanel5.Controls.Add(label6, 0, 4);
      tableLayoutPanel5.Controls.Add(label5, 0, 3);
      tableLayoutPanel5.Controls.Add(label7, 0, 1);
      tableLayoutPanel5.Controls.Add(label8, 0, 2);
      tableLayoutPanel5.Controls.Add(txtPhoneForOfficeAddress, 1, 2);
      tableLayoutPanel5.Controls.Add(txtDescription, 1, 5);
      tableLayoutPanel5.Controls.Add(label4, 0, 5);
      tableLayoutPanel5.Controls.Add(txtAgentAddress, 1, 3);
      tableLayoutPanel5.Controls.Add(txtCompanyName, 1, 0);
      tableLayoutPanel5.Location = new Point(8, 68);
      tableLayoutPanel5.Name = "tableLayoutPanel5";
      tableLayoutPanel5.RowCount = 6;
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
      tableLayoutPanel5.Size = new Size(959, 397);
      tableLayoutPanel5.TabIndex = 8;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.Transparent;
      label1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label1.Location = new Point(0, 0);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(200, 66);
      label1.TabIndex = 43;
      label1.Text = "Tên công ty";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtOfficeAddress
      // 
      txtOfficeAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtOfficeAddress.BackColor = SystemColors.Window;
      txtOfficeAddress.BorderColor = Color.Black;
      txtOfficeAddress.BorderFocusColor = Color.HotPink;
      txtOfficeAddress.BorderRadius = 5;
      txtOfficeAddress.BorderSize = 2;
      txtOfficeAddress.Font = new Font("Roboto", 14F);
      txtOfficeAddress.ForeColor = Color.FromArgb(64, 64, 64);
      txtOfficeAddress.Location = new Point(204, 80);
      txtOfficeAddress.Margin = new Padding(4);
      txtOfficeAddress.Multiline = false;
      txtOfficeAddress.Name = "txtOfficeAddress";
      txtOfficeAddress.Padding = new Padding(10, 7, 10, 7);
      txtOfficeAddress.PasswordChar = false;
      txtOfficeAddress.PlaceholderColor = Color.DarkGray;
      txtOfficeAddress.PlaceholderText = "";
      txtOfficeAddress.Size = new Size(751, 38);
      txtOfficeAddress.TabIndex = 41;
      txtOfficeAddress.Texts = "";
      txtOfficeAddress.UnderlinedStyle = false;
      // 
      // txtAgentAddressForOfficeAddress
      // 
      txtAgentAddressForOfficeAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtAgentAddressForOfficeAddress.BackColor = SystemColors.Window;
      txtAgentAddressForOfficeAddress.BorderColor = Color.Black;
      txtAgentAddressForOfficeAddress.BorderFocusColor = Color.HotPink;
      txtAgentAddressForOfficeAddress.BorderRadius = 5;
      txtAgentAddressForOfficeAddress.BorderSize = 2;
      txtAgentAddressForOfficeAddress.Font = new Font("Roboto", 14F);
      txtAgentAddressForOfficeAddress.ForeColor = Color.FromArgb(64, 64, 64);
      txtAgentAddressForOfficeAddress.Location = new Point(204, 278);
      txtAgentAddressForOfficeAddress.Margin = new Padding(4);
      txtAgentAddressForOfficeAddress.Multiline = false;
      txtAgentAddressForOfficeAddress.Name = "txtAgentAddressForOfficeAddress";
      txtAgentAddressForOfficeAddress.Padding = new Padding(10, 7, 10, 7);
      txtAgentAddressForOfficeAddress.PasswordChar = false;
      txtAgentAddressForOfficeAddress.PlaceholderColor = Color.DarkGray;
      txtAgentAddressForOfficeAddress.PlaceholderText = "";
      txtAgentAddressForOfficeAddress.Size = new Size(751, 38);
      txtAgentAddressForOfficeAddress.TabIndex = 40;
      txtAgentAddressForOfficeAddress.Texts = "";
      txtAgentAddressForOfficeAddress.UnderlinedStyle = false;
      // 
      // label6
      // 
      label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label6.AutoSize = true;
      label6.BackColor = Color.Transparent;
      label6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label6.Location = new Point(0, 264);
      label6.Margin = new Padding(0);
      label6.Name = "label6";
      label6.Size = new Size(200, 66);
      label6.TabIndex = 3;
      label6.Text = "ĐT";
      label6.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label5.AutoSize = true;
      label5.BackColor = Color.Transparent;
      label5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label5.Location = new Point(0, 198);
      label5.Margin = new Padding(0);
      label5.Name = "label5";
      label5.Size = new Size(200, 66);
      label5.TabIndex = 3;
      label5.Text = "Địa chỉ cơ sở/đại lý";
      label5.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label7
      // 
      label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label7.AutoSize = true;
      label7.BackColor = Color.Transparent;
      label7.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label7.Location = new Point(0, 66);
      label7.Margin = new Padding(0);
      label7.Name = "label7";
      label7.Size = new Size(200, 66);
      label7.TabIndex = 3;
      label7.Text = "Địa chỉ văn phòng";
      label7.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label8
      // 
      label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label8.AutoSize = true;
      label8.BackColor = Color.Transparent;
      label8.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label8.Location = new Point(0, 132);
      label8.Margin = new Padding(0);
      label8.Name = "label8";
      label8.Size = new Size(200, 66);
      label8.TabIndex = 3;
      label8.Text = "ĐT";
      label8.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtPhoneForOfficeAddress
      // 
      txtPhoneForOfficeAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtPhoneForOfficeAddress.BackColor = SystemColors.Window;
      txtPhoneForOfficeAddress.BorderColor = Color.Black;
      txtPhoneForOfficeAddress.BorderFocusColor = Color.HotPink;
      txtPhoneForOfficeAddress.BorderRadius = 5;
      txtPhoneForOfficeAddress.BorderSize = 2;
      txtPhoneForOfficeAddress.Font = new Font("Roboto", 14F);
      txtPhoneForOfficeAddress.ForeColor = Color.FromArgb(64, 64, 64);
      txtPhoneForOfficeAddress.Location = new Point(204, 146);
      txtPhoneForOfficeAddress.Margin = new Padding(4);
      txtPhoneForOfficeAddress.Multiline = false;
      txtPhoneForOfficeAddress.Name = "txtPhoneForOfficeAddress";
      txtPhoneForOfficeAddress.Padding = new Padding(10, 7, 10, 7);
      txtPhoneForOfficeAddress.PasswordChar = false;
      txtPhoneForOfficeAddress.PlaceholderColor = Color.DarkGray;
      txtPhoneForOfficeAddress.PlaceholderText = "";
      txtPhoneForOfficeAddress.Size = new Size(751, 38);
      txtPhoneForOfficeAddress.TabIndex = 6;
      txtPhoneForOfficeAddress.Texts = "";
      txtPhoneForOfficeAddress.UnderlinedStyle = false;
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
      txtDescription.Location = new Point(204, 344);
      txtDescription.Margin = new Padding(4);
      txtDescription.Multiline = false;
      txtDescription.Name = "txtDescription";
      txtDescription.Padding = new Padding(10, 7, 10, 7);
      txtDescription.PasswordChar = false;
      txtDescription.PlaceholderColor = Color.DarkGray;
      txtDescription.PlaceholderText = "";
      txtDescription.Size = new Size(751, 38);
      txtDescription.TabIndex = 4;
      txtDescription.Texts = "";
      txtDescription.UnderlinedStyle = false;
      // 
      // label4
      // 
      label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label4.AutoSize = true;
      label4.BackColor = Color.Transparent;
      label4.Font = new Font("Roboto", 14F);
      label4.Location = new Point(0, 330);
      label4.Margin = new Padding(0);
      label4.Name = "label4";
      label4.Size = new Size(200, 67);
      label4.TabIndex = 2;
      label4.Text = "Mô tả";
      label4.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtAgentAddress
      // 
      txtAgentAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtAgentAddress.BackColor = SystemColors.Window;
      txtAgentAddress.BorderColor = Color.Black;
      txtAgentAddress.BorderFocusColor = Color.HotPink;
      txtAgentAddress.BorderRadius = 5;
      txtAgentAddress.BorderSize = 2;
      txtAgentAddress.Font = new Font("Roboto", 14F);
      txtAgentAddress.ForeColor = Color.FromArgb(64, 64, 64);
      txtAgentAddress.Location = new Point(204, 212);
      txtAgentAddress.Margin = new Padding(4);
      txtAgentAddress.Multiline = false;
      txtAgentAddress.Name = "txtAgentAddress";
      txtAgentAddress.Padding = new Padding(10, 7, 10, 7);
      txtAgentAddress.PasswordChar = false;
      txtAgentAddress.PlaceholderColor = Color.DarkGray;
      txtAgentAddress.PlaceholderText = "";
      txtAgentAddress.Size = new Size(751, 38);
      txtAgentAddress.TabIndex = 3;
      txtAgentAddress.Texts = "";
      txtAgentAddress.UnderlinedStyle = false;
      // 
      // txtCompanyName
      // 
      txtCompanyName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtCompanyName.BackColor = SystemColors.Window;
      txtCompanyName.BorderColor = Color.Black;
      txtCompanyName.BorderFocusColor = Color.HotPink;
      txtCompanyName.BorderRadius = 5;
      txtCompanyName.BorderSize = 2;
      txtCompanyName.Font = new Font("Roboto", 14F);
      txtCompanyName.ForeColor = Color.FromArgb(64, 64, 64);
      txtCompanyName.Location = new Point(204, 14);
      txtCompanyName.Margin = new Padding(4);
      txtCompanyName.Multiline = false;
      txtCompanyName.Name = "txtCompanyName";
      txtCompanyName.Padding = new Padding(10, 7, 10, 7);
      txtCompanyName.PasswordChar = false;
      txtCompanyName.PlaceholderColor = Color.DarkGray;
      txtCompanyName.PlaceholderText = "";
      txtCompanyName.Size = new Size(751, 38);
      txtCompanyName.TabIndex = 42;
      txtCompanyName.Texts = "";
      txtCompanyName.UnderlinedStyle = false;
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
      lbTitle.Size = new Size(965, 60);
      lbTitle.TabIndex = 0;
      lbTitle.Text = "Thông tin Khách hàng giao";
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
      tableLayoutPanel2.Location = new Point(5, 468);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(965, 60);
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
      btnConfirm.Location = new Point(608, 3);
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
      btnClose.Location = new Point(788, 3);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(174, 54);
      btnClose.TabIndex = 1;
      btnClose.Text = "       Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      // 
      // PopupDelivery
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(975, 538);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupDelivery";
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
    private Label label7;
    private Label label6;
    private Label label8;
    private Label label5;
    private Common.Custom.RJTextBox txtPhoneForOfficeAddress;
    private Common.Custom.RJTextBox txtDescription;
    private Label label4;
    private Common.Custom.RJTextBox txtAgentAddress;
    private Label lbTitle;
    private TableLayoutPanel tableLayoutPanel2;
    private Common.Custom.RJButton btnConfirm;
    private Common.Custom.RJButton btnClose;
    private Common.Custom.RJTextBox txtOfficeAddress;
    private Common.Custom.RJTextBox txtAgentAddressForOfficeAddress;
    private Label label1;
    private Common.Custom.RJTextBox txtCompanyName;
  }
}