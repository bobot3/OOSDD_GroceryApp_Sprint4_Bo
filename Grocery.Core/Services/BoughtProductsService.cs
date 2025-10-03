
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository=groceryListItemsRepository;
            _groceryListRepository=groceryListRepository;
            _clientRepository=clientRepository;
            _productRepository=productRepository;
        }
        public List<BoughtProducts> Get(int? productId)
        {
            List<BoughtProducts> boughtProducts = new List<BoughtProducts>();
            var groceryListItems = _groceryListItemsRepository.GetAll();

            System.Diagnostics.Debug.WriteLine($"=== BoughtProductsService.Get({productId}) ===");
            System.Diagnostics.Debug.WriteLine($"Total GroceryListItems: {groceryListItems.Count}");

            if (productId != null)
            {
                groceryListItems = groceryListItems.Where(gli => gli.ProductId == productId).ToList();
                System.Diagnostics.Debug.WriteLine($"Filtered to ProductId {productId}: {groceryListItems.Count} items");
            }

            foreach (var item in groceryListItems)
            {
                System.Diagnostics.Debug.WriteLine($"Processing item {item.Id}: GroceryListId={item.GroceryListId}, ProductId={item.ProductId}");

                var groceryList = _groceryListRepository.Get(item.GroceryListId);
                if (groceryList == null)
                {
                    System.Diagnostics.Debug.WriteLine($"  GroceryList {item.GroceryListId} NOT FOUND");
                    continue;
                }
                System.Diagnostics.Debug.WriteLine($"  GroceryList found: {groceryList.Name}, ClientId={groceryList.ClientId}");

                var client = _clientRepository.Get(groceryList.ClientId);
                if (client == null)
                {
                    System.Diagnostics.Debug.WriteLine($"  Client {groceryList.ClientId} NOT FOUND");
                    continue;
                }
                System.Diagnostics.Debug.WriteLine($"  Client found: {client.Name}");

                var product = _productRepository.Get(item.ProductId);
                if (product == null)
                {
                    System.Diagnostics.Debug.WriteLine($"  Product {item.ProductId} NOT FOUND");
                    continue;
                }
                System.Diagnostics.Debug.WriteLine($"  Product found: {product.Name}");

                boughtProducts.Add(new BoughtProducts(client, groceryList, product));
                System.Diagnostics.Debug.WriteLine($"  ADDED to result");
            }

            System.Diagnostics.Debug.WriteLine($"Returning {boughtProducts.Count} items");
            return boughtProducts;
        }
    }
}
