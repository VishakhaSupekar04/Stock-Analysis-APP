using System;
using Entry = Microcharts.Entry;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace Midterm.Model
{
    public class GetChartData
    {
        Entry e;
        public void DisplayChart(int daycount,List<Entry> listEntry,int lable_index)
        {
            for (int i = 0; i < daycount; i++)
            {

                //capture the stock price in a temporary variable
                float price = float.Parse(GetData.dailyStockList[i].The2High);

                //print the ValueLable for chart just a few times 
                if (i % lable_index == 0)
                {
                    e = new Entry(price)
                    {
                        Color = SKColor.Parse("#3498db"),
                        Label = "",
                        ValueLabel = GetData.dailyStockList[i].The2High,
                    };
                }
                else
                {
                    e = new Entry(price)
                    {
                        Color = SKColor.Parse("#3498db"),
                    };
                }
                listEntry.Add(e);
            }

        }
    }
}
