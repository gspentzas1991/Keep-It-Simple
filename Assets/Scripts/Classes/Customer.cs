using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class Customer
    {
        [SerializeField]private GameObject customerPrefab;
        public int tableNumber { get; set; }

        public Customer()
        {

        }
    }
}
