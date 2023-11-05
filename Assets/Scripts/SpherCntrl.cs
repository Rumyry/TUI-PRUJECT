using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherCntrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y != -111)
        {
            int crash = PlayerPrefs.GetInt("crash");
            if (crash == 1)
            {
                PlayerPrefs.SetInt("crash", 0);
                Destroy(gameObject);
            }
            float speed = PlayerPrefs.GetFloat("speed");
            transform.localScale = new Vector3(transform.localScale.x + speed * Time.deltaTime, transform.localScale.y + speed * Time.deltaTime, transform.localScale.z + speed * Time.deltaTime);
        }
    }
}
