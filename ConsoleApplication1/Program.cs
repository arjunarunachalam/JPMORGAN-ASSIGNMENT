using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JPMorgan.Test
{
    class Program
    {
        /// <summary>
        /// Author: Arjun Arunachalam
        /// Description: This function checks whether the user wants to do the specified operation.
        /// </summary>
        /// <param name="strMessage"> the operation which can be performed by the user</param>
        /// <returns> boolean variable whether the user wants to perform the operation</returns>
        public static bool checkYesOrNo(string strMessage)
        {
            Console.WriteLine(strMessage);
            string strReply = Console.ReadLine().ToUpper();
            if (strReply == "Y")
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// Author: Arjun Arunachalam
        /// Description: This function checks whether the value of the stock symbols is contained in the sample table
        /// </summary>
        /// <returns>string variable which is a value in the stock symbol specified</returns>
        public static string checkForStockSymbols()
        {
            int i = 0;
            string strStockSymbol = "";
            while (i == 0)
            {
                Console.WriteLine("Enter Stock Symbol within these 5 values 'TEA','POP','ALE','GIN','JOE':");
                strStockSymbol = Console.ReadLine().ToUpper();
                if (strStockSymbol == "TEA" || strStockSymbol == "POP" || strStockSymbol == "ALE" || strStockSymbol == "GIN" || strStockSymbol == "JOE")
                {
                    i = 1;
                }
                else
                {
                    Console.WriteLine("Please retry.");
                }
            }
            return strStockSymbol;
        }

        /// <summary>
        /// Author: Arjun Arunachalam
        /// Description: This function checks whether the user entered value is integer
        /// </summary>
        /// <param name="strMessage"> The message contains the parameter for which the integer value is required</param>
        /// <returns> integer variable which is mandatory</returns>
        public static int checkForInteger(string strMessage)
        {
            int i = 0;
            int intReturnValue = 0;
            while (i == 0)
            {
                Console.WriteLine(strMessage);

                if (int.TryParse(Console.ReadLine(), out intReturnValue))
                {
                    i = 1;
                }
                else
                {
                    Console.WriteLine("Please retry with integer values");
                }
            }
            return intReturnValue;
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Program Started.");
                Console.WriteLine("*********************************************************************");
                Console.WriteLine();
                JPMorgan.Test.Code.insertValues();
                int i = 0;
                while (i != 1)
                {
                    try
                    {
                        
                        Console.WriteLine("*********************************************************************");
                        Console.WriteLine();
                        if (checkYesOrNo("Do you want to calculate dividend yield or P/E ratio Type 'Y' for Yes or any other value for No:"))
                        {
                            string strStockSymbol = checkForStockSymbols();
                            int intPrice = checkForInteger("Enter price:");
                            int intValue = checkForInteger("Enter 0 for dividend Yield OR any value other than 0 for P/E Ratio:");
                            double intReturnValue = 0;
                            JPMorgan.Test.Code.CalculateDividendAndPERatio(intPrice, strStockSymbol, out intReturnValue, intValue);
                            if (intReturnValue < 0)
                            {
                                Console.WriteLine("There is an exception in the function please try again.");
                            }
                            else
                            {
                                if (intValue != 0)
                                {
                                    Console.WriteLine(" PERatio:" + intReturnValue);
                                }
                                else
                                {
                                    Console.WriteLine("Dividend Yield:" + intReturnValue);
                                }
                            }

                        }

                        Console.WriteLine("*********************************************************************");
                        Console.WriteLine();

                        int n = 0;
                        while (n == 0)
                        {
                            if (checkYesOrNo("Do you want to record a trade ? Type 'Y' for Yes or any other value for No:"))
                            {
                                string strStockSymbol = checkForStockSymbols();
                                int intQuantity = checkForInteger("Enter the Quantity of Shares");
                                int intPrice = checkForInteger("Enter price:");
                                int intSet = checkForInteger("Enter 0 for buying and 1 for selling:");
                                JPMorgan.Test.Code.RecordTrade(strStockSymbol, intQuantity, intPrice, intSet);
                               

                            }
                            else
                            {
                                n = 1;
                            }
                            Console.WriteLine("*********************************************************************");
                            Console.WriteLine();
                        }

                        Console.WriteLine("*********************************************************************");
                        Console.WriteLine();

                        if (checkYesOrNo("Do you want the value of volume weighted stock price ? Type 'Y' for Yes or any other value for No:"))
                        {
                            double intReturnValue = 0;
                            JPMorgan.Test.Code.volumeWeightedStockPrice(out intReturnValue);
                            if (intReturnValue < 0)
                            {
                                Console.WriteLine("There is an exception in the function please try again.");
                            }
                            else
                            {
                                Console.WriteLine("Volume Weighted Stock Price:" + intReturnValue);
                            }
                        }

                        Console.WriteLine("*********************************************************************");
                        Console.WriteLine();

                        if (checkYesOrNo("Do you want the value of Gbce All share index ? Type 'Y' for Yes or any other value for No:"))
                        {
                            double dbGbceIndex = 0;
                            JPMorgan.Test.Code.gbceAllShareIndex(out dbGbceIndex);
                            if (dbGbceIndex < 0)
                            {
                                Console.WriteLine("There is an exception in the function please try again.");
                            }
                            else
                            {
                                Console.WriteLine("Gbce All share index:" + dbGbceIndex);
                            }
                           
                        }

                        Console.WriteLine("*********************************************************************");
                        Console.WriteLine();
                        if (!checkYesOrNo("Do you want to retry the program"))
                        {
                            i = 1;
                        }

                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception Message" + ex.Message);
                        i = 0;
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Message" + ex.Message);


            }
        }
    }

}
