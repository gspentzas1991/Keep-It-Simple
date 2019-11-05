using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes.OrderModels;
using System.IO;

public class MenuManagerScript : MonoBehaviour
{
    private Menu CompleteMenu = new Menu();
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

    public List<Product> GenerateOrderForTable()
    {
        List<Product> tableOrder = new List<Product>();
        foreach (ProductCategory category in CompleteMenu.ProductCategories)
        {
            foreach (Product product in category.Products)
            {
                if (Random.Range(0, 2) == 1)
                {
                    Product productToAdd = new Product();
                    productToAdd.Id = product.Id;
                    productToAdd.Name = product.Name;
                    productToAdd.Price = product.Price;
                    productToAdd.Quantity = Random.Range(1, 3);
                    productToAdd.PreferenceCategories = new List<PreferenceCategory>();
                    foreach (PreferenceCategory preferenceCategory in product.PreferenceCategories)
                    {
                        PreferenceCategory preferenceCategoryToAdd = new PreferenceCategory();
                        preferenceCategoryToAdd.Name = preferenceCategory.Name;
                        preferenceCategoryToAdd.SelectionMode = preferenceCategory.SelectionMode;
                        preferenceCategoryToAdd.Preferences = new List<Preference>();
                        foreach (Preference preference in preferenceCategory.Preferences) {
                            if (Random.Range(0,2)==1)
                            {
                                Preference preferenceToAdd = new Preference();
                                preferenceToAdd.Id = preference.Id;
                                preferenceToAdd.Name = preference.Name;
                                preferenceToAdd.Price = preference.Price;
                                preferenceToAdd.Quantity = Random.Range(1, 2);
                                preferenceCategoryToAdd.Preferences.Add(preferenceToAdd);
                            }
                        }
                        productToAdd.PreferenceCategories.Add(preferenceCategoryToAdd);
                    }
                    tableOrder.Add(productToAdd);
                }
            }
        }
        return tableOrder;
    }
}
