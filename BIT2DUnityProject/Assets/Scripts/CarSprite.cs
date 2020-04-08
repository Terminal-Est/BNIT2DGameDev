using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSprite : MonoBehaviour
{
    public List<Sprite> cars = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
      Debug.Log(PlayerStats.vehicleNo);
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = cars[PlayerStats.vehicleNo];
      if (spriteRenderer = null)
      {
        spriteRenderer.sprite = cars[0];
      }
    }
}
