namespace work_tracker.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainerMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelLeft = new DevExpress.XtraEditors.PanelControl();
            this.panelCalendar = new DevExpress.XtraEditors.PanelControl();
            this.panelCalendarDays = new System.Windows.Forms.Panel();
            this.panelWeekDays = new System.Windows.Forms.Panel();
            this.panelCalendarHeader = new DevExpress.XtraEditors.PanelControl();
            this.btnNextMonth = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrevMonth = new DevExpress.XtraEditors.SimpleButton();
            this.lblMonthYear = new DevExpress.XtraEditors.LabelControl();
            this.panelStats = new DevExpress.XtraEditors.PanelControl();
            this.panelStatCards = new System.Windows.Forms.FlowLayoutPanel();
            this.panelStatCard1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTodayItems = new DevExpress.XtraEditors.LabelControl();
            this.lblTodayItemsTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelStatCard2 = new DevExpress.XtraEditors.PanelControl();
            this.lblTodayCompleted = new DevExpress.XtraEditors.LabelControl();
            this.lblTodayCompletedTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelStatCard3 = new DevExpress.XtraEditors.PanelControl();
            this.lblWeekItems = new DevExpress.XtraEditors.LabelControl();
            this.lblWeekItemsTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelStatCard4 = new DevExpress.XtraEditors.PanelControl();
            this.lblWeekCompleted = new DevExpress.XtraEditors.LabelControl();
            this.lblWeekCompletedTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelStatCard5 = new DevExpress.XtraEditors.PanelControl();
            this.lblActiveItems = new DevExpress.XtraEditors.LabelControl();
            this.lblActiveItemsTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelStatCard6 = new DevExpress.XtraEditors.PanelControl();
            this.lblPendingItems = new DevExpress.XtraEditors.LabelControl();
            this.lblPendingItemsTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelStatCard7 = new DevExpress.XtraEditors.PanelControl();
            this.lblTodayMeetings = new DevExpress.XtraEditors.LabelControl();
            this.lblTodayMeetingsTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelRight = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerRight = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelWorkItems = new DevExpress.XtraEditors.PanelControl();
            this.gridWorkItems = new DevExpress.XtraGrid.GridControl();
            this.gridViewWorkItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblWorkItemsHeader = new DevExpress.XtraEditors.LabelControl();
            this.panelMeetings = new DevExpress.XtraEditors.PanelControl();
            this.gridMeetings = new DevExpress.XtraGrid.GridControl();
            this.gridViewMeetings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMeetingSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeetingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeetingParticipants = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeetingSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblMeetingsHeader = new DevExpress.XtraEditors.LabelControl();
            this.panelDayHeader = new DevExpress.XtraEditors.PanelControl();
            this.btnSyncOutlook = new DevExpress.XtraEditors.SimpleButton();
            this.lblSelectedDate = new DevExpress.XtraEditors.LabelControl();
            this.panelToolbar = new DevExpress.XtraEditors.PanelControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnToday = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel1)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel2)).BeginInit();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelCalendar)).BeginInit();
            this.panelCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelCalendarHeader)).BeginInit();
            this.panelCalendarHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStats)).BeginInit();
            this.panelStats.SuspendLayout();
            this.panelStatCards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard1)).BeginInit();
            this.panelStatCard1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard2)).BeginInit();
            this.panelStatCard2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard3)).BeginInit();
            this.panelStatCard3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard4)).BeginInit();
            this.panelStatCard4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard5)).BeginInit();
            this.panelStatCard5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard6)).BeginInit();
            this.panelStatCard6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard7)).BeginInit();
            this.panelStatCard7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight.Panel1)).BeginInit();
            this.splitContainerRight.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight.Panel2)).BeginInit();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelWorkItems)).BeginInit();
            this.panelWorkItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMeetings)).BeginInit();
            this.panelMeetings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMeetings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMeetings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDayHeader)).BeginInit();
            this.panelDayHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelToolbar)).BeginInit();
            this.panelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 50);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelLeft);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelRight);
            this.splitContainerMain.Size = new System.Drawing.Size(1400, 700);
            this.splitContainerMain.SplitterPosition = 620;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.panelCalendar);
            this.panelLeft.Controls.Add(this.panelStats);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(620, 700);
            this.panelLeft.TabIndex = 0;
            // 
            // panelCalendar
            // 
            this.panelCalendar.Controls.Add(this.panelCalendarDays);
            this.panelCalendar.Controls.Add(this.panelWeekDays);
            this.panelCalendar.Controls.Add(this.panelCalendarHeader);
            this.panelCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCalendar.Location = new System.Drawing.Point(2, 142);
            this.panelCalendar.Name = "panelCalendar";
            this.panelCalendar.Size = new System.Drawing.Size(616, 556);
            this.panelCalendar.TabIndex = 1;
            // 
            // panelCalendarDays
            // 
            this.panelCalendarDays.AutoScroll = true;
            this.panelCalendarDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCalendarDays.Location = new System.Drawing.Point(2, 72);
            this.panelCalendarDays.Name = "panelCalendarDays";
            this.panelCalendarDays.Size = new System.Drawing.Size(612, 482);
            this.panelCalendarDays.TabIndex = 2;
            // 
            // panelWeekDays
            // 
            this.panelWeekDays.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWeekDays.Location = new System.Drawing.Point(2, 42);
            this.panelWeekDays.Name = "panelWeekDays";
            this.panelWeekDays.Size = new System.Drawing.Size(612, 30);
            this.panelWeekDays.TabIndex = 1;
            // 
            // panelCalendarHeader
            // 
            this.panelCalendarHeader.Controls.Add(this.btnNextMonth);
            this.panelCalendarHeader.Controls.Add(this.btnPrevMonth);
            this.panelCalendarHeader.Controls.Add(this.lblMonthYear);
            this.panelCalendarHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCalendarHeader.Location = new System.Drawing.Point(2, 2);
            this.panelCalendarHeader.Name = "panelCalendarHeader";
            this.panelCalendarHeader.Size = new System.Drawing.Size(612, 40);
            this.panelCalendarHeader.TabIndex = 0;
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Location = new System.Drawing.Point(250, 8);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(30, 25);
            this.btnNextMonth.TabIndex = 2;
            this.btnNextMonth.Text = "▶";
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);
            // 
            // btnPrevMonth
            // 
            this.btnPrevMonth.Location = new System.Drawing.Point(10, 8);
            this.btnPrevMonth.Name = "btnPrevMonth";
            this.btnPrevMonth.Size = new System.Drawing.Size(30, 25);
            this.btnPrevMonth.TabIndex = 1;
            this.btnPrevMonth.Text = "◀";
            this.btnPrevMonth.Click += new System.EventHandler(this.btnPrevMonth_Click);
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMonthYear.Appearance.Options.UseFont = true;
            this.lblMonthYear.Appearance.Options.UseTextOptions = true;
            this.lblMonthYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMonthYear.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMonthYear.Location = new System.Drawing.Point(50, 10);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(190, 21);
            this.lblMonthYear.TabIndex = 0;
            this.lblMonthYear.Text = "Aralık 2024";
            // 
            // panelStats
            // 
            this.panelStats.Controls.Add(this.panelStatCards);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(2, 2);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(616, 140);
            this.panelStats.TabIndex = 0;
            // 
            // panelStatCards
            // 
            this.panelStatCards.Controls.Add(this.panelStatCard1);
            this.panelStatCards.Controls.Add(this.panelStatCard2);
            this.panelStatCards.Controls.Add(this.panelStatCard3);
            this.panelStatCards.Controls.Add(this.panelStatCard4);
            this.panelStatCards.Controls.Add(this.panelStatCard5);
            this.panelStatCards.Controls.Add(this.panelStatCard6);
            this.panelStatCards.Controls.Add(this.panelStatCard7);
            this.panelStatCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatCards.Location = new System.Drawing.Point(2, 2);
            this.panelStatCards.Name = "panelStatCards";
            this.panelStatCards.Padding = new System.Windows.Forms.Padding(5);
            this.panelStatCards.Size = new System.Drawing.Size(612, 136);
            this.panelStatCards.TabIndex = 0;
            // 
            // panelStatCard1
            // 
            this.panelStatCard1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.panelStatCard1.Appearance.Options.UseBackColor = true;
            this.panelStatCard1.Controls.Add(this.lblTodayItems);
            this.panelStatCard1.Controls.Add(this.lblTodayItemsTitle);
            this.panelStatCard1.Location = new System.Drawing.Point(8, 8);
            this.panelStatCard1.Name = "panelStatCard1";
            this.panelStatCard1.Size = new System.Drawing.Size(130, 55);
            this.panelStatCard1.TabIndex = 0;
            // 
            // lblTodayItems
            // 
            this.lblTodayItems.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTodayItems.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTodayItems.Appearance.Options.UseFont = true;
            this.lblTodayItems.Appearance.Options.UseForeColor = true;
            this.lblTodayItems.Location = new System.Drawing.Point(10, 25);
            this.lblTodayItems.Name = "lblTodayItems";
            this.lblTodayItems.Size = new System.Drawing.Size(14, 32);
            this.lblTodayItems.TabIndex = 1;
            this.lblTodayItems.Text = "0";
            // 
            // lblTodayItemsTitle
            // 
            this.lblTodayItemsTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTodayItemsTitle.Appearance.Options.UseFont = true;
            this.lblTodayItemsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTodayItemsTitle.Name = "lblTodayItemsTitle";
            this.lblTodayItemsTitle.Size = new System.Drawing.Size(83, 15);
            this.lblTodayItemsTitle.TabIndex = 0;
            this.lblTodayItemsTitle.Text = "📥 Bugün Gelen";
            // 
            // panelStatCard2
            // 
            this.panelStatCard2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.panelStatCard2.Appearance.Options.UseBackColor = true;
            this.panelStatCard2.Controls.Add(this.lblTodayCompleted);
            this.panelStatCard2.Controls.Add(this.lblTodayCompletedTitle);
            this.panelStatCard2.Location = new System.Drawing.Point(144, 8);
            this.panelStatCard2.Name = "panelStatCard2";
            this.panelStatCard2.Size = new System.Drawing.Size(130, 55);
            this.panelStatCard2.TabIndex = 1;
            // 
            // lblTodayCompleted
            // 
            this.lblTodayCompleted.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTodayCompleted.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblTodayCompleted.Appearance.Options.UseFont = true;
            this.lblTodayCompleted.Appearance.Options.UseForeColor = true;
            this.lblTodayCompleted.Location = new System.Drawing.Point(10, 25);
            this.lblTodayCompleted.Name = "lblTodayCompleted";
            this.lblTodayCompleted.Size = new System.Drawing.Size(14, 32);
            this.lblTodayCompleted.TabIndex = 1;
            this.lblTodayCompleted.Text = "0";
            // 
            // lblTodayCompletedTitle
            // 
            this.lblTodayCompletedTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTodayCompletedTitle.Appearance.Options.UseFont = true;
            this.lblTodayCompletedTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTodayCompletedTitle.Name = "lblTodayCompletedTitle";
            this.lblTodayCompletedTitle.Size = new System.Drawing.Size(77, 15);
            this.lblTodayCompletedTitle.TabIndex = 0;
            this.lblTodayCompletedTitle.Text = "✅ Bugün Biten";
            // 
            // panelStatCard3
            // 
            this.panelStatCard3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(224)))));
            this.panelStatCard3.Appearance.Options.UseBackColor = true;
            this.panelStatCard3.Controls.Add(this.lblWeekItems);
            this.panelStatCard3.Controls.Add(this.lblWeekItemsTitle);
            this.panelStatCard3.Location = new System.Drawing.Point(280, 8);
            this.panelStatCard3.Name = "panelStatCard3";
            this.panelStatCard3.Size = new System.Drawing.Size(130, 55);
            this.panelStatCard3.TabIndex = 2;
            // 
            // lblWeekItems
            // 
            this.lblWeekItems.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblWeekItems.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblWeekItems.Appearance.Options.UseFont = true;
            this.lblWeekItems.Appearance.Options.UseForeColor = true;
            this.lblWeekItems.Location = new System.Drawing.Point(10, 25);
            this.lblWeekItems.Name = "lblWeekItems";
            this.lblWeekItems.Size = new System.Drawing.Size(14, 32);
            this.lblWeekItems.TabIndex = 1;
            this.lblWeekItems.Text = "0";
            // 
            // lblWeekItemsTitle
            // 
            this.lblWeekItemsTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWeekItemsTitle.Appearance.Options.UseFont = true;
            this.lblWeekItemsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblWeekItemsTitle.Name = "lblWeekItemsTitle";
            this.lblWeekItemsTitle.Size = new System.Drawing.Size(89, 15);
            this.lblWeekItemsTitle.TabIndex = 0;
            this.lblWeekItemsTitle.Text = "📊 Haftalık Gelen";
            // 
            // panelStatCard4
            // 
            this.panelStatCard4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(229)))), ((int)(((byte)(245)))));
            this.panelStatCard4.Appearance.Options.UseBackColor = true;
            this.panelStatCard4.Controls.Add(this.lblWeekCompleted);
            this.panelStatCard4.Controls.Add(this.lblWeekCompletedTitle);
            this.panelStatCard4.Location = new System.Drawing.Point(416, 8);
            this.panelStatCard4.Name = "panelStatCard4";
            this.panelStatCard4.Size = new System.Drawing.Size(130, 55);
            this.panelStatCard4.TabIndex = 3;
            // 
            // lblWeekCompleted
            // 
            this.lblWeekCompleted.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblWeekCompleted.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblWeekCompleted.Appearance.Options.UseFont = true;
            this.lblWeekCompleted.Appearance.Options.UseForeColor = true;
            this.lblWeekCompleted.Location = new System.Drawing.Point(10, 25);
            this.lblWeekCompleted.Name = "lblWeekCompleted";
            this.lblWeekCompleted.Size = new System.Drawing.Size(14, 32);
            this.lblWeekCompleted.TabIndex = 1;
            this.lblWeekCompleted.Text = "0";
            // 
            // lblWeekCompletedTitle
            // 
            this.lblWeekCompletedTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWeekCompletedTitle.Appearance.Options.UseFont = true;
            this.lblWeekCompletedTitle.Location = new System.Drawing.Point(10, 5);
            this.lblWeekCompletedTitle.Name = "lblWeekCompletedTitle";
            this.lblWeekCompletedTitle.Size = new System.Drawing.Size(86, 15);
            this.lblWeekCompletedTitle.TabIndex = 0;
            this.lblWeekCompletedTitle.Text = "🎯 Haftalık Biten";
            // 
            // panelStatCard5
            // 
            this.panelStatCard5.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.panelStatCard5.Appearance.Options.UseBackColor = true;
            this.panelStatCard5.Controls.Add(this.lblActiveItems);
            this.panelStatCard5.Controls.Add(this.lblActiveItemsTitle);
            this.panelStatCard5.Location = new System.Drawing.Point(8, 69);
            this.panelStatCard5.Name = "panelStatCard5";
            this.panelStatCard5.Size = new System.Drawing.Size(130, 55);
            this.panelStatCard5.TabIndex = 4;
            // 
            // lblActiveItems
            // 
            this.lblActiveItems.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblActiveItems.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.lblActiveItems.Appearance.Options.UseFont = true;
            this.lblActiveItems.Appearance.Options.UseForeColor = true;
            this.lblActiveItems.Location = new System.Drawing.Point(10, 25);
            this.lblActiveItems.Name = "lblActiveItems";
            this.lblActiveItems.Size = new System.Drawing.Size(14, 32);
            this.lblActiveItems.TabIndex = 1;
            this.lblActiveItems.Text = "0";
            // 
            // lblActiveItemsTitle
            // 
            this.lblActiveItemsTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblActiveItemsTitle.Appearance.Options.UseFont = true;
            this.lblActiveItemsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblActiveItemsTitle.Name = "lblActiveItemsTitle";
            this.lblActiveItemsTitle.Size = new System.Drawing.Size(40, 15);
            this.lblActiveItemsTitle.TabIndex = 0;
            this.lblActiveItemsTitle.Text = "🔥 Aktif";
            // 
            // panelStatCard6
            // 
            this.panelStatCard6.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelStatCard6.Appearance.Options.UseBackColor = true;
            this.panelStatCard6.Controls.Add(this.lblPendingItems);
            this.panelStatCard6.Controls.Add(this.lblPendingItemsTitle);
            this.panelStatCard6.Location = new System.Drawing.Point(144, 69);
            this.panelStatCard6.Name = "panelStatCard6";
            this.panelStatCard6.Size = new System.Drawing.Size(130, 55);
            this.panelStatCard6.TabIndex = 5;
            // 
            // lblPendingItems
            // 
            this.lblPendingItems.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPendingItems.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(188)))), ((int)(((byte)(212)))));
            this.lblPendingItems.Appearance.Options.UseFont = true;
            this.lblPendingItems.Appearance.Options.UseForeColor = true;
            this.lblPendingItems.Location = new System.Drawing.Point(10, 25);
            this.lblPendingItems.Name = "lblPendingItems";
            this.lblPendingItems.Size = new System.Drawing.Size(14, 32);
            this.lblPendingItems.TabIndex = 1;
            this.lblPendingItems.Text = "0";
            // 
            // lblPendingItemsTitle
            // 
            this.lblPendingItemsTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPendingItemsTitle.Appearance.Options.UseFont = true;
            this.lblPendingItemsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblPendingItemsTitle.Name = "lblPendingItemsTitle";
            this.lblPendingItemsTitle.Size = new System.Drawing.Size(55, 15);
            this.lblPendingItemsTitle.TabIndex = 0;
            this.lblPendingItemsTitle.Text = "⏳ Bekleyen";
            // 
            // panelStatCard7
            // 
            this.panelStatCard7.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(196)))));
            this.panelStatCard7.Appearance.Options.UseBackColor = true;
            this.panelStatCard7.Controls.Add(this.lblTodayMeetings);
            this.panelStatCard7.Controls.Add(this.lblTodayMeetingsTitle);
            this.panelStatCard7.Location = new System.Drawing.Point(280, 69);
            this.panelStatCard7.Name = "panelStatCard7";
            this.panelStatCard7.Size = new System.Drawing.Size(130, 55);
            this.panelStatCard7.TabIndex = 6;
            // 
            // lblTodayMeetings
            // 
            this.lblTodayMeetings.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTodayMeetings.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblTodayMeetings.Appearance.Options.UseFont = true;
            this.lblTodayMeetings.Appearance.Options.UseForeColor = true;
            this.lblTodayMeetings.Location = new System.Drawing.Point(10, 25);
            this.lblTodayMeetings.Name = "lblTodayMeetings";
            this.lblTodayMeetings.Size = new System.Drawing.Size(14, 32);
            this.lblTodayMeetings.TabIndex = 1;
            this.lblTodayMeetings.Text = "0";
            // 
            // lblTodayMeetingsTitle
            // 
            this.lblTodayMeetingsTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTodayMeetingsTitle.Appearance.Options.UseFont = true;
            this.lblTodayMeetingsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTodayMeetingsTitle.Name = "lblTodayMeetingsTitle";
            this.lblTodayMeetingsTitle.Size = new System.Drawing.Size(96, 15);
            this.lblTodayMeetingsTitle.TabIndex = 0;
            this.lblTodayMeetingsTitle.Text = "📅 Bugün Toplantı";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.splitContainerRight);
            this.panelRight.Controls.Add(this.panelDayHeader);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(770, 700);
            this.panelRight.TabIndex = 0;
            // 
            // splitContainerRight
            // 
            this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRight.Horizontal = false;
            this.splitContainerRight.Location = new System.Drawing.Point(2, 52);
            this.splitContainerRight.Name = "splitContainerRight";
            // 
            // splitContainerRight.Panel1
            // 
            this.splitContainerRight.Panel1.Controls.Add(this.panelWorkItems);
            // 
            // splitContainerRight.Panel2
            // 
            this.splitContainerRight.Panel2.Controls.Add(this.panelMeetings);
            this.splitContainerRight.Size = new System.Drawing.Size(766, 646);
            this.splitContainerRight.SplitterPosition = 400;
            this.splitContainerRight.TabIndex = 1;
            // 
            // panelWorkItems
            // 
            this.panelWorkItems.Controls.Add(this.gridWorkItems);
            this.panelWorkItems.Controls.Add(this.lblWorkItemsHeader);
            this.panelWorkItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkItems.Location = new System.Drawing.Point(0, 0);
            this.panelWorkItems.Name = "panelWorkItems";
            this.panelWorkItems.Size = new System.Drawing.Size(766, 400);
            this.panelWorkItems.TabIndex = 0;
            // 
            // gridWorkItems
            // 
            this.gridWorkItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridWorkItems.Location = new System.Drawing.Point(2, 32);
            this.gridWorkItems.MainView = this.gridViewWorkItems;
            this.gridWorkItems.Name = "gridWorkItems";
            this.gridWorkItems.Size = new System.Drawing.Size(762, 366);
            this.gridWorkItems.TabIndex = 1;
            this.gridWorkItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewWorkItems});
            // 
            // gridViewWorkItems
            // 
            this.gridViewWorkItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colTitle,
            this.colProject,
            this.colType,
            this.colStatus,
            this.colCategory});
            this.gridViewWorkItems.GridControl = this.gridWorkItems;
            this.gridViewWorkItems.Name = "gridViewWorkItems";
            this.gridViewWorkItems.OptionsBehavior.Editable = false;
            this.gridViewWorkItems.OptionsView.ShowGroupPanel = false;
            this.gridViewWorkItems.DoubleClick += new System.EventHandler(this.gridViewWorkItems_DoubleClick);
            // 
            // colId
            // 
            this.colId.Caption = "ID";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            this.colId.Width = 50;
            // 
            // colTitle
            // 
            this.colTitle.Caption = "Başlık";
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 1;
            this.colTitle.Width = 300;
            // 
            // colProject
            // 
            this.colProject.Caption = "Proje";
            this.colProject.FieldName = "ProjectName";
            this.colProject.Name = "colProject";
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 2;
            this.colProject.Width = 120;
            // 
            // colType
            // 
            this.colType.Caption = "Tip";
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 3;
            this.colType.Width = 100;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Durum";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 4;
            this.colStatus.Width = 100;
            // 
            // colCategory
            // 
            this.colCategory.Caption = "Kategori";
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.Visible = true;
            this.colCategory.VisibleIndex = 5;
            this.colCategory.Width = 100;
            // 
            // lblWorkItemsHeader
            // 
            this.lblWorkItemsHeader.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblWorkItemsHeader.Appearance.Options.UseFont = true;
            this.lblWorkItemsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWorkItemsHeader.Location = new System.Drawing.Point(2, 2);
            this.lblWorkItemsHeader.Name = "lblWorkItemsHeader";
            this.lblWorkItemsHeader.Padding = new System.Windows.Forms.Padding(5);
            this.lblWorkItemsHeader.Size = new System.Drawing.Size(118, 30);
            this.lblWorkItemsHeader.TabIndex = 0;
            this.lblWorkItemsHeader.Text = "📋 İş Kalemleri";
            // 
            // panelMeetings
            // 
            this.panelMeetings.Controls.Add(this.gridMeetings);
            this.panelMeetings.Controls.Add(this.lblMeetingsHeader);
            this.panelMeetings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMeetings.Location = new System.Drawing.Point(0, 0);
            this.panelMeetings.Name = "panelMeetings";
            this.panelMeetings.Size = new System.Drawing.Size(766, 236);
            this.panelMeetings.TabIndex = 0;
            // 
            // gridMeetings
            // 
            this.gridMeetings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMeetings.Location = new System.Drawing.Point(2, 32);
            this.gridMeetings.MainView = this.gridViewMeetings;
            this.gridMeetings.Name = "gridMeetings";
            this.gridMeetings.Size = new System.Drawing.Size(762, 202);
            this.gridMeetings.TabIndex = 1;
            this.gridMeetings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMeetings});
            // 
            // gridViewMeetings
            // 
            this.gridViewMeetings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMeetingSubject,
            this.colMeetingDate,
            this.colMeetingParticipants,
            this.colMeetingSource});
            this.gridViewMeetings.GridControl = this.gridMeetings;
            this.gridViewMeetings.Name = "gridViewMeetings";
            this.gridViewMeetings.OptionsBehavior.Editable = false;
            this.gridViewMeetings.OptionsView.ShowGroupPanel = false;
            this.gridViewMeetings.DoubleClick += new System.EventHandler(this.gridViewMeetings_DoubleClick);
            // 
            // colMeetingSubject
            // 
            this.colMeetingSubject.Caption = "Konu";
            this.colMeetingSubject.FieldName = "Subject";
            this.colMeetingSubject.Name = "colMeetingSubject";
            this.colMeetingSubject.Visible = true;
            this.colMeetingSubject.VisibleIndex = 0;
            this.colMeetingSubject.Width = 300;
            // 
            // colMeetingDate
            // 
            this.colMeetingDate.Caption = "Tarih/Saat";
            this.colMeetingDate.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.colMeetingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colMeetingDate.FieldName = "MeetingDate";
            this.colMeetingDate.Name = "colMeetingDate";
            this.colMeetingDate.Visible = true;
            this.colMeetingDate.VisibleIndex = 1;
            this.colMeetingDate.Width = 150;
            // 
            // colMeetingParticipants
            // 
            this.colMeetingParticipants.Caption = "Katılımcılar";
            this.colMeetingParticipants.FieldName = "Participants";
            this.colMeetingParticipants.Name = "colMeetingParticipants";
            this.colMeetingParticipants.Visible = true;
            this.colMeetingParticipants.VisibleIndex = 2;
            this.colMeetingParticipants.Width = 200;
            // 
            // colMeetingSource
            // 
            this.colMeetingSource.Caption = "Kaynak";
            this.colMeetingSource.FieldName = "Source";
            this.colMeetingSource.Name = "colMeetingSource";
            this.colMeetingSource.Visible = true;
            this.colMeetingSource.VisibleIndex = 3;
            this.colMeetingSource.Width = 100;
            // 
            // lblMeetingsHeader
            // 
            this.lblMeetingsHeader.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMeetingsHeader.Appearance.Options.UseFont = true;
            this.lblMeetingsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMeetingsHeader.Location = new System.Drawing.Point(2, 2);
            this.lblMeetingsHeader.Name = "lblMeetingsHeader";
            this.lblMeetingsHeader.Padding = new System.Windows.Forms.Padding(5);
            this.lblMeetingsHeader.Size = new System.Drawing.Size(111, 30);
            this.lblMeetingsHeader.TabIndex = 0;
            this.lblMeetingsHeader.Text = "📅 Toplantılar";
            // 
            // panelDayHeader
            // 
            this.panelDayHeader.Controls.Add(this.btnSyncOutlook);
            this.panelDayHeader.Controls.Add(this.lblSelectedDate);
            this.panelDayHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDayHeader.Location = new System.Drawing.Point(2, 2);
            this.panelDayHeader.Name = "panelDayHeader";
            this.panelDayHeader.Size = new System.Drawing.Size(766, 50);
            this.panelDayHeader.TabIndex = 0;
            // 
            // btnSyncOutlook
            // 
            this.btnSyncOutlook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSyncOutlook.Location = new System.Drawing.Point(615, 12);
            this.btnSyncOutlook.Name = "btnSyncOutlook";
            this.btnSyncOutlook.Size = new System.Drawing.Size(140, 28);
            this.btnSyncOutlook.TabIndex = 1;
            this.btnSyncOutlook.Text = "📨 Outlook Sync";
            this.btnSyncOutlook.Click += new System.EventHandler(this.btnSyncOutlook_Click);
            // 
            // lblSelectedDate
            // 
            this.lblSelectedDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSelectedDate.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblSelectedDate.Appearance.Options.UseFont = true;
            this.lblSelectedDate.Appearance.Options.UseForeColor = true;
            this.lblSelectedDate.Location = new System.Drawing.Point(10, 12);
            this.lblSelectedDate.Name = "lblSelectedDate";
            this.lblSelectedDate.Size = new System.Drawing.Size(217, 25);
            this.lblSelectedDate.TabIndex = 0;
            this.lblSelectedDate.Text = "01 Aralık 2024, Pazartesi";
            // 
            // panelToolbar
            // 
            this.panelToolbar.Controls.Add(this.btnRefresh);
            this.panelToolbar.Controls.Add(this.btnToday);
            this.panelToolbar.Controls.Add(this.lblTitle);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(1400, 50);
            this.panelToolbar.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1300, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 28);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄 Yenile";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnToday
            // 
            this.btnToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToday.Location = new System.Drawing.Point(1200, 12);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(90, 28);
            this.btnToday.TabIndex = 1;
            this.btnToday.Text = "📅 Bugün";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 Dashboard";
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelToolbar);
            this.Name = "DashboardForm";
            this.Text = "📊 Dashboard";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel1)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain.Panel2)).EndInit();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).EndInit();
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelCalendar)).EndInit();
            this.panelCalendar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelCalendarHeader)).EndInit();
            this.panelCalendarHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelStats)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStatCards.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard1)).EndInit();
            this.panelStatCard1.ResumeLayout(false);
            this.panelStatCard1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard2)).EndInit();
            this.panelStatCard2.ResumeLayout(false);
            this.panelStatCard2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard3)).EndInit();
            this.panelStatCard3.ResumeLayout(false);
            this.panelStatCard3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard4)).EndInit();
            this.panelStatCard4.ResumeLayout(false);
            this.panelStatCard4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard5)).EndInit();
            this.panelStatCard5.ResumeLayout(false);
            this.panelStatCard5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard6)).EndInit();
            this.panelStatCard6.ResumeLayout(false);
            this.panelStatCard6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelStatCard7)).EndInit();
            this.panelStatCard7.ResumeLayout(false);
            this.panelStatCard7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelRight)).EndInit();
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight.Panel1)).EndInit();
            this.splitContainerRight.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight.Panel2)).EndInit();
            this.splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
            this.splitContainerRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelWorkItems)).EndInit();
            this.panelWorkItems.ResumeLayout(false);
            this.panelWorkItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWorkItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMeetings)).EndInit();
            this.panelMeetings.ResumeLayout(false);
            this.panelMeetings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMeetings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMeetings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDayHeader)).EndInit();
            this.panelDayHeader.ResumeLayout(false);
            this.panelDayHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelToolbar)).EndInit();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerMain;
        private DevExpress.XtraEditors.PanelControl panelLeft;
        private DevExpress.XtraEditors.PanelControl panelCalendar;
        private System.Windows.Forms.Panel panelCalendarDays;
        private System.Windows.Forms.Panel panelWeekDays;
        private DevExpress.XtraEditors.PanelControl panelCalendarHeader;
        private DevExpress.XtraEditors.SimpleButton btnNextMonth;
        private DevExpress.XtraEditors.SimpleButton btnPrevMonth;
        private DevExpress.XtraEditors.LabelControl lblMonthYear;
        private DevExpress.XtraEditors.PanelControl panelStats;
        private System.Windows.Forms.FlowLayoutPanel panelStatCards;
        private DevExpress.XtraEditors.PanelControl panelStatCard1;
        private DevExpress.XtraEditors.LabelControl lblTodayItems;
        private DevExpress.XtraEditors.LabelControl lblTodayItemsTitle;
        private DevExpress.XtraEditors.PanelControl panelStatCard2;
        private DevExpress.XtraEditors.LabelControl lblTodayCompleted;
        private DevExpress.XtraEditors.LabelControl lblTodayCompletedTitle;
        private DevExpress.XtraEditors.PanelControl panelStatCard3;
        private DevExpress.XtraEditors.LabelControl lblWeekItems;
        private DevExpress.XtraEditors.LabelControl lblWeekItemsTitle;
        private DevExpress.XtraEditors.PanelControl panelStatCard4;
        private DevExpress.XtraEditors.LabelControl lblWeekCompleted;
        private DevExpress.XtraEditors.LabelControl lblWeekCompletedTitle;
        private DevExpress.XtraEditors.PanelControl panelStatCard5;
        private DevExpress.XtraEditors.LabelControl lblActiveItems;
        private DevExpress.XtraEditors.LabelControl lblActiveItemsTitle;
        private DevExpress.XtraEditors.PanelControl panelStatCard6;
        private DevExpress.XtraEditors.LabelControl lblPendingItems;
        private DevExpress.XtraEditors.LabelControl lblPendingItemsTitle;
        private DevExpress.XtraEditors.PanelControl panelStatCard7;
        private DevExpress.XtraEditors.LabelControl lblTodayMeetings;
        private DevExpress.XtraEditors.LabelControl lblTodayMeetingsTitle;
        private DevExpress.XtraEditors.PanelControl panelRight;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerRight;
        private DevExpress.XtraEditors.PanelControl panelWorkItems;
        private DevExpress.XtraGrid.GridControl gridWorkItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewWorkItems;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraEditors.LabelControl lblWorkItemsHeader;
        private DevExpress.XtraEditors.PanelControl panelMeetings;
        private DevExpress.XtraGrid.GridControl gridMeetings;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMeetings;
        private DevExpress.XtraGrid.Columns.GridColumn colMeetingSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colMeetingDate;
        private DevExpress.XtraGrid.Columns.GridColumn colMeetingParticipants;
        private DevExpress.XtraGrid.Columns.GridColumn colMeetingSource;
        private DevExpress.XtraEditors.LabelControl lblMeetingsHeader;
        private DevExpress.XtraEditors.PanelControl panelDayHeader;
        private DevExpress.XtraEditors.SimpleButton btnSyncOutlook;
        private DevExpress.XtraEditors.LabelControl lblSelectedDate;
        private DevExpress.XtraEditors.PanelControl panelToolbar;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnToday;
        private DevExpress.XtraEditors.LabelControl lblTitle;
    }
}
