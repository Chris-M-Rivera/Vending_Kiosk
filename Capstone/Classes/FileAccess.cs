using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileAccess
    {
        // all files for this application should be in this directory
        // it must be created on your computer
        private string filePath = @"C:\Catering\cateringsystem.csv";

        private string fileOutput = @"C:\Catering\Log.txt";

        public List<CateringItem> GetItemsFromFile()
        {
        List<CateringItem> itemsFromTextFile = new List<CateringItem>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] split = line.Split("|");

                    CateringItem cateringItem = new CateringItem();

                    cateringItem.Type = split[0];
                    cateringItem.ProductCode = split[1];
                    cateringItem.Name = split[2];
                    cateringItem.Price = double.Parse(split[3]);

                    itemsFromTextFile.Add(cateringItem);
                }
            }
            return itemsFromTextFile;
        }

        // the following methods write all transactions to an audit file @"C:\Catering\Log.txt";
        public void AuditAddMoney(double money, double balance)
        {
            using (StreamWriter sw = new StreamWriter(fileOutput, true))
            {
                sw.WriteLine($"{DateTime.Now} ADD MONEY: {money:C} {balance:C}");
            }
        }
        
        public void AuditGiveChange(double change)
        {
            using (StreamWriter sw = new StreamWriter(fileOutput, true))
            {
                sw.WriteLine($"{DateTime.Now} GIVE CHANGE: {change:C} $0.00");
            }
        }        
        
        public void AuditItemsBought(int quantity, string item, string productCode, double priceOfItems, double balance)
        {
            using (StreamWriter sw = new StreamWriter(fileOutput, true))
            {
                sw.WriteLine($"{DateTime.Now} {quantity} {item} {productCode} {priceOfItems:C} {balance:C}");
            }
        }
    }
}
