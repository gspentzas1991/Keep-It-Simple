using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes.OrderModels;
using System.IO;
using System.Linq;
using Assets.Scripts.Classes;

public class MenuManagerScript : MonoBehaviour
{
    private Menu CompleteMenu = new Menu();
    const int MaxProductsInOrder = 3;
    const int MaxPreferencesPerProduct = 2;
    const int MaxQuantityOfProduct = 2;
    const int MaxQuantityOfPreference = 2;



    void Start()
    {
        //Reads the menu.json file
        string filePath = Path.Combine(Application.streamingAssetsPath, "menu.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            CompleteMenu = JsonUtility.FromJson<Menu>(dataAsJson);
        }
    }

    public List<OrderedProduct> GenerateOrderForTable()
    {
        List<OrderedProduct> orderedProductList = new List<OrderedProduct>();
        //Creates 1-3 random products from the menu and adds them to the tableOrder
        int productCount = Random.Range(1, MaxProductsInOrder+1);
        for (int i = 0;i < productCount; i++ )
        {
            OrderedProduct orderedProduct = new OrderedProduct();
            orderedProduct.Product = GetRandomProduct();
            orderedProductList.Add(orderedProduct);
        }
        //Randomly gets preferences for the ordered products
        foreach (OrderedProduct orderedProduct in orderedProductList)
        {
            if (orderedProduct.Product.PreferenceCategoriesIds.Count>0)
            {
                orderedProduct.Preferences = GetRandomPreferenceListForProduct(orderedProduct.Product);
            }
            else
            {
                orderedProduct.Preferences = new List<Preference>();
            }
        }
        return orderedProductList;
    }

    /// <summary>
    /// Returns a random product from the menu, with Quantity 1-MaxQuantityOfProduct
    /// </summary>
    private Product GetRandomProduct()
    {
        Product productToReturn = new Product();
        Product randomProductFromMenu = CompleteMenu.Products[Random.Range(0,CompleteMenu.Products.Count)];
        productToReturn.Id = randomProductFromMenu.Id;
        productToReturn.ProductCategoryId = randomProductFromMenu.ProductCategoryId;
        productToReturn.Name = randomProductFromMenu.Name;
        productToReturn.PreferenceCategoriesIds = randomProductFromMenu.PreferenceCategoriesIds;
        productToReturn.Price = randomProductFromMenu.Price;
        productToReturn.Quantity = Random.Range(1, MaxQuantityOfProduct+1);
        return productToReturn;
    }

    /// <summary>
    /// Returns a list of random preferences, available to the product with Quantity 1-MaxQuantityOfPreference
    /// </summary>
    private List<Preference> GetRandomPreferenceListForProduct(Product product)
    {
        List<Preference> preferenceListToReturn = new List<Preference>();
        int preferenceCountInProduct = Random.Range(0, MaxPreferencesPerProduct + 1);
        List<Preference> availablePreferencesForProduct = CompleteMenu.Preferences.Where(x => product.PreferenceCategoriesIds.Contains(x.PreferenceCategoryId)).ToList();
        //A list of the selected preferences ids on the product
        List<int> selectedPreferencesIds = new List<int>();
        for (int i = 0; i < preferenceCountInProduct; i++)
        {
            //Gets a list of all available preference Ids that have not been selected already
            var uniqueAvailablePreferenceIds = availablePreferencesForProduct.Select(x=>x.Id).Except(selectedPreferencesIds).ToList();
            //Gets a random preferenceId from the uniqueAvailablePreferenceIds list
            int randomPreferenceId = uniqueAvailablePreferenceIds[Random.Range(0, uniqueAvailablePreferenceIds.Count)];
            selectedPreferencesIds.Add(randomPreferenceId);
            Preference randomPreference = availablePreferencesForProduct.Where(x=>x.Id==randomPreferenceId).First();
            Preference preferenceToBeAdded = new Preference();
            preferenceToBeAdded.Id = randomPreference.Id;
            preferenceToBeAdded.Name = randomPreference.Name;
            preferenceToBeAdded.PreferenceCategoryId = randomPreference.PreferenceCategoryId;
            preferenceToBeAdded.Price = randomPreference.Price;
            preferenceToBeAdded.Quantity = Random.Range(1, MaxQuantityOfPreference);
            preferenceListToReturn.Add(preferenceToBeAdded);
        }
        return preferenceListToReturn;
    }
}
