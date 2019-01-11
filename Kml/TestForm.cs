using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;
using System.IO;


namespace KmlExtensionSample
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = ThinkGeoCloudMapsOverlay.GetZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-10777598, 3912998, -10776008, 3912026);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Add ThinkGeoCloudMapsOverlay as basemap
            ThinkGeoCloudMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudMapsOverlay();
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            KmlFeatureLayer kmlFeatureLayer = new KmlFeatureLayer("../../App_Data/ThinkGeoHeadquarters.kml");
            kmlFeatureLayer.StylingType = KmlStylingType.StandardStyling;
            // Set area style,line style,point style and text style.
            TextStyle textStyle = new TextStyle("name", (new GeoFont("Arial", 12)), new GeoSolidBrush(GeoColor.StandardColors.DarkOliveGreen));
            textStyle.HaloPen = new GeoPen(GeoColor.StandardColors.FloralWhite, 5);
            textStyle.SplineType = SplineType.ForceSplining;
            kmlFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillSolidBrush = new GeoSolidBrush(new GeoColor(50, GeoColor.SimpleColors.Orange));
            kmlFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen = new GeoPen(GeoColor.SimpleColors.Black);
            kmlFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.SimpleColors.Orange, 5));
            kmlFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.SymbolPen = new GeoPen(GeoColor.FromArgb(255, GeoColor.StandardColors.Green), 8);
            kmlFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
            kmlFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay overlay = new LayerOverlay();
            overlay.Layers.Add("KmlLayer", kmlFeatureLayer);
            winformsMap1.Overlays.Add("Overlay", overlay);

            winformsMap1.Refresh();
        }

        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            //Displays world coordinates.
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(pointShape.X, 4) + " Y:" + Math.Round(pointShape.Y, 4);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
