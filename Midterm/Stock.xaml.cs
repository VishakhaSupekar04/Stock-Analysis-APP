using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using XFGloss;

namespace Midterm
{

    public partial class Stock : ContentPage
    {
        //public List<TimeDaily> stock;
        public Stock()
        {
            InitializeComponent();

            // Manually construct a multi-color gradient at an angle of our choosing
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

        private async void Handle_GetStockInfo(object sender, EventArgs e)
        {
            float max_value = float.MinValue; // max is 0.000
            float min_value = float.MaxValue; // min is Highest value of float
            GetData.flag = false;

            // for parsing the TimeSeriesData 
            List<ParsedDayData> dataList = new List<ParsedDayData>();

            //Enetered stock name 
            string selection = stockName.Text;
            string key = "L0DPQVDUEO0WGBV";

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://www.alphavantage.co/query?" +
            	"function=TIME_SERIES_DAILY&symbol="+selection+"&apikey="+key);

            var stock = TimeDaily.FromJson(response);
            //check for valid stock name. If invalid stock name then nofify user
            try
            {

                GetData.dailyStockList = stock.TimeSeriesDaily.Values.ToList();
                GetData.stockDate = stock.TimeSeriesDaily.Keys.ToList();

                // get data from the json into the list

                int count = GetData.dailyStockList.Count;
                ParsedDayData currentdate = new ParsedDayData();

                //loop through the list 
                for (int i = 0; i < count; i++)
                {
                    //Convert date to "Jan 22, 2019" format
                    DateTime dt = DateTime.Parse(GetData.stockDate[i]);
                    currentdate.Date = dt.ToString("MMM dd, yyyy");

                    //high_prc and low_prc are in float for checking with min_value and max_value 
                    float high_prc = float.Parse(GetData.dailyStockList[i].The2High);
                    float low_prc = float.Parse(GetData.dailyStockList[i].The3Low);

                    //converting high_prc and low_prc to string 
                    currentdate.High = "$" + high_prc.ToString();
                    currentdate.Low = "$" + low_prc.ToString();

                    float open_prc = float.Parse(GetData.dailyStockList[i].The4Close);
                    float close_prc = float.Parse(GetData.dailyStockList[i].The1Open);

                    currentdate.Close = "$" + open_prc.ToString();
                    currentdate.Open = "$" + close_prc;

                    /*Find the Highest and Lowest for the entered stock */
                    if (high_prc > max_value)
                        max_value = high_prc;
                    if (low_prc < min_value)
                        min_value = low_prc;
                    

                    dataList.Add(currentdate);
                    currentdate = new ParsedDayData();
                    GetData.flag = true;
                }

                /*Display the data on screen*/

                StockListView.ItemsSource = dataList;
                Highest.Text = "$"+max_value.ToString();
                Lowest.Text = "$" +min_value.ToString();

            }
            catch (NullReferenceException)
            {
                InitializeComponent();
                GetData.flag = false;
                await DisplayAlert(selection+":Invalid stock Name", "Please Enter valid stock Name","Ok");
            }
           
        }
    }
}