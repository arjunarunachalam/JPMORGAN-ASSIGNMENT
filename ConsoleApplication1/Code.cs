using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPMorgan.Test
{
    public class Code
    {
        public static Dictionary<string, StockValue> dcStock=null;
        public static Dictionary<DateTime, TradeDetails> dcTrade= new Dictionary<DateTime, TradeDetails>();
        /// <summary>
        /// Author: Arjun Arunachalam
        /// Description: This function inserts the value from the sample table
        /// </summary>
        public static void insertValues()
        {
            try
            {
                dcStock = new Dictionary<string, StockValue>();
                Console.WriteLine("Values are being inserted to the Dictionary.");
                dcStock.Add("TEA", new StockValue("TEA", "Common", 0, null, 100));
                dcStock.Add("POP", new StockValue("POP", "Common", 8, null, 100));
                dcStock.Add("ALE", new StockValue("ALE", "Common", 23, null, 60));
                dcStock.Add("GIN", new StockValue("GIN", "Preferred", 8, 2, 100));
                dcStock.Add("JOE", new StockValue("TEA", "Common", 13, null, 250));
                Console.WriteLine("Values have been inserted from the table.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in function insertvalues,exception is: " + ex.Message);
            }

        }
        /// <summary>
        /// Author: Arjun Arunachalam
        /// Description: This function performs the operation to calculate the Dividend Yield and PE Ratio
        /// </summary>
        /// <param name="price"> the price value based on which the calculation is performed</param>
        /// <param name="stockName">the stock symbol which is required to get the values to perform calculation</param>
        /// <param name="intreturnValue"> the value to be returned which can be either the dividend yield or PE ratio</param>
        /// <param name="intValueToGet"> which specifies the value to returned</param>
        public static void CalculateDividendAndPERatio(int price, string stockName, out double intreturnValue,int intValueToGet)
        {
            intreturnValue = -1;
            try
            {
                
                if (dcStock.ContainsKey(stockName))
                {
                    StockValue objStockDetails = new StockValue();
                    dcStock.TryGetValue(stockName, out objStockDetails);
                    if (objStockDetails != null)
                    {
                        if (price > 0 && objStockDetails.intLastDividend>0)
                        {
                            if (intValueToGet == 0)
                            {
                                if (objStockDetails.strType != "Preferred")
                                {
                                    intreturnValue = (objStockDetails.intLastDividend / (double)price);
                                }
                                else
                                {
                                    int intfixDividend = 0;
                                    if (objStockDetails.intFixedDividend != null)
                                    {
                                        intfixDividend = (int)objStockDetails.intFixedDividend;
                                    }

                                    intreturnValue = (intfixDividend * objStockDetails.intParValue) / price;

                                }
                            }
                            else
                            {
                                intreturnValue = (price / (double)objStockDetails.intLastDividend);
                            }

                            //double intDividend = intreturnValue * price;
                            //intPERatio = 0;
                            //if (intDividend > 0)
                            //{
                            //    intPERatio = price / intDividend;
                            //}

                        }
                        else
                        {
                            intreturnValue = 0;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in function CalculateDividendAndPERatio,exception is: " + ex.Message);
            }
        }

        /// <summary>
        /// Author: Arjun Arunachalam
        /// Description: This function performs the operation of recording the trade
        /// </summary>
        /// <param name="strStockSymbol"> this specifies the stock which is being traded</param>
        /// <param name="intQuantity">the number of stocks being traded</param>
        /// <param name="intPrice"> the price of each individual stock</param>
        /// <param name="intIndicator">indicate whether buying or selling- where 0 is buying and any other value greater than zero is selling</param>

        public static void RecordTrade(string strStockSymbol,int intQuantity,int intPrice,int intIndicator)
        {
            try
            {
                
                Console.WriteLine("Values are being inserted to the Trade Details Dictionary.");
                dcTrade.Add(DateTime.Now, new TradeDetails(strStockSymbol,DateTime.Now,intQuantity, intIndicator, intPrice));
                Console.WriteLine("trade recorded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in function RecordTrade,exception is: " + ex.Message);
            }
        }

        /// <summary>
        /// Author: Arjun Arunachalam
        /// Description: This function performs the operation of calculating volume weighted stock price
        /// </summary>
        /// <param name="intVolumeWeighted">the return value which contains the result</param>
        public static void volumeWeightedStockPrice( out double intVolumeWeighted)
        {
            intVolumeWeighted = -1;
            try
            {
                DateTime dtNow = DateTime.Now;
                DateTime dtUpdated = dtNow.Add(new TimeSpan(0,-15, 0));
                int intQuantity = 0;
                int intPrice = 0;
                foreach (KeyValuePair < DateTime, TradeDetails > entry in dcTrade)
                {
                    if (dtUpdated<= entry.Key && entry.Key <= dtNow)
                    {
                        TradeDetails dtTrade = entry.Value;
                        intQuantity += dtTrade.intQuantity;
                        intPrice += (dtTrade.intPrice* dtTrade.intQuantity);
                    }
                }
                
                if (intQuantity>0 && intPrice>0)
                {
                    intVolumeWeighted = intPrice / (double)intQuantity;
                }
                else
                {
                    intVolumeWeighted = 0;
                }

              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in function volumeWeightedStockPrice,exception is: " + ex.Message);
            }
        }
        /// <summary>
        /// Author: Arjun Arunachalam
        /// Description: This function performs the operation of calculating gbce All share index
        /// </summary>
        /// <param name="gbceIndex"> the return value which contains the calculated result</param>
        public static void gbceAllShareIndex(out double gbceIndex)

        {
            gbceIndex = -1;
            try
            {
                double dbPrice = 0;
                foreach (KeyValuePair<DateTime, TradeDetails> entry in dcTrade)
                {
                   
                        TradeDetails dtTrade = entry.Value;
                        double logValue = Math.Log(dtTrade.intPrice, 2);
                        dbPrice +=logValue;
                   
                }

                if (dbPrice>0)
                { 
                dbPrice *= 1.0 / (double)dcTrade.Count;
                gbceIndex= Math.Pow(2.0, dbPrice);
                }
                else
                {
                    gbceIndex = 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in function volumeWeightedStockPrice,exception is: " + ex.Message);
            }
        }


    }

   
   
    public class StockValue
    {
        public string strStockSymbol { get; set; }
        public string strType { get; set; }
        public int intLastDividend { get; set; }
        public int? intFixedDividend { get; set; }
        public int intParValue { get; set; }
        public StockValue()
        { }
        public StockValue(string strStockSymbol, string strType, int intLastDividend, int? intFixedDividend, int intParValue)
        {
            this.strStockSymbol = strStockSymbol;
            this.strType = strType;
            this.intLastDividend = intLastDividend;
            this.intFixedDividend = intFixedDividend;
            this.intParValue = intParValue;

        }


    }


    public class TradeDetails
    {
        public string strStockSymbol { get; set; }
        public DateTime dtTime { get; set; }
        public int intQuantity{ get; set; }
        public int intBuyOrSell { get; set; }
        public int intPrice { get; set; }
        public TradeDetails()
        { }
        public TradeDetails(string strStockSymbol, DateTime dtTime, int intQuantity, int intBuyOrSell, int intPrice)
        {
            this.strStockSymbol = strStockSymbol;
            this.dtTime = dtTime;
            this.intQuantity = intQuantity;
            this.intBuyOrSell = intBuyOrSell;
            this.intPrice = intPrice;

        }


    }
}
