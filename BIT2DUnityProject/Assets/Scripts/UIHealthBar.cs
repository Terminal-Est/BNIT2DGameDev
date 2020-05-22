using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
  private float health;
  private float healthScaled;

  private Vector3 scaleChange;

  Transform tf;


  void Awake()
  {
      tf = GetComponent<Transform>();
  }

  // Update is called once per frame
  void Update()
  {
      health = PlayerStats.playerHealth;
      if (health <= 0)
        health = 0;
      healthScaled = health / 100;
      scaleChange = new Vector3(healthScaled, 1f);
      tf.localScale = scaleChange;
  }
}
