/*
 * Created by SharpDevelop.
 * User: abms2
 * Date: 10/10/2014
 * Time: 10:41 AM
 *Time:.Harshada
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Hotal_Managment_Syatem
{
	partial class Counter_Order_Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelItemsCount;
        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btn_close;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView DGV_tblOrder;
		private System.Windows.Forms.Label label_orderID;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label_counterNo;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label_tableNo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView listView_items;
		private System.Windows.Forms.ListView listViewCounter;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Counter_Order_Form));
            this.labelItemsCount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.DGV_tblOrder = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_orderID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_counterNo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_tableNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listView_items = new System.Windows.Forms.ListView();
            this.listViewCounter = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_custSugggestion = new System.Windows.Forms.TextBox();
            this.lbl_total = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlcalc = new System.Windows.Forms.Panel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cmbWaiterName = new System.Windows.Forms.ComboBox();
            this.label_date = new System.Windows.Forms.Label();
            this.lbl_viewOrderID = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.initialRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotelDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distributeMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEmployeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExpencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.gUESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.customerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCustomerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelOrderDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dayEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.kichenExpensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExpensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expensesDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalCollectionReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDetailsReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datewiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialWiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waiterWiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableWiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insentiveDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categorywiseReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletedItemReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelKOTReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accessControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userMailIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.logoutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_tblOrder)).BeginInit();
            this.pnlcalc.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelItemsCount
            // 
            this.labelItemsCount.AutoSize = true;
            this.labelItemsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemsCount.Location = new System.Drawing.Point(81, 611);
            this.labelItemsCount.Name = "labelItemsCount";
            this.labelItemsCount.Size = new System.Drawing.Size(21, 24);
            this.labelItemsCount.TabIndex = 107;
            this.labelItemsCount.Text = "0";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Snow;
            this.button1.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_clear;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.DarkRed;
            this.button1.Location = new System.Drawing.Point(153, 647);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 45);
            this.button1.TabIndex = 104;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Snow;
            this.btn_close.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_close;
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_close.Location = new System.Drawing.Point(281, 647);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(122, 45);
            this.btn_close.TabIndex = 102;
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Snow;
            this.button2.BackgroundImage = global::Hotal_Managment_Syatem.Properties.Resources.btn_print;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.DarkRed;
            this.button2.Location = new System.Drawing.Point(25, 647);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 45);
            this.button2.TabIndex = 101;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(24, 611);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 24);
            this.label10.TabIndex = 100;
            this.label10.Text = "Items:";
            // 
            // DGV_tblOrder
            // 
            this.DGV_tblOrder.AllowUserToAddRows = false;
            this.DGV_tblOrder.AllowUserToOrderColumns = true;
            this.DGV_tblOrder.AllowUserToResizeColumns = false;
            this.DGV_tblOrder.AllowUserToResizeRows = false;
            this.DGV_tblOrder.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_tblOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_tblOrder.ColumnHeadersHeight = 30;
            this.DGV_tblOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Amount});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_tblOrder.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGV_tblOrder.Location = new System.Drawing.Point(12, 115);
            this.DGV_tblOrder.Name = "DGV_tblOrder";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_tblOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGV_tblOrder.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGV_tblOrder.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DGV_tblOrder.RowTemplate.Height = 40;
            this.DGV_tblOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_tblOrder.Size = new System.Drawing.Size(425, 483);
            this.DGV_tblOrder.TabIndex = 99;
            this.DGV_tblOrder.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_tblOrder_CellClick);
            this.DGV_tblOrder.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_tblOrder_CellContentClick);
            this.DGV_tblOrder.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_tblOrder_CellEndEdit);
            this.DGV_tblOrder.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DGV_tblOrder_EditingControlShowing);
            this.DGV_tblOrder.Leave += new System.EventHandler(this.DGV_tblOrder_Leave);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FillWeight = 60F;
            this.Column1.HeaderText = "Remove";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Text = "Remove";
            this.Column1.UseColumnTextForButtonValue = true;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 180F;
            this.Column2.HeaderText = "Item Name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 180;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 60F;
            this.Column3.HeaderText = "Qty";
            this.Column3.MaxInputLength = 3;
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // label_orderID
            // 
            this.label_orderID.AutoSize = true;
            this.label_orderID.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_orderID.ForeColor = System.Drawing.Color.Purple;
            this.label_orderID.Location = new System.Drawing.Point(269, 49);
            this.label_orderID.Name = "label_orderID";
            this.label_orderID.Size = new System.Drawing.Size(110, 29);
            this.label_orderID.TabIndex = 98;
            this.label_orderID.Text = "Order Id";
            this.label_orderID.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(106, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 26);
            this.label8.TabIndex = 97;
            this.label8.Text = "Order Id:";
            // 
            // label_counterNo
            // 
            this.label_counterNo.AutoSize = true;
            this.label_counterNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_counterNo.ForeColor = System.Drawing.Color.Purple;
            this.label_counterNo.Location = new System.Drawing.Point(122, 49);
            this.label_counterNo.Name = "label_counterNo";
            this.label_counterNo.Size = new System.Drawing.Size(141, 31);
            this.label_counterNo.TabIndex = 96;
            this.label_counterNo.Text = "Table No.";
            this.label_counterNo.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(24, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 20);
            this.label7.TabIndex = 95;
            this.label7.Text = "Counter No.";
            this.label7.Visible = false;
            // 
            // label_tableNo
            // 
            this.label_tableNo.AutoSize = true;
            this.label_tableNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_tableNo.Location = new System.Drawing.Point(327, 281);
            this.label_tableNo.Name = "label_tableNo";
            this.label_tableNo.Size = new System.Drawing.Size(93, 24);
            this.label_tableNo.TabIndex = 94;
            this.label_tableNo.Text = "Table No.";
            this.label_tableNo.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(210, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 93;
            this.label3.Text = "Captain Name :";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(245, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 92;
            this.label4.Text = "Table No.";
            this.label4.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(421, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 36);
            this.label2.TabIndex = 91;
            this.label2.Text = "Items";
            // 
            // listView_items
            // 
            this.listView_items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_items.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_items.GridLines = true;
            this.listView_items.Location = new System.Drawing.Point(443, 115);
            this.listView_items.Margin = new System.Windows.Forms.Padding(20);
            this.listView_items.Name = "listView_items";
            this.listView_items.Size = new System.Drawing.Size(748, 577);
            this.listView_items.TabIndex = 89;
            this.listView_items.TileSize = new System.Drawing.Size(300, 50);
            this.listView_items.UseCompatibleStateImageBehavior = false;
            this.listView_items.View = System.Windows.Forms.View.Tile;
            this.listView_items.SelectedIndexChanged += new System.EventHandler(this.listView_items_SelectedIndexChanged);
            this.listView_items.Click += new System.EventHandler(this.listView_items_Click);
            // 
            // listViewCounter
            // 
            this.listViewCounter.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewCounter.GridLines = true;
            this.listViewCounter.Location = new System.Drawing.Point(1138, 702);
            this.listViewCounter.Name = "listViewCounter";
            this.listViewCounter.Size = new System.Drawing.Size(53, 41);
            this.listViewCounter.TabIndex = 88;
            this.listViewCounter.TileSize = new System.Drawing.Size(200, 40);
            this.listViewCounter.UseCompatibleStateImageBehavior = false;
            this.listViewCounter.View = System.Windows.Forms.View.Tile;
            this.listViewCounter.Visible = false;
            this.listViewCounter.SelectedIndexChanged += new System.EventHandler(this.listViewCounter_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(456, 465);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 105;
            this.label5.Text = "Remark :";
            this.label5.Visible = false;
            // 
            // textBox_custSugggestion
            // 
            this.textBox_custSugggestion.Location = new System.Drawing.Point(458, 495);
            this.textBox_custSugggestion.Multiline = true;
            this.textBox_custSugggestion.Name = "textBox_custSugggestion";
            this.textBox_custSugggestion.Size = new System.Drawing.Size(299, 61);
            this.textBox_custSugggestion.TabIndex = 106;
            this.textBox_custSugggestion.Visible = false;
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total.Location = new System.Drawing.Point(355, 609);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(30, 31);
            this.lbl_total.TabIndex = 109;
            this.lbl_total.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(225, 612);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 24);
            this.label9.TabIndex = 108;
            this.label9.Text = "Total Amount:";
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search.Location = new System.Drawing.Point(647, 84);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(291, 26);
            this.txt_search.TabIndex = 110;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(560, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 26);
            this.label6.TabIndex = 111;
            this.label6.Text = "Search";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnlcalc
            // 
            this.pnlcalc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlcalc.Controls.Add(this.linkLabel2);
            this.pnlcalc.Controls.Add(this.linkLabel1);
            this.pnlcalc.Controls.Add(this.button13);
            this.pnlcalc.Controls.Add(this.button12);
            this.pnlcalc.Controls.Add(this.button9);
            this.pnlcalc.Controls.Add(this.button10);
            this.pnlcalc.Controls.Add(this.button11);
            this.pnlcalc.Controls.Add(this.button6);
            this.pnlcalc.Controls.Add(this.button7);
            this.pnlcalc.Controls.Add(this.button8);
            this.pnlcalc.Controls.Add(this.button5);
            this.pnlcalc.Controls.Add(this.button4);
            this.pnlcalc.Controls.Add(this.button3);
            this.pnlcalc.Location = new System.Drawing.Point(409, 141);
            this.pnlcalc.Name = "pnlcalc";
            this.pnlcalc.Size = new System.Drawing.Size(282, 321);
            this.pnlcalc.TabIndex = 112;
            this.pnlcalc.Visible = false;
            this.pnlcalc.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlcalc_Paint);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(133, 51);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(59, 25);
            this.linkLabel2.TabIndex = 12;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Clear";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(201, 51);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(63, 25);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Close";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.Location = new System.Drawing.Point(104, 292);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(150, 62);
            this.button13.TabIndex = 10;
            this.button13.Text = "Enter";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.Location = new System.Drawing.Point(16, 294);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(74, 60);
            this.button12.TabIndex = 9;
            this.button12.Text = "0";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(187, 222);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(74, 60);
            this.button9.TabIndex = 8;
            this.button9.Text = "9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(104, 222);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(74, 60);
            this.button10.TabIndex = 7;
            this.button10.Text = "8";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(16, 222);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(74, 60);
            this.button11.TabIndex = 6;
            this.button11.Text = "7";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(187, 153);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(74, 60);
            this.button6.TabIndex = 5;
            this.button6.Text = "6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(104, 153);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(74, 60);
            this.button7.TabIndex = 4;
            this.button7.Text = "5";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(16, 153);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(74, 60);
            this.button8.TabIndex = 3;
            this.button8.Text = "4";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(187, 83);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(74, 60);
            this.button5.TabIndex = 2;
            this.button5.Text = "3";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(104, 83);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 60);
            this.button4.TabIndex = 1;
            this.button4.Text = "2";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(16, 83);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 60);
            this.button3.TabIndex = 0;
            this.button3.Text = "1";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmbWaiterName
            // 
            this.cmbWaiterName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbWaiterName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbWaiterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaiterName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWaiterName.FormattingEnabled = true;
            this.cmbWaiterName.Location = new System.Drawing.Point(23, 53);
            this.cmbWaiterName.Name = "cmbWaiterName";
            this.cmbWaiterName.Size = new System.Drawing.Size(212, 27);
            this.cmbWaiterName.TabIndex = 113;
            // 
            // label_date
            // 
            this.label_date.BackColor = System.Drawing.Color.Transparent;
            this.label_date.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_date.Location = new System.Drawing.Point(1021, 78);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(140, 29);
            this.label_date.TabIndex = 114;
            this.label_date.Text = "Date";
            // 
            // lbl_viewOrderID
            // 
            this.lbl_viewOrderID.AutoSize = true;
            this.lbl_viewOrderID.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_viewOrderID.ForeColor = System.Drawing.Color.Purple;
            this.lbl_viewOrderID.Location = new System.Drawing.Point(199, 78);
            this.lbl_viewOrderID.Name = "lbl_viewOrderID";
            this.lbl_viewOrderID.Size = new System.Drawing.Size(170, 29);
            this.lbl_viewOrderID.TabIndex = 115;
            this.lbl_viewOrderID.Text = "view Order Id";
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initialRecordToolStripMenuItem,
            this.toolStripSeparator2,
            this.gUESTToolStripMenuItem,
            this.toolStripSeparator3,
            this.customerToolStripMenuItem,
            this.toolStripSeparator6,
            this.logoutToolStripMenuItem,
            this.toolStripSeparator7,
            this.kichenExpensesToolStripMenuItem,
            this.toolStripSeparator17,
            this.hELPToolStripMenuItem,
            this.toolStripSeparator19,
            this.settingToolStripMenuItem,
            this.toolStripSeparator20,
            this.logoutToolStripMenuItem1,
            this.toolStripSeparator10});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1203, 50);
            this.menuStrip1.TabIndex = 116;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // initialRecordToolStripMenuItem
            // 
            this.initialRecordToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.categoryToolStripMenuItem,
            this.hotelDetailsToolStripMenuItem,
            this.materialToolStripMenuItem,
            this.distributeMaterialToolStripMenuItem,
            this.addSupplierToolStripMenuItem,
            this.addCustomerToolStripMenuItem,
            this.addEmployeeToolStripMenuItem,
            this.addExpencesToolStripMenuItem});
            this.initialRecordToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initialRecordToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.initialRecordToolStripMenuItem.Image = global::Hotal_Managment_Syatem.Properties.Resources.initial1;
            this.initialRecordToolStripMenuItem.Name = "initialRecordToolStripMenuItem";
            this.initialRecordToolStripMenuItem.Size = new System.Drawing.Size(118, 46);
            this.initialRecordToolStripMenuItem.Text = "Initial Record";
            // 
            // categoryToolStripMenuItem
            // 
            this.categoryToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.categoryToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.categoryToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            this.categoryToolStripMenuItem.Size = new System.Drawing.Size(191, 24);
            this.categoryToolStripMenuItem.Text = "Menu Category";
            this.categoryToolStripMenuItem.Click += new System.EventHandler(this.categoryToolStripMenuItem_Click);
            // 
            // hotelDetailsToolStripMenuItem
            // 
            this.hotelDetailsToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.hotelDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.hotelDetailsToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.hotelDetailsToolStripMenuItem.Name = "hotelDetailsToolStripMenuItem";
            this.hotelDetailsToolStripMenuItem.Size = new System.Drawing.Size(191, 24);
            this.hotelDetailsToolStripMenuItem.Text = "Hotel Details";
            this.hotelDetailsToolStripMenuItem.Click += new System.EventHandler(this.hotelDetailsToolStripMenuItem_Click);
            // 
            // materialToolStripMenuItem
            // 
            this.materialToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.materialToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.materialToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.materialToolStripMenuItem.Name = "materialToolStripMenuItem";
            this.materialToolStripMenuItem.Size = new System.Drawing.Size(191, 24);
            this.materialToolStripMenuItem.Text = "Material";
            this.materialToolStripMenuItem.Click += new System.EventHandler(this.materialToolStripMenuItem_Click);
            // 
            // distributeMaterialToolStripMenuItem
            // 
            this.distributeMaterialToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.distributeMaterialToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.distributeMaterialToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.distributeMaterialToolStripMenuItem.Name = "distributeMaterialToolStripMenuItem";
            this.distributeMaterialToolStripMenuItem.Size = new System.Drawing.Size(191, 24);
            this.distributeMaterialToolStripMenuItem.Text = "Distribute Material";
            this.distributeMaterialToolStripMenuItem.Click += new System.EventHandler(this.distributeMaterialToolStripMenuItem_Click);
            // 
            // addSupplierToolStripMenuItem
            // 
            this.addSupplierToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.addSupplierToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.addSupplierToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.addSupplierToolStripMenuItem.Name = "addSupplierToolStripMenuItem";
            this.addSupplierToolStripMenuItem.Size = new System.Drawing.Size(191, 24);
            this.addSupplierToolStripMenuItem.Text = "Add Supplier";
            this.addSupplierToolStripMenuItem.Click += new System.EventHandler(this.addSupplierToolStripMenuItem_Click);
            // 
            // addCustomerToolStripMenuItem
            // 
            this.addCustomerToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.addCustomerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.addCustomerToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.addCustomerToolStripMenuItem.Name = "addCustomerToolStripMenuItem";
            this.addCustomerToolStripMenuItem.Size = new System.Drawing.Size(191, 24);
            this.addCustomerToolStripMenuItem.Text = "Add Customer";
            this.addCustomerToolStripMenuItem.Click += new System.EventHandler(this.addCustomerToolStripMenuItem_Click);
            // 
            // addEmployeeToolStripMenuItem
            // 
            this.addEmployeeToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.addEmployeeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.addEmployeeToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.addEmployeeToolStripMenuItem.Name = "addEmployeeToolStripMenuItem";
            this.addEmployeeToolStripMenuItem.Size = new System.Drawing.Size(191, 24);
            this.addEmployeeToolStripMenuItem.Text = "Add Employee";
            this.addEmployeeToolStripMenuItem.Click += new System.EventHandler(this.addEmployeeToolStripMenuItem_Click);
            // 
            // addExpencesToolStripMenuItem
            // 
            this.addExpencesToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.addExpencesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.addExpencesToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.addExpencesToolStripMenuItem.Name = "addExpencesToolStripMenuItem";
            this.addExpencesToolStripMenuItem.Size = new System.Drawing.Size(191, 24);
            this.addExpencesToolStripMenuItem.Text = "Add Expences";
            this.addExpencesToolStripMenuItem.Click += new System.EventHandler(this.addExpencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 46);
            // 
            // gUESTToolStripMenuItem
            // 
            this.gUESTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vageToolStripMenuItem,
            this.updateMenuToolStripMenuItem});
            this.gUESTToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gUESTToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.gUESTToolStripMenuItem.Image = global::Hotal_Managment_Syatem.Properties.Resources.menu;
            this.gUESTToolStripMenuItem.Name = "gUESTToolStripMenuItem";
            this.gUESTToolStripMenuItem.Size = new System.Drawing.Size(74, 46);
            this.gUESTToolStripMenuItem.Text = "Menu";
            // 
            // vageToolStripMenuItem
            // 
            this.vageToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.vageToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.vageToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.vageToolStripMenuItem.Name = "vageToolStripMenuItem";
            this.vageToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.vageToolStripMenuItem.Text = "Add Menu";
            this.vageToolStripMenuItem.Click += new System.EventHandler(this.vageToolStripMenuItem_Click);
            // 
            // updateMenuToolStripMenuItem
            // 
            this.updateMenuToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.updateMenuToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.updateMenuToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.updateMenuToolStripMenuItem.Name = "updateMenuToolStripMenuItem";
            this.updateMenuToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.updateMenuToolStripMenuItem.Text = "Update Menu";
            this.updateMenuToolStripMenuItem.Click += new System.EventHandler(this.updateMenuToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 46);
            // 
            // customerToolStripMenuItem
            // 
            this.customerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCustomerToolStripMenuItem1,
            this.receiptToolStripMenuItem});
            this.customerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.customerToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.customerToolStripMenuItem.Image = global::Hotal_Managment_Syatem.Properties.Resources.customer;
            this.customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            this.customerToolStripMenuItem.Size = new System.Drawing.Size(101, 46);
            this.customerToolStripMenuItem.Text = "Customer";
            // 
            // addCustomerToolStripMenuItem1
            // 
            this.addCustomerToolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.addCustomerToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.addCustomerToolStripMenuItem1.ForeColor = System.Drawing.Color.Navy;
            this.addCustomerToolStripMenuItem1.Name = "addCustomerToolStripMenuItem1";
            this.addCustomerToolStripMenuItem1.Size = new System.Drawing.Size(167, 24);
            this.addCustomerToolStripMenuItem1.Text = "Add Customer";
            this.addCustomerToolStripMenuItem1.Click += new System.EventHandler(this.addCustomerToolStripMenuItem1_Click);
            // 
            // receiptToolStripMenuItem
            // 
            this.receiptToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.receiptToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.receiptToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.receiptToolStripMenuItem.Name = "receiptToolStripMenuItem";
            this.receiptToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.receiptToolStripMenuItem.Text = "Receipt";
            this.receiptToolStripMenuItem.Click += new System.EventHandler(this.receiptToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 46);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salesItemToolStripMenuItem,
            this.cancelOrderDetailsToolStripMenuItem,
            this.dayEndToolStripMenuItem});
            this.logoutToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.logoutToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.logoutToolStripMenuItem.Image = global::Hotal_Managment_Syatem.Properties.Resources.transaction;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(114, 46);
            this.logoutToolStripMenuItem.Text = "Transaction";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // salesItemToolStripMenuItem
            // 
            this.salesItemToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.salesItemToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.salesItemToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.salesItemToolStripMenuItem.Name = "salesItemToolStripMenuItem";
            this.salesItemToolStripMenuItem.Size = new System.Drawing.Size(203, 24);
            this.salesItemToolStripMenuItem.Text = "Today Counter";
            this.salesItemToolStripMenuItem.Click += new System.EventHandler(this.salesItemToolStripMenuItem_Click);
            // 
            // cancelOrderDetailsToolStripMenuItem
            // 
            this.cancelOrderDetailsToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.cancelOrderDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cancelOrderDetailsToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.cancelOrderDetailsToolStripMenuItem.Name = "cancelOrderDetailsToolStripMenuItem";
            this.cancelOrderDetailsToolStripMenuItem.Size = new System.Drawing.Size(203, 24);
            this.cancelOrderDetailsToolStripMenuItem.Text = "Cancel Order Details";
            this.cancelOrderDetailsToolStripMenuItem.Click += new System.EventHandler(this.cancelOrderDetailsToolStripMenuItem_Click);
            // 
            // dayEndToolStripMenuItem
            // 
            this.dayEndToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.dayEndToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dayEndToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.dayEndToolStripMenuItem.Name = "dayEndToolStripMenuItem";
            this.dayEndToolStripMenuItem.Size = new System.Drawing.Size(203, 24);
            this.dayEndToolStripMenuItem.Text = "Day End";
            this.dayEndToolStripMenuItem.Click += new System.EventHandler(this.dayEndToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 46);
            // 
            // kichenExpensesToolStripMenuItem
            // 
            this.kichenExpensesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addExpensesToolStripMenuItem,
            this.expensesDetailsToolStripMenuItem});
            this.kichenExpensesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.kichenExpensesToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.kichenExpensesToolStripMenuItem.Image = global::Hotal_Managment_Syatem.Properties.Resources.expences;
            this.kichenExpensesToolStripMenuItem.Name = "kichenExpensesToolStripMenuItem";
            this.kichenExpensesToolStripMenuItem.Size = new System.Drawing.Size(97, 46);
            this.kichenExpensesToolStripMenuItem.Text = "Expenses";
            // 
            // addExpensesToolStripMenuItem
            // 
            this.addExpensesToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.addExpensesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.addExpensesToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.addExpensesToolStripMenuItem.Name = "addExpensesToolStripMenuItem";
            this.addExpensesToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.addExpensesToolStripMenuItem.Text = "Add Expenses";
            this.addExpensesToolStripMenuItem.Click += new System.EventHandler(this.addExpensesToolStripMenuItem_Click);
            // 
            // expensesDetailsToolStripMenuItem
            // 
            this.expensesDetailsToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.expensesDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.expensesDetailsToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.expensesDetailsToolStripMenuItem.Name = "expensesDetailsToolStripMenuItem";
            this.expensesDetailsToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.expensesDetailsToolStripMenuItem.Text = "Expenses Details";
            this.expensesDetailsToolStripMenuItem.Click += new System.EventHandler(this.expensesDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 46);
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.hELPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalCollectionReportToolStripMenuItem,
            this.menuDetailsReportToolStripMenuItem,
            this.salesReportToolStripMenuItem,
            this.categorywiseReportToolStripMenuItem,
            this.deletedItemReportToolStripMenuItem,
            this.cancelKOTReportToolStripMenuItem,
            this.excelReportToolStripMenuItem});
            this.hELPToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.hELPToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.hELPToolStripMenuItem.Image = global::Hotal_Managment_Syatem.Properties.Resources.report;
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(83, 46);
            this.hELPToolStripMenuItem.Text = "Report";
            // 
            // totalCollectionReportToolStripMenuItem
            // 
            this.totalCollectionReportToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.totalCollectionReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.totalCollectionReportToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.totalCollectionReportToolStripMenuItem.Name = "totalCollectionReportToolStripMenuItem";
            this.totalCollectionReportToolStripMenuItem.Size = new System.Drawing.Size(212, 24);
            this.totalCollectionReportToolStripMenuItem.Text = "Stock Details Report";
            // 
            // menuDetailsReportToolStripMenuItem
            // 
            this.menuDetailsReportToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.menuDetailsReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuDetailsReportToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.menuDetailsReportToolStripMenuItem.Name = "menuDetailsReportToolStripMenuItem";
            this.menuDetailsReportToolStripMenuItem.Size = new System.Drawing.Size(212, 24);
            this.menuDetailsReportToolStripMenuItem.Text = "Sales Item Report";
            this.menuDetailsReportToolStripMenuItem.Click += new System.EventHandler(this.menuDetailsReportToolStripMenuItem_Click);
            // 
            // salesReportToolStripMenuItem
            // 
            this.salesReportToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.salesReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datewiseToolStripMenuItem,
            this.materialWiseToolStripMenuItem,
            this.waiterWiseToolStripMenuItem,
            this.tableWiseToolStripMenuItem,
            this.insentiveDetailsToolStripMenuItem});
            this.salesReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.salesReportToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.salesReportToolStripMenuItem.Name = "salesReportToolStripMenuItem";
            this.salesReportToolStripMenuItem.Size = new System.Drawing.Size(212, 24);
            this.salesReportToolStripMenuItem.Text = "Sales Report";
            // 
            // datewiseToolStripMenuItem
            // 
            this.datewiseToolStripMenuItem.Name = "datewiseToolStripMenuItem";
            this.datewiseToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.datewiseToolStripMenuItem.Text = "Datewise";
            this.datewiseToolStripMenuItem.Click += new System.EventHandler(this.datewiseToolStripMenuItem_Click);
            // 
            // materialWiseToolStripMenuItem
            // 
            this.materialWiseToolStripMenuItem.Name = "materialWiseToolStripMenuItem";
            this.materialWiseToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.materialWiseToolStripMenuItem.Text = "Item Wise";
            this.materialWiseToolStripMenuItem.Click += new System.EventHandler(this.materialWiseToolStripMenuItem_Click);
            // 
            // waiterWiseToolStripMenuItem
            // 
            this.waiterWiseToolStripMenuItem.Name = "waiterWiseToolStripMenuItem";
            this.waiterWiseToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.waiterWiseToolStripMenuItem.Text = "Waiter Wise";
            this.waiterWiseToolStripMenuItem.Click += new System.EventHandler(this.waiterWiseToolStripMenuItem_Click);
            // 
            // tableWiseToolStripMenuItem
            // 
            this.tableWiseToolStripMenuItem.Name = "tableWiseToolStripMenuItem";
            this.tableWiseToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.tableWiseToolStripMenuItem.Text = "Table Wise";
            this.tableWiseToolStripMenuItem.Click += new System.EventHandler(this.tableWiseToolStripMenuItem_Click);
            // 
            // insentiveDetailsToolStripMenuItem
            // 
            this.insentiveDetailsToolStripMenuItem.Name = "insentiveDetailsToolStripMenuItem";
            this.insentiveDetailsToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.insentiveDetailsToolStripMenuItem.Text = "Insentive Details";
            this.insentiveDetailsToolStripMenuItem.Click += new System.EventHandler(this.insentiveDetailsToolStripMenuItem_Click);
            // 
            // categorywiseReportToolStripMenuItem
            // 
            this.categorywiseReportToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.categorywiseReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.categorywiseReportToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.categorywiseReportToolStripMenuItem.Name = "categorywiseReportToolStripMenuItem";
            this.categorywiseReportToolStripMenuItem.Size = new System.Drawing.Size(212, 24);
            this.categorywiseReportToolStripMenuItem.Text = "Category Wise Report";
            this.categorywiseReportToolStripMenuItem.Click += new System.EventHandler(this.categorywiseReportToolStripMenuItem_Click);
            // 
            // deletedItemReportToolStripMenuItem
            // 
            this.deletedItemReportToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.deletedItemReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.deletedItemReportToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.deletedItemReportToolStripMenuItem.Name = "deletedItemReportToolStripMenuItem";
            this.deletedItemReportToolStripMenuItem.Size = new System.Drawing.Size(212, 24);
            this.deletedItemReportToolStripMenuItem.Text = "Deleted Item Report";
            this.deletedItemReportToolStripMenuItem.Click += new System.EventHandler(this.deletedItemReportToolStripMenuItem_Click);
            // 
            // cancelKOTReportToolStripMenuItem
            // 
            this.cancelKOTReportToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.cancelKOTReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cancelKOTReportToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.cancelKOTReportToolStripMenuItem.Name = "cancelKOTReportToolStripMenuItem";
            this.cancelKOTReportToolStripMenuItem.Size = new System.Drawing.Size(212, 24);
            this.cancelKOTReportToolStripMenuItem.Text = "Cancel KOT Report";
            this.cancelKOTReportToolStripMenuItem.Click += new System.EventHandler(this.cancelKOTReportToolStripMenuItem_Click);
            // 
            // excelReportToolStripMenuItem
            // 
            this.excelReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.excelReportToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.excelReportToolStripMenuItem.Name = "excelReportToolStripMenuItem";
            this.excelReportToolStripMenuItem.Size = new System.Drawing.Size(212, 24);
            this.excelReportToolStripMenuItem.Text = "Excel Report";
            this.excelReportToolStripMenuItem.Click += new System.EventHandler(this.excelReportToolStripMenuItem_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 46);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.receiptFormatToolStripMenuItem,
            this.userToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.manageOrdersToolStripMenuItem,
            this.accessControlToolStripMenuItem,
            this.databaseBackupToolStripMenuItem,
            this.configurationPanelToolStripMenuItem,
            this.configurationSettingToolStripMenuItem,
            this.userMailIdToolStripMenuItem});
            this.settingToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.settingToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.settingToolStripMenuItem.Image = global::Hotal_Managment_Syatem.Properties.Resources.setting;
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(84, 46);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // receiptFormatToolStripMenuItem
            // 
            this.receiptFormatToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.receiptFormatToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.receiptFormatToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.receiptFormatToolStripMenuItem.Name = "receiptFormatToolStripMenuItem";
            this.receiptFormatToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.receiptFormatToolStripMenuItem.Text = "Receipt Format";
            this.receiptFormatToolStripMenuItem.Click += new System.EventHandler(this.receiptFormatToolStripMenuItem_Click);
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.userToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.userToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.userToolStripMenuItem.Text = "Create User";
            this.userToolStripMenuItem.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.optionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // manageOrdersToolStripMenuItem
            // 
            this.manageOrdersToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.manageOrdersToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.manageOrdersToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.manageOrdersToolStripMenuItem.Name = "manageOrdersToolStripMenuItem";
            this.manageOrdersToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.manageOrdersToolStripMenuItem.Text = "Manage Orders";
            this.manageOrdersToolStripMenuItem.Click += new System.EventHandler(this.manageOrdersToolStripMenuItem_Click);
            // 
            // accessControlToolStripMenuItem
            // 
            this.accessControlToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.accessControlToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.accessControlToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.accessControlToolStripMenuItem.Name = "accessControlToolStripMenuItem";
            this.accessControlToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.accessControlToolStripMenuItem.Text = "Access Control";
            this.accessControlToolStripMenuItem.Click += new System.EventHandler(this.accessControlToolStripMenuItem_Click);
            // 
            // databaseBackupToolStripMenuItem
            // 
            this.databaseBackupToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.databaseBackupToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.databaseBackupToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.databaseBackupToolStripMenuItem.Name = "databaseBackupToolStripMenuItem";
            this.databaseBackupToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.databaseBackupToolStripMenuItem.Text = "Database Backup";
            this.databaseBackupToolStripMenuItem.Click += new System.EventHandler(this.databaseBackupToolStripMenuItem_Click);
            // 
            // configurationPanelToolStripMenuItem
            // 
            this.configurationPanelToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.configurationPanelToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.configurationPanelToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.configurationPanelToolStripMenuItem.Name = "configurationPanelToolStripMenuItem";
            this.configurationPanelToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.configurationPanelToolStripMenuItem.Text = "Configuration Panel";
            this.configurationPanelToolStripMenuItem.Visible = false;
            this.configurationPanelToolStripMenuItem.Click += new System.EventHandler(this.configurationPanelToolStripMenuItem_Click);
            // 
            // configurationSettingToolStripMenuItem
            // 
            this.configurationSettingToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.configurationSettingToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.configurationSettingToolStripMenuItem.Name = "configurationSettingToolStripMenuItem";
            this.configurationSettingToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.configurationSettingToolStripMenuItem.Text = "Configuration Setting";
            this.configurationSettingToolStripMenuItem.Click += new System.EventHandler(this.configurationSettingToolStripMenuItem_Click);
            // 
            // userMailIdToolStripMenuItem
            // 
            this.userMailIdToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.userMailIdToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.userMailIdToolStripMenuItem.Name = "userMailIdToolStripMenuItem";
            this.userMailIdToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.userMailIdToolStripMenuItem.Text = "User Mail Id";
            this.userMailIdToolStripMenuItem.Click += new System.EventHandler(this.userMailIdToolStripMenuItem_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(6, 46);
            // 
            // logoutToolStripMenuItem1
            // 
            this.logoutToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.logoutToolStripMenuItem1.ForeColor = System.Drawing.Color.Navy;
            this.logoutToolStripMenuItem1.Image = global::Hotal_Managment_Syatem.Properties.Resources.logout1;
            this.logoutToolStripMenuItem1.Name = "logoutToolStripMenuItem1";
            this.logoutToolStripMenuItem1.Size = new System.Drawing.Size(84, 46);
            this.logoutToolStripMenuItem1.Text = "Logout";
            this.logoutToolStripMenuItem1.Click += new System.EventHandler(this.logoutToolStripMenuItem1_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 46);
            // 
            // Counter_Order_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(1203, 698);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lbl_viewOrderID);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.cmbWaiterName);
            this.Controls.Add(this.pnlcalc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.lbl_total);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelItemsCount);
            this.Controls.Add(this.textBox_custSugggestion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.DGV_tblOrder);
            this.Controls.Add(this.label_orderID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label_counterNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_tableNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView_items);
            this.Controls.Add(this.listViewCounter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Counter_Order_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Self Counter Order";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Counter_Order_Form_FormClosed);
            this.Load += new System.EventHandler(this.Counter_Order_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_tblOrder)).EndInit();
            this.pnlcalc.ResumeLayout(false);
            this.pnlcalc.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_custSugggestion;
        private System.Windows.Forms.Label lbl_total;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel pnlcalc;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.ComboBox cmbWaiterName;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.Label lbl_viewOrderID;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem initialRecordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotelDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distributeMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSupplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEmployeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addExpencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem gUESTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem customerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCustomerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem receiptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelOrderDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dayEndToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem kichenExpensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addExpensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expensesDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalCollectionReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuDetailsReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datewiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialWiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waiterWiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableWiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insentiveDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categorywiseReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletedItemReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelKOTReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiptFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accessControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userMailIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
	}
}
