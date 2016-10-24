using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VolvoWrench.Heatmap
{
    public partial class Heatmap : Form
    {
        private List<HeatPoint> HeatPoints = new List<HeatPoint>();

        public Heatmap(List<Point> Points, Image LevelOverView)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            // Create new memory bitmap the same size as the picture box
            // Initialize random number generator
            var rRand = new Random();
            // Loop variables
            int iX;
            int iY;
            byte iIntense;
            // Lets loop 500 times and create a random point each iteration
            foreach (var v in Points)
            {               
                // Pick random locations and intensity
                iIntense = (byte) rRand.Next(0, 120);
                // Add heat point to heat points list
                HeatPoints.Add(new HeatPoint(v.X, v.Y, iIntense));
            }
            // Call CreateIntensityMask, give it the memory bitmap, and use it's output to set the picture box image
            pictureBox1.Image = CreateIntensityMask(new Bitmap(pictureBox1.Image), HeatPoints);
        }

        private Bitmap CreateIntensityMask(Bitmap bSurface, List<HeatPoint> aHeatPoints)
        {
            // Create new graphics surface from memory bitmap
            var DrawSurface = Graphics.FromImage(bSurface);
            // Set background color to white so that pixels can be correctly colorized
            // Traverse heat point data and draw masks for each heat point
            foreach (var DataPoint in aHeatPoints)
            {
                // Render current heat point on draw surface
                DrawHeatPoint(DrawSurface, DataPoint, 25);
            }
            return bSurface;
        }

        private void DrawHeatPoint(Graphics Canvas, HeatPoint HeatPoint, int Radius)
        {
            // Create points generic list of points to hold circumference points
            var CircumferencePointsList = new List<Point>();
            // Create an empty point to predefine the point struct used in the circumference loop
            Point CircumferencePoint;
            // Create an empty array that will be populated with points from the generic list
            Point[] CircumferencePointsArray;
            // Calculate ratio to scale byte intensity range from 0-255 to 0-1
            var fRatio = 1F/byte.MaxValue;
            // Precalulate half of byte max value
            byte bHalf = byte.MaxValue/2;
            // Flip intensity on it's center value from low-high to high-low
            int iIntensity = (byte) (HeatPoint.Intensity - ((HeatPoint.Intensity - bHalf)*2));
            // Store scaled and flipped intensity value for use with gradient center location
            var fIntensity = iIntensity*fRatio;
            // Loop through all angles of a circle
            // Define loop variable as a double to prevent casting in each iteration
            // Iterate through loop on 10 degree deltas, this can change to improve performance
            for (double i = 0; i <= 360; i += 10)
            {
                // Replace last iteration point with new empty point struct
                CircumferencePoint = new Point();
                // Plot new point on the circumference of a circle of the defined radius
                // Using the point coordinates, radius, and angle
                // Calculate the position of this iterations point on the circle
                CircumferencePoint.X = Convert.ToInt32(HeatPoint.X + Radius*Math.Cos(ConvertDegreesToRadians(i)));
                CircumferencePoint.Y = Convert.ToInt32(HeatPoint.Y + Radius*Math.Sin(ConvertDegreesToRadians(i)));
                // Add newly plotted circumference point to generic point list
                CircumferencePointsList.Add(CircumferencePoint);
            }
            // Populate empty points system array from generic points array list
            // Do this to satisfy the datatype of the PathGradientBrush and FillPolygon methods
            CircumferencePointsArray = CircumferencePointsList.ToArray();
            // Create new PathGradientBrush to create a radial gradient using the circumference points
            var GradientShaper = new PathGradientBrush(CircumferencePointsArray);
            // Create new color blend to tell the PathGradientBrush what colors to use and where to put them
            var GradientSpecifications = new ColorBlend(3);
            // Define positions of gradient colors, use intesity to adjust the middle color to
            // show more mask or less mask
            GradientSpecifications.Positions = new float[3] {0, fIntensity, 1};
            // Define gradient colors and their alpha values, adjust alpha of gradient colors to match intensity
            GradientSpecifications.Colors = new Color[3]
            {
                Color.FromArgb(0, Color.Blue),
                Color.FromArgb(HeatPoint.Intensity, Color.Yellow),
                Color.FromArgb(HeatPoint.Intensity, Color.Red)
            };
            // Pass off color blend to PathGradientBrush to instruct it how to generate the gradient
            GradientShaper.InterpolationColors = GradientSpecifications;
            // Draw polygon (circle) using our point array and gradient brush
            Canvas.FillPolygon(GradientShaper, CircumferencePointsArray);
        }

        private double ConvertDegreesToRadians(double degrees)
        {
            var radians = (Math.PI/180)*degrees;
            return (radians);
        }

        public struct HeatPoint
        {
            public byte Intensity;
            public int X;
            public int Y;

            public HeatPoint(int iX, int iY, byte bIntensity)
            {
                X = iX;
                Y = iY;
                Intensity = bIntensity;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = CreateIntensityMask(new Bitmap(pictureBox1.Image), new List<HeatPoint>() { new HeatPoint(e.X,e.Y,120)});
        }
    }
}