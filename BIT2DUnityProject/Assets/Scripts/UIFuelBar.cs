using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFuelBar : MonoBehaviour
{
    private float fuel;
    private float fuelScaled;

    private Vector3 scaleChange;

    Transform tf;


    void Awake()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        fuel = PlayerStats.fuel;
        fuelScaled = fuel / 600;
        scaleChange = new Vector3(fuelScaled, 1f);
        tf.localScale = scaleChange;
    }
}
