using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes;
using System.Linq;


public class CustomerManager : MonoBehaviour
{
    private List<Customer> customerList = new List<Customer>();
    [SerializeField] private float customerSpawnSpeed=3f;
    [SerializeField] private int maxCustomers = 3;
    private bool spawningIsActive = true;
    private Dictionary<int, TableStatus> tableAvailabilityList = new Dictionary<int, TableStatus>();

    void Start()
    {
        //Makes all tables available
        int tableGameobjectCount = GameObject.FindGameObjectsWithTag("Table").Count();
        for (int i = 1; i <= tableGameobjectCount; i++)
        {
            tableAvailabilityList.Add(i, TableStatus.Available);
        }
        //Creates the fist customer
        Customer startingCustomer = new Customer();
        startingCustomer.tableNumber = Random.Range(1, 9);
        customerList.Add(startingCustomer);
        tableAvailabilityList[startingCustomer.tableNumber]= TableStatus.Occupied;
        StartCoroutine(AutomaticCustomerSpawning(customerSpawnSpeed));
    }

    /// <summary>
    /// Spawns a new customer every spawnSpeed seconds
    /// </summary>
    IEnumerator AutomaticCustomerSpawning(float spawnSpeed)
    {
        while (spawningIsActive)
        {
            yield return new WaitForSeconds(spawnSpeed);
            var occupiedSeats = tableAvailabilityList.Count(x => x.Value == TableStatus.Occupied);
            //If there are 3 customers seated, we don't accept new customers
            if (occupiedSeats < maxCustomers && occupiedSeats!=tableAvailabilityList.Count)
            {
                Customer newCustomer = new Customer();
                //Keeps trying to find an available table randomly
                do
                {
                    newCustomer.tableNumber = Random.Range(1, 9);
                } while (tableAvailabilityList[newCustomer.tableNumber] == TableStatus.Occupied);
                tableAvailabilityList[newCustomer.tableNumber] = TableStatus.Occupied;
                customerList.Add(newCustomer);
            }
        }

    }

}
