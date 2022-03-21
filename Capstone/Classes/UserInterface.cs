using System;
using System.Collections.Generic;
using System.Text;


namespace Capstone.Classes
{
    public class UserInterface
    {
        private Catering catering = new Catering();

        public void RunInterface()
        {

            bool done = false;
            
            while (!done)
            {
                MainMenuText();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        DisplayUpdatedListOfItems();
                        break;
                    case "2":
                        SubMenu();
                        break;
                    case "3":
                        done = true;
                        break;
                    default:
                        PrintRed("Please enter a valid choice.");
                        break;
                }
            }
        }
        public void SubMenu()
        {
            bool done = false;

            while (!done)
            {
                SubMenuText();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddMoneyText();
                        break;
                    case "2":
                        DisplayUpdatedListOfItems();
                        AddToCartText();                        
                        break;
                    case "3":
                        DisplayReciept();
                        catering.EndOfTransaction();
                        done = true;
                        break;
                    default:
                        PrintRed("Please enter a valid choice.");
                        break;
                }
            }
        }


        public void MainMenuText()
        {
            Console.WriteLine();
            Console.WriteLine("(1) Display Catering Items");
            Console.WriteLine("(2) Order");
            Console.WriteLine("(3) Quit");
            Console.WriteLine();
        }

        public void SubMenuText()
        {
            Console.WriteLine();
            Console.WriteLine("(1) Add Money");
            Console.WriteLine("(2) Select Products");
            Console.WriteLine("(3) Complete Transaction");
            Console.WriteLine();
            Console.Write("Current Account Balance: ");
            PrintGreen($"{Math.Round(catering.ReturnCurrentBalance(), 2):C}");
            Console.WriteLine();
        }

        public void DisplayUpdatedListOfItems()
        {
            CateringItem[] productsAvailable;

            string productCode = "Product Code";
            string description = "Description";
            string quantity = "Qty";
            string price = "Price";

            Console.WriteLine();
            Console.WriteLine($"{productCode.PadRight(15, ' ')}{description.PadRight(25, ' ')}{quantity.PadRight(10, ' ')}{price.PadRight(15, ' ')}");

            productsAvailable = catering.GetItems();

            foreach (CateringItem item in productsAvailable)
            {
                string priceAsString = item.Price.ToString("C");

                if (item.Quantity < 1)
                {
                    string soldOut = "SOLD OUT";
                    Console.WriteLine($"{item.ProductCode.PadRight(15, ' ')}{item.Name.PadRight(25, ' ')}{soldOut.PadRight(10, ' ')}{priceAsString.PadRight(15, ' ')}");
                }
                else
                {
                    string quantityAsString = item.Quantity.ToString();
                    Console.WriteLine($"{item.ProductCode.PadRight(15, ' ')}{item.Name.PadRight(25, ' ')}{quantityAsString.PadRight(10, ' ')}{priceAsString.PadRight(15, ' ')}");
                }
            }
            Console.WriteLine();
        }

        public void AddMoneyText()
        {
            Console.Write("Please insert bill (valid bill amounts are 1, 5, 10, 20, 50, 100): ");
            double depositedBill = 0;
            try
            {
                double tempDepositedBill = double.Parse(Console.ReadLine());
                depositedBill = tempDepositedBill;
            }
            catch (FormatException ex)
            {
                PrintRed("Please enter a valid bill.");
                Console.WriteLine();
                return;
            }

            if (catering.ValidBillCheck(depositedBill))
            {
                bool successfulDeposit = catering.AddMoney(depositedBill);
                if (!successfulDeposit)
                {
                    PrintRed("Balance may not go over $1500.");
                    Console.WriteLine();
                }
            }
            else
            {
                PrintRed("Please enter a valid bill.");
                Console.WriteLine();
            }
        }

        public void AddToCartText()
        {
            Console.Write("Please enter the product code: ");
            string productCodeInput = Console.ReadLine().ToUpper();
            CateringItem chosenCateringItem = catering.ConvertCodeToItem(productCodeInput);
            bool exists = catering.DoesProductExist(productCodeInput);
            if (!exists)
            {
                PrintRed("Product does not exist.");
                return;
            }

            Console.Write("Please enter the quantity: ");
            int quantityOfProducts = 0;

            try
            {
                int tempQuantityOfProducts = int.Parse(Console.ReadLine());
                quantityOfProducts = tempQuantityOfProducts;
            }
            catch (FormatException ex)
            {
                PrintRed("Invalid quantity.");
                Console.WriteLine();
                return;
            }

            bool soldOut = catering.SoldOutChecker(chosenCateringItem);
            if (soldOut)
            {
                PrintRed("Item is SOLD OUT!");
                Console.WriteLine();
                return;
            }
            bool haveStock = catering.SufficientStock(chosenCateringItem, quantityOfProducts);
            if (!haveStock)
            {
                PrintRed("Insufficient stock.");
                Console.WriteLine();
                return;
            }
            bool sufficentFunds = catering.SufficientFundsCheck(chosenCateringItem, quantityOfProducts);
            if (!sufficentFunds)
            {
                PrintRed("Insufficient funds to add items.");
                Console.WriteLine();
                return;
            }
            catering.MoveItemsToCart(chosenCateringItem, quantityOfProducts);
        }

        public void DisplayReciept()
        {
            Console.WriteLine();
            string[] recieptInfo = catering.Receipt();
            for(int i = 0; i < recieptInfo.Length; i++)
            {
                Console.WriteLine(recieptInfo[i]);
            }
            Console.WriteLine();
            Console.WriteLine($"Total: {catering.AmountDue():C}");
            Console.WriteLine();
            Console.WriteLine(catering.ChangeToReturn());
        }

        internal void PrintGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        internal void PrintRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
