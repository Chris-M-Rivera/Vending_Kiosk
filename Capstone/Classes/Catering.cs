using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {

        public Catering()
        {
            ItemDirectory = Data.GetItemsFromFile();
        }

        public List<CateringItem> ItemDirectory = new List<CateringItem>();

        public List<CateringItem> ShoppingCart = new List<CateringItem>();

        public FileAccess Data = new FileAccess();

        private double Balance = 0;


        public CateringItem[] GetItems()
        {
            CateringItem[] result = new CateringItem[ItemDirectory.Count];

            ItemDirectory.Sort((x, y) => x.ProductCode.CompareTo(y.ProductCode));

            for (int i = 0; i < ItemDirectory.Count; i++)
            {
                result[i] = ItemDirectory[i];
            }
            return result;
        }

        public bool AddMoney(double money)
        {
            if (Balance + money > 1500)
            {
                return false;
            }
            else
            {
                Balance += money;
                Data.AuditAddMoney(money, Balance);
                return true;
            }
        }

        public double ReturnCurrentBalance()
        {
            return Balance;
        }


        public bool DoesProductExist(string productCode)
        {
            foreach (CateringItem item in ItemDirectory)
            {
                if (item.ProductCode == productCode)
                {
                    return true;
                }
            }
            return false;
        }

        public CateringItem ConvertCodeToItem(string productCode)
        {
            CateringItem result = new CateringItem();
            foreach (CateringItem item in ItemDirectory)
            {
                if (item.ProductCode == productCode)
                {
                    result.Name = item.Name;
                    result.Price = item.Price;
                    result.Type = item.Type;
                    result.ProductCode = item.ProductCode;
                    result.Quantity = item.Quantity;
                }
            }
            return result;
        }

        public bool SoldOutChecker(CateringItem wantedItem)
        {
            foreach (CateringItem item in ItemDirectory)
            {
                if (item.ProductCode == wantedItem.ProductCode)
                {
                    if (item.Quantity == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool SufficientStock(CateringItem wantedItem, int quantityOfProduct)
        {
            foreach (CateringItem item in ItemDirectory)
            {
                if (item.ProductCode == wantedItem.ProductCode)
                {
                    if (item.Quantity >= quantityOfProduct)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool SufficientFundsCheck(CateringItem wantedItem, int quantityOfProduct)
        {
            double wantedItemsValue = 0;

            foreach (CateringItem item in ItemDirectory)
            {
                if (item.ProductCode == wantedItem.ProductCode)
                {
                    wantedItemsValue = (item.Price * quantityOfProduct);
                }
            }

            if ((wantedItemsValue) < Balance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsItemInShoppingCart(CateringItem wantedItem)
        {
            bool result = false;
            foreach (CateringItem item in ShoppingCart)
            {
                if (item.ProductCode == wantedItem.ProductCode)
                {
                    result = true;
                }
            }
            return result;
        }

        public void MoveItemsToCart(CateringItem wantedItem, int quantityOfProduct)
        {
            if (IsItemInShoppingCart(wantedItem))
            {
                foreach (CateringItem item in ShoppingCart)
                {
                    if (item.ProductCode == wantedItem.ProductCode)
                    {
                        item.Quantity += quantityOfProduct;
                    }
                }
            }
            else
            {
                wantedItem.Quantity = quantityOfProduct;
                ShoppingCart.Add(wantedItem);
            }
            foreach (CateringItem item in ItemDirectory)
            {
                if (item.ProductCode == wantedItem.ProductCode)
                {
                    item.Quantity -= quantityOfProduct;
                }
            }
            Balance -= (wantedItem.Price * quantityOfProduct);
            Data.AuditItemsBought(quantityOfProduct, wantedItem.Name, wantedItem.ProductCode, (wantedItem.Price * quantityOfProduct), Balance);
        }

        public string[] Receipt()
        {
            string[] result = new string[ShoppingCart.Count];

            for (int i = 0; i < ShoppingCart.Count; i++)
            {
                string itemType = "";
                string message = "";
                switch (ShoppingCart[i].Type)
                {
                    case "A":
                        itemType = "Appetizer";
                        message = "You might need extra plates.";
                        break;
                    case "B":
                        itemType = "Beverage";
                        message = "Don't forget ice.";
                        break;
                    case "D":
                        itemType = "Dessert";
                        message = "Coffee goes with dessert.";
                        break;
                    case "E":
                        itemType = "Entree";
                        message = "Did you remember dessert.";
                        break;
                }

                string quantity = ShoppingCart[i].Quantity.ToString();
                string price = ShoppingCart[i].Price.ToString("C");
                string totalCost = (ShoppingCart[i].Price * ShoppingCart[i].Quantity).ToString("C");

                result[i] = ($"{quantity.PadRight(5, ' ')}{itemType.PadRight(15, ' ')}{ShoppingCart[i].Name.PadRight(25, ' ')}{price.PadRight(8, ' ')}{totalCost.PadLeft(8, ' ')}  {message}");
            }
            return result;
        }

        public bool ValidBillCheck(double depositedBill)
        {
            if((depositedBill == 1) || (depositedBill == 5) || (depositedBill == 10) || (depositedBill == 20) || (depositedBill == 50) || (depositedBill == 100))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public double AmountDue()
        {
            double result = 0;
            foreach (CateringItem item in ShoppingCart)
            {
                result += (item.Price * item.Quantity);
            }
            return result;
        }

        public string ChangeToReturn()
        {
            int sumOfCart = 0;
            int runningTotal = sumOfCart + (int)(Balance * 100);
            string result = "You received";
            
            foreach (CateringItem item in ShoppingCart)
            {
                sumOfCart += (int)(Math.Round((item.Price * item.Quantity), 2) * 100);
            }

            Dictionary<string, int> change = new Dictionary<string, int>();

            change.Add("Hundreds",(runningTotal / 10000));
            runningTotal %= 10000;
            change.Add("Fifties", (runningTotal / 5000));
            runningTotal %= 5000;
            change.Add("Twenties", (runningTotal / 2000));
            runningTotal %= 2000;
            change.Add("Tens", (runningTotal / 1000));
            runningTotal %= 1000;
            change.Add("Fives", (runningTotal / 500));
            runningTotal %= 500;
            change.Add("Ones", (runningTotal / 100));
            runningTotal %= 100;
            change.Add("Quarters", (runningTotal / 25));
            runningTotal %= 25;
            change.Add("Dimes", (runningTotal / 10));
            runningTotal %= 10;
            change.Add("Nickels", (runningTotal / 5));

            Data.AuditGiveChange(runningTotal);

            foreach(KeyValuePair<string,int> kvp in change)
            {
                if (kvp.Value != 0)
                {
                    result += ($" ({kvp.Value}) {kvp.Key},");
                }
            }

            result = result.Substring(0, result.Length - 1) + " in change.";

            return result;
        }

        public void EndOfTransaction()
        {
            Balance = 0;
            ShoppingCart.Clear();
        }
    }
}
