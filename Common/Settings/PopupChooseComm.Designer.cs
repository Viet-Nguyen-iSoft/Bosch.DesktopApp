namespace Common.Settings
{
  partial class PopupChooseComm
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
      label2 = new Label();
      cbbConnectionType = new ComboBox();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel1.SuspendLayout();
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
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
      tableLayoutPanel3.Size = new Size(534, 241);
      tableLayoutPanel3.TabIndex = 2;
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
      label1.Text = "Chọn chuẩn kết nối";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(label2, 0, 0);
      tableLayoutPanel1.Controls.Add(cbbConnectionType, 1, 0);
      tableLayoutPanel1.Location = new Point(10, 60);
      tableLayoutPanel1.Margin = new Padding(10, 0, 10, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel1.Size = new Size(514, 80);
      tableLayoutPanel1.TabIndex = 3;
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
      label2.Size = new Size(140, 80);
      label2.TabIndex = 1;
      label2.Text = "Chuẩn kết nối";
      label2.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // cbbConnectionType
      // 
      cbbConnectionType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      cbbConnectionType.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbConnectionType.Font = new Font("Roboto", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
      cbbConnectionType.FormattingEnabled = true;
      cbbConnectionType.Location = new Point(143, 21);
      cbbConnectionType.Name = "cbbConnectionType";
      cbbConnectionType.Size = new Size(368, 37);
      cbbConnectionType.TabIndex = 2;
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
      tableLayoutPanel2.Location = new Point(10, 171);
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
      // PopupChooseComm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(534, 241);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupChooseComm";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private Label label1;
    private TableLayoutPanel tableLayoutPanel1;
    private Label label2;
    private ComboBox cbbConnectionType;
    private TableLayoutPanel tableLayoutPanel2;
    private Custom.RJButton btnConfirm;
    private Custom.RJButton btnClose;
  }
}