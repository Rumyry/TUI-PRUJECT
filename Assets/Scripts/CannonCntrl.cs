using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms.Impl;

public class CannonCntrl : MonoBehaviour
{
    public GameObject cannon;
    public GameObject plane;
    public GameObject bullet;
    public float speedBullet;
    public float speed;
    Quaternion target;


    // Start is called before the first frame update
    void Start()
    {
    }
    public float reload = 0;
    // Update is called once per frame
    void Update()
    {
        int IsShoot = PlayerPrefs.GetInt("shoot");
        if (IsShoot == 1)
        {
            double planeSpeed, bulletSpeed;
            planeSpeed = PlayerPrefs.GetFloat("SpeedPlane");
            bulletSpeed = speedBullet;

            double xPlane = plane.transform.position.x, yPlane = plane.transform.position.y, zPlane = plane.transform.position.z, discr = 0f;
            bool case1 = false;

            if (planeSpeed * planeSpeed != bulletSpeed * bulletSpeed)
                discr = (2 * bulletSpeed * bulletSpeed * xPlane) * (2 * bulletSpeed * bulletSpeed * xPlane) - 4 * (planeSpeed * planeSpeed - bulletSpeed * bulletSpeed) * (planeSpeed * planeSpeed * yPlane * yPlane + planeSpeed * planeSpeed * zPlane * zPlane - bulletSpeed * bulletSpeed * xPlane * xPlane);
            else if (planeSpeed * planeSpeed == bulletSpeed * bulletSpeed)
            {
                discr = (xPlane * xPlane - yPlane * yPlane - zPlane * zPlane) / (2 * xPlane);
                case1 = true;
            }

            double cannonTargetX;
            double cannonTargetY;
            double cannonTargetZ;
            if (case1)
            {
                cannonTargetX = discr;
                cannonTargetY = yPlane;
                cannonTargetZ = zPlane;
            }
            else if (!case1 && discr >= 0)
            {
                if (planeSpeed >= 0)
                    cannonTargetX = (-2 * bulletSpeed * bulletSpeed * xPlane - Math.Sqrt(discr)) / (2 * (planeSpeed * planeSpeed - bulletSpeed * bulletSpeed));
                else
                    cannonTargetX = (-2 * bulletSpeed * bulletSpeed * xPlane + Math.Sqrt(discr)) / (2 * (planeSpeed * planeSpeed - bulletSpeed * bulletSpeed));
                cannonTargetY = yPlane;
                cannonTargetZ = zPlane;
            }
            else
            {
                cannonTargetX = 10;
                cannonTargetY = 100;
                cannonTargetZ = 50;
            }

            Vector3 targt = new Vector3((float)cannonTargetX, (float)cannonTargetY, (float)cannonTargetZ);


            PlayerPrefs.SetFloat("x", (float)cannonTargetX);
            PlayerPrefs.SetFloat("y", (float)cannonTargetY);
            PlayerPrefs.SetFloat("z", (float)cannonTargetZ);




            reload -= Time.deltaTime;
            double distz = targt.z - cannon.transform.position.z;
            double disty = targt.y - cannon.transform.position.y;
            double distx = targt.x - cannon.transform.position.x;
            double tanY = 0;
            if (distx == 0) { tanY = 0; }
            else { tanY = distz / distx * 1.0f; }
            double RotationY = Math.Atan(tanY);
            float y = (float)(RotationY / ((Math.PI / 180.0f)));
            y = 90 - y;
            if (y > 90) { y -= 180; }

            double tanX = 0;
            if (distz != 0) { tanX = disty / distz; }
            double RotationX = Math.Atan(tanX);
            float x = (float)(RotationX / ((Math.PI / 180.0f)));
            target = Quaternion.Euler(-x, y, cannon.transform.rotation.z);


            float difX = cannon.transform.rotation.x - target.x;
            if (difX < -difX) { difX = -difX; }
            float difY = cannon.transform.rotation.y - target.y;
            if (difY < -difY) { difY = -difY; }
            float maxx = difX;
            if (difY > maxx) { maxx = difY; }
            cannon.transform.rotation = Quaternion.Slerp(cannon.transform.rotation, target, 0.8f / maxx * Time.deltaTime);
            print(speed);
            
            print(difX);
            print(difY);
            int cnt = PlayerPrefs.GetInt("cnt");
            int shoot = PlayerPrefs.GetInt("shoot");
            if ((difX <= 0.001f) && (difY <= 0.001f) && reload <= 0)
            {
                PlayerPrefs.SetInt("cnt", cnt + 1);
                Shoot();
                reload = 0f;
            }
        }
    }
    void Shoot()
    {
        PlayerPrefs.SetInt("shoot", 0);
        Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
