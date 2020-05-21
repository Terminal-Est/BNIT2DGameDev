using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    private int money;
    public Color Green;

    Text text;
    Text greenCashMoney;

    void Awake()
    {
        text = GetComponent<Text>();
        greenCashMoney = GetComponent<Text>();
    }

    void Update()
    {
      money = PlayerStats.cash;
      if (money < 0)
        money = 0;
      greenCashMoney.color = Green;
      greenCashMoney.text = money.ToString();
      text.text = "$" + greenCashMoney.text;
    }
}
