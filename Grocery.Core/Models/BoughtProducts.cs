namespace Grocery.Core.Models
{
    public class BoughtProducts
    {
        public Product Product { get; set; }
        public Client Client { get; set; }
        public GroceryList GroceryList { get; set; }

        // Add these for safer binding
        public string ClientName => Client?.Name ?? "Onbekend";
        public string GroceryListName => GroceryList?.Name ?? "Onbekend";
        public string ProductName => Product?.Name ?? "Onbekend";

        public BoughtProducts() { }

        public BoughtProducts(Client client, GroceryList groceryList, Product product)
        {
            Client = client;
            GroceryList = groceryList;
            Product = product;
        }
    }
}