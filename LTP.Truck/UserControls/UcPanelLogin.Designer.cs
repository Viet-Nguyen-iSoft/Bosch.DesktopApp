namespace LTP.Truck.UserControls
{
  partial class UcPanelLogin
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
      label1 = new Label();
      tableLayoutPanel2 = new TableLayoutPanel();
      label2 = new Label();
      txtAccount = new LTP.Truck.Custom.RJTextBox();
      tableLayoutPanel3 = new TableLayoutPanel();
      label3 = new Label();
      tableLayoutPanel4 = new TableLayoutPanel();
      txtPass = new LTP.Truck.Custom.RJTextBox();
      btnHide = new Common.Custom.RJButton();
      btnLogin = new LTP.Truck.Custom.RJButton();
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel4.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = Color.FromArgb(255, 204, 204);
      tableLayoutPanel1.ColumnCount = 3;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
      tableLayoutPanel1.Controls.Add(label1, 1, 1);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 3);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 5);
      tableLayoutPanel1.Controls.Add(btnLogin, 1, 7);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Margin = new Padding(0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 9;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
      tableLayoutPanel1.Size = new Size(650, 530);
      tableLayoutPanel1.TabIndex = 1;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.Font = new Font("Roboto", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label1.ForeColor = Color.Red;
      label1.Location = new Point(30, 30);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(590, 80);
      label1.TabIndex = 0;
      label1.Text = "ĐĂNG NHẬP";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.ColumnCount = 1;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Controls.Add(label2, 0, 0);
      tableLayoutPanel2.Controls.Add(txtAccount, 0, 1);
      tableLayoutPanel2.Dock = DockStyle.Fill;
      tableLayoutPanel2.Location = new Point(30, 130);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 2;
      tableLayoutPanel2.RowStyles.Add(new RowStyle());
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(590, 110);
      tableLayoutPanel2.TabIndex = 1;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.Font = new Font("Roboto", 20.25F, FontStyle.Bold);
      label2.ForeColor = Color.FromArgb(255, 56, 60);
      label2.Location = new Point(0, 0);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(590, 37);
      label2.TabIndex = 1;
      label2.Text = "Tên đăng nhập";
      label2.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtAccount
      // 
      txtAccount.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtAccount.BackColor = SystemColors.Window;
      txtAccount.BorderColor = Color.White;
      txtAccount.BorderFocusColor = Color.HotPink;
      txtAccount.BorderRadius = 5;
      txtAccount.BorderSize = 2;
      txtAccount.Font = new Font("Roboto", 26.25F);
      txtAccount.ForeColor = Color.FromArgb(64, 64, 64);
      txtAccount.Location = new Point(4, 45);
      txtAccount.Margin = new Padding(4);
      txtAccount.Multiline = false;
      txtAccount.Name = "txtAccount";
      txtAccount.Padding = new Padding(10, 7, 10, 7);
      txtAccount.PasswordChar = false;
      txtAccount.PlaceholderColor = Color.DarkGray;
      txtAccount.PlaceholderText = "";
      txtAccount.Size = new Size(582, 57);
      txtAccount.TabIndex = 2;
      txtAccount.Texts = "";
      txtAccount.UnderlinedStyle = false;
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(label3, 0, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 1);
      tableLayoutPanel3.Location = new Point(30, 255);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 2;
      tableLayoutPanel3.RowStyles.Add(new RowStyle());
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Size = new Size(590, 110);
      tableLayoutPanel3.TabIndex = 2;
      // 
      // label3
      // 
      label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label3.AutoSize = true;
      label3.Font = new Font("Roboto", 20.25F, FontStyle.Bold);
      label3.ForeColor = Color.FromArgb(255, 56, 60);
      label3.Location = new Point(0, 0);
      label3.Margin = new Padding(0);
      label3.Name = "label3";
      label3.Size = new Size(590, 37);
      label3.TabIndex = 1;
      label3.Text = "Mật khẩu";
      label3.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel4
      // 
      tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel4.ColumnCount = 2;
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 86.64384F));
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.356164F));
      tableLayoutPanel4.Controls.Add(txtPass, 0, 0);
      tableLayoutPanel4.Controls.Add(btnHide, 1, 0);
      tableLayoutPanel4.Location = new Point(3, 40);
      tableLayoutPanel4.Name = "tableLayoutPanel4";
      tableLayoutPanel4.RowCount = 1;
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel4.Size = new Size(584, 67);
      tableLayoutPanel4.TabIndex = 2;
      // 
      // txtPass
      // 
      txtPass.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtPass.BackColor = SystemColors.Window;
      txtPass.BorderColor = Color.White;
      txtPass.BorderFocusColor = Color.HotPink;
      txtPass.BorderRadius = 5;
      txtPass.BorderSize = 2;
      txtPass.Font = new Font("Roboto", 26.25F);
      txtPass.ForeColor = Color.FromArgb(64, 64, 64);
      txtPass.Location = new Point(4, 5);
      txtPass.Margin = new Padding(4);
      txtPass.Multiline = false;
      txtPass.Name = "txtPass";
      txtPass.Padding = new Padding(10, 7, 10, 7);
      txtPass.PasswordChar = true;
      txtPass.PlaceholderColor = Color.DarkGray;
      txtPass.PlaceholderText = "";
      txtPass.Size = new Size(498, 57);
      txtPass.TabIndex = 3;
      txtPass.Texts = "";
      txtPass.UnderlinedStyle = false;
      // 
      // btnHide
      // 
      btnHide.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnHide.BackColor = Color.FromArgb(64, 107, 177);
      btnHide.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnHide.BorderColor = Color.PaleVioletRed;
      btnHide.BorderRadius = 5;
      btnHide.BorderSize = 0;
      btnHide.FlatAppearance.BorderSize = 0;
      btnHide.FlatStyle = FlatStyle.Flat;
      btnHide.ForeColor = Color.White;
      btnHide.Image = Properties.Resources.icon_hide;
      btnHide.Location = new Point(509, 5);
      btnHide.Name = "btnHide";
      btnHide.Size = new Size(72, 57);
      btnHide.TabIndex = 4;
      btnHide.TextColor = Color.White;
      btnHide.UseVisualStyleBackColor = false;
      btnHide.Click += btnHide_Click;
      // 
      // btnLogin
      // 
      btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnLogin.BackColor = Color.Red;
      btnLogin.BackgroundColor = Color.Red;
      btnLogin.BorderColor = Color.PaleVioletRed;
      btnLogin.BorderRadius = 5;
      btnLogin.BorderSize = 0;
      btnLogin.FlatAppearance.BorderSize = 0;
      btnLogin.FlatStyle = FlatStyle.Flat;
      btnLogin.Font = new Font("Roboto", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnLogin.ForeColor = Color.White;
      btnLogin.Location = new Point(33, 433);
      btnLogin.Name = "btnLogin";
      btnLogin.Size = new Size(584, 64);
      btnLogin.TabIndex = 3;
      btnLogin.Text = "Đăng nhập";
      btnLogin.TextColor = Color.White;
      btnLogin.UseVisualStyleBackColor = false;
      btnLogin.Click += btnLogin_Click;
      // 
      // UcPanelLogin
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(tableLayoutPanel1);
      Name = "UcPanelLogin";
      Size = new Size(650, 530);
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel2.PerformLayout();
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel4.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private Label label1;
    private TableLayoutPanel tableLayoutPanel2;
    private Label label2;
    private TableLayoutPanel tableLayoutPanel3;
    private Label label3;
    private Custom.RJTextBox txtAccount;
    private Custom.RJTextBox txtPass;
    private Custom.RJButton btnLogin;
    private TableLayoutPanel tableLayoutPanel4;
    private Common.Custom.RJButton btnHide;
  }
}
