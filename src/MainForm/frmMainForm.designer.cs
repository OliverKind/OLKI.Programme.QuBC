namespace OLKI.Programme.QuBC.src.MainForm
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.TabPage tabPageSelect;
        private System.Windows.Forms.GroupBox grbTaskProgressBackup;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_File;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_File_New;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_File_Open;
		private System.Windows.Forms.ToolStripSeparator mnuMain_File_SepSave;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_File_Save;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_File_SaveAs;
		private System.Windows.Forms.ToolStripSeparator mnuMain_File_SepRecentFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_Exit;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_Help;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_Help_About;

		
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuMain_File = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_New = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_Clean = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_SepSave = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMain_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_SepRecentFiles = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMain_File_RecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_RecentFiles_File0 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_RecentFiles_File1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_RecentFiles_File2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_RecentFiles_File3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_SepExit = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMain_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_Extras = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_Extras_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_Help_CheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_Help_SepAbout = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMain_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageSelect = new System.Windows.Forms.TabPage();
            this.spcExplorer = new System.Windows.Forms.SplitContainer();
            this.trvExplorer = new OLKI.Programme.QuBC.src.MainForm.ExplorerTreeView();
            this.imlTreeViewIcons = new System.Windows.Forms.ImageList(this.components);
            this.grbDirectoryScope = new System.Windows.Forms.GroupBox();
            this.rabSaveNothing = new System.Windows.Forms.RadioButton();
            this.btnLsvExplorerChangeSelect = new System.Windows.Forms.Button();
            this.rabSaveAll = new System.Windows.Forms.RadioButton();
            this.rabSaveSelected = new System.Windows.Forms.RadioButton();
            this.lblDirectoryScopeDisabled = new System.Windows.Forms.Label();
            this.lsvDirectoryContent = new OLKI.Toolbox.Widgets.SortListView();
            this.chLsvExplorer_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLsvExplorer_Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLsvExplorer_LastChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlListViewIcon = new System.Windows.Forms.ImageList(this.components);
            this.prgItemProperty = new OLKI.Toolbox.Widgets.ReadOnlyPropertyGrid();
            this.txtExplorerPath = new System.Windows.Forms.TextBox();
            this.btnExplorerGoTop = new System.Windows.Forms.Button();
            this.btnGoToFolder = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabPageBackup = new System.Windows.Forms.TabPage();
            this.grbTaskControleBackup = new System.Windows.Forms.GroupBox();
            this.uscTaskControleBackup = new OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscTaskControle.TaskControle();
            this.grbTaskProgressBackup = new System.Windows.Forms.GroupBox();
            this.uscTaskProgressBackup = new OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscProgress.TaskProgress();
            this.tabPageRestore = new System.Windows.Forms.TabPage();
            this.grbTaskControleRestore = new System.Windows.Forms.GroupBox();
            this.uscTaskControleRestore = new OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscTaskControle.TaskControle();
            this.grbTaskProgressRestore = new System.Windows.Forms.GroupBox();
            this.uscTaskProgressRestore = new OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscProgress.TaskProgress();
            this.tabPageConclusion = new System.Windows.Forms.TabPage();
            this.grbException = new System.Windows.Forms.GroupBox();
            this.btnExceptionTargetGoTo = new System.Windows.Forms.Button();
            this.btnExceptionSourceGoTo = new System.Windows.Forms.Button();
            this.lsvErrorLog = new OLKI.Toolbox.Widgets.SortListView();
            this.chLsvErrorLog_Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLsvErrorLog_Source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLsvErrorLog_Target = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLsvErrorLog_Exception = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblExceptionMessage = new System.Windows.Forms.Label();
            this.txtExceptionMessage = new System.Windows.Forms.TextBox();
            this.lblExceptionTargetPath = new System.Windows.Forms.Label();
            this.txtExceptionTargetPath = new System.Windows.Forms.TextBox();
            this.txtExceptionSourcePath = new System.Windows.Forms.TextBox();
            this.lblExceptionSourcePath = new System.Windows.Forms.Label();
            this.lblExceptionCount = new System.Windows.Forms.Label();
            this.txtExceptionCount = new System.Windows.Forms.TextBox();
            this.lblCopiedDuration = new System.Windows.Forms.Label();
            this.txtConclusionDuration = new System.Windows.Forms.TextBox();
            this.lblCopiedFiles = new System.Windows.Forms.Label();
            this.txtConclusionFiles = new System.Windows.Forms.TextBox();
            this.txtConclusionDirectories = new System.Windows.Forms.TextBox();
            this.lbltxtCopiedDirectories = new System.Windows.Forms.Label();
            this.imlTabIcons = new System.Windows.Forms.ImageList(this.components);
            this.mnuMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcExplorer)).BeginInit();
            this.spcExplorer.Panel1.SuspendLayout();
            this.spcExplorer.Panel2.SuspendLayout();
            this.spcExplorer.SuspendLayout();
            this.grbDirectoryScope.SuspendLayout();
            this.tabPageBackup.SuspendLayout();
            this.grbTaskControleBackup.SuspendLayout();
            this.grbTaskProgressBackup.SuspendLayout();
            this.tabPageRestore.SuspendLayout();
            this.grbTaskControleRestore.SuspendLayout();
            this.grbTaskProgressRestore.SuspendLayout();
            this.tabPageConclusion.SuspendLayout();
            this.grbException.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_File,
            this.mnuMain_Extras,
            this.mnuMain_Help});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1007, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuMain_File
            // 
            this.mnuMain_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_File_New,
            this.mnuMain_File_Open,
            this.mnuMain_File_Clean,
            this.mnuMain_File_SepSave,
            this.mnuMain_File_Save,
            this.mnuMain_File_SaveAs,
            this.mnuMain_File_SepRecentFiles,
            this.mnuMain_File_RecentFiles,
            this.mnuMain_File_SepExit,
            this.mnuMain_File_Exit});
            this.mnuMain_File.Name = "mnuMain_File";
            this.mnuMain_File.Size = new System.Drawing.Size(46, 20);
            this.mnuMain_File.Text = "&Datei";
            // 
            // mnuMain_File_New
            // 
            this.mnuMain_File_New.Image = ((System.Drawing.Image)(resources.GetObject("mnuMain_File_New.Image")));
            this.mnuMain_File_New.Name = "mnuMain_File_New";
            this.mnuMain_File_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuMain_File_New.Size = new System.Drawing.Size(291, 22);
            this.mnuMain_File_New.Text = " &Neues Profil";
            this.mnuMain_File_New.Click += new System.EventHandler(this.mnuMain_File_New_Click);
            // 
            // mnuMain_File_Open
            // 
            this.mnuMain_File_Open.Image = ((System.Drawing.Image)(resources.GetObject("mnuMain_File_Open.Image")));
            this.mnuMain_File_Open.Name = "mnuMain_File_Open";
            this.mnuMain_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuMain_File_Open.Size = new System.Drawing.Size(291, 22);
            this.mnuMain_File_Open.Text = "Profil &öffnen";
            this.mnuMain_File_Open.Click += new System.EventHandler(this.mnuMain_File_Open_Click);
            // 
            // mnuMain_File_Clean
            // 
            this.mnuMain_File_Clean.Image = ((System.Drawing.Image)(resources.GetObject("mnuMain_File_Clean.Image")));
            this.mnuMain_File_Clean.Name = "mnuMain_File_Clean";
            this.mnuMain_File_Clean.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.mnuMain_File_Clean.Size = new System.Drawing.Size(291, 22);
            this.mnuMain_File_Clean.Text = "Projektdatei &bereinigen";
            this.mnuMain_File_Clean.Click += new System.EventHandler(this.mnuMain_File_Clean_Click);
            // 
            // mnuMain_File_SepSave
            // 
            this.mnuMain_File_SepSave.Name = "mnuMain_File_SepSave";
            this.mnuMain_File_SepSave.Size = new System.Drawing.Size(288, 6);
            // 
            // mnuMain_File_Save
            // 
            this.mnuMain_File_Save.Image = ((System.Drawing.Image)(resources.GetObject("mnuMain_File_Save.Image")));
            this.mnuMain_File_Save.Name = "mnuMain_File_Save";
            this.mnuMain_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuMain_File_Save.Size = new System.Drawing.Size(291, 22);
            this.mnuMain_File_Save.Text = "&Speichern";
            this.mnuMain_File_Save.Click += new System.EventHandler(this.mnuMain_File_Save_Click);
            // 
            // mnuMain_File_SaveAs
            // 
            this.mnuMain_File_SaveAs.Name = "mnuMain_File_SaveAs";
            this.mnuMain_File_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mnuMain_File_SaveAs.Size = new System.Drawing.Size(291, 22);
            this.mnuMain_File_SaveAs.Text = "Speichern &unter...";
            this.mnuMain_File_SaveAs.Click += new System.EventHandler(this.mnuMain_File_SaveAs_Click);
            // 
            // mnuMain_File_SepRecentFiles
            // 
            this.mnuMain_File_SepRecentFiles.Name = "mnuMain_File_SepRecentFiles";
            this.mnuMain_File_SepRecentFiles.Size = new System.Drawing.Size(288, 6);
            // 
            // mnuMain_File_RecentFiles
            // 
            this.mnuMain_File_RecentFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_File_RecentFiles_File0,
            this.mnuMain_File_RecentFiles_File1,
            this.mnuMain_File_RecentFiles_File2,
            this.mnuMain_File_RecentFiles_File3});
            this.mnuMain_File_RecentFiles.Name = "mnuMain_File_RecentFiles";
            this.mnuMain_File_RecentFiles.Size = new System.Drawing.Size(291, 22);
            this.mnuMain_File_RecentFiles.Text = "&Zuletzt geöffnete Dateien";
            // 
            // mnuMain_File_RecentFiles_File0
            // 
            this.mnuMain_File_RecentFiles_File0.Name = "mnuMain_File_RecentFiles_File0";
            this.mnuMain_File_RecentFiles_File0.Size = new System.Drawing.Size(110, 22);
            this.mnuMain_File_RecentFiles_File0.Text = "Datei 0";
            this.mnuMain_File_RecentFiles_File0.Click += new System.EventHandler(this.mnuMain_File_RecentFiles_File0_Click);
            // 
            // mnuMain_File_RecentFiles_File1
            // 
            this.mnuMain_File_RecentFiles_File1.Name = "mnuMain_File_RecentFiles_File1";
            this.mnuMain_File_RecentFiles_File1.Size = new System.Drawing.Size(110, 22);
            this.mnuMain_File_RecentFiles_File1.Text = "Datei 1";
            this.mnuMain_File_RecentFiles_File1.Click += new System.EventHandler(this.mnuMain_File_RecentFiles_File1_Click);
            // 
            // mnuMain_File_RecentFiles_File2
            // 
            this.mnuMain_File_RecentFiles_File2.Name = "mnuMain_File_RecentFiles_File2";
            this.mnuMain_File_RecentFiles_File2.Size = new System.Drawing.Size(110, 22);
            this.mnuMain_File_RecentFiles_File2.Text = "Datei 2";
            this.mnuMain_File_RecentFiles_File2.Click += new System.EventHandler(this.mnuMain_File_RecentFiles_File2_Click);
            // 
            // mnuMain_File_RecentFiles_File3
            // 
            this.mnuMain_File_RecentFiles_File3.Name = "mnuMain_File_RecentFiles_File3";
            this.mnuMain_File_RecentFiles_File3.Size = new System.Drawing.Size(110, 22);
            this.mnuMain_File_RecentFiles_File3.Text = "Datei 3";
            this.mnuMain_File_RecentFiles_File3.Click += new System.EventHandler(this.mnuMain_File_RecentFiles_File3_Click);
            // 
            // mnuMain_File_SepExit
            // 
            this.mnuMain_File_SepExit.Name = "mnuMain_File_SepExit";
            this.mnuMain_File_SepExit.Size = new System.Drawing.Size(288, 6);
            // 
            // mnuMain_File_Exit
            // 
            this.mnuMain_File_Exit.Name = "mnuMain_File_Exit";
            this.mnuMain_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuMain_File_Exit.Size = new System.Drawing.Size(291, 22);
            this.mnuMain_File_Exit.Text = "&Beenden";
            this.mnuMain_File_Exit.Click += new System.EventHandler(this.mnuMain_File_Exit_Click);
            // 
            // mnuMain_Extras
            // 
            this.mnuMain_Extras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_Extras_Options});
            this.mnuMain_Extras.Name = "mnuMain_Extras";
            this.mnuMain_Extras.Size = new System.Drawing.Size(50, 20);
            this.mnuMain_Extras.Text = "&Extras";
            // 
            // mnuMain_Extras_Options
            // 
            this.mnuMain_Extras_Options.Image = ((System.Drawing.Image)(resources.GetObject("mnuMain_Extras_Options.Image")));
            this.mnuMain_Extras_Options.Name = "mnuMain_Extras_Options";
            this.mnuMain_Extras_Options.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.mnuMain_Extras_Options.Size = new System.Drawing.Size(252, 22);
            this.mnuMain_Extras_Options.Text = "&Optionen";
            this.mnuMain_Extras_Options.Click += new System.EventHandler(this.mnuMain_Extras_Options_Click);
            // 
            // mnuMain_Help
            // 
            this.mnuMain_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_Help_CheckUpdate,
            this.mnuMain_Help_SepAbout,
            this.mnuMain_Help_About});
            this.mnuMain_Help.Name = "mnuMain_Help";
            this.mnuMain_Help.Size = new System.Drawing.Size(44, 20);
            this.mnuMain_Help.Text = "&Hilfe";
            // 
            // mnuMain_Help_CheckUpdate
            // 
            this.mnuMain_Help_CheckUpdate.Name = "mnuMain_Help_CheckUpdate";
            this.mnuMain_Help_CheckUpdate.Size = new System.Drawing.Size(226, 22);
            this.mnuMain_Help_CheckUpdate.Text = "Nach Updates suchen";
            this.mnuMain_Help_CheckUpdate.Click += new System.EventHandler(this.mnuMain_Help_CheckUpdate_Click);
            // 
            // mnuMain_Help_SepAbout
            // 
            this.mnuMain_Help_SepAbout.Name = "mnuMain_Help_SepAbout";
            this.mnuMain_Help_SepAbout.Size = new System.Drawing.Size(223, 6);
            // 
            // mnuMain_Help_About
            // 
            this.mnuMain_Help_About.Image = ((System.Drawing.Image)(resources.GetObject("mnuMain_Help_About.Image")));
            this.mnuMain_Help_About.Name = "mnuMain_Help_About";
            this.mnuMain_Help_About.ShortcutKeyDisplayString = "";
            this.mnuMain_Help_About.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F1)));
            this.mnuMain_Help_About.Size = new System.Drawing.Size(226, 22);
            this.mnuMain_Help_About.Text = "Info";
            this.mnuMain_Help_About.Click += new System.EventHandler(this.mnuMain_Help_About_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageSelect);
            this.tabControlMain.Controls.Add(this.tabPageBackup);
            this.tabControlMain.Controls.Add(this.tabPageRestore);
            this.tabControlMain.Controls.Add(this.tabPageConclusion);
            this.tabControlMain.ImageList = this.imlTabIcons;
            this.tabControlMain.Location = new System.Drawing.Point(12, 27);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlMain.MinimumSize = new System.Drawing.Size(983, 598);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(983, 598);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageSelect
            // 
            this.tabPageSelect.Controls.Add(this.spcExplorer);
            this.tabPageSelect.ImageIndex = 0;
            this.tabPageSelect.Location = new System.Drawing.Point(4, 23);
            this.tabPageSelect.Name = "tabPageSelect";
            this.tabPageSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSelect.Size = new System.Drawing.Size(975, 571);
            this.tabPageSelect.TabIndex = 0;
            this.tabPageSelect.Text = "Sicherung - Quelle";
            this.tabPageSelect.UseVisualStyleBackColor = true;
            // 
            // spcExplorer
            // 
            this.spcExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcExplorer.Location = new System.Drawing.Point(3, 3);
            this.spcExplorer.Margin = new System.Windows.Forms.Padding(0);
            this.spcExplorer.Name = "spcExplorer";
            // 
            // spcExplorer.Panel1
            // 
            this.spcExplorer.Panel1.Controls.Add(this.trvExplorer);
            this.spcExplorer.Panel1MinSize = 230;
            // 
            // spcExplorer.Panel2
            // 
            this.spcExplorer.Panel2.Controls.Add(this.grbDirectoryScope);
            this.spcExplorer.Panel2.Controls.Add(this.lsvDirectoryContent);
            this.spcExplorer.Panel2.Controls.Add(this.prgItemProperty);
            this.spcExplorer.Panel2.Controls.Add(this.txtExplorerPath);
            this.spcExplorer.Panel2.Controls.Add(this.btnExplorerGoTop);
            this.spcExplorer.Panel2.Controls.Add(this.btnGoToFolder);
            this.spcExplorer.Panel2.Controls.Add(this.btnRefresh);
            this.spcExplorer.Panel2MinSize = 700;
            this.spcExplorer.Size = new System.Drawing.Size(969, 565);
            this.spcExplorer.SplitterDistance = 230;
            this.spcExplorer.TabIndex = 11;
            // 
            // trvExplorer
            // 
            this.trvExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvExplorer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.trvExplorer.DirectoryList = null;
            this.trvExplorer.ImageIndex = 16;
            this.trvExplorer.ImageList = this.imlTreeViewIcons;
            this.trvExplorer.Location = new System.Drawing.Point(3, 3);
            this.trvExplorer.Name = "trvExplorer";
            this.trvExplorer.SelectedImageIndex = 0;
            this.trvExplorer.ShowNodeToolTips = true;
            this.trvExplorer.Size = new System.Drawing.Size(225, 559);
            this.trvExplorer.TabIndex = 10;
            this.trvExplorer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvExplorer_AfterSelect);
            // 
            // imlTreeViewIcons
            // 
            this.imlTreeViewIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeViewIcons.ImageStream")));
            this.imlTreeViewIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlTreeViewIcons.Images.SetKeyName(0, "00--3.5_Disk_Drive.ico");
            this.imlTreeViewIcons.Images.SetKeyName(1, "01--3.5_Disk_Drive_NoCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(2, "02--3.5_Disk_Drive_Check.ico");
            this.imlTreeViewIcons.Images.SetKeyName(3, "03--3.5_Disk_Drive_IntCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(4, "04--Hard_Drive.ico");
            this.imlTreeViewIcons.Images.SetKeyName(5, "05--Hard_Drive_NoCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(6, "06--Hard_Drive_Check.ico");
            this.imlTreeViewIcons.Images.SetKeyName(7, "07--Hard_Drive_IntCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(8, "08--CD_Drive.ico");
            this.imlTreeViewIcons.Images.SetKeyName(9, "09--CD_Drive_NoCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(10, "10--CD_Drive_Check.ico");
            this.imlTreeViewIcons.Images.SetKeyName(11, "11-CD_Drive_IntCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(12, "12--Network_Drive.ico");
            this.imlTreeViewIcons.Images.SetKeyName(13, "13--Network_Drive_NoCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(14, "14--Network_Drive_Check.ico");
            this.imlTreeViewIcons.Images.SetKeyName(15, "15--Network_IntCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(16, "16--folder.ico");
            this.imlTreeViewIcons.Images.SetKeyName(17, "17--folder_NoCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(18, "18--folder_Check.ico");
            this.imlTreeViewIcons.Images.SetKeyName(19, "19--folder_IntCheck.ico");
            this.imlTreeViewIcons.Images.SetKeyName(20, "20--File.ico");
            // 
            // grbDirectoryScope
            // 
            this.grbDirectoryScope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbDirectoryScope.Controls.Add(this.rabSaveNothing);
            this.grbDirectoryScope.Controls.Add(this.btnLsvExplorerChangeSelect);
            this.grbDirectoryScope.Controls.Add(this.rabSaveAll);
            this.grbDirectoryScope.Controls.Add(this.rabSaveSelected);
            this.grbDirectoryScope.Controls.Add(this.lblDirectoryScopeDisabled);
            this.grbDirectoryScope.Location = new System.Drawing.Point(3, 3);
            this.grbDirectoryScope.Name = "grbDirectoryScope";
            this.grbDirectoryScope.Size = new System.Drawing.Size(729, 138);
            this.grbDirectoryScope.TabIndex = 1;
            this.grbDirectoryScope.TabStop = false;
            this.grbDirectoryScope.Text = "Sicherungsoption für Ordner";
            // 
            // rabSaveNothing
            // 
            this.rabSaveNothing.AutoSize = true;
            this.rabSaveNothing.ForeColor = System.Drawing.Color.Red;
            this.rabSaveNothing.Location = new System.Drawing.Point(6, 19);
            this.rabSaveNothing.Name = "rabSaveNothing";
            this.rabSaveNothing.Size = new System.Drawing.Size(316, 17);
            this.rabSaveNothing.TabIndex = 0;
            this.rabSaveNothing.Text = "Diesen Ordner und alle Unterordner und Dateien nicht sichern";
            this.rabSaveNothing.UseVisualStyleBackColor = true;
            this.rabSaveNothing.CheckedChanged += new System.EventHandler(this.rabSaveNothing_CheckedChanged);
            // 
            // btnLsvExplorerChangeSelect
            // 
            this.btnLsvExplorerChangeSelect.Location = new System.Drawing.Point(6, 109);
            this.btnLsvExplorerChangeSelect.Name = "btnLsvExplorerChangeSelect";
            this.btnLsvExplorerChangeSelect.Size = new System.Drawing.Size(357, 23);
            this.btnLsvExplorerChangeSelect.TabIndex = 3;
            this.btnLsvExplorerChangeSelect.Text = "Alle Ordner und Dateien an-/abwählen";
            this.btnLsvExplorerChangeSelect.UseVisualStyleBackColor = true;
            this.btnLsvExplorerChangeSelect.Click += new System.EventHandler(this.btnLsvExplorerChangeSelect_Click);
            // 
            // rabSaveAll
            // 
            this.rabSaveAll.AutoSize = true;
            this.rabSaveAll.ForeColor = System.Drawing.Color.Green;
            this.rabSaveAll.Location = new System.Drawing.Point(6, 49);
            this.rabSaveAll.Name = "rabSaveAll";
            this.rabSaveAll.Size = new System.Drawing.Size(290, 17);
            this.rabSaveAll.TabIndex = 1;
            this.rabSaveAll.Text = "Diesen Ordner und alle Unterordner und Dateien sichern";
            this.rabSaveAll.UseVisualStyleBackColor = true;
            this.rabSaveAll.CheckedChanged += new System.EventHandler(this.rabSaveAll_CheckedChanged);
            // 
            // rabSaveSelected
            // 
            this.rabSaveSelected.AutoSize = true;
            this.rabSaveSelected.Checked = true;
            this.rabSaveSelected.ForeColor = System.Drawing.Color.Blue;
            this.rabSaveSelected.Location = new System.Drawing.Point(6, 79);
            this.rabSaveSelected.Name = "rabSaveSelected";
            this.rabSaveSelected.Size = new System.Drawing.Size(397, 17);
            this.rabSaveSelected.TabIndex = 2;
            this.rabSaveSelected.TabStop = true;
            this.rabSaveSelected.Text = "Diesen Ordner und ausgewählte Unterordner und ausgewählte Dateien sichern";
            this.rabSaveSelected.UseVisualStyleBackColor = true;
            this.rabSaveSelected.CheckedChanged += new System.EventHandler(this.rabSaveSelected_CheckedChanged);
            // 
            // lblDirectoryScopeDisabled
            // 
            this.lblDirectoryScopeDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirectoryScopeDisabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirectoryScopeDisabled.Location = new System.Drawing.Point(6, 16);
            this.lblDirectoryScopeDisabled.Name = "lblDirectoryScopeDisabled";
            this.lblDirectoryScopeDisabled.Size = new System.Drawing.Size(717, 119);
            this.lblDirectoryScopeDisabled.TabIndex = 10;
            this.lblDirectoryScopeDisabled.Text = "Aufgrung der Speicheroptionen eines oder mehrerer Übergeordneter Ordner können di" +
    "e Speicheroptionen für diesen Ordner nicht geändert werden.\r\n";
            this.lblDirectoryScopeDisabled.Visible = false;
            // 
            // lsvDirectoryContent
            // 
            this.lsvDirectoryContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvDirectoryContent.CheckBoxes = true;
            this.lsvDirectoryContent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLsvExplorer_Name,
            this.chLsvExplorer_Length,
            this.chLsvExplorer_LastChange});
            this.lsvDirectoryContent.FullRowSelect = true;
            this.lsvDirectoryContent.GridLines = true;
            this.lsvDirectoryContent.HideSelection = false;
            this.lsvDirectoryContent.Location = new System.Drawing.Point(2, 176);
            this.lsvDirectoryContent.MultiSelect = false;
            this.lsvDirectoryContent.Name = "lsvDirectoryContent";
            this.lsvDirectoryContent.Size = new System.Drawing.Size(479, 386);
            this.lsvDirectoryContent.SmallImageList = this.imlListViewIcon;
            this.lsvDirectoryContent.TabIndex = 3;
            this.lsvDirectoryContent.UseCompatibleStateImageBehavior = false;
            this.lsvDirectoryContent.View = System.Windows.Forms.View.Details;
            this.lsvDirectoryContent.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lsvExplorer_ItemChecked);
            this.lsvDirectoryContent.SelectedIndexChanged += new System.EventHandler(this.lsvExplorer_SelectedIndexChanged);
            // 
            // chLsvExplorer_Name
            // 
            this.chLsvExplorer_Name.Text = "Name";
            this.chLsvExplorer_Name.Width = 255;
            // 
            // chLsvExplorer_Length
            // 
            this.chLsvExplorer_Length.Text = "Größe";
            this.chLsvExplorer_Length.Width = 75;
            // 
            // chLsvExplorer_LastChange
            // 
            this.chLsvExplorer_LastChange.Text = "Geändert am";
            this.chLsvExplorer_LastChange.Width = 120;
            // 
            // imlListViewIcon
            // 
            this.imlListViewIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlListViewIcon.ImageStream")));
            this.imlListViewIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imlListViewIcon.Images.SetKeyName(0, "16--folder.ico");
            this.imlListViewIcon.Images.SetKeyName(1, "17--folder_NoCheck.ico");
            this.imlListViewIcon.Images.SetKeyName(2, "18--folder_Check.ico");
            this.imlListViewIcon.Images.SetKeyName(3, "19--folder_IntCheck.ico");
            // 
            // prgItemProperty
            // 
            this.prgItemProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgItemProperty.LineColor = System.Drawing.SystemColors.ControlDark;
            this.prgItemProperty.Location = new System.Drawing.Point(487, 173);
            this.prgItemProperty.Name = "prgItemProperty";
            this.prgItemProperty.ReadOnly = true;
            this.prgItemProperty.Size = new System.Drawing.Size(245, 389);
            this.prgItemProperty.TabIndex = 4;
            this.prgItemProperty.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            // 
            // txtExplorerPath
            // 
            this.txtExplorerPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExplorerPath.Location = new System.Drawing.Point(3, 147);
            this.txtExplorerPath.Name = "txtExplorerPath";
            this.txtExplorerPath.ReadOnly = true;
            this.txtExplorerPath.Size = new System.Drawing.Size(391, 20);
            this.txtExplorerPath.TabIndex = 2;
            // 
            // btnExplorerGoTop
            // 
            this.btnExplorerGoTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplorerGoTop.Image = ((System.Drawing.Image)(resources.GetObject("btnExplorerGoTop.Image")));
            this.btnExplorerGoTop.Location = new System.Drawing.Point(400, 147);
            this.btnExplorerGoTop.Name = "btnExplorerGoTop";
            this.btnExplorerGoTop.Size = new System.Drawing.Size(23, 23);
            this.btnExplorerGoTop.TabIndex = 6;
            this.btnExplorerGoTop.UseVisualStyleBackColor = true;
            this.btnExplorerGoTop.Click += new System.EventHandler(this.btnExplorerGoTop_Click);
            // 
            // btnGoToFolder
            // 
            this.btnGoToFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnGoToFolder.Image")));
            this.btnGoToFolder.Location = new System.Drawing.Point(429, 147);
            this.btnGoToFolder.Name = "btnGoToFolder";
            this.btnGoToFolder.Size = new System.Drawing.Size(23, 23);
            this.btnGoToFolder.TabIndex = 8;
            this.btnGoToFolder.UseVisualStyleBackColor = true;
            this.btnGoToFolder.Click += new System.EventHandler(this.btnGoToFolder_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(458, 147);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 23);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabPageBackup
            // 
            this.tabPageBackup.Controls.Add(this.grbTaskControleBackup);
            this.tabPageBackup.Controls.Add(this.grbTaskProgressBackup);
            this.tabPageBackup.ImageIndex = 1;
            this.tabPageBackup.Location = new System.Drawing.Point(4, 23);
            this.tabPageBackup.Name = "tabPageBackup";
            this.tabPageBackup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBackup.Size = new System.Drawing.Size(975, 571);
            this.tabPageBackup.TabIndex = 1;
            this.tabPageBackup.Text = "Sicherung - Ausführen";
            this.tabPageBackup.UseVisualStyleBackColor = true;
            // 
            // grbTaskControleBackup
            // 
            this.grbTaskControleBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTaskControleBackup.Controls.Add(this.uscTaskControleBackup);
            this.grbTaskControleBackup.Location = new System.Drawing.Point(6, 6);
            this.grbTaskControleBackup.Name = "grbTaskControleBackup";
            this.grbTaskControleBackup.Size = new System.Drawing.Size(963, 258);
            this.grbTaskControleBackup.TabIndex = 21;
            this.grbTaskControleBackup.TabStop = false;
            this.grbTaskControleBackup.Text = "Sicherungsoptionen";
            // 
            // uscTaskControleBackup
            // 
            this.uscTaskControleBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uscTaskControleBackup.Location = new System.Drawing.Point(0, 17);
            this.uscTaskControleBackup.Mode = OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscTaskControle.TaskControle.ControleMode.CreateBackup;
            this.uscTaskControleBackup.Name = "uscTaskControleBackup";
            this.uscTaskControleBackup.Size = new System.Drawing.Size(957, 235);
            this.uscTaskControleBackup.TabIndex = 0;
            this.uscTaskControleBackup.TaskFinishedCanceled += new System.EventHandler(this.uscTaskControleBackup_TaskFinishedCanceled);
            this.uscTaskControleBackup.TaskStarted += new System.EventHandler(this.uscTaskControleBackup_TaskStarted);
            // 
            // grbTaskProgressBackup
            // 
            this.grbTaskProgressBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTaskProgressBackup.Controls.Add(this.uscTaskProgressBackup);
            this.grbTaskProgressBackup.Location = new System.Drawing.Point(6, 270);
            this.grbTaskProgressBackup.Name = "grbTaskProgressBackup";
            this.grbTaskProgressBackup.Size = new System.Drawing.Size(963, 296);
            this.grbTaskProgressBackup.TabIndex = 20;
            this.grbTaskProgressBackup.TabStop = false;
            this.grbTaskProgressBackup.Text = "Sicherungsvorgang";
            // 
            // uscTaskProgressBackup
            // 
            this.uscTaskProgressBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uscTaskProgressBackup.Location = new System.Drawing.Point(6, 20);
            this.uscTaskProgressBackup.Name = "uscTaskProgressBackup";
            this.uscTaskProgressBackup.Size = new System.Drawing.Size(951, 270);
            this.uscTaskProgressBackup.TabIndex = 0;
            // 
            // tabPageRestore
            // 
            this.tabPageRestore.Controls.Add(this.grbTaskControleRestore);
            this.tabPageRestore.Controls.Add(this.grbTaskProgressRestore);
            this.tabPageRestore.ImageIndex = 2;
            this.tabPageRestore.Location = new System.Drawing.Point(4, 23);
            this.tabPageRestore.Name = "tabPageRestore";
            this.tabPageRestore.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRestore.Size = new System.Drawing.Size(975, 571);
            this.tabPageRestore.TabIndex = 3;
            this.tabPageRestore.Text = "Sicherung - Wiederherstellen";
            this.tabPageRestore.UseVisualStyleBackColor = true;
            // 
            // grbTaskControleRestore
            // 
            this.grbTaskControleRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTaskControleRestore.Controls.Add(this.uscTaskControleRestore);
            this.grbTaskControleRestore.Location = new System.Drawing.Point(6, 6);
            this.grbTaskControleRestore.Name = "grbTaskControleRestore";
            this.grbTaskControleRestore.Size = new System.Drawing.Size(963, 258);
            this.grbTaskControleRestore.TabIndex = 30;
            this.grbTaskControleRestore.TabStop = false;
            this.grbTaskControleRestore.Text = "Wiederherstellungsoptionen";
            // 
            // uscTaskControleRestore
            // 
            this.uscTaskControleRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uscTaskControleRestore.Location = new System.Drawing.Point(0, 17);
            this.uscTaskControleRestore.Mode = OLKI.Programme.QuBC.src.MainForm.Usercontroles.uscTaskControle.TaskControle.ControleMode.RestoreBackup;
            this.uscTaskControleRestore.Name = "uscTaskControleRestore";
            this.uscTaskControleRestore.Size = new System.Drawing.Size(957, 235);
            this.uscTaskControleRestore.TabIndex = 0;
            this.uscTaskControleRestore.TaskFinishedCanceled += new System.EventHandler(this.uscTaskControleRestore_TaskFinishedCanceled);
            this.uscTaskControleRestore.TaskStarted += new System.EventHandler(this.uscTaskControleRestore_TaskStarted);
            // 
            // grbTaskProgressRestore
            // 
            this.grbTaskProgressRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTaskProgressRestore.Controls.Add(this.uscTaskProgressRestore);
            this.grbTaskProgressRestore.Location = new System.Drawing.Point(6, 270);
            this.grbTaskProgressRestore.Name = "grbTaskProgressRestore";
            this.grbTaskProgressRestore.Size = new System.Drawing.Size(963, 296);
            this.grbTaskProgressRestore.TabIndex = 21;
            this.grbTaskProgressRestore.TabStop = false;
            this.grbTaskProgressRestore.Text = "Wiederherstellung";
            // 
            // uscTaskProgressRestore
            // 
            this.uscTaskProgressRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uscTaskProgressRestore.Location = new System.Drawing.Point(6, 20);
            this.uscTaskProgressRestore.Name = "uscTaskProgressRestore";
            this.uscTaskProgressRestore.Size = new System.Drawing.Size(951, 270);
            this.uscTaskProgressRestore.TabIndex = 0;
            // 
            // tabPageConclusion
            // 
            this.tabPageConclusion.Controls.Add(this.grbException);
            this.tabPageConclusion.Controls.Add(this.lblExceptionCount);
            this.tabPageConclusion.Controls.Add(this.txtExceptionCount);
            this.tabPageConclusion.Controls.Add(this.lblCopiedDuration);
            this.tabPageConclusion.Controls.Add(this.txtConclusionDuration);
            this.tabPageConclusion.Controls.Add(this.lblCopiedFiles);
            this.tabPageConclusion.Controls.Add(this.txtConclusionFiles);
            this.tabPageConclusion.Controls.Add(this.txtConclusionDirectories);
            this.tabPageConclusion.Controls.Add(this.lbltxtCopiedDirectories);
            this.tabPageConclusion.ImageIndex = 3;
            this.tabPageConclusion.Location = new System.Drawing.Point(4, 23);
            this.tabPageConclusion.Name = "tabPageConclusion";
            this.tabPageConclusion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConclusion.Size = new System.Drawing.Size(975, 571);
            this.tabPageConclusion.TabIndex = 2;
            this.tabPageConclusion.Text = "Zusammenfassung - Fehler";
            this.tabPageConclusion.UseVisualStyleBackColor = true;
            // 
            // grbException
            // 
            this.grbException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbException.Controls.Add(this.btnExceptionTargetGoTo);
            this.grbException.Controls.Add(this.btnExceptionSourceGoTo);
            this.grbException.Controls.Add(this.lsvErrorLog);
            this.grbException.Controls.Add(this.lblExceptionMessage);
            this.grbException.Controls.Add(this.txtExceptionMessage);
            this.grbException.Controls.Add(this.lblExceptionTargetPath);
            this.grbException.Controls.Add(this.txtExceptionTargetPath);
            this.grbException.Controls.Add(this.txtExceptionSourcePath);
            this.grbException.Controls.Add(this.lblExceptionSourcePath);
            this.grbException.Location = new System.Drawing.Point(6, 110);
            this.grbException.Name = "grbException";
            this.grbException.Size = new System.Drawing.Size(966, 458);
            this.grbException.TabIndex = 8;
            this.grbException.TabStop = false;
            this.grbException.Text = "Kopierfehler";
            // 
            // btnExceptionTargetGoTo
            // 
            this.btnExceptionTargetGoTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExceptionTargetGoTo.Image = ((System.Drawing.Image)(resources.GetObject("btnExceptionTargetGoTo.Image")));
            this.btnExceptionTargetGoTo.Location = new System.Drawing.Point(937, 366);
            this.btnExceptionTargetGoTo.Name = "btnExceptionTargetGoTo";
            this.btnExceptionTargetGoTo.Size = new System.Drawing.Size(23, 23);
            this.btnExceptionTargetGoTo.TabIndex = 10;
            this.btnExceptionTargetGoTo.UseVisualStyleBackColor = true;
            this.btnExceptionTargetGoTo.Click += new System.EventHandler(this.btnExceptionTargetGoTo_Click);
            // 
            // btnExceptionSourceGoTo
            // 
            this.btnExceptionSourceGoTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExceptionSourceGoTo.Image = ((System.Drawing.Image)(resources.GetObject("btnExceptionSourceGoTo.Image")));
            this.btnExceptionSourceGoTo.Location = new System.Drawing.Point(937, 340);
            this.btnExceptionSourceGoTo.Name = "btnExceptionSourceGoTo";
            this.btnExceptionSourceGoTo.Size = new System.Drawing.Size(23, 23);
            this.btnExceptionSourceGoTo.TabIndex = 9;
            this.btnExceptionSourceGoTo.UseVisualStyleBackColor = true;
            this.btnExceptionSourceGoTo.Click += new System.EventHandler(this.btnExceptionSourceGoTo_Click);
            // 
            // lsvErrorLog
            // 
            this.lsvErrorLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvErrorLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lsvErrorLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLsvErrorLog_Number,
            this.chLsvErrorLog_Source,
            this.chLsvErrorLog_Target,
            this.chLsvErrorLog_Exception});
            this.lsvErrorLog.FullRowSelect = true;
            this.lsvErrorLog.GridLines = true;
            this.lsvErrorLog.HideSelection = false;
            this.lsvErrorLog.Location = new System.Drawing.Point(6, 19);
            this.lsvErrorLog.MultiSelect = false;
            this.lsvErrorLog.Name = "lsvErrorLog";
            this.lsvErrorLog.ShowItemToolTips = true;
            this.lsvErrorLog.Size = new System.Drawing.Size(954, 315);
            this.lsvErrorLog.TabIndex = 0;
            this.lsvErrorLog.UseCompatibleStateImageBehavior = false;
            this.lsvErrorLog.View = System.Windows.Forms.View.Details;
            this.lsvErrorLog.SelectedIndexChanged += new System.EventHandler(this.lsvErrorLog_SelectedIndexChanged);
            // 
            // chLsvErrorLog_Number
            // 
            this.chLsvErrorLog_Number.Text = "Nummer";
            this.chLsvErrorLog_Number.Width = 30;
            // 
            // chLsvErrorLog_Source
            // 
            this.chLsvErrorLog_Source.Text = "Quelle";
            this.chLsvErrorLog_Source.Width = 240;
            // 
            // chLsvErrorLog_Target
            // 
            this.chLsvErrorLog_Target.Text = "Ziel";
            this.chLsvErrorLog_Target.Width = 240;
            // 
            // chLsvErrorLog_Exception
            // 
            this.chLsvErrorLog_Exception.Text = "Fehler";
            this.chLsvErrorLog_Exception.Width = 360;
            // 
            // lblExceptionMessage
            // 
            this.lblExceptionMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExceptionMessage.Location = new System.Drawing.Point(6, 397);
            this.lblExceptionMessage.Name = "lblExceptionMessage";
            this.lblExceptionMessage.Size = new System.Drawing.Size(45, 23);
            this.lblExceptionMessage.TabIndex = 5;
            this.lblExceptionMessage.Text = "Fehler:";
            // 
            // txtExceptionMessage
            // 
            this.txtExceptionMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExceptionMessage.Location = new System.Drawing.Point(82, 394);
            this.txtExceptionMessage.Multiline = true;
            this.txtExceptionMessage.Name = "txtExceptionMessage";
            this.txtExceptionMessage.ReadOnly = true;
            this.txtExceptionMessage.Size = new System.Drawing.Size(878, 58);
            this.txtExceptionMessage.TabIndex = 6;
            // 
            // lblExceptionTargetPath
            // 
            this.lblExceptionTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExceptionTargetPath.Location = new System.Drawing.Point(6, 371);
            this.lblExceptionTargetPath.Name = "lblExceptionTargetPath";
            this.lblExceptionTargetPath.Size = new System.Drawing.Size(45, 23);
            this.lblExceptionTargetPath.TabIndex = 3;
            this.lblExceptionTargetPath.Text = "Ziel:";
            // 
            // txtExceptionTargetPath
            // 
            this.txtExceptionTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExceptionTargetPath.Location = new System.Drawing.Point(82, 368);
            this.txtExceptionTargetPath.Name = "txtExceptionTargetPath";
            this.txtExceptionTargetPath.ReadOnly = true;
            this.txtExceptionTargetPath.Size = new System.Drawing.Size(849, 20);
            this.txtExceptionTargetPath.TabIndex = 4;
            // 
            // txtExceptionSourcePath
            // 
            this.txtExceptionSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExceptionSourcePath.Location = new System.Drawing.Point(82, 342);
            this.txtExceptionSourcePath.Name = "txtExceptionSourcePath";
            this.txtExceptionSourcePath.ReadOnly = true;
            this.txtExceptionSourcePath.Size = new System.Drawing.Size(849, 20);
            this.txtExceptionSourcePath.TabIndex = 2;
            // 
            // lblExceptionSourcePath
            // 
            this.lblExceptionSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExceptionSourcePath.Location = new System.Drawing.Point(6, 345);
            this.lblExceptionSourcePath.Name = "lblExceptionSourcePath";
            this.lblExceptionSourcePath.Size = new System.Drawing.Size(45, 23);
            this.lblExceptionSourcePath.TabIndex = 1;
            this.lblExceptionSourcePath.Text = "Quelle:";
            // 
            // lblExceptionCount
            // 
            this.lblExceptionCount.Location = new System.Drawing.Point(6, 87);
            this.lblExceptionCount.Name = "lblExceptionCount";
            this.lblExceptionCount.Size = new System.Drawing.Size(213, 23);
            this.lblExceptionCount.TabIndex = 6;
            this.lblExceptionCount.Text = "Fehler:";
            // 
            // txtExceptionCount
            // 
            this.txtExceptionCount.Location = new System.Drawing.Point(225, 84);
            this.txtExceptionCount.Name = "txtExceptionCount";
            this.txtExceptionCount.ReadOnly = true;
            this.txtExceptionCount.Size = new System.Drawing.Size(200, 20);
            this.txtExceptionCount.TabIndex = 7;
            this.txtExceptionCount.TextChanged += new System.EventHandler(this.txtExceptionCount_TextChanged);
            // 
            // lblCopiedDuration
            // 
            this.lblCopiedDuration.Location = new System.Drawing.Point(6, 61);
            this.lblCopiedDuration.Name = "lblCopiedDuration";
            this.lblCopiedDuration.Size = new System.Drawing.Size(213, 23);
            this.lblCopiedDuration.TabIndex = 4;
            this.lblCopiedDuration.Text = "Dauer:";
            // 
            // txtConclusionDuration
            // 
            this.txtConclusionDuration.Location = new System.Drawing.Point(225, 58);
            this.txtConclusionDuration.Name = "txtConclusionDuration";
            this.txtConclusionDuration.ReadOnly = true;
            this.txtConclusionDuration.Size = new System.Drawing.Size(200, 20);
            this.txtConclusionDuration.TabIndex = 5;
            // 
            // lblCopiedFiles
            // 
            this.lblCopiedFiles.Location = new System.Drawing.Point(6, 35);
            this.lblCopiedFiles.Name = "lblCopiedFiles";
            this.lblCopiedFiles.Size = new System.Drawing.Size(213, 23);
            this.lblCopiedFiles.TabIndex = 3;
            this.lblCopiedFiles.Text = "Gesicherte / Wiederhergestellte Dateien:";
            // 
            // txtConclusionFiles
            // 
            this.txtConclusionFiles.Location = new System.Drawing.Point(225, 32);
            this.txtConclusionFiles.Name = "txtConclusionFiles";
            this.txtConclusionFiles.ReadOnly = true;
            this.txtConclusionFiles.Size = new System.Drawing.Size(200, 20);
            this.txtConclusionFiles.TabIndex = 2;
            // 
            // txtConclusionDirectories
            // 
            this.txtConclusionDirectories.Location = new System.Drawing.Point(225, 6);
            this.txtConclusionDirectories.Name = "txtConclusionDirectories";
            this.txtConclusionDirectories.ReadOnly = true;
            this.txtConclusionDirectories.Size = new System.Drawing.Size(200, 20);
            this.txtConclusionDirectories.TabIndex = 1;
            // 
            // lbltxtCopiedDirectories
            // 
            this.lbltxtCopiedDirectories.Location = new System.Drawing.Point(6, 9);
            this.lbltxtCopiedDirectories.Name = "lbltxtCopiedDirectories";
            this.lbltxtCopiedDirectories.Size = new System.Drawing.Size(213, 23);
            this.lbltxtCopiedDirectories.TabIndex = 0;
            this.lbltxtCopiedDirectories.Text = "Gesicherte / Wiederhergestellte Ordner:";
            // 
            // imlTabIcons
            // 
            this.imlTabIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTabIcons.ImageStream")));
            this.imlTabIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlTabIcons.Images.SetKeyName(0, "BackupOptions.ico");
            this.imlTabIcons.Images.SetKeyName(1, "BackupCreate.ico");
            this.imlTabIcons.Images.SetKeyName(2, "BackupRestore.ico");
            this.imlTabIcons.Images.SetKeyName(3, "BackupReport.ico");
            this.imlTabIcons.Images.SetKeyName(4, "BackupReportWarning.ico");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1007, 634);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MinimumSize = new System.Drawing.Size(1023, 673);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "{0}";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageSelect.ResumeLayout(false);
            this.spcExplorer.Panel1.ResumeLayout(false);
            this.spcExplorer.Panel2.ResumeLayout(false);
            this.spcExplorer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcExplorer)).EndInit();
            this.spcExplorer.ResumeLayout(false);
            this.grbDirectoryScope.ResumeLayout(false);
            this.grbDirectoryScope.PerformLayout();
            this.tabPageBackup.ResumeLayout(false);
            this.grbTaskControleBackup.ResumeLayout(false);
            this.grbTaskProgressBackup.ResumeLayout(false);
            this.tabPageRestore.ResumeLayout(false);
            this.grbTaskControleRestore.ResumeLayout(false);
            this.grbTaskProgressRestore.ResumeLayout(false);
            this.tabPageConclusion.ResumeLayout(false);
            this.tabPageConclusion.PerformLayout();
            this.grbException.ResumeLayout(false);
            this.grbException.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private OLKI.Toolbox.Widgets.ReadOnlyPropertyGrid prgItemProperty;
        private OLKI.Toolbox.Widgets.SortListView lsvDirectoryContent;
        private System.Windows.Forms.ColumnHeader chLsvExplorer_Name;
        private System.Windows.Forms.ColumnHeader chLsvExplorer_Length;
        private System.Windows.Forms.ColumnHeader chLsvExplorer_LastChange;
        private System.Windows.Forms.GroupBox grbDirectoryScope;
        private System.Windows.Forms.Button btnLsvExplorerChangeSelect;
        private System.Windows.Forms.RadioButton rabSaveAll;
        private System.Windows.Forms.RadioButton rabSaveSelected;
        private System.Windows.Forms.ImageList imlTreeViewIcons;
        private System.Windows.Forms.TextBox txtExplorerPath;
        private System.Windows.Forms.RadioButton rabSaveNothing;
        private System.Windows.Forms.Button btnExplorerGoTop;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnGoToFolder;
        internal System.Windows.Forms.TabControl tabControlMain;
        internal System.Windows.Forms.TabPage tabPageBackup;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_Extras;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_Extras_Options;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_RecentFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_RecentFiles_File0;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_RecentFiles_File1;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_RecentFiles_File2;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_RecentFiles_File3;
        private System.Windows.Forms.ToolStripSeparator mnuMain_File_SepExit;
        internal System.Windows.Forms.TabPage tabPageRestore;
        internal System.Windows.Forms.TabPage tabPageConclusion;
        private System.Windows.Forms.GroupBox grbException;
        internal OLKI.Toolbox.Widgets.SortListView lsvErrorLog;
        private System.Windows.Forms.ColumnHeader chLsvErrorLog_Number;
        private System.Windows.Forms.ColumnHeader chLsvErrorLog_Source;
        private System.Windows.Forms.ColumnHeader chLsvErrorLog_Target;
        private System.Windows.Forms.ColumnHeader chLsvErrorLog_Exception;
        private System.Windows.Forms.Label lblExceptionMessage;
        private System.Windows.Forms.TextBox txtExceptionMessage;
        private System.Windows.Forms.Label lblExceptionTargetPath;
        private System.Windows.Forms.TextBox txtExceptionTargetPath;
        private System.Windows.Forms.TextBox txtExceptionSourcePath;
        private System.Windows.Forms.Label lblExceptionSourcePath;
        private System.Windows.Forms.Label lblExceptionCount;
        internal System.Windows.Forms.TextBox txtExceptionCount;
        private System.Windows.Forms.Label lblCopiedDuration;
        internal System.Windows.Forms.TextBox txtConclusionDuration;
        private System.Windows.Forms.Label lblCopiedFiles;
        internal System.Windows.Forms.TextBox txtConclusionFiles;
        internal System.Windows.Forms.TextBox txtConclusionDirectories;
        private System.Windows.Forms.Label lbltxtCopiedDirectories;
        private System.Windows.Forms.GroupBox grbTaskControleBackup;
        private System.Windows.Forms.GroupBox grbTaskControleRestore;
        private System.Windows.Forms.GroupBox grbTaskProgressRestore;
        private Usercontroles.uscTaskControle.TaskControle uscTaskControleBackup;
        private Usercontroles.uscTaskControle.TaskControle uscTaskControleRestore;
        private Usercontroles.uscProgress.TaskProgress uscTaskProgressBackup;
        private Usercontroles.uscProgress.TaskProgress uscTaskProgressRestore;
        private System.Windows.Forms.ImageList imlTabIcons;
        private ExplorerTreeView trvExplorer;
        private System.Windows.Forms.SplitContainer spcExplorer;
        private System.Windows.Forms.ImageList imlListViewIcon;
        private System.Windows.Forms.Label lblDirectoryScopeDisabled;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_Help_CheckUpdate;
        private System.Windows.Forms.ToolStripSeparator mnuMain_Help_SepAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_Clean;
        private System.Windows.Forms.Button btnExceptionTargetGoTo;
        private System.Windows.Forms.Button btnExceptionSourceGoTo;
    }
}