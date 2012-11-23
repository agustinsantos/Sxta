using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sxta.Rti1516.MilitarySample
{
    public partial class MapWindow : DockContent
    {
        public MapWindow()
        {
            InitializeComponent();
        }

        public void LoadAndDrawData()
        {
            this.MainMapImage.Map.BackColor = Color.LightBlue;
            this.MainMapImage.Map.Center = new SharpMap.Geometries.Point(-4, 40); //Set center of map
            this.MainMapImage.Map.Zoom = 12; //Set zoom level
            this.MainMapImage.Map.Size = new System.Drawing.Size(1000, 1000); //Set output size

            //Set up the countries layer
            SharpMap.Layers.VectorLayer layCountries = new SharpMap.Layers.VectorLayer("Countries");
            //Set the datasource to a shapefile in the App_data folder
            layCountries.DataSource = new SharpMap.Data.Providers.ShapeFile(@"Data\coastlines.shp");
            //Set fill-style to green
            layCountries.Style.Fill = new SolidBrush(Color.Black);
            //Set the polygons to have a black outline
            layCountries.Style.Outline = System.Drawing.Pens.Black;
            layCountries.Style.EnableOutline = true;
            layCountries.SRID = 4321;

            //Set up a political area layer
            SharpMap.Layers.VectorLayer layArea = new SharpMap.Layers.VectorLayer("Areas");
            //Set the datasource to a shapefile in the App_data folder
            layArea.DataSource = new SharpMap.Data.Providers.ShapeFile(@"Data\areas.shp");
            //Define a blue 1px wide pen
            layArea.Style.Line = new Pen(Color.Red, 10.4f);
            layArea.Style.Fill = new SolidBrush(Color.Beige);
            layArea.SRID = 4322;

            //Set up a road layer
            SharpMap.Layers.VectorLayer layRoads = new SharpMap.Layers.VectorLayer("Roads");
            //Set the datasource to a shapefile in the App_data folder
            layRoads.DataSource = new SharpMap.Data.Providers.ShapeFile(@"Data\roads.shp");
            //Define a blue 1px wide pen
            layRoads.Style.Line = new Pen(Color.Red, 2.0f);
            layRoads.MaxVisible = 15;
            layRoads.SRID = 4323;
            SharpMap.Rendering.Thematics.CustomTheme iTheme = new SharpMap.Rendering.Thematics.CustomTheme(GetRoadCustomStyle);
            SharpMap.Styles.VectorStyle defaultstyle = new SharpMap.Styles.VectorStyle(); //Create default renderstyle
            defaultstyle.Line = new Pen(Color.Red, 0.5f);
            iTheme.DefaultStyle = defaultstyle;
            layRoads.Theme = iTheme;

            SharpMap.Layers.VectorLayer layCities = new SharpMap.Layers.VectorLayer("Cities");
            //Set the datasource to a shapefile in the App_data folder
            layCities.DataSource = new SharpMap.Data.Providers.ShapeFile(@"Data\cities.shp");
            //Define a blue 1px wide pen
            layCities.Style.Symbol = new Bitmap(@"Data\city.png");
            layCities.Style.SymbolScale = 1.8f;
            layCities.MaxVisible = 10;
            layCities.SRID = 4324;

            //Set up a river layer
            SharpMap.Layers.VectorLayer layRivers = new SharpMap.Layers.VectorLayer("Rivers");
            //Set the datasource to a shapefile in the App_data folder
            layRivers.DataSource = new SharpMap.Data.Providers.ShapeFile(@"Data\rivers.shp");
            //Define a blue 1px wide pen
            layRivers.Style.Line = new Pen(Color.Blue, 0.4f);
            layRivers.MaxVisible = 7;
            layRivers.SRID = 4325;

            //Add the layers to the map object.
            //The order we add them in are the order they are drawn, so we add the rivers last to put them on top
            MainMapImage.Map.Layers.Add(layCountries);
            MainMapImage.Map.Layers.Add(layArea);
            MainMapImage.Map.Layers.Add(layRivers);
            MainMapImage.Map.Layers.Add(layRoads);
            MainMapImage.Map.Layers.Add(layCities);

            MainMapImage.Map.Center = new SharpMap.Geometries.Point(-4, 40); //Set center of map
            MainMapImage.Map.Zoom = 12; //Set zoom level
            MainMapImage.Map.Size = new System.Drawing.Size(1000, 1000); //Set output size
            MainMapImage.ActiveTool = SharpMap.Forms.MapImage.Tools.Pan;
            MainMapImage.WheelZoomMagnitude = 1.5;
            MainMapImage.Refresh();
        }

        private SharpMap.Styles.VectorStyle GetRoadCustomStyle(SharpMap.Data.FeatureDataRow row)
        {
            double zoom = MainMapImage.Map.Zoom;
            bool isPrimary = row["ROUTE_INTE"].ToString().Equals("14 (Primary Route)");
            if (zoom > 10)
            {
                if (isPrimary)
                {
                    SharpMap.Styles.VectorStyle style = new SharpMap.Styles.VectorStyle();
                    style.Line = new Pen(Color.DarkRed, 2.0f);
                    return style;
                }
                else
                {
                    SharpMap.Styles.VectorStyle style = new SharpMap.Styles.VectorStyle();
                    style.Line = new Pen(Color.Transparent, 0.1f);
                    return style;
                }
            }
            else
                if (isPrimary)
                {
                    SharpMap.Styles.VectorStyle style = new SharpMap.Styles.VectorStyle();
                    style.Line = new Pen(Color.DarkRed, 3.0f);
                    return style;
                }
                else
                    return null; //Return null which will render the default style
        }
    }
}