/*
Project: WareHouseLocator 
Description: To get Serial Data from IOT devices and push the data through SQL Server and get data from SQL Server
Release : V1
Author: Shivaprakash Pasupathibalan
@ 15 April 2020
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace WarehouseLocator
{

    class Program
    {
        public static string SERVER = "",PASSWORD = "",USER_ID = "",GET_LOCATION = "",UP_LOCATION = "",CATALOG =""; 
        static void Main(string[] args)
        {
            DBConfigXMLRead();
            Error_log(SERVER);
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
        private static void DBConfigXMLRead()                                              //Method to Get DB Details From config.xml file
        {
            try                                                                      
            {
                XmlDocument Config = new XmlDocument();
                Config.Load("config.xml");
                XmlNodeList DBConfig = Config.GetElementsByTagName("CONFIG_DB");
                SERVER = DBConfig[0].SelectSingleNode("SERVER").InnerText.ToString();
                USER_ID = DBConfig[0].SelectSingleNode("USER_ID").InnerText.ToString();
                PASSWORD = DBConfig[0].SelectSingleNode("PASSWORD").InnerText.ToString();
                CATALOG = DBConfig[0].SelectSingleNode("CATALOG").InnerText.ToString();
                GET_LOCATION = DBConfig[0].SelectSingleNode("SP_LOCATIONS").InnerText.ToString();
                UP_LOCATION = DBConfig[0].SelectSingleNode("SP_UPDATE").InnerText.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Reading Config" + ex.Message);
                Thread.Sleep(1000);
            }
        }
        private static void Error_log(string msg)
        {
            try
            {
                StreamWriter Error = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Errorlog.txt",true);
                Error.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t\t"+msg);
                Error.Flush();
                Error.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in error log" + ex.Message);
                Thread.Sleep(1000);
            }
        }
    }
}
