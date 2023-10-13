using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Planecntrl : MonoBehaviour
{
    public GameObject plane;
    public float speed;
    public float x, y, z;
    int work = 1;
    float ff = 0f;
    void Start()
    {
        PlayerPrefs.SetFloat("SpeedPlane", speed);
        PlayerPrefs.SetInt("shoot", 1);
        plane.transform.position = new Vector3(x, y, z);
        if (plane.transform.position == new Vector3(0, -11.11f, 0))
        {
            work = 0;
        }
    }
    void Update()
    {
        speed = PlayerPrefs.GetFloat("SpeedPlane");
        if (work!=0 )
        {
            plane.transform.position = new Vector3(plane.transform.position.x + speed * Time.deltaTime, plane.transform.position.y, plane.transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "bullet")
        {
            PlayerPrefs.SetInt("res", 1);
            z = Random.Range(50, 200);
            x = Random.Range(-z + 50, z - 50);
            y = Random.Range(10, 50);
            PlayerPrefs.SetFloat("SpeedPlane", Random.Range(-30, 30)) ;
            if (y > z / 3.0f) { y = z / 3.0f; }
            plane.transform.position = new Vector3(x, y, z);
            PlayerPrefs.SetInt("shoot", 1);
        }
    }
}
