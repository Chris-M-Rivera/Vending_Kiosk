using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTest
    {

        Catering testObject;
        CateringItem[] testObjectArray = new CateringItem[4];

        [TestInitialize]

        // B |B3  |Beer   |2.55
        // A | A5 | Wings | 3.55
        // D | D1 | Ice Cream | 1.50
        // E | E5 | Steak | 12.55
        // Arrange 
        public void Setup()
        {
            testObject = new Catering();
            testObject.ItemDirectory = GetItemsFromFile();

            CateringItem one = new CateringItem();
            one.Name = "Wings";
            one.ProductCode = "A5";
            one.Price = 3.55;
            one.Type = "A";
            one.Quantity = 25;
            testObjectArray[0] = one;

            CateringItem two = new CateringItem();
            two.Name = "Beer";
            two.ProductCode = "B3";
            two.Price = 2.55;
            two.Type = "B";
            two.Quantity = 25;
            testObjectArray[1] = two;

            CateringItem three = new CateringItem();
            three.Name = "Ice Cream";
            three.ProductCode = "D1";
            three.Price = 1.50;
            three.Type = "D";
            three.Quantity = 25;
            testObjectArray[2] = three;

            CateringItem four = new CateringItem();
            four.Name = "Steak";
            four.ProductCode = "E5";
            four.Price = 12.55;
            four.Type = "E";
            four.Quantity = 25;
            testObjectArray[3] = four;
        }

        private string filePath = @"C:\Catering\TestCateringItems.csv";


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

        [TestMethod]

        public void CateringConstructorTest()
        {
            Assert.IsNotNull(testObject);
        }


        [TestMethod]

        public void GetItemsTest()
        {
            CateringItem[] result = testObject.GetItems();

            //CollectionAssert.AreEqual(testObjectArray, result);

            Assert.AreEqual(testObjectArray[0].ProductCode, result[0].ProductCode);
            Assert.AreEqual(testObjectArray[3].Name, result[3].Name);
            Assert.AreEqual(testObjectArray[0].Type, result[0].Type);
            Assert.AreEqual(testObjectArray[2].Quantity, result[2].Quantity);
            Assert.AreEqual(testObjectArray[1].Price, result[1].Price);
        }

        [TestMethod]

        public void AddMoneyTest()
        {
            bool result = testObject.AddMoney(1501);
            bool result2 = testObject.AddMoney(10);

            Assert.IsFalse(result);
            Assert.IsTrue(result2);
        }

        [TestMethod]

        public void ReturnCurrentBalanceTest()
        {
            double result = testObject.ReturnCurrentBalance();
            Assert.AreEqual(0, result);
        }

        [TestMethod]

        public void DoesProductExistTest()
        {
            bool result = testObject.DoesProductExist("D1");
            bool result2 = testObject.DoesProductExist("D6");

            Assert.IsTrue(result);
            Assert.IsFalse(result2);
        }

        [TestMethod]

        public void ConvertCodeToItemTest()
        {
            CateringItem result = testObject.ConvertCodeToItem("D1");
            Assert.AreEqual(testObjectArray[2].Name, result.Name);
        }

        [TestMethod]

        public void SoldOutCheckerTest()
        {
            bool result = testObject.SoldOutChecker(testObjectArray[2]);
           
            Assert.IsFalse(result);
        }

        [TestMethod]
        
        public void SufficicientStockTest()
        {
            bool result = testObject.SufficientStock(testObjectArray[2], 1);
            bool result2 = testObject.SufficientStock(testObjectArray[2], 26);

            Assert.IsTrue(result);
            Assert.IsFalse(result2);
        }
        
        [TestMethod]
        public void SufficicientFundsCheckTest()
        {
            bool result = testObject.SufficientFundsCheck(testObjectArray[2], 1);

            Assert.IsFalse(result);
        }

        [TestMethod]

        public void IsItemInShoppingCartTest()
        {
            bool result = testObject.IsItemInShoppingCart(testObjectArray[2]);

            Assert.IsFalse(result);
        }

        [TestMethod]

        public void MoveItemsToCartTest()
        {
            testObject.MoveItemsToCart(testObjectArray[2], 10);

            Assert.AreEqual(testObject.ShoppingCart[0].Name, testObjectArray[2].Name);
        }

        [TestMethod]

        public void ReceiptTest()
        {
            string[] result = testObject.Receipt();

            CollectionAssert.AreEqual(new string[] { }, result);
        }

        [TestMethod]

        public void AmountDueTest()
        {
            double result = testObject.AmountDue();

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ChangeToReturnTest()
        {
            string result = testObject.ChangeToReturn();

            Assert.AreEqual("You receive in change.", result);
        }
    }
}



