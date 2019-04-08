using System;
using System.Collections.Generic;
using Microcharts.Forms;
using SkiaSharp;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using Microcharts;
using Midterm.Model;
using XFGloss;

namespace Midterm
{
    public partial class Charts : ContentPage
    {
        void Handle_Appearing(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            GetChart();
        }

        public static List<Entry> listEntry1 = new List<Entry> { };
        public static List<Entry> listEntry2 = new List<Entry> { };

        public Charts()
        {
            InitializeComponent();

            // Manually construct a multi-color gradient at an angle of our choosing
            var bkgrndGradient = new Gradient()
            {
                Rotation = 150,
                Steps = new GradientStepCollection()
                {
                    new GradientStep(Color.LightBlue, 0),
                    new GradientStep(Color.SkyBlue, .25),
                    new GradientStep(Color.DeepSkyBlue, .5),
                    new GradientStep(Color.FromHex("#ccd9ff"), 1)
                }
            };

            ContentPageGloss.SetBackgroundGradient(this, bkgrndGradient);
        }

        private async void GetChart()
        {
            if (GetData.flag == true)
            {
                GetChartData getchart1 = new GetChartData();
                GetChartData getchart2 = new GetChartData();
                listEntry1.Clear();
                listEntry2.Clear();
                try
                {
                    getchart1.DisplayChart(30, listEntry1, 3);
                    Chart1.Chart = new LineChart
                    {
                        Entries = listEntry1,
                        LineMode = LineMode.Spline,
                        LineSize = 9,
                        PointMode = PointMode.None,
                        LabelTextSize = 30,
                    };

                    getchart1.DisplayChart(100, listEntry2, 6);
                    Chart2.Chart = new LineChart
                    {
                        Entries = listEntry2,
                        LineMode = LineMode.Spline,
                        LineSize = 9,
                        PointMode = PointMode.None,
                        LabelTextSize = 30,
                    };

                }
                catch (NullReferenceException)
                {
                    await DisplayAlert("Error", "Select a valid stock from the 'Stocks' tab", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Select a valid stock from the 'Stocks' tab", "Ok");
            }

        }
    }
  }

