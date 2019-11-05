using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftEarningsScript : MonoBehaviour
{
    public float Earnings;
    private Text ShiftEarningsText;
    // Start is called before the first frame update
    void Start()
    {
        ShiftEarningsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ShiftEarningsText.text = Earnings.ToString() + "€";
    }
}
