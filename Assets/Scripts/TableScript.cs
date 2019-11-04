using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes;
using UnityEngine.UI;

public class TableScript : MonoBehaviour
{
    public FoodOrder Order { get; private set; }
    public bool WantToPay;
    public GameObject CustomerPrefab;
    private GameObject CustomerObjectInstance;
    //The position offset from the table for the customer to appear on the seat
    private Vector3 customerSeatOffset = new Vector3(0, 0.25f, -0.5f);
    [SerializeField] private Text NotePadText;
    [SerializeField] private Image CustomerEatingIcon;
    [SerializeField] private Image CustomerPayIcon;
    [SerializeField] private float CustomerEatingTimer;
    private float CurrentCustomerEatingTimer=0;

    private void Start()
    {
        //Sets the rotation of table icons
        CustomerEatingIcon.rectTransform.LookAt(Camera.main.transform);
        CustomerPayIcon.rectTransform.LookAt(Camera.main.transform);
    }

    void Update()
    {
        if (CurrentCustomerEatingTimer > 0)
        {
            CurrentCustomerEatingTimer -= Time.deltaTime;
            //The frame the timer reaches zero, the timer stops and the table wants to pay
            if (CurrentCustomerEatingTimer<=0)
            {
                CurrentCustomerEatingTimer = 0;
                WantToPay = true;
                CustomerPayIcon.enabled = true;
            }
            CustomerEatingIcon.fillAmount = CurrentCustomerEatingTimer / CustomerEatingTimer;
        }
    }

    /// <summary>
    /// Generates a random FoodOrder for the table and spawns a customer
    /// </summary>
    public void GenerateCustomer()
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
        WantToPay = false;
        CustomerPayIcon.enabled = false;
        Destroy(CustomerObjectInstance);
        Order = null;
    }

    /// <summary>
    /// Starts the customer eating counter and spawns food on the table
    /// </summary>
    public void DeliverFoodToTable()
    {
        CurrentCustomerEatingTimer = CustomerEatingTimer;
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
