using System.Linq;
using System.Collections.Generic;

Dictionary<string, double> inventory = new Dictionary<string, double>()
{
    {"applesauce", 3.00 },
    {"sparkling water", 4.99 },
    {"coffee", 1.00 },
    {"chips", 4.50 },
    {"pasta", 6.00 },
    {"tomato sauce", 5.50},
    {"bread", 3.00 },
    {"butter", 1.50 }
};

bool runProgram = true;


while(runProgram == true)
{
    bool addingInventory = true;
    List<string> shoppingList = new List<string>();
    //List<double> prices = new List<double>();
    int i = 0;

    //display inventory
    foreach (KeyValuePair<string, double> kvp in inventory)
    {
        Console.WriteLine($"{i}: {kvp.Key}: ${kvp.Value}");
        i++;
    }

    //get user input on item
    while (addingInventory == true)
    {
        Console.WriteLine("\nEnter an item name: ");
        string userinput = Console.ReadLine();
        var testfornumber = int.TryParse(userinput, out int usernumber);

        // for when the item is in stock (by name)
        if (inventory.ContainsKey(userinput) == true)
        {
            shoppingList.Add(userinput);
            //prices.Add(inventory[userinput]);
            Console.WriteLine("Would you like to buy something else? y/n");
            addingInventory  = Console.ReadLine().ToLower().Trim() == "y" ? true : false;
        }
        // for when item is in stock (by number)
        else if (usernumber >= 0 && usernumber < i)
        {
            shoppingList.Add(inventory.ElementAt(usernumber).Key);
            //prices.Add(inventory.ElementAt(usernumber).Value);
            Console.WriteLine("Would you like to buy something else? y/n");
            addingInventory = Console.ReadLine().ToLower().Trim() == "y" ? true : false;
        }
        // for when item is not in stock
        else
        {
            Console.WriteLine("That item is not in the store. Please try again.");
        }
        
    }
    //
    List<string> uniqueItems = shoppingList.Distinct().ToList();
    Dictionary<string, double> itemcosts = new Dictionary<string, double>();
    double total = 0;

    foreach (string item in uniqueItems)
    {
        itemcosts.Add(item, shoppingList.Where(name => name == item).Count()*inventory[item]);
        total += itemcosts[item];
        Console.WriteLine($"{item}: ${itemcosts[item]}"); //original lab output line
    }
    var keyOfMaxValue = itemcosts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
    var keyOfMinValue = itemcosts.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;

    //Dictionary<string, double> sortedDict = (from entry in itemcosts orderby entry.Value ascending select entry);
    //foreach (KeyValuePair<string,double> kvp in sortedDict)
    //{
    //    Console.WriteLine($"{kvp.Key}: ${kvp.Value}");
    //}

    Console.WriteLine($"Your total is: ${total}");
    Console.WriteLine($"The maximum cost item is {keyOfMaxValue} at ${itemcosts[keyOfMaxValue]}");
    Console.WriteLine($"The maximum cost item is {keyOfMinValue} at ${itemcosts[keyOfMinValue]}");

    //option to end program
    Console.WriteLine("Enter 'y' to start a new list.");
    runProgram = Console.ReadLine().ToLower().Trim() == "y" ? true : false;
}