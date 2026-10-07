

using LTP.Truck.Custom;

namespace LTP.Truck.Forms
{
  partial class FrmHomeTruck
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
      DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
      tableLayoutPanel1 = new TableLayoutPanel();
      tableLayoutPanel2 = new TableLayoutPanel();
      tableLayoutPanel3 = new TableLayoutPanel();
      tableLayoutPanel12 = new TableLayoutPanel();
      tableLayoutPanel17 = new TableLayoutPanel();
      lbWeightTrigger = new Label();
      label2 = new Label();
      tableLayoutPanel18 = new TableLayoutPanel();
      label3 = new Label();
      lbWeightValue = new Label();
      label5 = new Label();
      label1 = new Label();
      tableLayoutPanel6 = new TableLayoutPanel();
      btnWeightTime02 = new RJButton();
      btnWeightTime01 = new RJButton();
      btnTriggerWeight = new RJButton();
      btnBack = new RJButton();
      btnZero = new RJButton();
      btnCreate = new RJButton();
      tableLayoutPanel4 = new TableLayoutPanel();
      tableLayoutPanel8 = new TableLayoutPanel();
      tableLayoutPanel20 = new TableLayoutPanel();
      label9 = new Label();
      txtValueTareForTruck = new RJTextBox();
      cbbTareForTruck = new ComboBox();
      label11 = new Label();
      numericUpDownNumberTare = new NumericUpDown();
      label7 = new Label();
      tableLayoutPanel22 = new TableLayoutPanel();
      label16 = new Label();
      label25 = new Label();
      label23 = new Label();
      label24 = new Label();
      tableLayoutPanel19 = new TableLayoutPanel();
      label21 = new Label();
      label22 = new Label();
      label20 = new Label();
      txtNameDriver = new RJTextBox();
      txtNoLabel = new RJTextBox();
      label19 = new Label();
      tableLayoutPanel15 = new TableLayoutPanel();
      txtWareHouse = new RJTextBox();
      btnLoadWarehouse = new RJButton();
      tableLayoutPanel14 = new TableLayoutPanel();
      txtTypeGoods = new RJTextBox();
      btnLoadTypeGoods = new RJButton();
      tableLayoutPanel13 = new TableLayoutPanel();
      txtClient = new RJTextBox();
      btnLoadClient = new RJButton();
      label6 = new Label();
      tableLayoutPanel5 = new TableLayoutPanel();
      label15 = new Label();
      label8 = new Label();
      tableLayoutPanel11 = new TableLayoutPanel();
      label12 = new Label();
      txtNoLabelAuto = new RJTextBox();
      cbbType = new ComboBox();
      tableLayoutPanel16 = new TableLayoutPanel();
      txtLicensePlate = new RJTextBox();
      label13 = new Label();
      txtIdCard = new RJTextBox();
      txtDocument = new TextBox();
      label10 = new Label();
      tableLayoutPanel9 = new TableLayoutPanel();
      ucItemOffsetWeightAndTare = new LTP.Truck.UserControls.UcItem();
      ucItemTareForTruck = new LTP.Truck.UserControls.UcItem();
      ucItemOffsetWeight = new LTP.Truck.UserControls.UcItem();
      label14 = new Label();
      ucItemWeight01 = new LTP.Truck.UserControls.UcItem();
      ucItemWeight02 = new LTP.Truck.UserControls.UcItem();
      tableLayoutPanel7 = new TableLayoutPanel();
      dgv = new DataGridView();
      tableLayoutPanel10 = new TableLayoutPanel();
      label4 = new Label();
      txtSearchKey = new RJTextBox();
      label17 = new Label();
      label18 = new Label();
      ucTimeSearchFrom = new LTP.Truck.UserControls.UcTimeSearch();
      ucTimeSearchTo = new LTP.Truck.UserControls.UcTimeSearch();
      btnFilter = new Common.Custom.RJButton();
      btnPrint = new RJButton();
      btnSearchHistorical = new RJButton();
      btnAddManual = new RJButton();
      label27 = new Label();
      tableLayoutPanel1.SuspendLayout();
      tableLayoutPanel2.SuspendLayout();
      tableLayoutPanel3.SuspendLayout();
      tableLayoutPanel12.SuspendLayout();
      tableLayoutPanel17.SuspendLayout();
      tableLayoutPanel18.SuspendLayout();
      tableLayoutPanel6.SuspendLayout();
      tableLayoutPanel4.SuspendLayout();
      tableLayoutPanel8.SuspendLayout();
      tableLayoutPanel20.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)numericUpDownNumberTare).BeginInit();
      tableLayoutPanel22.SuspendLayout();
      tableLayoutPanel19.SuspendLayout();
      tableLayoutPanel15.SuspendLayout();
      tableLayoutPanel14.SuspendLayout();
      tableLayoutPanel13.SuspendLayout();
      tableLayoutPanel5.SuspendLayout();
      tableLayoutPanel11.SuspendLayout();
      tableLayoutPanel16.SuspendLayout();
      tableLayoutPanel9.SuspendLayout();
      tableLayoutPanel7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
      tableLayoutPanel10.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.BackColor = Color.White;
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
      tableLayoutPanel1.Controls.Add(tableLayoutPanel7, 0, 2);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(0, 0);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 4;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel1.Size = new Size(1530, 1084);
      tableLayoutPanel1.TabIndex = 1;
      // 
      // tableLayoutPanel2
      // 
      tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel2.ColumnCount = 5;
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
      tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
      tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 2, 0);
      tableLayoutPanel2.Controls.Add(tableLayoutPanel9, 4, 0);
      tableLayoutPanel2.Location = new Point(0, 0);
      tableLayoutPanel2.Margin = new Padding(0);
      tableLayoutPanel2.Name = "tableLayoutPanel2";
      tableLayoutPanel2.RowCount = 1;
      tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel2.Size = new Size(1525, 590);
      tableLayoutPanel2.TabIndex = 0;
      // 
      // tableLayoutPanel3
      // 
      tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel3.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel3.ColumnCount = 1;
      tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.Controls.Add(tableLayoutPanel12, 0, 2);
      tableLayoutPanel3.Controls.Add(label5, 0, 1);
      tableLayoutPanel3.Controls.Add(label1, 0, 0);
      tableLayoutPanel3.Controls.Add(tableLayoutPanel6, 0, 4);
      tableLayoutPanel3.Location = new Point(0, 0);
      tableLayoutPanel3.Margin = new Padding(0);
      tableLayoutPanel3.Name = "tableLayoutPanel3";
      tableLayoutPanel3.RowCount = 6;
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 125F));
      tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel3.Size = new Size(632, 590);
      tableLayoutPanel3.TabIndex = 0;
      // 
      // tableLayoutPanel12
      // 
      tableLayoutPanel12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel12.ColumnCount = 1;
      tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel12.Controls.Add(tableLayoutPanel17, 0, 1);
      tableLayoutPanel12.Controls.Add(tableLayoutPanel18, 0, 0);
      tableLayoutPanel12.Location = new Point(0, 100);
      tableLayoutPanel12.Margin = new Padding(0);
      tableLayoutPanel12.Name = "tableLayoutPanel12";
      tableLayoutPanel12.RowCount = 2;
      tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel12.Size = new Size(632, 355);
      tableLayoutPanel12.TabIndex = 4;
      // 
      // tableLayoutPanel17
      // 
      tableLayoutPanel17.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel17.ColumnCount = 2;
      tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel17.Controls.Add(lbWeightTrigger, 1, 0);
      tableLayoutPanel17.Controls.Add(label2, 0, 0);
      tableLayoutPanel17.Location = new Point(0, 305);
      tableLayoutPanel17.Margin = new Padding(0);
      tableLayoutPanel17.Name = "tableLayoutPanel17";
      tableLayoutPanel17.RowCount = 1;
      tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel17.Size = new Size(632, 50);
      tableLayoutPanel17.TabIndex = 3;
      // 
      // lbWeightTrigger
      // 
      lbWeightTrigger.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbWeightTrigger.AutoSize = true;
      lbWeightTrigger.BackColor = Color.Transparent;
      lbWeightTrigger.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lbWeightTrigger.Location = new Point(222, 0);
      lbWeightTrigger.Margin = new Padding(0);
      lbWeightTrigger.Name = "lbWeightTrigger";
      lbWeightTrigger.Size = new Size(410, 50);
      lbWeightTrigger.TabIndex = 4;
      lbWeightTrigger.Text = "0.000";
      lbWeightTrigger.TextAlign = ContentAlignment.BottomLeft;
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.BackColor = Color.Transparent;
      label2.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label2.Location = new Point(0, 0);
      label2.Margin = new Padding(0);
      label2.Name = "label2";
      label2.Size = new Size(222, 50);
      label2.TabIndex = 3;
      label2.Text = "Giá trị cân xác nhận(Kg):";
      label2.TextAlign = ContentAlignment.BottomLeft;
      // 
      // tableLayoutPanel18
      // 
      tableLayoutPanel18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel18.ColumnCount = 2;
      tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel18.Controls.Add(label3, 1, 0);
      tableLayoutPanel18.Controls.Add(lbWeightValue, 0, 0);
      tableLayoutPanel18.Location = new Point(0, 0);
      tableLayoutPanel18.Margin = new Padding(0);
      tableLayoutPanel18.Name = "tableLayoutPanel18";
      tableLayoutPanel18.RowCount = 1;
      tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel18.Size = new Size(632, 305);
      tableLayoutPanel18.TabIndex = 4;
      // 
      // label3
      // 
      label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label3.AutoSize = true;
      label3.BackColor = Color.Transparent;
      label3.Font = new Font("Roboto", 39.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label3.Location = new Point(542, 0);
      label3.Margin = new Padding(0);
      label3.Name = "label3";
      label3.Size = new Size(90, 305);
      label3.TabIndex = 3;
      label3.Text = "Kg";
      label3.TextAlign = ContentAlignment.BottomLeft;
      // 
      // lbWeightValue
      // 
      lbWeightValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      lbWeightValue.AutoSize = true;
      lbWeightValue.BackColor = Color.Transparent;
      lbWeightValue.Font = new Font("Roboto", 120F, FontStyle.Bold);
      lbWeightValue.Location = new Point(0, 0);
      lbWeightValue.Margin = new Padding(0);
      lbWeightValue.Name = "lbWeightValue";
      lbWeightValue.Size = new Size(542, 305);
      lbWeightValue.TabIndex = 2;
      lbWeightValue.Text = "---";
      lbWeightValue.TextAlign = ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label5.AutoSize = true;
      label5.BackColor = Color.Transparent;
      label5.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label5.Location = new Point(0, 50);
      label5.Margin = new Padding(0);
      label5.Name = "label5";
      label5.Size = new Size(632, 50);
      label5.TabIndex = 2;
      label5.Text = "Trạng thái cân";
      label5.TextAlign = ContentAlignment.MiddleRight;
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
      label1.Size = new Size(632, 50);
      label1.TabIndex = 0;
      label1.Text = "Thông tin cân";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel6
      // 
      tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel6.ColumnCount = 3;
      tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
      tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
      tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
      tableLayoutPanel6.Controls.Add(btnWeightTime02, 2, 0);
      tableLayoutPanel6.Controls.Add(btnWeightTime01, 1, 0);
      tableLayoutPanel6.Controls.Add(btnTriggerWeight, 0, 0);
      tableLayoutPanel6.Controls.Add(btnBack, 0, 1);
      tableLayoutPanel6.Controls.Add(btnZero, 1, 1);
      tableLayoutPanel6.Controls.Add(btnCreate, 2, 1);
      tableLayoutPanel6.Location = new Point(0, 460);
      tableLayoutPanel6.Margin = new Padding(0);
      tableLayoutPanel6.Name = "tableLayoutPanel6";
      tableLayoutPanel6.Padding = new Padding(5, 0, 5, 0);
      tableLayoutPanel6.RowCount = 2;
      tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel6.Size = new Size(632, 125);
      tableLayoutPanel6.TabIndex = 2;
      // 
      // btnWeightTime02
      // 
      btnWeightTime02.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnWeightTime02.BackColor = Color.FromArgb(64, 107, 177);
      btnWeightTime02.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnWeightTime02.BorderColor = Color.White;
      btnWeightTime02.BorderRadius = 5;
      btnWeightTime02.BorderSize = 0;
      btnWeightTime02.FlatAppearance.BorderColor = Color.White;
      btnWeightTime02.FlatAppearance.BorderSize = 0;
      btnWeightTime02.FlatStyle = FlatStyle.Flat;
      btnWeightTime02.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnWeightTime02.ForeColor = Color.White;
      btnWeightTime02.Image = Properties.Resources.icon_weight;
      btnWeightTime02.ImageAlign = ContentAlignment.MiddleLeft;
      btnWeightTime02.Location = new Point(422, 3);
      btnWeightTime02.Name = "btnWeightTime02";
      btnWeightTime02.Padding = new Padding(10, 0, 0, 0);
      btnWeightTime02.Size = new Size(202, 56);
      btnWeightTime02.TabIndex = 19;
      btnWeightTime02.Text = "Cân lần 02";
      btnWeightTime02.TextColor = Color.White;
      btnWeightTime02.UseVisualStyleBackColor = false;
      btnWeightTime02.Click += btnWeightTime02_Click;
      // 
      // btnWeightTime01
      // 
      btnWeightTime01.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnWeightTime01.BackColor = Color.FromArgb(64, 107, 177);
      btnWeightTime01.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnWeightTime01.BorderColor = Color.White;
      btnWeightTime01.BorderRadius = 5;
      btnWeightTime01.BorderSize = 0;
      btnWeightTime01.FlatAppearance.BorderColor = Color.White;
      btnWeightTime01.FlatAppearance.BorderSize = 0;
      btnWeightTime01.FlatStyle = FlatStyle.Flat;
      btnWeightTime01.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnWeightTime01.ForeColor = Color.White;
      btnWeightTime01.Image = Properties.Resources.icon_weight;
      btnWeightTime01.ImageAlign = ContentAlignment.MiddleLeft;
      btnWeightTime01.Location = new Point(215, 3);
      btnWeightTime01.Name = "btnWeightTime01";
      btnWeightTime01.Padding = new Padding(10, 0, 0, 0);
      btnWeightTime01.Size = new Size(201, 56);
      btnWeightTime01.TabIndex = 18;
      btnWeightTime01.Text = "Cân lần 01";
      btnWeightTime01.TextColor = Color.White;
      btnWeightTime01.UseVisualStyleBackColor = false;
      btnWeightTime01.Click += btnWeightTime01_Click;
      // 
      // btnTriggerWeight
      // 
      btnTriggerWeight.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnTriggerWeight.BackColor = Color.FromArgb(64, 107, 177);
      btnTriggerWeight.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnTriggerWeight.BorderColor = Color.White;
      btnTriggerWeight.BorderRadius = 5;
      btnTriggerWeight.BorderSize = 0;
      btnTriggerWeight.FlatAppearance.BorderColor = Color.White;
      btnTriggerWeight.FlatAppearance.BorderSize = 0;
      btnTriggerWeight.FlatStyle = FlatStyle.Flat;
      btnTriggerWeight.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnTriggerWeight.ForeColor = Color.White;
      btnTriggerWeight.Image = Properties.Resources.icon_weight_log;
      btnTriggerWeight.ImageAlign = ContentAlignment.MiddleLeft;
      btnTriggerWeight.Location = new Point(8, 3);
      btnTriggerWeight.Name = "btnTriggerWeight";
      btnTriggerWeight.Padding = new Padding(10, 0, 0, 0);
      btnTriggerWeight.Size = new Size(201, 55);
      btnTriggerWeight.TabIndex = 17;
      btnTriggerWeight.Text = "Xác nhận cân";
      btnTriggerWeight.TextColor = Color.White;
      btnTriggerWeight.UseVisualStyleBackColor = false;
      btnTriggerWeight.Click += btnTriggerWeight_Click;
      // 
      // btnBack
      // 
      btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnBack.BackColor = Color.FromArgb(64, 107, 177);
      btnBack.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnBack.BorderColor = Color.White;
      btnBack.BorderRadius = 5;
      btnBack.BorderSize = 0;
      btnBack.FlatAppearance.BorderColor = Color.White;
      btnBack.FlatAppearance.BorderSize = 0;
      btnBack.FlatStyle = FlatStyle.Flat;
      btnBack.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnBack.ForeColor = Color.White;
      btnBack.Image = Properties.Resources.icon_back;
      btnBack.ImageAlign = ContentAlignment.MiddleLeft;
      btnBack.Location = new Point(8, 65);
      btnBack.Name = "btnBack";
      btnBack.Padding = new Padding(10, 0, 0, 0);
      btnBack.Size = new Size(201, 57);
      btnBack.TabIndex = 21;
      btnBack.Text = "Quay lại";
      btnBack.TextColor = Color.White;
      btnBack.UseVisualStyleBackColor = false;
      btnBack.Click += btnBack_Click;
      // 
      // btnZero
      // 
      btnZero.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnZero.BackColor = Color.FromArgb(64, 107, 177);
      btnZero.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnZero.BorderColor = Color.White;
      btnZero.BorderRadius = 5;
      btnZero.BorderSize = 0;
      btnZero.FlatAppearance.BorderColor = Color.White;
      btnZero.FlatAppearance.BorderSize = 0;
      btnZero.FlatStyle = FlatStyle.Flat;
      btnZero.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnZero.ForeColor = Color.White;
      btnZero.Image = Properties.Resources.icon_zero;
      btnZero.ImageAlign = ContentAlignment.MiddleLeft;
      btnZero.Location = new Point(215, 65);
      btnZero.Name = "btnZero";
      btnZero.Padding = new Padding(10, 0, 0, 0);
      btnZero.Size = new Size(201, 57);
      btnZero.TabIndex = 22;
      btnZero.Text = "Zero";
      btnZero.TextColor = Color.White;
      btnZero.UseVisualStyleBackColor = false;
      btnZero.Click += btnZero_Click;
      // 
      // btnCreate
      // 
      btnCreate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnCreate.BackColor = Color.FromArgb(64, 107, 177);
      btnCreate.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnCreate.BorderColor = Color.White;
      btnCreate.BorderRadius = 5;
      btnCreate.BorderSize = 0;
      btnCreate.FlatAppearance.BorderColor = Color.White;
      btnCreate.FlatAppearance.BorderSize = 0;
      btnCreate.FlatStyle = FlatStyle.Flat;
      btnCreate.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnCreate.ForeColor = Color.White;
      btnCreate.Image = Properties.Resources.icon_new;
      btnCreate.ImageAlign = ContentAlignment.MiddleLeft;
      btnCreate.Location = new Point(422, 65);
      btnCreate.Name = "btnCreate";
      btnCreate.Padding = new Padding(15, 0, 0, 0);
      btnCreate.Size = new Size(202, 57);
      btnCreate.TabIndex = 23;
      btnCreate.Text = "Phiếu mới";
      btnCreate.TextColor = Color.White;
      btnCreate.UseVisualStyleBackColor = false;
      btnCreate.Click += btnCreate_Click;
      // 
      // tableLayoutPanel4
      // 
      tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel4.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel4.ColumnCount = 1;
      tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.Controls.Add(tableLayoutPanel8, 0, 1);
      tableLayoutPanel4.Controls.Add(label10, 0, 0);
      tableLayoutPanel4.Location = new Point(637, 0);
      tableLayoutPanel4.Margin = new Padding(0);
      tableLayoutPanel4.Name = "tableLayoutPanel4";
      tableLayoutPanel4.RowCount = 2;
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel4.Size = new Size(632, 590);
      tableLayoutPanel4.TabIndex = 1;
      // 
      // tableLayoutPanel8
      // 
      tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel8.ColumnCount = 2;
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel8.Controls.Add(tableLayoutPanel20, 1, 7);
      tableLayoutPanel8.Controls.Add(label7, 0, 7);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel22, 0, 6);
      tableLayoutPanel8.Controls.Add(label23, 0, 4);
      tableLayoutPanel8.Controls.Add(label24, 0, 5);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel19, 0, 3);
      tableLayoutPanel8.Controls.Add(label20, 0, 1);
      tableLayoutPanel8.Controls.Add(txtNameDriver, 1, 5);
      tableLayoutPanel8.Controls.Add(txtNoLabel, 1, 1);
      tableLayoutPanel8.Controls.Add(label19, 0, 9);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel15, 1, 4);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel14, 1, 3);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel13, 1, 2);
      tableLayoutPanel8.Controls.Add(label6, 0, 0);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel5, 0, 2);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel11, 1, 0);
      tableLayoutPanel8.Controls.Add(tableLayoutPanel16, 1, 6);
      tableLayoutPanel8.Controls.Add(txtDocument, 1, 9);
      tableLayoutPanel8.Location = new Point(3, 53);
      tableLayoutPanel8.Name = "tableLayoutPanel8";
      tableLayoutPanel8.RowCount = 10;
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
      tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
      tableLayoutPanel8.Size = new Size(626, 534);
      tableLayoutPanel8.TabIndex = 2;
      // 
      // tableLayoutPanel20
      // 
      tableLayoutPanel20.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel20.ColumnCount = 7;
      tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
      tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
      tableLayoutPanel20.Controls.Add(label9, 5, 0);
      tableLayoutPanel20.Controls.Add(txtValueTareForTruck, 6, 0);
      tableLayoutPanel20.Controls.Add(cbbTareForTruck, 0, 0);
      tableLayoutPanel20.Controls.Add(label11, 2, 0);
      tableLayoutPanel20.Controls.Add(numericUpDownNumberTare, 3, 0);
      tableLayoutPanel20.Location = new Point(139, 378);
      tableLayoutPanel20.Margin = new Padding(0);
      tableLayoutPanel20.Name = "tableLayoutPanel20";
      tableLayoutPanel20.RowCount = 1;
      tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel20.Size = new Size(487, 54);
      tableLayoutPanel20.TabIndex = 39;
      // 
      // label9
      // 
      label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label9.AutoSize = true;
      label9.BackColor = Color.Transparent;
      label9.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label9.Location = new Point(285, 0);
      label9.Margin = new Padding(0);
      label9.Name = "label9";
      label9.Size = new Size(102, 54);
      label9.TabIndex = 17;
      label9.Text = "Giá trị (Kg)";
      label9.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtValueTareForTruck
      // 
      txtValueTareForTruck.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtValueTareForTruck.BackColor = SystemColors.Window;
      txtValueTareForTruck.BorderColor = Color.Black;
      txtValueTareForTruck.BorderFocusColor = Color.HotPink;
      txtValueTareForTruck.BorderRadius = 5;
      txtValueTareForTruck.BorderSize = 2;
      txtValueTareForTruck.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtValueTareForTruck.ForeColor = Color.FromArgb(64, 64, 64);
      txtValueTareForTruck.Location = new Point(391, 8);
      txtValueTareForTruck.Margin = new Padding(4);
      txtValueTareForTruck.Multiline = false;
      txtValueTareForTruck.Name = "txtValueTareForTruck";
      txtValueTareForTruck.Padding = new Padding(10, 7, 10, 7);
      txtValueTareForTruck.PasswordChar = false;
      txtValueTareForTruck.PlaceholderColor = Color.DarkGray;
      txtValueTareForTruck.PlaceholderText = "";
      txtValueTareForTruck.Size = new Size(92, 38);
      txtValueTareForTruck.TabIndex = 18;
      txtValueTareForTruck.Texts = "";
      txtValueTareForTruck.UnderlinedStyle = false;
      // 
      // cbbTareForTruck
      // 
      cbbTareForTruck.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      cbbTareForTruck.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbTareForTruck.Font = new Font("Roboto", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
      cbbTareForTruck.FormattingEnabled = true;
      cbbTareForTruck.Location = new Point(3, 10);
      cbbTareForTruck.Name = "cbbTareForTruck";
      cbbTareForTruck.Size = new Size(108, 33);
      cbbTareForTruck.TabIndex = 19;
      // 
      // label11
      // 
      label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label11.AutoSize = true;
      label11.BackColor = Color.Transparent;
      label11.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label11.Location = new Point(134, 0);
      label11.Margin = new Padding(0);
      label11.Name = "label11";
      label11.Size = new Size(31, 54);
      label11.TabIndex = 20;
      label11.Text = "SL";
      label11.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // numericUpDownNumberTare
      // 
      numericUpDownNumberTare.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      numericUpDownNumberTare.Font = new Font("Roboto", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
      numericUpDownNumberTare.Location = new Point(168, 10);
      numericUpDownNumberTare.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      numericUpDownNumberTare.Name = "numericUpDownNumberTare";
      numericUpDownNumberTare.Size = new Size(94, 33);
      numericUpDownNumberTare.TabIndex = 21;
      numericUpDownNumberTare.TextAlign = HorizontalAlignment.Right;
      numericUpDownNumberTare.Value = new decimal(new int[] { 1, 0, 0, 0 });
      // 
      // label7
      // 
      label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label7.AutoSize = true;
      label7.BackColor = Color.Transparent;
      label7.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label7.Location = new Point(0, 378);
      label7.Margin = new Padding(0);
      label7.Name = "label7";
      label7.Size = new Size(139, 54);
      label7.TabIndex = 38;
      label7.Text = "Loại Tare";
      label7.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel22
      // 
      tableLayoutPanel22.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel22.ColumnCount = 2;
      tableLayoutPanel22.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel22.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel22.Controls.Add(label16, 1, 0);
      tableLayoutPanel22.Controls.Add(label25, 0, 0);
      tableLayoutPanel22.Location = new Point(0, 324);
      tableLayoutPanel22.Margin = new Padding(0);
      tableLayoutPanel22.Name = "tableLayoutPanel22";
      tableLayoutPanel22.RowCount = 1;
      tableLayoutPanel22.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel22.Size = new Size(139, 54);
      tableLayoutPanel22.TabIndex = 37;
      // 
      // label16
      // 
      label16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label16.AutoSize = true;
      label16.BackColor = Color.Transparent;
      label16.Font = new Font("Roboto", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label16.ForeColor = Color.Red;
      label16.Location = new Point(74, 0);
      label16.Margin = new Padding(0);
      label16.Name = "label16";
      label16.Size = new Size(65, 54);
      label16.TabIndex = 4;
      label16.Text = "*";
      label16.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label25
      // 
      label25.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label25.AutoSize = true;
      label25.BackColor = Color.Transparent;
      label25.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label25.Location = new Point(0, 0);
      label25.Margin = new Padding(0);
      label25.Name = "label25";
      label25.Size = new Size(74, 54);
      label25.TabIndex = 3;
      label25.Text = "Biển số";
      label25.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label23
      // 
      label23.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label23.AutoSize = true;
      label23.BackColor = Color.Transparent;
      label23.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label23.Location = new Point(0, 216);
      label23.Margin = new Padding(0);
      label23.Name = "label23";
      label23.Size = new Size(139, 54);
      label23.TabIndex = 3;
      label23.Text = "Kho hàng";
      label23.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label24
      // 
      label24.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label24.AutoSize = true;
      label24.BackColor = Color.Transparent;
      label24.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label24.Location = new Point(0, 270);
      label24.Margin = new Padding(0);
      label24.Name = "label24";
      label24.Size = new Size(139, 54);
      label24.TabIndex = 3;
      label24.Text = "Tên lái xe";
      label24.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel19
      // 
      tableLayoutPanel19.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel19.ColumnCount = 2;
      tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel19.Controls.Add(label21, 1, 0);
      tableLayoutPanel19.Controls.Add(label22, 0, 0);
      tableLayoutPanel19.Location = new Point(0, 162);
      tableLayoutPanel19.Margin = new Padding(0);
      tableLayoutPanel19.Name = "tableLayoutPanel19";
      tableLayoutPanel19.RowCount = 1;
      tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel19.Size = new Size(139, 54);
      tableLayoutPanel19.TabIndex = 34;
      // 
      // label21
      // 
      label21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label21.AutoSize = true;
      label21.BackColor = Color.Transparent;
      label21.Font = new Font("Roboto", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label21.ForeColor = Color.Red;
      label21.Location = new Point(93, 0);
      label21.Margin = new Padding(0);
      label21.Name = "label21";
      label21.Size = new Size(46, 54);
      label21.TabIndex = 4;
      label21.Text = "*";
      label21.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label22
      // 
      label22.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label22.AutoSize = true;
      label22.BackColor = Color.Transparent;
      label22.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label22.Location = new Point(0, 0);
      label22.Margin = new Padding(0);
      label22.Name = "label22";
      label22.Size = new Size(93, 54);
      label22.TabIndex = 3;
      label22.Text = "Loại hàng";
      label22.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label20
      // 
      label20.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label20.AutoSize = true;
      label20.BackColor = Color.Transparent;
      label20.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label20.Location = new Point(0, 54);
      label20.Margin = new Padding(0);
      label20.Name = "label20";
      label20.Size = new Size(139, 54);
      label20.TabIndex = 24;
      label20.Text = "Phiếu nhà máy";
      label20.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtNameDriver
      // 
      txtNameDriver.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtNameDriver.BackColor = SystemColors.Window;
      txtNameDriver.BorderColor = Color.Black;
      txtNameDriver.BorderFocusColor = Color.HotPink;
      txtNameDriver.BorderRadius = 5;
      txtNameDriver.BorderSize = 2;
      txtNameDriver.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtNameDriver.ForeColor = Color.FromArgb(64, 64, 64);
      txtNameDriver.Location = new Point(143, 278);
      txtNameDriver.Margin = new Padding(4);
      txtNameDriver.Multiline = false;
      txtNameDriver.Name = "txtNameDriver";
      txtNameDriver.Padding = new Padding(10, 7, 10, 7);
      txtNameDriver.PasswordChar = false;
      txtNameDriver.PlaceholderColor = Color.DarkGray;
      txtNameDriver.PlaceholderText = "";
      txtNameDriver.Size = new Size(479, 38);
      txtNameDriver.TabIndex = 15;
      txtNameDriver.Texts = "";
      txtNameDriver.UnderlinedStyle = false;
      // 
      // txtNoLabel
      // 
      txtNoLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtNoLabel.BackColor = SystemColors.Window;
      txtNoLabel.BorderColor = Color.Black;
      txtNoLabel.BorderFocusColor = Color.HotPink;
      txtNoLabel.BorderRadius = 5;
      txtNoLabel.BorderSize = 2;
      txtNoLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtNoLabel.ForeColor = Color.FromArgb(64, 64, 64);
      txtNoLabel.Location = new Point(143, 62);
      txtNoLabel.Margin = new Padding(4);
      txtNoLabel.Multiline = false;
      txtNoLabel.Name = "txtNoLabel";
      txtNoLabel.Padding = new Padding(10, 7, 10, 7);
      txtNoLabel.PasswordChar = false;
      txtNoLabel.PlaceholderColor = Color.DarkGray;
      txtNoLabel.PlaceholderText = "";
      txtNoLabel.Size = new Size(479, 38);
      txtNoLabel.TabIndex = 16;
      txtNoLabel.Texts = "";
      txtNoLabel.UnderlinedStyle = false;
      // 
      // label19
      // 
      label19.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label19.AutoSize = true;
      label19.BackColor = Color.Transparent;
      label19.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label19.Location = new Point(0, 437);
      label19.Margin = new Padding(0);
      label19.Name = "label19";
      label19.Size = new Size(139, 97);
      label19.TabIndex = 17;
      label19.Text = "Ghi chú";
      // 
      // tableLayoutPanel15
      // 
      tableLayoutPanel15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel15.ColumnCount = 2;
      tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel15.Controls.Add(txtWareHouse, 0, 0);
      tableLayoutPanel15.Controls.Add(btnLoadWarehouse, 1, 0);
      tableLayoutPanel15.Location = new Point(139, 216);
      tableLayoutPanel15.Margin = new Padding(0);
      tableLayoutPanel15.Name = "tableLayoutPanel15";
      tableLayoutPanel15.RowCount = 1;
      tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel15.Size = new Size(487, 54);
      tableLayoutPanel15.TabIndex = 20;
      // 
      // txtWareHouse
      // 
      txtWareHouse.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtWareHouse.BackColor = SystemColors.Window;
      txtWareHouse.BorderColor = Color.Black;
      txtWareHouse.BorderFocusColor = Color.HotPink;
      txtWareHouse.BorderRadius = 5;
      txtWareHouse.BorderSize = 2;
      txtWareHouse.Enabled = false;
      txtWareHouse.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtWareHouse.ForeColor = Color.FromArgb(64, 64, 64);
      txtWareHouse.Location = new Point(4, 8);
      txtWareHouse.Margin = new Padding(4);
      txtWareHouse.Multiline = false;
      txtWareHouse.Name = "txtWareHouse";
      txtWareHouse.Padding = new Padding(10, 7, 10, 7);
      txtWareHouse.PasswordChar = false;
      txtWareHouse.PlaceholderColor = Color.DarkGray;
      txtWareHouse.PlaceholderText = "";
      txtWareHouse.Size = new Size(419, 38);
      txtWareHouse.TabIndex = 15;
      txtWareHouse.Texts = "";
      txtWareHouse.UnderlinedStyle = false;
      // 
      // btnLoadWarehouse
      // 
      btnLoadWarehouse.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnLoadWarehouse.BackColor = Color.White;
      btnLoadWarehouse.BackgroundColor = Color.White;
      btnLoadWarehouse.BorderColor = Color.Black;
      btnLoadWarehouse.BorderRadius = 5;
      btnLoadWarehouse.BorderSize = 0;
      btnLoadWarehouse.FlatAppearance.BorderColor = Color.Black;
      btnLoadWarehouse.FlatAppearance.BorderSize = 3;
      btnLoadWarehouse.FlatStyle = FlatStyle.Flat;
      btnLoadWarehouse.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnLoadWarehouse.ForeColor = Color.Black;
      btnLoadWarehouse.Location = new Point(430, 8);
      btnLoadWarehouse.Name = "btnLoadWarehouse";
      btnLoadWarehouse.Size = new Size(54, 38);
      btnLoadWarehouse.TabIndex = 16;
      btnLoadWarehouse.Text = "...";
      btnLoadWarehouse.TextColor = Color.Black;
      btnLoadWarehouse.UseVisualStyleBackColor = false;
      btnLoadWarehouse.Click += btnLoadWarehouse_Click;
      // 
      // tableLayoutPanel14
      // 
      tableLayoutPanel14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel14.ColumnCount = 2;
      tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel14.Controls.Add(txtTypeGoods, 0, 0);
      tableLayoutPanel14.Controls.Add(btnLoadTypeGoods, 1, 0);
      tableLayoutPanel14.Location = new Point(139, 162);
      tableLayoutPanel14.Margin = new Padding(0);
      tableLayoutPanel14.Name = "tableLayoutPanel14";
      tableLayoutPanel14.RowCount = 1;
      tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel14.Size = new Size(487, 54);
      tableLayoutPanel14.TabIndex = 19;
      // 
      // txtTypeGoods
      // 
      txtTypeGoods.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtTypeGoods.BackColor = SystemColors.Window;
      txtTypeGoods.BorderColor = Color.Black;
      txtTypeGoods.BorderFocusColor = Color.HotPink;
      txtTypeGoods.BorderRadius = 5;
      txtTypeGoods.BorderSize = 2;
      txtTypeGoods.Enabled = false;
      txtTypeGoods.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtTypeGoods.ForeColor = Color.FromArgb(64, 64, 64);
      txtTypeGoods.Location = new Point(4, 8);
      txtTypeGoods.Margin = new Padding(4);
      txtTypeGoods.Multiline = false;
      txtTypeGoods.Name = "txtTypeGoods";
      txtTypeGoods.Padding = new Padding(10, 7, 10, 7);
      txtTypeGoods.PasswordChar = false;
      txtTypeGoods.PlaceholderColor = Color.DarkGray;
      txtTypeGoods.PlaceholderText = "";
      txtTypeGoods.Size = new Size(419, 38);
      txtTypeGoods.TabIndex = 15;
      txtTypeGoods.Texts = "";
      txtTypeGoods.UnderlinedStyle = false;
      // 
      // btnLoadTypeGoods
      // 
      btnLoadTypeGoods.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnLoadTypeGoods.BackColor = Color.White;
      btnLoadTypeGoods.BackgroundColor = Color.White;
      btnLoadTypeGoods.BorderColor = Color.Black;
      btnLoadTypeGoods.BorderRadius = 5;
      btnLoadTypeGoods.BorderSize = 0;
      btnLoadTypeGoods.FlatAppearance.BorderColor = Color.Black;
      btnLoadTypeGoods.FlatAppearance.BorderSize = 3;
      btnLoadTypeGoods.FlatStyle = FlatStyle.Flat;
      btnLoadTypeGoods.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnLoadTypeGoods.ForeColor = Color.Black;
      btnLoadTypeGoods.Location = new Point(430, 8);
      btnLoadTypeGoods.Name = "btnLoadTypeGoods";
      btnLoadTypeGoods.Size = new Size(54, 38);
      btnLoadTypeGoods.TabIndex = 16;
      btnLoadTypeGoods.Text = "...";
      btnLoadTypeGoods.TextColor = Color.Black;
      btnLoadTypeGoods.UseVisualStyleBackColor = false;
      btnLoadTypeGoods.Click += btnLoadTypeGoods_Click;
      // 
      // tableLayoutPanel13
      // 
      tableLayoutPanel13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel13.ColumnCount = 2;
      tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel13.Controls.Add(txtClient, 0, 0);
      tableLayoutPanel13.Controls.Add(btnLoadClient, 1, 0);
      tableLayoutPanel13.Location = new Point(139, 108);
      tableLayoutPanel13.Margin = new Padding(0);
      tableLayoutPanel13.Name = "tableLayoutPanel13";
      tableLayoutPanel13.RowCount = 1;
      tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel13.Size = new Size(487, 54);
      tableLayoutPanel13.TabIndex = 18;
      // 
      // txtClient
      // 
      txtClient.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtClient.BackColor = SystemColors.Window;
      txtClient.BorderColor = Color.Black;
      txtClient.BorderFocusColor = Color.HotPink;
      txtClient.BorderRadius = 5;
      txtClient.BorderSize = 2;
      txtClient.Enabled = false;
      txtClient.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtClient.ForeColor = Color.FromArgb(64, 64, 64);
      txtClient.Location = new Point(4, 8);
      txtClient.Margin = new Padding(4);
      txtClient.Multiline = false;
      txtClient.Name = "txtClient";
      txtClient.Padding = new Padding(10, 7, 10, 7);
      txtClient.PasswordChar = false;
      txtClient.PlaceholderColor = Color.DarkGray;
      txtClient.PlaceholderText = "";
      txtClient.Size = new Size(419, 38);
      txtClient.TabIndex = 15;
      txtClient.Texts = "";
      txtClient.UnderlinedStyle = false;
      // 
      // btnLoadClient
      // 
      btnLoadClient.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnLoadClient.BackColor = Color.White;
      btnLoadClient.BackgroundColor = Color.White;
      btnLoadClient.BorderColor = Color.Black;
      btnLoadClient.BorderRadius = 5;
      btnLoadClient.BorderSize = 0;
      btnLoadClient.FlatAppearance.BorderColor = Color.Black;
      btnLoadClient.FlatAppearance.BorderSize = 3;
      btnLoadClient.FlatStyle = FlatStyle.Flat;
      btnLoadClient.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnLoadClient.ForeColor = Color.Black;
      btnLoadClient.Location = new Point(430, 8);
      btnLoadClient.Name = "btnLoadClient";
      btnLoadClient.Size = new Size(54, 38);
      btnLoadClient.TabIndex = 16;
      btnLoadClient.Text = "...";
      btnLoadClient.TextColor = Color.Black;
      btnLoadClient.UseVisualStyleBackColor = false;
      btnLoadClient.Click += btnLoadClient_Click;
      // 
      // label6
      // 
      label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label6.AutoSize = true;
      label6.BackColor = Color.Transparent;
      label6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label6.Location = new Point(0, 0);
      label6.Margin = new Padding(0);
      label6.Name = "label6";
      label6.Size = new Size(139, 54);
      label6.TabIndex = 1;
      label6.Text = "Số phiếu";
      label6.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel5
      // 
      tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel5.ColumnCount = 2;
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Controls.Add(label15, 1, 0);
      tableLayoutPanel5.Controls.Add(label8, 0, 0);
      tableLayoutPanel5.Location = new Point(0, 108);
      tableLayoutPanel5.Margin = new Padding(0);
      tableLayoutPanel5.Name = "tableLayoutPanel5";
      tableLayoutPanel5.RowCount = 1;
      tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel5.Size = new Size(139, 54);
      tableLayoutPanel5.TabIndex = 33;
      // 
      // label15
      // 
      label15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label15.AutoSize = true;
      label15.BackColor = Color.Transparent;
      label15.Font = new Font("Roboto", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label15.ForeColor = Color.Red;
      label15.Location = new Point(109, 0);
      label15.Margin = new Padding(0);
      label15.Name = "label15";
      label15.Size = new Size(30, 54);
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
      label8.Size = new Size(109, 54);
      label8.TabIndex = 3;
      label8.Text = "Khách hàng";
      label8.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel11
      // 
      tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel11.ColumnCount = 4;
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel11.Controls.Add(label12, 2, 0);
      tableLayoutPanel11.Controls.Add(txtNoLabelAuto, 0, 0);
      tableLayoutPanel11.Controls.Add(cbbType, 3, 0);
      tableLayoutPanel11.Location = new Point(139, 0);
      tableLayoutPanel11.Margin = new Padding(0);
      tableLayoutPanel11.Name = "tableLayoutPanel11";
      tableLayoutPanel11.RowCount = 1;
      tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel11.Size = new Size(487, 54);
      tableLayoutPanel11.TabIndex = 16;
      // 
      // label12
      // 
      label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label12.AutoSize = true;
      label12.BackColor = Color.Transparent;
      label12.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label12.Location = new Point(224, 0);
      label12.Margin = new Padding(0);
      label12.Name = "label12";
      label12.Size = new Size(83, 54);
      label12.TabIndex = 17;
      label12.Text = "Kiểu cân";
      label12.TextAlign = ContentAlignment.MiddleRight;
      // 
      // txtNoLabelAuto
      // 
      txtNoLabelAuto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtNoLabelAuto.BackColor = SystemColors.Window;
      txtNoLabelAuto.BorderColor = Color.Black;
      txtNoLabelAuto.BorderFocusColor = Color.HotPink;
      txtNoLabelAuto.BorderRadius = 5;
      txtNoLabelAuto.BorderSize = 2;
      txtNoLabelAuto.Enabled = false;
      txtNoLabelAuto.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtNoLabelAuto.ForeColor = Color.FromArgb(64, 64, 64);
      txtNoLabelAuto.Location = new Point(4, 8);
      txtNoLabelAuto.Margin = new Padding(4);
      txtNoLabelAuto.Multiline = false;
      txtNoLabelAuto.Name = "txtNoLabelAuto";
      txtNoLabelAuto.Padding = new Padding(10, 7, 10, 7);
      txtNoLabelAuto.PasswordChar = false;
      txtNoLabelAuto.PlaceholderColor = Color.DarkGray;
      txtNoLabelAuto.PlaceholderText = "";
      txtNoLabelAuto.Size = new Size(196, 38);
      txtNoLabelAuto.TabIndex = 15;
      txtNoLabelAuto.Texts = "";
      txtNoLabelAuto.UnderlinedStyle = false;
      // 
      // cbbType
      // 
      cbbType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      cbbType.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbType.Font = new Font("Roboto", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
      cbbType.FormattingEnabled = true;
      cbbType.Items.AddRange(new object[] { "Xuất hàng", "Nhập hàng", "Khác" });
      cbbType.Location = new Point(310, 10);
      cbbType.Name = "cbbType";
      cbbType.Size = new Size(174, 33);
      cbbType.TabIndex = 20;
      // 
      // tableLayoutPanel16
      // 
      tableLayoutPanel16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel16.ColumnCount = 4;
      tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
      tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
      tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
      tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel16.Controls.Add(txtLicensePlate, 0, 0);
      tableLayoutPanel16.Controls.Add(label13, 2, 0);
      tableLayoutPanel16.Controls.Add(txtIdCard, 3, 0);
      tableLayoutPanel16.Location = new Point(139, 324);
      tableLayoutPanel16.Margin = new Padding(0);
      tableLayoutPanel16.Name = "tableLayoutPanel16";
      tableLayoutPanel16.RowCount = 1;
      tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel16.Size = new Size(487, 54);
      tableLayoutPanel16.TabIndex = 21;
      // 
      // txtLicensePlate
      // 
      txtLicensePlate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtLicensePlate.BackColor = SystemColors.Window;
      txtLicensePlate.BorderColor = Color.Black;
      txtLicensePlate.BorderFocusColor = Color.HotPink;
      txtLicensePlate.BorderRadius = 5;
      txtLicensePlate.BorderSize = 2;
      txtLicensePlate.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtLicensePlate.ForeColor = Color.FromArgb(64, 64, 64);
      txtLicensePlate.Location = new Point(4, 8);
      txtLicensePlate.Margin = new Padding(4);
      txtLicensePlate.Multiline = false;
      txtLicensePlate.Name = "txtLicensePlate";
      txtLicensePlate.Padding = new Padding(10, 7, 10, 7);
      txtLicensePlate.PasswordChar = false;
      txtLicensePlate.PlaceholderColor = Color.DarkGray;
      txtLicensePlate.PlaceholderText = "";
      txtLicensePlate.Size = new Size(192, 38);
      txtLicensePlate.TabIndex = 15;
      txtLicensePlate.Texts = "";
      txtLicensePlate.UnderlinedStyle = false;
      // 
      // label13
      // 
      label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label13.AutoSize = true;
      label13.BackColor = Color.Transparent;
      label13.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label13.Location = new Point(220, 0);
      label13.Margin = new Padding(0);
      label13.Name = "label13";
      label13.Size = new Size(120, 54);
      label13.TabIndex = 17;
      label13.Text = "CCCD";
      label13.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtIdCard
      // 
      txtIdCard.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtIdCard.BackColor = SystemColors.Window;
      txtIdCard.BorderColor = Color.Black;
      txtIdCard.BorderFocusColor = Color.HotPink;
      txtIdCard.BorderRadius = 5;
      txtIdCard.BorderSize = 2;
      txtIdCard.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtIdCard.ForeColor = Color.FromArgb(64, 64, 64);
      txtIdCard.Location = new Point(344, 8);
      txtIdCard.Margin = new Padding(4);
      txtIdCard.Multiline = false;
      txtIdCard.Name = "txtIdCard";
      txtIdCard.Padding = new Padding(10, 7, 10, 7);
      txtIdCard.PasswordChar = false;
      txtIdCard.PlaceholderColor = Color.DarkGray;
      txtIdCard.PlaceholderText = "";
      txtIdCard.Size = new Size(139, 38);
      txtIdCard.TabIndex = 18;
      txtIdCard.Texts = "";
      txtIdCard.UnderlinedStyle = false;
      // 
      // txtDocument
      // 
      txtDocument.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      txtDocument.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtDocument.Location = new Point(142, 440);
      txtDocument.Multiline = true;
      txtDocument.Name = "txtDocument";
      txtDocument.Size = new Size(481, 91);
      txtDocument.TabIndex = 25;
      // 
      // label10
      // 
      label10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label10.AutoSize = true;
      label10.BackColor = Color.FromArgb(199, 199, 199);
      label10.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label10.Location = new Point(0, 0);
      label10.Margin = new Padding(0);
      label10.Name = "label10";
      label10.Size = new Size(632, 50);
      label10.TabIndex = 0;
      label10.Text = "Thông tin phiếu cân";
      label10.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel9
      // 
      tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel9.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel9.ColumnCount = 1;
      tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel9.Controls.Add(ucItemOffsetWeightAndTare, 0, 5);
      tableLayoutPanel9.Controls.Add(ucItemTareForTruck, 0, 4);
      tableLayoutPanel9.Controls.Add(ucItemOffsetWeight, 0, 3);
      tableLayoutPanel9.Controls.Add(label14, 0, 0);
      tableLayoutPanel9.Controls.Add(ucItemWeight01, 0, 1);
      tableLayoutPanel9.Controls.Add(ucItemWeight02, 0, 2);
      tableLayoutPanel9.Location = new Point(1274, 0);
      tableLayoutPanel9.Margin = new Padding(0);
      tableLayoutPanel9.Name = "tableLayoutPanel9";
      tableLayoutPanel9.RowCount = 6;
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 18.5185184F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 18.5185184F));
      tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 18.5185184F));
      tableLayoutPanel9.Size = new Size(251, 590);
      tableLayoutPanel9.TabIndex = 2;
      // 
      // ucItemOffsetWeightAndTare
      // 
      ucItemOffsetWeightAndTare.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemOffsetWeightAndTare.BackColor = Color.FromArgb(223, 239, 255);
      ucItemOffsetWeightAndTare.Location = new Point(3, 493);
      ucItemOffsetWeightAndTare.Name = "ucItemOffsetWeightAndTare";
      ucItemOffsetWeightAndTare.Size = new Size(245, 94);
      ucItemOffsetWeightAndTare.TabIndex = 7;
      // 
      // ucItemTareForTruck
      // 
      ucItemTareForTruck.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemTareForTruck.BackColor = Color.FromArgb(223, 239, 255);
      ucItemTareForTruck.Location = new Point(3, 393);
      ucItemTareForTruck.Name = "ucItemTareForTruck";
      ucItemTareForTruck.Size = new Size(245, 94);
      ucItemTareForTruck.TabIndex = 6;
      // 
      // ucItemOffsetWeight
      // 
      ucItemOffsetWeight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemOffsetWeight.BackColor = Color.FromArgb(223, 239, 255);
      ucItemOffsetWeight.Location = new Point(3, 293);
      ucItemOffsetWeight.Name = "ucItemOffsetWeight";
      ucItemOffsetWeight.Size = new Size(245, 94);
      ucItemOffsetWeight.TabIndex = 5;
      // 
      // label14
      // 
      label14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label14.AutoSize = true;
      label14.BackColor = Color.FromArgb(199, 199, 199);
      label14.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label14.Location = new Point(0, 0);
      label14.Margin = new Padding(0);
      label14.Name = "label14";
      label14.Size = new Size(251, 50);
      label14.TabIndex = 1;
      label14.Text = "Dữ liệu cân";
      label14.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // ucItemWeight01
      // 
      ucItemWeight01.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemWeight01.BackColor = Color.FromArgb(223, 239, 255);
      ucItemWeight01.Location = new Point(3, 53);
      ucItemWeight01.Name = "ucItemWeight01";
      ucItemWeight01.Size = new Size(245, 114);
      ucItemWeight01.TabIndex = 2;
      // 
      // ucItemWeight02
      // 
      ucItemWeight02.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucItemWeight02.BackColor = Color.FromArgb(223, 239, 255);
      ucItemWeight02.Location = new Point(3, 173);
      ucItemWeight02.Name = "ucItemWeight02";
      ucItemWeight02.Size = new Size(245, 114);
      ucItemWeight02.TabIndex = 3;
      // 
      // tableLayoutPanel7
      // 
      tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel7.BackColor = Color.FromArgb(236, 236, 236);
      tableLayoutPanel7.ColumnCount = 1;
      tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Controls.Add(dgv, 0, 2);
      tableLayoutPanel7.Controls.Add(tableLayoutPanel10, 0, 1);
      tableLayoutPanel7.Controls.Add(label27, 0, 0);
      tableLayoutPanel7.Location = new Point(0, 595);
      tableLayoutPanel7.Margin = new Padding(0);
      tableLayoutPanel7.Name = "tableLayoutPanel7";
      tableLayoutPanel7.RowCount = 3;
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
      tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel7.Size = new Size(1525, 483);
      tableLayoutPanel7.TabIndex = 2;
      // 
      // dgv
      // 
      dgv.AllowUserToResizeColumns = false;
      dgv.AllowUserToResizeRows = false;
      dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dgv.BackgroundColor = Color.FromArgb(236, 236, 236);
      dgv.BorderStyle = BorderStyle.None;
      dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = SystemColors.Control;
      dataGridViewCellStyle4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
      dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = SystemColors.Window;
      dataGridViewCellStyle5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
      dgv.DefaultCellStyle = dataGridViewCellStyle5;
      dgv.EnableHeadersVisualStyles = false;
      dgv.Location = new Point(3, 115);
      dgv.Name = "dgv";
      dgv.ReadOnly = true;
      dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = SystemColors.Control;
      dataGridViewCellStyle6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
      dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      dgv.RowHeadersVisible = false;
      dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgv.Size = new Size(1519, 365);
      dgv.TabIndex = 23;
      // 
      // tableLayoutPanel10
      // 
      tableLayoutPanel10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tableLayoutPanel10.ColumnCount = 12;
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle());
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
      tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
      tableLayoutPanel10.Controls.Add(label4, 0, 0);
      tableLayoutPanel10.Controls.Add(txtSearchKey, 1, 0);
      tableLayoutPanel10.Controls.Add(label17, 3, 0);
      tableLayoutPanel10.Controls.Add(label18, 5, 0);
      tableLayoutPanel10.Controls.Add(ucTimeSearchFrom, 4, 0);
      tableLayoutPanel10.Controls.Add(ucTimeSearchTo, 6, 0);
      tableLayoutPanel10.Controls.Add(btnFilter, 7, 0);
      tableLayoutPanel10.Controls.Add(btnPrint, 10, 0);
      tableLayoutPanel10.Controls.Add(btnSearchHistorical, 9, 0);
      tableLayoutPanel10.Controls.Add(btnAddManual, 11, 0);
      tableLayoutPanel10.Location = new Point(0, 50);
      tableLayoutPanel10.Margin = new Padding(0);
      tableLayoutPanel10.Name = "tableLayoutPanel10";
      tableLayoutPanel10.RowCount = 1;
      tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel10.Size = new Size(1525, 62);
      tableLayoutPanel10.TabIndex = 22;
      // 
      // label4
      // 
      label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label4.AutoSize = true;
      label4.BackColor = Color.Transparent;
      label4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label4.Location = new Point(0, 0);
      label4.Margin = new Padding(0);
      label4.Name = "label4";
      label4.Size = new Size(95, 62);
      label4.TabIndex = 17;
      label4.Text = "Tìm kiếm:";
      label4.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // txtSearchKey
      // 
      txtSearchKey.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      txtSearchKey.BackColor = SystemColors.Window;
      txtSearchKey.BorderColor = Color.Black;
      txtSearchKey.BorderFocusColor = Color.HotPink;
      txtSearchKey.BorderRadius = 5;
      txtSearchKey.BorderSize = 2;
      txtSearchKey.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      txtSearchKey.ForeColor = Color.FromArgb(64, 64, 64);
      txtSearchKey.Location = new Point(99, 12);
      txtSearchKey.Margin = new Padding(4);
      txtSearchKey.Multiline = false;
      txtSearchKey.Name = "txtSearchKey";
      txtSearchKey.Padding = new Padding(10, 7, 10, 7);
      txtSearchKey.PasswordChar = false;
      txtSearchKey.PlaceholderColor = Color.DarkGray;
      txtSearchKey.PlaceholderText = "";
      txtSearchKey.Size = new Size(43, 38);
      txtSearchKey.TabIndex = 18;
      txtSearchKey.Texts = "";
      txtSearchKey.UnderlinedStyle = false;
      // 
      // label17
      // 
      label17.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label17.AutoSize = true;
      label17.BackColor = Color.Transparent;
      label17.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label17.Location = new Point(196, 0);
      label17.Margin = new Padding(0);
      label17.Name = "label17";
      label17.Size = new Size(32, 62);
      label17.TabIndex = 21;
      label17.Text = "Từ";
      label17.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label18
      // 
      label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label18.AutoSize = true;
      label18.BackColor = Color.Transparent;
      label18.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
      label18.Location = new Point(548, 0);
      label18.Margin = new Padding(0);
      label18.Name = "label18";
      label18.Size = new Size(42, 62);
      label18.TabIndex = 22;
      label18.Text = "đến";
      label18.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // ucTimeSearchFrom
      // 
      ucTimeSearchFrom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucTimeSearchFrom.Location = new Point(231, 3);
      ucTimeSearchFrom.Name = "ucTimeSearchFrom";
      ucTimeSearchFrom.Size = new Size(314, 56);
      ucTimeSearchFrom.TabIndex = 30;
      // 
      // ucTimeSearchTo
      // 
      ucTimeSearchTo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ucTimeSearchTo.Location = new Point(593, 3);
      ucTimeSearchTo.Name = "ucTimeSearchTo";
      ucTimeSearchTo.Size = new Size(314, 56);
      ucTimeSearchTo.TabIndex = 31;
      // 
      // btnFilter
      // 
      btnFilter.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnFilter.BackColor = Color.FromArgb(64, 107, 177);
      btnFilter.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnFilter.BorderColor = Color.PaleVioletRed;
      btnFilter.BorderRadius = 5;
      btnFilter.BorderSize = 0;
      btnFilter.FlatAppearance.BorderSize = 0;
      btnFilter.FlatStyle = FlatStyle.Flat;
      btnFilter.ForeColor = Color.White;
      btnFilter.Image = Properties.Resources.icon_filter;
      btnFilter.Location = new Point(913, 3);
      btnFilter.Name = "btnFilter";
      btnFilter.Size = new Size(54, 55);
      btnFilter.TabIndex = 32;
      btnFilter.TextColor = Color.White;
      btnFilter.UseVisualStyleBackColor = false;
      // 
      // btnPrint
      // 
      btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnPrint.BackColor = Color.FromArgb(64, 107, 177);
      btnPrint.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnPrint.BorderColor = Color.White;
      btnPrint.BorderRadius = 5;
      btnPrint.BorderSize = 0;
      btnPrint.FlatAppearance.BorderColor = Color.White;
      btnPrint.FlatAppearance.BorderSize = 0;
      btnPrint.FlatStyle = FlatStyle.Flat;
      btnPrint.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnPrint.ForeColor = Color.White;
      btnPrint.Image = Properties.Resources.icon_print;
      btnPrint.ImageAlign = ContentAlignment.MiddleLeft;
      btnPrint.Location = new Point(1158, 3);
      btnPrint.Name = "btnPrint";
      btnPrint.Padding = new Padding(10, 0, 0, 0);
      btnPrint.Size = new Size(174, 56);
      btnPrint.TabIndex = 20;
      btnPrint.Text = "       In phiếu";
      btnPrint.TextAlign = ContentAlignment.MiddleLeft;
      btnPrint.TextColor = Color.White;
      btnPrint.UseVisualStyleBackColor = false;
      btnPrint.Click += btnPrint_Click;
      // 
      // btnSearchHistorical
      // 
      btnSearchHistorical.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      btnSearchHistorical.BackColor = Color.FromArgb(64, 107, 177);
      btnSearchHistorical.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnSearchHistorical.BorderColor = Color.White;
      btnSearchHistorical.BorderRadius = 5;
      btnSearchHistorical.BorderSize = 0;
      btnSearchHistorical.FlatAppearance.BorderColor = Color.White;
      btnSearchHistorical.FlatAppearance.BorderSize = 0;
      btnSearchHistorical.FlatStyle = FlatStyle.Flat;
      btnSearchHistorical.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnSearchHistorical.ForeColor = Color.White;
      btnSearchHistorical.Image = Properties.Resources.icon_search;
      btnSearchHistorical.ImageAlign = ContentAlignment.MiddleLeft;
      btnSearchHistorical.Location = new Point(978, 3);
      btnSearchHistorical.Name = "btnSearchHistorical";
      btnSearchHistorical.Padding = new Padding(15, 0, 0, 0);
      btnSearchHistorical.Size = new Size(174, 55);
      btnSearchHistorical.TabIndex = 27;
      btnSearchHistorical.Text = "       Tìm kiếm";
      btnSearchHistorical.TextAlign = ContentAlignment.MiddleLeft;
      btnSearchHistorical.TextColor = Color.White;
      btnSearchHistorical.UseVisualStyleBackColor = false;
      btnSearchHistorical.Click += btnSearchHistorical_Click;
      // 
      // btnAddManual
      // 
      btnAddManual.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      btnAddManual.BackColor = Color.FromArgb(64, 107, 177);
      btnAddManual.BackgroundColor = Color.FromArgb(64, 107, 177);
      btnAddManual.BorderColor = Color.White;
      btnAddManual.BorderRadius = 5;
      btnAddManual.BorderSize = 0;
      btnAddManual.FlatAppearance.BorderColor = Color.White;
      btnAddManual.FlatAppearance.BorderSize = 0;
      btnAddManual.FlatStyle = FlatStyle.Flat;
      btnAddManual.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      btnAddManual.ForeColor = Color.White;
      btnAddManual.Image = Properties.Resources.icon_create_manual;
      btnAddManual.ImageAlign = ContentAlignment.MiddleLeft;
      btnAddManual.Location = new Point(1338, 3);
      btnAddManual.Name = "btnAddManual";
      btnAddManual.Padding = new Padding(10, 0, 0, 0);
      btnAddManual.Size = new Size(184, 56);
      btnAddManual.TabIndex = 33;
      btnAddManual.Text = "       Tạo thủ công";
      btnAddManual.TextAlign = ContentAlignment.MiddleLeft;
      btnAddManual.TextColor = Color.White;
      btnAddManual.UseVisualStyleBackColor = false;
      // 
      // label27
      // 
      label27.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      label27.AutoSize = true;
      label27.BackColor = Color.FromArgb(199, 199, 199);
      label27.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
      label27.Location = new Point(0, 0);
      label27.Margin = new Padding(0);
      label27.Name = "label27";
      label27.Size = new Size(1525, 50);
      label27.TabIndex = 0;
      label27.Text = "Lịch sử cân";
      label27.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // FrmHomeTruck
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1530, 1084);
      Controls.Add(tableLayoutPanel1);
      Name = "FrmHomeTruck";
      Text = "FrmHome";
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel2.ResumeLayout(false);
      tableLayoutPanel3.ResumeLayout(false);
      tableLayoutPanel3.PerformLayout();
      tableLayoutPanel12.ResumeLayout(false);
      tableLayoutPanel17.ResumeLayout(false);
      tableLayoutPanel17.PerformLayout();
      tableLayoutPanel18.ResumeLayout(false);
      tableLayoutPanel18.PerformLayout();
      tableLayoutPanel6.ResumeLayout(false);
      tableLayoutPanel4.ResumeLayout(false);
      tableLayoutPanel4.PerformLayout();
      tableLayoutPanel8.ResumeLayout(false);
      tableLayoutPanel8.PerformLayout();
      tableLayoutPanel20.ResumeLayout(false);
      tableLayoutPanel20.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)numericUpDownNumberTare).EndInit();
      tableLayoutPanel22.ResumeLayout(false);
      tableLayoutPanel22.PerformLayout();
      tableLayoutPanel19.ResumeLayout(false);
      tableLayoutPanel19.PerformLayout();
      tableLayoutPanel15.ResumeLayout(false);
      tableLayoutPanel14.ResumeLayout(false);
      tableLayoutPanel13.ResumeLayout(false);
      tableLayoutPanel5.ResumeLayout(false);
      tableLayoutPanel5.PerformLayout();
      tableLayoutPanel11.ResumeLayout(false);
      tableLayoutPanel11.PerformLayout();
      tableLayoutPanel16.ResumeLayout(false);
      tableLayoutPanel16.PerformLayout();
      tableLayoutPanel9.ResumeLayout(false);
      tableLayoutPanel9.PerformLayout();
      tableLayoutPanel7.ResumeLayout(false);
      tableLayoutPanel7.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
      tableLayoutPanel10.ResumeLayout(false);
      tableLayoutPanel10.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private TableLayoutPanel tableLayoutPanel3;
    private Label lbWeightValue;
    private Label label5;
    private Label label3;
    private Label label1;
    private TableLayoutPanel tableLayoutPanel6;
    private TableLayoutPanel tableLayoutPanel4;
    private TableLayoutPanel tableLayoutPanel8;
    private Label label6;
    private Label label8;
    private Label label10;
    private TableLayoutPanel tableLayoutPanel9;
    private Label label14;
    private UserControls.UcItem ucItemWeight01;
    private UserControls.UcItem ucItemWeight02;
    private TableLayoutPanel tableLayoutPanel11;
    private Custom.RJTextBox txtNoLabelAuto;
    private Label label12;
    private Custom.RJTextBox txtNoLabel;
    private TableLayoutPanel tableLayoutPanel14;
    private Custom.RJTextBox txtTypeGoods;
    private RJButton btnLoadTypeGoods;
    private TableLayoutPanel tableLayoutPanel13;
    private Custom.RJTextBox txtClient;
    private RJButton btnLoadClient;
    private TableLayoutPanel tableLayoutPanel15;
    private Custom.RJTextBox txtWareHouse;
    private RJButton btnLoadWarehouse;
    private TableLayoutPanel tableLayoutPanel16;
    private Custom.RJTextBox txtLicensePlate;
    private Label label13;
    private Custom.RJTextBox txtIdCard;
    private Custom.RJTextBox txtNameDriver;
    private Label label19;
    private RJButton btnZero;
    private RJButton btnBack;
    private RJButton btnPrint;
    private RJButton btnWeightTime02;
    private RJButton btnWeightTime01;
    private RJButton btnTriggerWeight;
    private TableLayoutPanel tableLayoutPanel7;
    private Label label27;
    private TableLayoutPanel tableLayoutPanel10;
    private Label label4;
    private Custom.RJTextBox txtSearchKey;
    private Label label17;
    private Label label18;
    private RJButton btnSearchHistorical;
    private TableLayoutPanel tableLayoutPanel12;
    private TableLayoutPanel tableLayoutPanel17;
    private Label lbWeightTrigger;
    private Label label2;
    private TableLayoutPanel tableLayoutPanel18;
    private DataGridView dgv;
    private RJButton btnCreate;
    private Label label20;
    private UserControls.UcItem ucItemOffsetWeight;
    private UserControls.UcTimeSearch ucTimeSearchFrom;
    private UserControls.UcTimeSearch ucTimeSearchTo;
    private Common.Custom.RJButton btnFilter;
    private TextBox txtDocument;
    private TableLayoutPanel tableLayoutPanel5;
    private Label label15;
    private TableLayoutPanel tableLayoutPanel22;
    private Label label16;
    private Label label25;
    private Label label24;
    private Label label23;
    private TableLayoutPanel tableLayoutPanel19;
    private Label label21;
    private Label label22;
    private TableLayoutPanel tableLayoutPanel20;
    private Label label9;
    private RJTextBox txtValueTareForTruck;
    private Label label7;
    private ComboBox cbbTareForTruck;
    private UserControls.UcItem ucItemTareForTruck;
    private UserControls.UcItem ucItemOffsetWeightAndTare;
    private ComboBox cbbType;
    private RJButton btnAddManual;
    private Label label11;
    private NumericUpDown numericUpDownNumberTare;
  }
}
