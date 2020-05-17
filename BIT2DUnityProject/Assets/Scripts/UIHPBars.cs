using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHPBars : MonoBehaviour
{
  private float hp;
  private float hpScaled;

  private Vector3 scaleChange;

  Transform tf;


  void Awake()
  {
      tf = GetComponent<Transform>();
  }

  // Update is called once per frame
  void Update()
  {
      hp = PlayerStats.playerHealth;
      hpScaled = hp / 100;
      scaleChange = new Vector3(hpScaled, 1f);
      tf.localScale = scaleChange;
  }
}
