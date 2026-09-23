namespace Common.Settings
{
  partial class PopupSettingTcpClient
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
      tableLayoutPanel3 = new TableLayoutPanel();
      label1 = new Label();
      tableLayoutPanel1 = new TableLayoutPanel();
      iconAutoConnect = new PictureBox();
      txtTimeout = new Common.Custom.RJTextBox();
      txtPort = new Common.Custom.RJTextBox();
      label3 = new Label();
      label2 = new Label();
      label5 = new Label();
      label4 = new Label();
      label6 = new Label();
      txtIP = new Common.Custom.RJTextBox();
      iconSendReq = new PictureBox();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)iconAutoConnect).BeginInit();
      ((System.ComponentModel.ISupportInitialize)iconSendReq).BeginInit();
      tableLayoutPanel2.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(label1, 0, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 0, 1);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 3);
      tableLayoutPanel3.Dock = DockStyle.Fill;
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 5;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 350F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel3.Size = new Size(534, 534);
      tableLayoutPanel3.TabIndex = 4;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.FromArgb(199, 199, 199);
      label1.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label1.Location = new Point(0, 0);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(534, 60);
      label1.TabIndex = 0;
      label1.Text = "Cài đặt chuẩn kết nối TCP";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(iconAutoConnect, 1, 4);
      tableLayoutPanel1.Controls.Add(txtTimeout, 1, 2);
      tableLayoutPanel1.Controls.Add(txtPort, 1, 1);
      tableLayoutPanel1.Controls.Add(label3, 0, 1);
      tableLayoutPanel1.Controls.Add(label2, 0, 0);
      tableLayoutPanel1.Controls.Add(label5, 0, 3);
      tableLayoutPanel1.Controls.Add(label4, 0, 2);
      tableLayoutPanel1.Controls.Add(label6, 0, 4);
      tableLayoutPanel1.Controls.Add(txtIP, 1, 0);
      tableLayoutPanel1.Controls.Add(iconSendReq, 1, 3);
      tableLayoutPanel1.Location = new Point(10, 60);
      tableLayoutPanel1.Margin = new Padding(10, 0, 10, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 5;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20.666666F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 19.333334F));
      tableLayoutPanel1.Size = new Size(514, 350);
      tableLayoutPanel1.TabIndex = 3;
      // 
      // iconAutoConnect
      // 
      iconAutoConnect.Anchor = AnchorStyles.Left;
      iconAutoConnect.Image = Properties.Resources.switch_off;
      iconAutoConnect.Location = new Point(181, 285);
      iconAutoConnect.Name = "iconAutoConnect";
      iconAutoConnect.Size = new Size(104, 62);
      iconAutoConnect.SizeMode = PictureBoxSizeMode.StretchImage;
      iconAutoConnect.TabIndex = 12;
      iconAutoConnect.TabStop = false;
      iconAutoConnect.Click += iconAutoConnect_Click;
      // 
      // txtTimeout
      // 
      txtTimeout.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtTimeout.BackColor = SystemColors.Window;
      txtTimeout.BorderColor = Color.Black;
      txtTimeout.BorderFocusColor = Color.HotPink;
      txtTimeout.BorderRadius = 5;
      txtTimeout.BorderSize = 2;
      txtTimeout.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtTimeout.ForeColor = Color.FromArgb(64, 64, 64);
      txtTimeout.Location = new Point(182, 156);
      txtTimeout.Margin = new Padding(4);
      txtTimeout.Multiline = false;
      txtTimeout.Name = "txtTimeout";
      txtTimeout.Padding = new Padding(10, 7, 10, 7);
      txtTimeout.PasswordChar = false;
      txtTimeout.PlaceholderColor = Color.DarkGray;
      txtTimeout.PlaceholderText = "";
      txtTimeout.Size = new Size(328, 38);
      txtTimeout.TabIndex = 10;
      txtTimeout.Texts = "";
      txtTimeout.UnderlinedStyle = false;
      // 
      // txtPort
      // 
      txtPort.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtPort.BackColor = SystemColors.Window;
      txtPort.BorderColor = Color.Black;
      txtPort.BorderFocusColor = Color.HotPink;
      txtPort.BorderRadius = 5;
      txtPort.BorderSize = 2;
      txtPort.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtPort.ForeColor = Color.FromArgb(64, 64, 64);
      txtPort.Location = new Point(182, 86);
      txtPort.Margin = new Padding(4);
      txtPort.Multiline = false;
      txtPort.Name = "txtPort";
      txtPort.Padding = new Padding(10, 7, 10, 7);
      txtPort.PasswordChar = false;
      txtPort.PlaceholderColor = Color.DarkGray;
      txtPort.PlaceholderText = "";
      txtPort.Size = new Size(328, 38);
      txtPort.TabIndex = 9;
      txtPort.Texts = "";
      txtPort.UnderlinedStyle = false;
      // 
      // label3
      // 
      label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label3.AutoSize = true;
      label3.BackColor = Color.Transparent;
      label3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label3.Location = new Point(0, 70);
      label3.Margin = new Padding(0);
      label3.Name = "label3";
      label3.Size = new Size(178, 70);
      label3.TabIndex = 3;
      label3.Text = "Port:";
      label3.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.BackColor = Color.Transparent;
      label2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label2.Location = new Point(0, 0);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(178, 70);
      label2.TabIndex = 1;
      label2.Text = "Địa chỉ IP:";
      label2.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label5.AutoSize = true;
      label5.BackColor = Color.Transparent;
      label5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label5.Location = new Point(0, 210);
      label5.Margin = new Padding(0);
      label5.Name = "label5";
      label5.Size = new Size(178, 72);
      label5.TabIndex = 5;
      label5.Text = "Gửi lệnh lấy dữ liệu:";
      label5.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label4
      // 
      label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label4.AutoSize = true;
      label4.BackColor = Color.Transparent;
      label4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label4.Location = new Point(0, 140);
      label4.Margin = new Padding(0);
      label4.Name = "label4";
      label4.Size = new Size(178, 70);
      label4.TabIndex = 4;
      label4.Text = "Timeout (s):";
      label4.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label6
      // 
      label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label6.AutoSize = true;
      label6.BackColor = Color.Transparent;
      label6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label6.Location = new Point(0, 282);
      label6.Margin = new Padding(0);
      label6.Name = "label6";
      label6.Size = new Size(178, 68);
      label6.TabIndex = 6;
      label6.Text = "Tự động kết nối:";
      label6.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtIP
      // 
      txtIP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtIP.BackColor = SystemColors.Window;
      txtIP.BorderColor = Color.Black;
      txtIP.BorderFocusColor = Color.HotPink;
      txtIP.BorderRadius = 5;
      txtIP.BorderSize = 2;
      txtIP.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtIP.ForeColor = Color.FromArgb(64, 64, 64);
      txtIP.Location = new Point(182, 16);
      txtIP.Margin = new Padding(4);
      txtIP.Multiline = false;
      txtIP.Name = "txtIP";
      txtIP.Padding = new Padding(10, 7, 10, 7);
      txtIP.PasswordChar = false;
      txtIP.PlaceholderColor = Color.DarkGray;
      txtIP.PlaceholderText = "";
      txtIP.Size = new Size(328, 38);
      txtIP.TabIndex = 8;
      txtIP.Texts = "";
      txtIP.UnderlinedStyle = false;
      // 
      // iconSendReq
      // 
      iconSendReq.Anchor = AnchorStyles.Left;
      iconSendReq.Image = Properties.Resources.switch_off;
      iconSendReq.Location = new Point(181, 213);
      iconSendReq.Name = "iconSendReq";
      iconSendReq.Size = new Size(104, 66);
      iconSendReq.SizeMode = PictureBoxSizeMode.StretchImage;
      iconSendReq.TabIndex = 11;
      iconSendReq.TabStop = false;
      iconSendReq.Click += iconSendReq_Click;
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
      tableLayoutPanel2.Location = new Point(10, 464);
      tableLayoutPanel2.Margin = new Padding(10, 0, 10, 0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(514, 60);
      tableLayoutPanel2.TabIndex = 4;
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
      btnConfirm.Image = Properties.Resources.icon_confirm;
      btnConfirm.ImageAlign = ContentAlignment.MiddleLeft;
      btnConfirm.Location = new Point(157, 3);
      btnConfirm.Name = "btnConfirm";
      btnConfirm.Padding = new Padding(10, 0, 0, 0);
      btnConfirm.Size = new Size(174, 54);
      btnConfirm.TabIndex = 0;
      btnConfirm.Text = "       Xác nhận";
      btnConfirm.TextAlign = ContentAlignment.MiddleLeft;
      btnConfirm.TextColor = Color.White;
      btnConfirm.UseVisualStyleBackColor = false;
      btnConfirm.Click += btnConfirm_Click;
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
      btnClose.Image = Properties.Resources.icon_close;
      btnClose.ImageAlign = ContentAlignment.MiddleLeft;
      btnClose.Location = new Point(337, 3);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(174, 54);
      btnClose.TabIndex = 1;
      btnClose.Text = "       Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      btnClose.Click += btnClose_Click;
      // 
      // PopupSettingTcpClient
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(534, 534);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupSettingTcpClient";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)iconAutoConnect).EndInit();
      ((System.ComponentModel.ISupportInitialize)iconSendReq).EndInit();
      tableLayoutPanel2.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private Label label1;
    private TableLayoutPanel tableLayoutPanel1;
    private Label label2;
    private TableLayoutPanel tableLayoutPanel2;
    private Custom.RJButton btnConfirm;
    private Custom.RJButton btnClose;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Custom.RJTextBox txtIP;
    private Custom.RJTextBox txtTimeout;
    private Custom.RJTextBox txtPort;
    private PictureBox iconSendReq;
    private PictureBox iconAutoConnect;
  }
}