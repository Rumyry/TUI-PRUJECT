using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class BulletCntrl : MonoBehaviour
{
    public GameObject Spher;
    float speedBullet;
    public GameObject plane;
    public GameObject bullet;
    Vector3 target;
    float tim;
    int destr = 1;
    float downSpeed;
    void Start()
    {
        downSpeed = 0f;
        speedBullet = PlayerPrefs.GetFloat("speedBullet");
        PlayerPrefs.SetFloat("bulletX", -1);
        PlayerPrefs.SetFloat("bulletY", -1);
        PlayerPrefs.SetFloat("bulletZ", -1);
        tim = 1000f;
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float z = PlayerPrefs.GetFloat("z");
        x -= bullet.transform.position.x;
        y -= bullet.transform.position.y;
        y -= bullet.transform.position.z;
        PlayerPrefs.SetFloat("x", x);
        PlayerPrefs.SetFloat("y", y);
        PlayerPrefs.SetFloat("z", z);
        target = new Vector3(x, y, z);
        if (transform.position.y == -1.11f) { target = Vector3.zero; destr = 0; }
    }
    
    // Update is called once per frame
    void Update()
    {
        
        float accelerationBullet = PlayerPrefs.GetFloat("accelerationBullet");
        speedBullet += accelerationBullet * Time.deltaTime;
        float IsAuto = PlayerPrefs.GetFloat("IsAuto");
        if (destr == 1)
        {
            float UseGravity = PlayerPrefs.GetFloat("UseGravity");
            if (UseGravity == 1)
            {
                float Valueg = PlayerPrefs.GetFloat("g");
                downSpeed += Valueg * Time.deltaTime;
            }
            bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y - downSpeed * Time.deltaTime, bullet.transform.position.z);
            float xx = PlayerPrefs.GetFloat("x") + bullet.transform.position.x;
            float yy = PlayerPrefs.GetFloat("y") + bullet.transform.position.y;
            float zz = PlayerPrefs.GetFloat("z") + bullet.transform.position.z;
            target = new Vector3(xx, yy, zz);
            float AutoExp = PlayerPrefs.GetFloat("AutoExp");

            if (AutoExp == 1)
            {
                double dist = Math.Sqrt(((plane.transform.position.x - bullet.transform.position.x) * (plane.transform.position.x - bullet.transform.position.x)) + ((plane.transform.position.y - bullet.transform.position.y) * (plane.transform.position.y - bullet.transform.position.y)) + ((plane.transform.position.z - bullet.transform.position.z) * (plane.transform.position.z - bullet.transform.position.z)));
                print(dist);
                if (dist <= PlayerPrefs.GetFloat("distance"))
                {
                    Instantiate(Spher, bullet.transform.position, Quaternion.identity);
                    Destroy(bullet);
                }
            }
            if (IsAuto == 1)
            {
                target = plane.transform.position;
                Vector3 targt = target;
                double distz = targt.z - transform.position.z;
                double disty = targt.y - transform.position.y;
                double distx = targt.x - transform.position.x;
                double tanY = 0;
                if (distx == 0) { tanY = 0; }
                else { tanY = distz / distx * 1.0f; }
                double RotationY = Math.Atan(tanY);
                float y = (float)(RotationY / ((Math.PI / 180.0f)));
                y = 90 - y;
                if (y > 90) { y -= 180; }

                double tanX = 0;
                if (distz != 0) { tanX = disty / distz; }
                print(disty);
                double RotationX = Math.Atan(tanX);
                float x = (float)(RotationX / ((Math.PI / 180.0f)));
                Quaternion targetRot = Quaternion.Euler(-x, y, transform.rotation.z);
                print("sasha lox");
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 100);
            }
            PlayerPrefs.SetFloat("bulletX", transform.position.x);
            PlayerPrefs.SetFloat("bulletY", transform.position.y);
            PlayerPrefs.SetFloat("bulletZ", transform.position.z);

            tim -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, speedBullet * Time.deltaTime);
            if (PlayerPrefs.GetInt("crash") == 1)
            {
                PlayerPrefs.SetInt("crash", 0);
                PlayerPrefs.SetFloat("bulletX", -1);
                PlayerPrefs.SetFloat("bulletY", -1);
                PlayerPrefs.SetFloat("bulletZ", -1);
                Destroy(bullet);

            }
        }
    }
}
