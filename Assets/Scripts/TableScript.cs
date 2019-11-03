using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes;
using UnityEngine.UI;

public class TableScript : MonoBehaviour
{
    public FoodOrder Order { get; private set; }
    public GameObject CustomerPrefab;
    private GameObject CustomerObjectInstance;
    //The position offset from the table for the customer to appear on the seat
    private Vector3 customerSeatOffset = new Vector3(0, 0.25f, -0.5f);
    [SerializeField] private Text NotePadText;
     
    /// <summary>
    /// Generates a random FoodOrder for the table
    /// </summary>
    public void GenerateFoodOrder()
    {
        Order = new FoodOrder();
        //We apply the offset on the table's position to get the correct customer position on the seat
        var customerGameObjectPosition = transform.position + customerSeatOffset;
        CustomerObjectInstance = Instantiate(CustomerPrefab, customerGameObjectPosition, new Quaternion());
    }
    
    /// <summary>
    /// Removes the customer from the customerList and makes his table available
    /// </summary>
    public void ClearTable()
    {
        Destroy(CustomerObjectInstance);
        Order = null;
    }

    /// <summary>
    /// Shows the table's order
    /// </summary>
    public void ShowOrder()
    {
        if (Order!=null)
        {
            NotePadText.text = this.name + "\n" + Order.test;
        }
    }
}
