namespace LTP.Truck.MasterData
{
  partial class PopupClient
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupClient));
      tableLayoutPanel3 = new TableLayoutPanel();
      tableLayoutPanel5 = new TableLayoutPanel();
      txtDescription = new Common.Custom.RJTextBox();
      label3 = new Label();
      label4 = new Label();
      txtName = new Common.Custom.RJTextBox();
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
      tableLayoutPanel3.Size = new Size(624, 274);
      tableLayoutPanel3.TabIndex = 3;
      // 
      // tableLayoutPanel5
      // 
      tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel5.ColumnCount = 2;
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Controls.Add(txtDescription, 1, 1);
      tableLayoutPanel5.Controls.Add(label3, 0, 0);
      tableLayoutPanel5.Controls.Add(label4, 0, 1);
      tableLayoutPanel5.Controls.Add(txtName, 1, 0);
      tableLayoutPanel5.Location = new Point(8, 68);
      tableLayoutPanel5.Name = "tableLayoutPanel5";
      tableLayoutPanel5.RowCount = 2;
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel5.Size = new Size(608, 133);
      tableLayoutPanel5.TabIndex = 8;
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
      txtDescription.Location = new Point(154, 80);
      txtDescription.Margin = new Padding(4);
      txtDescription.Multiline = false;
      txtDescription.Name = "txtDescription";
      txtDescription.Padding = new Padding(10, 7, 10, 7);
      txtDescription.PasswordChar = false;
      txtDescription.PlaceholderColor = Color.DarkGray;
      txtDescription.PlaceholderText = "";
      txtDescription.Size = new Size(450, 38);
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
      label3.Location = new Point(0, 0);
      label3.Margin = new Padding(0);
      label3.Name = "label3";
      label3.Size = new Size(150, 66);
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
      label4.Location = new Point(0, 66);
      label4.Margin = new Padding(0);
      label4.Name = "label4";
      label4.Size = new Size(150, 67);
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
      txtName.Location = new Point(154, 14);
      txtName.Margin = new Padding(4);
      txtName.Multiline = false;
      txtName.Name = "txtName";
      txtName.Padding = new Padding(10, 7, 10, 7);
      txtName.PasswordChar = false;
      txtName.PlaceholderColor = Color.DarkGray;
      txtName.PlaceholderText = "";
      txtName.Size = new Size(450, 38);
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
      lbTitle.Text = "Khách hàng";
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
      tableLayoutPanel2.Location = new Point(5, 204);
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
      // PopupClient
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(624, 274);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupClient";
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
    private Label lbTitle;
    private TableLayoutPanel tableLayoutPanel2;
    private Common.Custom.RJButton btnConfirm;
    private Common.Custom.RJButton btnClose;
    private TableLayoutPanel tableLayoutPanel5;
    private Common.Custom.RJTextBox txtDescription;
    private Label label3;
    private Label label4;
    private Common.Custom.RJTextBox txtName;
  }
}