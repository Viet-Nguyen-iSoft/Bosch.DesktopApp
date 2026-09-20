namespace Common.Settings
{
  partial class PopupSettingSerial
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
      cbbComm = new ComboBox();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      tableLayoutPanel1 = new TableLayoutPanel();
      iconAutoConnect = new PictureBox();
      label2 = new Label();
      label5 = new Label();
      label6 = new Label();
      iconSendReq = new PictureBox();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)iconAutoConnect).BeginInit();
      ((System.ComponentModel.ISupportInitialize)iconSendReq).BeginInit();
      SuspendLayout();
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 0, 1);
      tableLayoutPanel3.Controls.Add(label1, 0, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 2);
      tableLayoutPanel3.Dock = DockStyle.Fill;
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 4;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel3.Size = new Size(534, 315);
      tableLayoutPanel3.TabIndex = 3;
      // 
      // label1
      // 
      label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label1.AutoSize = true;
      label1.BackColor = Color.FromArgb(199, 199, 199);
      label1.Font = new Font("Roboto", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label1.Location = new Point(0, 0);
      label1.Margin = new Padding(0);
      label1.Name = "label1";
      label1.Size = new Size(534, 60);
      label1.TabIndex = 0;
      label1.Text = "Cài đặt chuẩn kết nối Serial";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // cbbComm
      // 
      cbbComm.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      cbbComm.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbComm.Font = new Font("Roboto", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
      cbbComm.FormattingEnabled = true;
      cbbComm.Location = new Point(193, 12);
      cbbComm.Name = "cbbComm";
      cbbComm.Size = new Size(318, 37);
      cbbComm.TabIndex = 2;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.ColumnCount = 3;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
      tableLayoutPanel2.Controls.Add(btnConfirm, 1, 0);
      tableLayoutPanel2.Controls.Add(btnClose, 2, 0);
      tableLayoutPanel2.Location = new Point(10, 245);
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
      btnConfirm.Font = new Font("Roboto", 16F, FontStyle.Bold);
      btnConfirm.ForeColor = Color.White;
      btnConfirm.Image = Properties.Resources.icon_confirm;
      btnConfirm.ImageAlign = ContentAlignment.MiddleLeft;
      btnConfirm.Location = new Point(197, 3);
      btnConfirm.Name = "btnConfirm";
      btnConfirm.Padding = new Padding(10, 0, 0, 0);
      btnConfirm.Size = new Size(154, 54);
      btnConfirm.TabIndex = 0;
      btnConfirm.Text = "       Xác nhận";
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
      btnClose.Font = new Font("Roboto", 16F, FontStyle.Bold);
      btnClose.ForeColor = Color.White;
      btnClose.Image = Properties.Resources.icon_close;
      btnClose.ImageAlign = ContentAlignment.MiddleLeft;
      btnClose.Location = new Point(357, 3);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(154, 54);
      btnClose.TabIndex = 1;
      btnClose.Text = "       Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      btnClose.Click += btnClose_Click;
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(iconAutoConnect, 1, 2);
      tableLayoutPanel1.Controls.Add(cbbComm, 1, 0);
      tableLayoutPanel1.Controls.Add(label2, 0, 0);
      tableLayoutPanel1.Controls.Add(label5, 0, 1);
      tableLayoutPanel1.Controls.Add(label6, 0, 2);
      tableLayoutPanel1.Controls.Add(iconSendReq, 1, 1);
      tableLayoutPanel1.Location = new Point(10, 60);
      tableLayoutPanel1.Margin = new Padding(10, 0, 10, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 3;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel1.Size = new Size(514, 185);
      tableLayoutPanel1.TabIndex = 5;
      // 
      // iconAutoConnect
      // 
      iconAutoConnect.Anchor = AnchorStyles.Left;
      iconAutoConnect.Image = Properties.Resources.switch_off;
      iconAutoConnect.Location = new Point(193, 125);
      iconAutoConnect.Name = "iconAutoConnect";
      iconAutoConnect.Size = new Size(104, 57);
      iconAutoConnect.SizeMode = PictureBoxSizeMode.StretchImage;
      iconAutoConnect.TabIndex = 12;
      iconAutoConnect.TabStop = false;
      iconAutoConnect.Click += iconAutoConnect_Click;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.BackColor = Color.Transparent;
      label2.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label2.Location = new Point(0, 0);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(190, 61);
      label2.TabIndex = 1;
      label2.Text = "COM:";
      label2.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label5.AutoSize = true;
      label5.BackColor = Color.Transparent;
      label5.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label5.Location = new Point(0, 61);
      label5.Margin = new Padding(0);
      label5.Name = "label5";
      label5.Size = new Size(190, 61);
      label5.TabIndex = 5;
      label5.Text = "Gửi lệnh lấy dữ liệu:";
      label5.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label6
      // 
      label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label6.AutoSize = true;
      label6.BackColor = Color.Transparent;
      label6.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label6.Location = new Point(0, 122);
      label6.Margin = new Padding(0);
      label6.Name = "label6";
      label6.Size = new Size(190, 63);
      label6.TabIndex = 6;
      label6.Text = "Tự động kết nối:";
      label6.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // iconSendReq
      // 
      iconSendReq.Anchor = AnchorStyles.Left;
      iconSendReq.Image = Properties.Resources.switch_off;
      iconSendReq.Location = new Point(193, 64);
      iconSendReq.Name = "iconSendReq";
      iconSendReq.Size = new Size(104, 55);
      iconSendReq.SizeMode = PictureBoxSizeMode.StretchImage;
      iconSendReq.TabIndex = 11;
      iconSendReq.TabStop = false;
      iconSendReq.Click += iconSendReq_Click;
      // 
      // PopupSettingSerial
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(534, 315);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupSettingSerial";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)iconAutoConnect).EndInit();
      ((System.ComponentModel.ISupportInitialize)iconSendReq).EndInit();
      ResumeLayout(false);
    }

    private static void ConfigureLabel(Label label, string text, int tabIndex)
    {
      label.AutoSize = true;
      label.Dock = DockStyle.Fill;
      label.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label.Margin = Padding.Empty;
      label.TabIndex = tabIndex;
      label.Text = text;
      label.TextAlign = ContentAlignment.MiddleLeft;
    }

    private static void ConfigureTextBox(TextBox textBox, string text, int tabIndex)
    {
      textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      textBox.BorderStyle = BorderStyle.FixedSingle;
      textBox.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
      textBox.Margin = new Padding(3, 10, 3, 10);
      textBox.TabIndex = tabIndex;
      textBox.Text = text;
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private Label label1;
    private ComboBox cbbComm;
    private TableLayoutPanel tableLayoutPanel2;
    private Custom.RJButton btnConfirm;
    private Custom.RJButton btnClose;
    private TableLayoutPanel tableLayoutPanel1;
    private PictureBox iconAutoConnect;
    private Label label2;
    private Label label5;
    private Label label6;
    private PictureBox iconSendReq;
  }
}
