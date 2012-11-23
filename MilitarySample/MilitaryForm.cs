using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using WeifenLuo.WinFormsUI.Docking;

using GeoPoint = SharpMap.Geometries.Point;
using SharpMap.Forms;
using SharpMap.Geometries;
using SharpMap.Data.Providers;
using SharpMap.Layers;

namespace Sxta.Rti1516.MilitarySample
{
    public partial class MilitaryForm : Form
    {
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private ForceStructureExplorer forceExplorerModelExplorer;
        public MapWindow mapWindow;
        protected MilitaryScenario militaryScenario;

        public MilitaryForm(MilitaryScenario milScenario)
        {
            InitializeComponent();
            militaryScenario = milScenario;
            forceExplorerModelExplorer = new ForceStructureExplorer();
            mapWindow = new MapWindow();
            mapWindow.MainMapImage.MouseMove += new SharpMap.Forms.MapImage.MouseEventHandler(OnMouseMove);
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            //ShowMap();
        }

        void OnMouseMove(SharpMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            CoordinatesLabel.Text = String.Format("Coordinates: {0:N5}, {1:N5}", WorldPos.X, WorldPos.Y);
        }

        private void OnLoad(object sender, System.EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockMilitaryPanel.config");

            if (File.Exists(configFile))
                dockPanel1.LoadFromXml(configFile, m_deserializeDockContent);
        }

        private void OnClose(object sender, FormClosedEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockMilitaryPanel.config");
            if (m_bSaveLayout)
                dockPanel1.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ForceStructureExplorer).ToString())
                return forceExplorerModelExplorer;
            else if (persistString == typeof(MapWindow).ToString())
                return mapWindow;
            else
                return null;
        }

        private void OnShowForceExplorer(object sender, EventArgs e)
        {
            forceExplorerModelExplorer.Show(dockPanel1, DockState.DockRight);
            forceExplorerModelExplorer.ShowForceInformation(militaryScenario.ForceStructure);
        }

        private void ShowMap()
        {
            if (dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                mapWindow.MdiParent = this;
                mapWindow.Show();
            }
            else
                mapWindow.Show(dockPanel1, DockState.Document);

            mapWindow.LoadAndDrawData();
        }

        private void OnShowMap(object sender, EventArgs e)
        {
            ShowMap();
        }

        private void OnMapZoomIn(object sender, EventArgs e)
        {
            mapWindow.MainMapImage.ActiveTool = MapImage.Tools.ZoomIn;
        }

        private void OnMapZoomOut(object sender, EventArgs e)
        {
            mapWindow.MainMapImage.ActiveTool = MapImage.Tools.ZoomOut;
        }

        private void OnMapPan(object sender, EventArgs e)
        {
            mapWindow.MainMapImage.ActiveTool = MapImage.Tools.Pan;
        }

        private void AddItems(object sender, EventArgs e)
        {
            Random rndGen = new Random();
            Collection<Geometry> geometry = new Collection<Geometry>();

            VectorLayer layer = new VectorLayer(String.Empty);

            generatePoints(geometry, rndGen);
            layer.Style.Symbol = new Bitmap(@"Data\city.png"); ;
            layer.LayerName = "Planes";
            GeometryProvider provider = new GeometryProvider(geometry);
            layer.DataSource = provider;
            mapWindow.MainMapImage.Map.Layers.Add(layer);
        }

        private void generatePoints(Collection<Geometry> geometry, Random rndGen)
        {
            int numPoints = rndGen.Next(10, 100);
            for (int pointIndex = 0; pointIndex < numPoints; pointIndex++)
            {
                GeoPoint point = new GeoPoint(rndGen.NextDouble() * 1000, rndGen.NextDouble() * 1000);
                geometry.Add(point);
            }
        }

    }
}