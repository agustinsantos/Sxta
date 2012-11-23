namespace Sxta.Rti1516.WinMain
{
    partial class PluginList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginList));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDescription = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderVersion = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderModuleId = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderModuleClass = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.AutoArrange = false;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDescription,
            this.columnHeaderVersion,
            this.columnHeaderModuleId,
            this.columnHeaderModuleClass});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(337, 370);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Plugin Name";
            this.columnHeaderName.Width = 102;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "Plugin Description";
            this.columnHeaderDescription.Width = 300;
            // 
            // columnHeaderVersion
            // 
            this.columnHeaderVersion.Text = "Version";
            this.columnHeaderVersion.Width = 150;
            // 
            // columnHeaderModuleId
            // 
            this.columnHeaderModuleId.Text = "Module Id";
            this.columnHeaderModuleId.Width = 89;
            // 
            // columnHeaderModuleClass
            // 
            this.columnHeaderModuleClass.Text = "Class";
            this.columnHeaderModuleClass.Width = 62;
            // 
            // PluginList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(337, 376);
            this.Controls.Add(this.listView1);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginList";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockTopAutoHide;
            this.TabText = "Plugin List";
            this.Text = "Plugin List";
            this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeaderModuleId;
		private System.Windows.Forms.ColumnHeader columnHeaderModuleClass;
		private System.Windows.Forms.ColumnHeader columnHeaderDescription;
		private System.Windows.Forms.ColumnHeader columnHeaderVersion;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
    }
}