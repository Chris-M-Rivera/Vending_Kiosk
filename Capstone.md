# Module 1 Capstone - Catering System Software

You’ve been asked to develop an application for a Catering Vendor, Weyland Corporation. It will keep track of catering items, as well as the money used to make transactions. 



## Application Requirements


1. The catering system needs to track beverages, entrees, appetizers, and desserts.

    - Each catering item has a Name and Price.

2. A main menu should display when the software is run presenting the following options:

        
        (1) Display Catering Items
        (2) Order
        (3) Quit
        
3. Catering inventory is stocked via an input file. (You will be given a file to test with, but DO NOT hardcode values specific to the file). 
4. The catering system is automatically restocked each time the application runs. (ONLY when the program starts up) The only way to restock the machine is to re-run the application.
5. When the customer selects ​(1) you need to Display Catering Items and its quantity remaining. Then reshow the main menu so the user can make a new choice. (The following is a SAMPLE. Your items will come from your inventory file)


        Product Code     Description        Qty      Price
         A1              Pretzel Bites      25       $4.25
         A2              Chicken Wingdings  25       $3.25
         B1              Pickle Juice Wine  25       $3.45
         B2              Alum Creek Lager   25       $1.30
         B3              Java Green Tea     25       $2.55
         B4              Java Blue Beer     25       $3.10
         D1              Mushroom Cake      25       $2.30
         D2              Donuts             25       $1.25
         E1              Chicken Sammich    25       $12.10
         E2              Buzzard Burgers    25       $12.75


    - Each catering product has a product code identifier and a purchase price.
    - Each item in the catering system has enough room for 25 of that product.
    - Every product is initially stocked to the maximum amount.
    - A product which has run out should indicate it is SOLD OUT.  Not '0'.
    - The list of catering items displayed must be formatted so columns align, and all items are listed in `alphabetical order by product code` (eg. A1, A2, A3, A4, B1, B2, etc.).  See SAMPLE above (The file contains different items)

6. When the customer selects (2) Order they are guided through the purchasing process via a sub-menu:

        
        (1) Add Money
        (2) Select Products
        (3) Complete Transaction
        Current Account Balance: $0.00


7. The purchase process flow is as follows
  - Selecting (1) Add Money ​
    - A customer can repeatedly feed money into the machine in 'whole dollar amounts up to $500' (e.g. $1, $5, $10, $20, $50, $100, etc.). The customer should be informed if they try to enter an invalid bill amount. Valid bill amounts are ones, fives, tens, twenties, fifties and one hundreds. 
    - Customers should be able to add money multiple times throughout the program usage so they can replenish their balance. However at no time shall they be allowed to go above $1500.
    - The Current Account Balance indicates how much money the customer has available in their account to make purchases.
    - Any time money is entered using the add money option, the updated current balance should be displayed showing the new balance. 
  - Selecting ​(2) Select Products ​allows the customer to select a product and quantity to add to their cart.
      - Selecting 2 must redisplay a list of products, current quantities available and price (same as Display Catering Items on the main menu) so that the user can see a list of products from which they select from.
      - If the product code a user enters does not exist, the customer is informed that the product code they selected does not exist and the customer is returned to the Purchase menu (sub-menu).
    - If a product is sold out, the customer is informed and returned to the Purchase menu (sub menu).
    - If not enough of the product is in stock for the quantity the customer requested then they should be informed there is 'insufficient stock'.  
    - If a valid product is selected, and there is enough money available, then it can be added to the customer cart.
    - After the product is added to the cart, the customer should returned to the Purchase menu(sub-menu) and the balance on the Purchase menu (sub-menu)should be updated accordingly. 
    - There is no 'remove from cart' option needed for this project (maybe in the future). Once a product has been added there is no way to remove it.
  - ceSelecting ​(3) Complete Transaction​ allows the customer to complete the transaction and reive an on screen receipt of the products they just ordered and any remaining change back. The report will be displayed in the console as described below.
      - The customer’s money is returned using nickels, dimes, quarters, ones, fives, tens, twenties, fifties, (no 100's). (Important: return change using the smallest amount of bills and coins possible) and only in the denominations listed. This should be displayed on the screen. You can format it how you choose as long as you show the breakdown of change as described.
    - The current balance on the Purchase menu (sub-menu) should be updated to $0 remaining after completing the transaction so it is ready for the next customer.
    - The screen report should show the number of items ordered, the amount of each item, the total cost for those items, a message based on the item type that recommends commonly missed items to customers, and the total amount for the order.
      * The reminder message will be different for each item type.
      
        * Beverages should recommend "Don't forget Ice."
        * Entrees should recommend "Did you remember Dessert?"
        * Desserts should recommend "Coffee goes with dessert."
        * Appetizers should recommend "You might need extra plates."

      * You can format the report as you like, BUT it must appear in columns with the data lined up, currency to two decimal places, and the use of the dollar sign.

    **<span style="text-decoration:underline;">EXAMPLE ON SCREEN REPORT</span>**



            10 	Beverage   Cola	              $1.50 	 $15.00	 Don't Forget Ice.
             2	Beverage   White wine	      $3.25		  $6.50	 Don't Forget Ice.
            12	Entree	   Chicken	          $8.50		$102.00	 Did you remember Dessert?
            12	Dessert	   Cake		          $2.25		 $27.00	 Coffee goes with dessert.

            Total: $150.50

            You received (1) Twenties, (1) Tens, (2) Quarters in change



    - After the transaction is completed, the customer should be returned to the MAIN menu and THE APPLICATION SHOULD CONTINUE TO RUN UNTIL THE USER SELECTS TO QUIT THE PROGRAM. If they return to the main menu, and list items, they should see that the previous transaction has reduced the appropriate item counts.  


  1. All purchases must be audited to track orders and amounts in the catering system
      - Each purchase should generate a line in a file called ​Log.txt
      - The Log.txt file should persist (appended to, not overwritten) when the program starts.
      - The audit entry should include the date, time, action taken, and new customer balance 
      - Actions Taken may be:
        - ADD MONEY
        - GIVE CHANGE
        - NUMBER_ORDERED  PRODUCT_NAME  PRODUCT_CODE
      - The audit entries should be in the format:
```
        01/01/2019 12:00:00 PM ADD MONEY: $500.00 $500.00
        01/01/2019 12:00:15 PM ADD MONEY: $250.00 $750.00
        01/01/2019 12:00:20 PM 15 Chicken E4 $112.50 $637.50
        01/01/2019 12:01:25 PM 9 Red Wine B2 $29.25 $608.25
        01/01/2019 12:01:35 PM GIVE CHANGE: $608.25 $0.00
```

## Files

Please place all data files (inventory, log, and optional sales report) in the directory C:\Catering.

## Unit Tests

Unit tests are required to demonstrate that the catering system works as expected. The Menu does not need to be unit tested. Hint: You should, however, have unit tests that test inventory levels, catering system balances, and any public method that takes a clear input and can return an expected result. etc. DO NOT wait to create unit tests until the end! 


## Catering System Data File

The input file that stocks the catering system  products is a pipe (|) delimited file. Each line is a

separate product in the file and follows the below format.  

**<span style="text-decoration:underline;">Columns:</span>**

```
Type|Code|Name|Price
```
*   **Product Type:** The type of the item:  B (beverage), E (entree), (A) appetizer, (D) dessert.
*   **Product Code:**  The product code used to identify the product when ordering.
*   **Product Name:** The display name of the catering system product.
*   **Price:** The purchase price for the product.


An example input file has been provided in your repository.  This input file IS AN EXAMPLE of what an inventory file will look like and while it should be used for development, it may not be the final file used with the system when released. You can assume that any file you receive will always have the same name, be in the same file location, and in the same format, but the actual product codes, product names, and prices may change. 


## Total System Sales Report (Bonus Requirement)

The total system sales report file is pipe-delimited for consistency and should be written to a file named TotalSales.rpt

Each line is a separate product with the total number and amount sold for the applicable product. 

At the end of the report is a blank line followed by **TOTAL SALES** $dollar amount indicating the gross sales of the catering system.

The Total System Sales Report should be persisted (saved) and not reset when the catering system is restarted.  

**<span style="text-decoration:underline;">Example Total System Sales Report</span>**


```
    Cola|150|$225
    Chicken|211|$1793.50
    Red Wine|18|$58.50
    Cake|73|$164.25

    **TOTAL SALES** $2077.00
```
