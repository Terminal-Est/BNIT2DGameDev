using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICarDmgBar : MonoBehaviour
{
  private float damage;
  private float damageScaled;

  private Vector3 scaleChange;

  Transform tf;


  void Awake()
  {
      tf = GetComponent<Transform>();
  }

  // Update is called once per frame
  void Update()
  {
      damage = PlayerStats.vehicleDamage;
      damageScaled = damage / 100;
      scaleChange = new Vector3(damageScaled, 1f);
      tf.localScale = scaleChange;
  }
}
