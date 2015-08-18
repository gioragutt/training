using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Navigation;
//using Parsing_html.Resources;
using System.Net.Http;
using System.Text;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace StocksTrain
{
    class StockData
    {
        public string Symbol
        {
            get;
            set;
        }
        public string Name
        {
            get; set;
        }
        public double Last
        {
            get; set;
        }
        public double Change
        {
            get; set;
        }
        public double Percent
        {
            get; set;
        }
        public double High
        {
            get; set;
        }
        public double Low
        {
            get; set;
        }
        public int Volume
        {
            get; set;
        }
        public string Time
        {
            get; set;
        }
    }

    class BarchartParser
    {
        static string BARCHART_WEBSITE   = "http://www.barchart.com/stocks/percentadvance.php";
        static string BARCHART_ENCODING  = "utf-8";
        static string UNCHANGED_SYMBOL   = "unch";

        public List<StockData> Stocks { get; set; }

        public BarchartParser()
        {
            // Initialize list
            Stocks = GetStocksData();
        }

        public void UpdateBarchartData()
        {
            Stocks = GetStocksData();
        }

        private List<StockData> GetStocksData()
        {
            List<StockData> stocksList = new List<StockData>();
            try
            {
                #region Get Html Document

                // Get response from site
                HttpClient http = new HttpClient();
                var response = http.GetByteArrayAsync(BARCHART_WEBSITE).Result;

                // Encode html response to UTF-8
                string source = Encoding.GetEncoding(BARCHART_ENCODING)
                                            .GetString(response, 0, response.Length - 1);

                // Get html
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(source);

                #endregion

                #region Get Data From Table

                // Get table containining stock info
                HtmlNode table = document.DocumentNode.Descendants()
                    .Single<HtmlNode>
                (
                    x => (x.Name == "table") &&
                         (x.Attributes["class"] != null) &&
                         (x.Attributes["class"].Value.Equals("datatable ajax")) &&
                         (x.Attributes["id"].Value.Equals("dt1"))
                );

                // Get 'tbody' element from table
                HtmlNode tbody = table.Descendants("tbody").FirstOrDefault();

                // Get all rows from the table
                List<HtmlNode> allStocks = tbody.Descendants("tr").ToList();

                // For each row, id is "td1_X" where X is the symbol of the stock
                foreach (HtmlNode row in allStocks)
                {
                    stocksList.Add(getDataFromRow(row));
                }

                #endregion
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                stocksList = new List<StockData>();
            }

            return stocksList;
        }

        private StockData getDataFromRow(HtmlNode row)
        {
            List<HtmlNode> columns = row.Descendants("td").ToList();
            string symbol = columns[0].Element("a").InnerText;
            string name = columns[1].Element("a").InnerText;
            double last = double.Parse(columns[2].InnerText);
            string changeString = columns[3].Element("span").InnerText;
            string percentString = columns[4].Element("span").InnerText;
            percentString = percentString.Substring(0, percentString.Length - 1);
            double change = 0;
            double percent = 0;
            if (changeString != UNCHANGED_SYMBOL)
            {
                change = double.Parse(changeString);
                percent = double.Parse(percentString);
            }
            double high = double.Parse(columns[5].InnerText);
            double low = double.Parse(columns[6].InnerText);
            int volume = int.Parse(columns[7].InnerText, System.Globalization.NumberStyles.AllowThousands);
            string time = columns[8].InnerText;
            StockData stock = new StockData()
            {
                Symbol = symbol,
                Name = name,
                Last = last,
                Change = change,
                Percent = percent,
                High = high,
                Low = low,
                Volume = volume,
                Time = time
            };

            return stock;
        }
    }
}