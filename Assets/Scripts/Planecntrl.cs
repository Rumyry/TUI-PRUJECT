using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Planecntrl : MonoBehaviour
{
    public GameObject plane;
    float speedPlane;
    public GameObject planeSprite;
    float acceleration;
    float x, y, z;
    private float downSpeed;
    public GameObject explosion;
    public GameObject rocket;
    public GameObject yadro;
    void Start()
    {
        
        downSpeed = 0f;
        float length = PlayerPrefs.GetFloat("length");
        float height = PlayerPrefs.GetFloat("height");
        float width = PlayerPrefs.GetFloat("width");
        plane.transform.localScale = new Vector3(1, 1, 1);
        rocket.transform.localScale = new Vector3(length * 2, height * 2, width * 2);
        speedPlane = PlayerPrefs.GetFloat("speedPlane");
        acceleration = PlayerPrefs.GetFloat("acceleration");
        PlayerPrefs.SetInt("shoot", 1);
        if (speedPlane > 0) 
        {
            planeSprite.transform.rotation = Quaternion.Euler(0 - planeSprite.transform.rotation.x, 0f - planeSprite.transform.rotation.y, 0 - planeSprite.transform.rotation.z);
            rocket.transform.rotation = Quaternion.Euler(0 - rocket.transform.rotation.x, 90f - rocket.transform.rotation.y, 0 - rocket.transform.rotation.z);
        }
        else
        {
            planeSprite.transform.rotation = Quaternion.Euler(180f - planeSprite.transform.rotation.x, 0f - planeSprite.transform.rotation.y, 180f - planeSprite.transform.rotation.z);
            rocket.transform.rotation = Quaternion.Euler(180f - rocket.transform.rotation.x, 90f - rocket.transform.rotation.y, 180f - rocket.transform.rotation.z);
        }
        x = PlayerPrefs.GetFloat("PositionXPlane");
        y = PlayerPrefs.GetFloat("PositionYPlane");
        z = PlayerPrefs.GetFloat("PositionZPlane");
        plane.transform.position = new Vector3(x, y, z);
    }
    void Update()
    {
        print(PlayerPrefs.GetInt("crash"));
        rocket.transform.position = plane.transform.position;
        float UseGravity = PlayerPrefs.GetFloat("UseGravity");
        if (UseGravity == 1)
        {
            downSpeed += PlayerPrefs.GetFloat("g") * Time.deltaTime;
        }
        speedPlane += acceleration * Time.deltaTime;
        plane.transform.position = new Vector3(plane.transform.position.x + speedPlane * Time.deltaTime, plane.transform.position.y - downSpeed * Time.deltaTime, plane.transform.position.z);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "bullet")
        {
            rocket.transform.position = new Vector3(0, -100, 0);
            var exp = Instantiate(explosion, plane.transform.position, Quaternion.identity);
            Destroy(exp, 1f);
            PlayerPrefs.SetInt("crash", 1);
            print("%%");
            PlayerPrefs.SetInt("res", 1);
            PlayerPrefs.SetFloat("speedPlane", Random.Range(-30, 30));
            speedPlane = PlayerPrefs.GetFloat("speedPlane");
            z = Random.Range(50, 250);
            x = Random.Range(-z + 70, z - 70);
            y = Random.Range(50, 120);

            if (speedPlane > 0)
            {
                planeSprite.transform.rotation = Quaternion.Euler(0 - planeSprite.transform.rotation.x, 0f - planeSprite.transform.rotation.y, 0 - planeSprite.transform.rotation.z);
                rocket.transform.rotation = Quaternion.Euler(0 - rocket.transform.rotation.x, 90f - rocket.transform.rotation.y, 0 - rocket.transform.rotation.z);
            }
            else
            {
                planeSprite.transform.rotation = Quaternion.Euler(180f - planeSprite.transform.rotation.x, 0f - planeSprite.transform.rotation.y, 180f - planeSprite.transform.rotation.z);
                rocket.transform.rotation = Quaternion.Euler(180f - rocket.transform.rotation.x, 90f - rocket.transform.rotation.y, 180f - rocket.transform.rotation.z);
            }
            if (y > z / 3.0f) { y = z / 3.0f; }
            plane.transform.position = new Vector3(x, y, z);
            PlayerPrefs.SetInt("shoot", 1);
            float UseGravity = PlayerPrefs.GetFloat("UseGravity");
            downSpeed = 0f;
        }
    }
}
