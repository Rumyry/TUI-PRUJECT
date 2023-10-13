using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class BulletCntrl : MonoBehaviour
{
    public float speed;
    public GameObject plane;
    public GameObject bullet;
    Vector3 target;
    float tim;
    int destr = 1;
    void Start()
    {
        PlayerPrefs.SetFloat("speedBullet", speed);
        PlayerPrefs.SetFloat("bulletX", -1);
        PlayerPrefs.SetFloat("bulletY", -1);
        PlayerPrefs.SetFloat("bulletZ", -1);
        tim = 1000f;
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float z = PlayerPrefs.GetFloat("z");
        target = new Vector3(x, y, z);
        if (transform.position.y == -1.11f) { target = Vector3.zero; destr = 0; }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (destr == 1)
        {
            PlayerPrefs.SetFloat("bulletX", transform.position.x);
            PlayerPrefs.SetFloat("bulletY", transform.position.y);
            PlayerPrefs.SetFloat("bulletZ", transform.position.z);
        }
        tim -= Time.deltaTime;
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target, speed * Time.deltaTime);
        if (bullet.transform.position == target && destr == 1) { if (tim <= 0) 
            {
                PlayerPrefs.SetFloat("bulletX", -1);
                PlayerPrefs.SetFloat("bulletY", -1);
                PlayerPrefs.SetFloat("bulletZ", -1);
                Destroy(bullet);
                
            } 
        }
        else
        {
            tim = 0.1f;
        }

        
    }
}
