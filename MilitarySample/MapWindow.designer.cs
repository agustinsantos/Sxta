namespace Sxta.Rti1516.MilitarySample
{
    partial class MapWindow
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
            this.components = new System.ComponentModel.Container();
            SharpMap.Map map1 = new SharpMap.Map();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapWindow));
            System.Drawing.Drawing2D.Matrix matrix1 = new System.Drawing.Drawing2D.Matrix();
            this.MainMapImage = new SharpMap.Forms.MapImage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.MainMapImage)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMapImage
            // 
            this.MainMapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.None;
            this.MainMapImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainMapImage.BackColor = System.Drawing.SystemColors.Info;
            this.MainMapImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MainMapImage.Location = new System.Drawing.Point(0, 0);
            map1.BackColor = System.Drawing.Color.Transparent;
            map1.Center = ((SharpMap.Geometries.Point)(resources.GetObject("map1.Center")));
            map1.MapTransform = matrix1;
            map1.MaximumZoom = 1.7976931348623157E+308;
            map1.MinimumZoom = 0;
            map1.PixelAspectRatio = 1;
            map1.Size = new System.Drawing.Size(100, 50);
            map1.Zoom = 1;
            this.MainMapImage.Map = map1;
            this.MainMapImage.Name = "MainMapImage";
            this.MainMapImage.QueryLayerIndex = 0;
            this.MainMapImage.Size = new System.Drawing.Size(516, 426);
            this.MainMapImage.TabIndex = 0;
            this.MainMapImage.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 26);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.MainMapImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 426);
            this.panel1.TabIndex = 1;
            // 
            // MapWindow
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(516, 430);
            this.Controls.Add(this.panel1);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapWindow";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide;
            this.TabText = "Map";
            this.Text = "Map";
            ((System.ComponentModel.ISupportInitialize)(this.MainMapImage)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public SharpMap.Forms.MapImage MainMapImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}