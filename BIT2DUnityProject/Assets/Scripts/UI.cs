using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{

    private float damage;
    private float money;
    private float health;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (text.tag == "CarDmg")
        {
            damage = PlayerStats.vehicleDamage;
            if (damage > 100)
              damage = 100;
            text.text = "Damage: " + Mathf.Round(damage);
        } else if (text.tag == "Cash")
        {
            money = PlayerStats.cash;
            if (money < 0)
              money = 0;
            text.text = "Cash: " + "$" + Mathf.Round(money);
        }
    }

}
