namespace LTP.Truck.MasterData
{
  partial class PopupDocument
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupDocument));
      tableLayoutPanel3 = new TableLayoutPanel();
      tableLayoutPanel5 = new TableLayoutPanel();
      tableLayoutPanel1 = new TableLayoutPanel();
      label15 = new Label();
      label8 = new Label();
      tableLayoutPanel4 = new TableLayoutPanel();
      label3 = new Label();
      label5 = new Label();
      txtValueTare = new Common.Custom.RJTextBox();
      txtDescription = new Common.Custom.RJTextBox();
      label4 = new Label();
      lbTitle = new Label();
      tableLayoutPanel2 = new TableLayoutPanel();
      btnConfirm = new Common.Custom.RJButton();
      btnClose = new Common.Custom.RJButton();
      dateTimePicker1 = new DateTimePicker();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel5.SuspendLayout();
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel4.SuspendLayout();
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
      tableLayoutPanel3.Size = new Size(685, 387);
      tableLayoutPanel3.TabIndex = 7;
      // 
      // tableLayoutPanel5
      // 
      tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel5.ColumnCount = 2;
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Controls.Add(tableLayoutPanel1, 0, 1);
      tableLayoutPanel5.Controls.Add(tableLayoutPanel4, 0, 0);
      tableLayoutPanel5.Controls.Add(txtValueTare, 1, 1);
      tableLayoutPanel5.Controls.Add(txtDescription, 1, 2);
      tableLayoutPanel5.Controls.Add(label4, 0, 2);
      tableLayoutPanel5.Controls.Add(dateTimePicker1, 1, 0);
      tableLayoutPanel5.Location = new Point(8, 68);
      tableLayoutPanel5.Name = "tableLayoutPanel5";
      tableLayoutPanel5.RowCount = 3;
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel5.Size = new Size(669, 246);
      tableLayoutPanel5.TabIndex = 8;
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Controls.Add(label15, 1, 0);
      tableLayoutPanel1.Controls.Add(label8, 0, 0);
      tableLayoutPanel1.Location = new Point(0, 82);
      tableLayoutPanel1.Margin = new Padding(0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Size = new Size(116, 82);
      tableLayoutPanel1.TabIndex = 38;
      // 
      // label15
      // 
      label15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label15.AutoSize = true;
      label15.BackColor = Color.Transparent;
      label15.Font = new Font("Roboto", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label15.ForeColor = Color.Red;
      label15.Location = new Point(89, 0);
      label15.Margin = new Padding(0);
      label15.Name = "label15";
      label15.Size = new Size(27, 82);
      label15.TabIndex = 4;
      label15.Text = "*";
      label15.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label8
      // 
      label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label8.AutoSize = true;
      label8.BackColor = Color.Transparent;
      label8.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label8.Location = new Point(0, 0);
      label8.Margin = new Padding(0);
      label8.Name = "label8";
      label8.Size = new Size(89, 82);
      label8.TabIndex = 3;
      label8.Text = "Chứng từ";
      label8.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel4
      // 
      tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel4.ColumnCount = 2;
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.Controls.Add(label3, 1, 0);
      tableLayoutPanel4.Controls.Add(label5, 0, 0);
      tableLayoutPanel4.Location = new Point(0, 0);
      tableLayoutPanel4.Margin = new Padding(0);
      tableLayoutPanel4.Name = "tableLayoutPanel4";
      tableLayoutPanel4.RowCount = 1;
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.Size = new Size(116, 82);
      tableLayoutPanel4.TabIndex = 39;
      // 
      // label3
      // 
      label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label3.AutoSize = true;
      label3.BackColor = Color.Transparent;
      label3.Font = new Font("Roboto", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label3.ForeColor = Color.Red;
      label3.Location = new Point(54, 0);
      label3.Margin = new Padding(0);
      label3.Name = "label3";
      label3.Size = new Size(62, 82);
      label3.TabIndex = 4;
      label3.Text = "*";
      label3.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label5.AutoSize = true;
      label5.BackColor = Color.Transparent;
      label5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label5.Location = new Point(0, 0);
      label5.Margin = new Padding(0);
      label5.Name = "label5";
      label5.Size = new Size(54, 82);
      label5.TabIndex = 3;
      label5.Text = "Ngày";
      label5.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtValueTare
      // 
      txtValueTare.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtValueTare.BackColor = SystemColors.Window;
      txtValueTare.BorderColor = Color.Black;
      txtValueTare.BorderFocusColor = Color.HotPink;
      txtValueTare.BorderRadius = 5;
      txtValueTare.BorderSize = 2;
      txtValueTare.Font = new Font("Roboto", 14F);
      txtValueTare.ForeColor = Color.FromArgb(64, 64, 64);
      txtValueTare.Location = new Point(120, 104);
      txtValueTare.Margin = new Padding(4);
      txtValueTare.Multiline = false;
      txtValueTare.Name = "txtValueTare";
      txtValueTare.Padding = new Padding(10, 7, 10, 7);
      txtValueTare.PasswordChar = false;
      txtValueTare.PlaceholderColor = Color.DarkGray;
      txtValueTare.PlaceholderText = "";
      txtValueTare.Size = new Size(545, 38);
      txtValueTare.TabIndex = 8;
      txtValueTare.Texts = "";
      txtValueTare.UnderlinedStyle = false;
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
      txtDescription.Location = new Point(120, 186);
      txtDescription.Margin = new Padding(4);
      txtDescription.Multiline = false;
      txtDescription.Name = "txtDescription";
      txtDescription.Padding = new Padding(10, 7, 10, 7);
      txtDescription.PasswordChar = false;
      txtDescription.PlaceholderColor = Color.DarkGray;
      txtDescription.PlaceholderText = "";
      txtDescription.Size = new Size(545, 38);
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
      label4.Location = new Point(0, 164);
      label4.Margin = new Padding(0);
      label4.Name = "label4";
      label4.Size = new Size(116, 82);
      label4.TabIndex = 2;
      label4.Text = "Mô tả";
      label4.TextAlign = ContentAlignment.MiddleLeft;
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
      lbTitle.Size = new Size(675, 60);
      lbTitle.TabIndex = 0;
      lbTitle.Text = "Chứng từ";
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
      tableLayoutPanel2.Location = new Point(5, 317);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(675, 60);
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
      btnConfirm.Location = new Point(318, 3);
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
      btnClose.Location = new Point(498, 3);
      btnClose.Name = "btnClose";
      btnClose.Padding = new Padding(10, 0, 0, 0);
      btnClose.Size = new Size(174, 54);
      btnClose.TabIndex = 1;
      btnClose.Text = "       Đóng";
      btnClose.TextColor = Color.White;
      btnClose.UseVisualStyleBackColor = false;
      // 
      // dateTimePicker1
      // 
      dateTimePicker1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      dateTimePicker1.Font = new Font("Roboto", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
      dateTimePicker1.Format = DateTimePickerFormat.Short;
      dateTimePicker1.Location = new Point(119, 24);
      dateTimePicker1.Name = "dateTimePicker1";
      dateTimePicker1.Size = new Size(547, 33);
      dateTimePicker1.TabIndex = 40;
      // 
      // PopupDocument
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(685, 387);
      ControlBox = false;
      Controls.Add(tableLayoutPanel3);
      Name = "PopupDocument";
      StartPosition = FormStartPosition.CenterParent;
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel5.ResumeLayout(false);
      tableLayoutPanel5.PerformLayout();
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      tableLayoutPanel4.ResumeLayout(false);
      tableLayoutPanel4.PerformLayout();
      tableLayoutPanel2.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel3;
    private TableLayoutPanel tableLayoutPanel5;
    private TableLayoutPanel tableLayoutPanel1;
    private Label label15;
    private Label label8;
    private TableLayoutPanel tableLayoutPanel4;
    private Label label3;
    private Label label5;
    private Common.Custom.RJTextBox txtValueTare;
    private Common.Custom.RJTextBox txtDescription;
    private Label label4;
    private Label lbTitle;
    private TableLayoutPanel tableLayoutPanel2;
    private Common.Custom.RJButton btnConfirm;
    private Common.Custom.RJButton btnClose;
    private DateTimePicker dateTimePicker1;
  }
}