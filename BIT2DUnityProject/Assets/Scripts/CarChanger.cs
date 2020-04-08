using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CarChanger : MonoBehaviour
{

    public SpriteRenderer car;
    public List<Sprite> cars = new List<Sprite>();
    // Start is called before the first frame update

    private int currentOption = 0;

    public void NextOption()
    {
      currentOption++;
      if (currentOption >= cars.Count)
      {
        currentOption = 0; //reset if at end of list
      }
      car.sprite = cars[currentOption];
    }

    public void PreviousOption()
    {
      currentOption--;
      if (currentOption < 0)
      {
        currentOption = cars.Count - 1; //reset if at end of list
      }
      car.sprite = cars[currentOption];
    }

    public void PurchaseCar()
    {
      PlayerStats.vehicleNo = currentOption;
    }

}
