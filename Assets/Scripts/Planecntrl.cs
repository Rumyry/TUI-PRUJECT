using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Planecntrl : MonoBehaviour
{
    public GameObject plane;
    float speedPlane;
    public float x, y, z;
    public GameObject planeSprite;
    void Start()
    {
        speedPlane = PlayerPrefs.GetFloat("speedPlane");
        
        PlayerPrefs.SetInt("shoot", 1);
        if (speedPlane > 0)
        {
            planeSprite.transform.rotation = Quaternion.Euler(0 - planeSprite.transform.rotation.x, 0f - planeSprite.transform.rotation.y, 0 - planeSprite.transform.rotation.z);
        }
        else
        {
            planeSprite.transform.rotation = Quaternion.Euler(180f - planeSprite.transform.rotation.x, 0f - planeSprite.transform.rotation.y, 180f - planeSprite.transform.rotation.z);
        }
        plane.transform.position = new Vector3(x, y, z);
    }
    void Update()
    {
        print(speedPlane);
        plane.transform.position = new Vector3(plane.transform.position.x + speedPlane * Time.deltaTime, plane.transform.position.y, plane.transform.position.z);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "bullet")
        {
            PlayerPrefs.SetInt("res", 1);
            PlayerPrefs.SetFloat("speedPlane", Random.Range(-30, 30));
            speedPlane = PlayerPrefs.GetFloat("speedPlane");
            z = Random.Range(50, 250);
            x = Random.Range(-z + 70, z - 70);
            y = Random.Range(10, 75);

            if (speedPlane > 0)
            {
                planeSprite.transform.rotation = Quaternion.Euler(0 - planeSprite.transform.rotation.x, 0f - planeSprite.transform.rotation.y, 0 - planeSprite.transform.rotation.z);
            }
            else
            {
                planeSprite.transform.rotation = Quaternion.Euler(180f - planeSprite.transform.rotation.x, 0f - planeSprite.transform.rotation.y, 180f - planeSprite.transform.rotation.z);
            }
            if (y > z / 3.0f) { y = z / 3.0f; }
            plane.transform.position = new Vector3(x, y, z);
            PlayerPrefs.SetInt("shoot", 1);
        }
    }
}
