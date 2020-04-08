using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{

    public float timeChange;
    private float fAngle;
    private bool fChange;
    // Start is called before the first frame update
    void Start()
    {
        fChange = false;
        StartCoroutine(ChangeAngle());
    }

    // Update is called once per frame
    void Update()
    {
        AreaEffector2D af = GetComponent<AreaEffector2D>();
        af.forceAngle = fAngle;
        if (fChange == true)
        {
            StartCoroutine(ChangeAngle());
        }
        fChange = false;

    }

    IEnumerator ChangeAngle()
    {
        yield return new WaitForSeconds(timeChange);
        fAngle = Random.Range(0, -180);
        fChange = true;
    }

}
