using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CarUIScript : MonoBehaviour
{

    private float damage;
    private float fuel;
    private float money;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (text.tag == "CarFuel")
        {
            fuel = PlayerStats.fuel;
            text.text = "Fuel: " + Mathf.Round(fuel);
        } else if (text.tag == "CarDmg")
        {
            damage = PlayerStats.vehicleDamage;
            text.text = "Damage: " + Mathf.Round(damage);
        } else if (text.tag == "Cash")
        {
            money = PlayerStats.cash;
            text.text = "Cash: " + Mathf.Round(money);
        }
    }

}
