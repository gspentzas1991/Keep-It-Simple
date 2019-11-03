﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes;
using System.Linq;


public class CustomerManager : MonoBehaviour
{
    [SerializeField] private readonly float CustomerSpawnTimer=3f;
    [SerializeField] private readonly int MaxCustomers = 3;
    private float CurrentCustomerSpawnTimer;
    private List<TableScript> TableList;

    void Start()
    {
        //spawnTimer initialization
        CurrentCustomerSpawnTimer = CustomerSpawnTimer;
        TableList = GameObject.FindGameObjectsWithTag("Table").Select(x=>x.GetComponent<TableScript>()).ToList();
    }

    void Update()
    {
        CurrentCustomerSpawnTimer -= Time.deltaTime;
        if (CurrentCustomerSpawnTimer <= 0)
        {
            CurrentCustomerSpawnTimer = CustomerSpawnTimer;
            StartCoroutine(SpawnCustomer());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            TableList.Where(x=>x.Order!=null).First().GetComponent<TableScript>().ClearTable();
        }
    }

    /// <summary>
    /// Tries to spawn a new customer
    /// </summary>
    IEnumerator SpawnCustomer()
    {
        var occupiedSeatCount = TableList.Count(x => x.Order != null);
        //If there are maxCustomers seated, or no tables available we don't accept new customers
        if (occupiedSeatCount < MaxCustomers && occupiedSeatCount!= TableList.Count)
        {
            //randomly gets an available table
            var availableTableList = TableList.Where(x => x.Order == null).ToList();
            var selectedTable = availableTableList[Random.Range(0, availableTableList.Count)];
            selectedTable.GenerateFoodOrder();
        }
        yield return null;
    }

}
