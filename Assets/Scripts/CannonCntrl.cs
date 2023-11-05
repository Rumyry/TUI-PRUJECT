using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms.Impl;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UI;

public class CannonCntrl : MonoBehaviour
{
    public GameObject cannon;
    public GameObject plane;
    public GameObject bullet;
    float speedCannon;
    UnityEngine.Quaternion target;
    public Image status;
    public Sprite fail;
    
    static double FindRealRoots(double a, double b, double c, double d, double e)
    {        
        double[] coefficients = { e, d, c, b, a };
 
        Polynomial polynomial = new Polynomial(coefficients);
        Complex[] roots = polynomial.Roots();

        print("razmer    "  + roots.Length);

        double time = 0f;
        List <double> res = new List<double> { };
        foreach (Complex root in roots) {
            if (root.Imaginary == 0 && root.Real > 0 && (time == 0f || root.Real < time))
                time = root.Real;
        }   

        return time;
    }

    static List <double> FindRoots(double a, double b, double c)
    {        
        double[] coefficients = { c, b, a };
 
        Polynomial polynomial = new Polynomial(coefficients);
        Complex[] roots = polynomial.Roots();

        print("razmer    "  + roots.Length);

        List <double> res = new List<double> { };
        foreach (Complex root in roots) {
            if (root.Imaginary == 0)
                res.Add(root.Real);
        }   

        res.Sort();
        return res;
    }
    
    static Complex CR(Complex num)
    {
        return Complex.Pow(num, 1.0 / 3.0);
    }

    void Start()
    {
        speedCannon = PlayerPrefs.GetFloat("speedCannon");
    }
    public float reload = 0;
    void Update()
    {
        if (PlayerPrefs.GetFloat("IsPossible") == 1)
        {
            int IsShoot = PlayerPrefs.GetInt("shoot");
            if (IsShoot == 1)
            {
                UnityEngine.Vector3 targt = new UnityEngine.Vector3();
                float IsAuto = PlayerPrefs.GetFloat("IsAuto");
                if (IsAuto == 1)
                {
                    targt = plane.transform.position;
                }
                else
                {
                    float acceleration = PlayerPrefs.GetFloat("acceleration");
                    double cannonTargetX = 0f, cannonTargetY = 0f, cannonTargetZ = 0f;

                    double planeSpeed, bulletSpeed;
                    planeSpeed = PlayerPrefs.GetFloat("speedPlane");
                    bulletSpeed = PlayerPrefs.GetFloat("speedBullet");

                    double xPlane = plane.transform.position.x, yPlane = plane.transform.position.y, zPlane = plane.transform.position.z, discr = 0f;
                    bool case1 = false;

                    if (acceleration == 0)
                    {   

                        /* double aKf, bKf, cKf;
                        aKf = planeSpeed * planeSpeed - bulletSpeed * bulletSpeed;
                        bKf = 2 * bulletSpeed * bulletSpeed * xPlane;
                        cKf = planeSpeed * planeSpeed * yPlane * yPlane + planeSpeed * planeSpeed * zPlane * zPlane - bulletSpeed * bulletSpeed * xPlane * xPlane;
                        
                        List<double> xpoint = FindRoots(aKf, bKf, cKf);
                        cannonTargetX = xpoint[0];
                        cannonTargetY = yPlane;
                        cannonTargetZ = zPlane;

                        print("my res: " + xpoint[0]);
                        */            
                        if (planeSpeed * planeSpeed != bulletSpeed * bulletSpeed)
                            discr = (2 * bulletSpeed * bulletSpeed * xPlane) * (2 * bulletSpeed * bulletSpeed * xPlane) - 4 * (planeSpeed * planeSpeed - bulletSpeed * bulletSpeed) * (planeSpeed * planeSpeed * yPlane * yPlane + planeSpeed * planeSpeed * zPlane * zPlane - bulletSpeed * bulletSpeed * xPlane * xPlane);
                        else if (planeSpeed * planeSpeed == bulletSpeed * bulletSpeed)
                        {
                            discr = (xPlane * xPlane - yPlane * yPlane - zPlane * zPlane) / (2 * xPlane);
                            case1 = true;
                        } 

                        if (case1)
                        {   
                            //print(discr);
                            
                            cannonTargetX = discr;
                            cannonTargetY = yPlane;
                            cannonTargetZ = zPlane;
                            
                        }
                        else if (!case1 && discr >= 0)
                        {
                            if (planeSpeed >= 0)
                                //print((-2 * bulletSpeed * bulletSpeed * xPlane - Math.Sqrt(discr)) / (2 * (planeSpeed * planeSpeed - bulletSpeed * bulletSpeed)));
                                cannonTargetX = (-2 * bulletSpeed * bulletSpeed * xPlane - Math.Sqrt(discr)) / (2 * (planeSpeed * planeSpeed - bulletSpeed * bulletSpeed));
                            else
                                //print((-2 * bulletSpeed * bulletSpeed * xPlane + Math.Sqrt(discr)) / (2 * (planeSpeed * planeSpeed - bulletSpeed * bulletSpeed)));
                                cannonTargetX = (-2 * bulletSpeed * bulletSpeed * xPlane + Math.Sqrt(discr)) / (2 * (planeSpeed * planeSpeed - bulletSpeed * bulletSpeed));
                            cannonTargetY = yPlane;
                            cannonTargetZ = zPlane;
                            
                        }
                        else
                        {
                            PlayerPrefs.SetFloat("IsPossible", 0);
                            print("Can not reach the target\n");
                        }
                        
                    }
                    else
                    {
                        bool gotTarget = false;
                        double aKf, bKf, cKf, dKf, eKf;
                        planeSpeed = PlayerPrefs.GetFloat("speedPlane");
                        bulletSpeed = PlayerPrefs.GetFloat("speedBullet"); ;

                        aKf = (acceleration * acceleration) / 4;
                        bKf = planeSpeed * acceleration;
                        cKf = planeSpeed * planeSpeed + xPlane * acceleration - bulletSpeed * bulletSpeed;
                        dKf = 2 * xPlane * planeSpeed;
                        eKf = xPlane * xPlane + yPlane * yPlane + zPlane * zPlane;
                        
                        double t = FindRealRoots(aKf, bKf, cKf, dKf, eKf);
                        if (t != 0)
                            gotTarget = true;
                        

                        /* List<double> t = FindRealRoots(aKf, bKf, cKf, dKf, eKf);
                        double time = 0f; */
                        /*foreach (double num in t) {
                            double pe = xPlane + planeSpeed * num + acceleration * acceleration * num / 2.0f;
                            double be = -1;
                            if (bulletSpeed * bulletSpeed * num * num - yPlane * yPlane - zPlane * zPlane >= 0)
                                be = (bulletSpeed * bulletSpeed * num * num - yPlane * yPlane - zPlane * zPlane);

                            print($"Plane coord: x0(${xPlane}) + v0(${planeSpeed}) * t(${num}) = ${planeSpeed * num}  +  a(${acceleration}) * a * t / 2 = ${acceleration * acceleration * num / 2}  ==> ${pe}");
                            print($"Bullet coord: v2(${bulletSpeed}) * v2 * t(${num}) * t  = ${bulletSpeed * bulletSpeed * num * num}  - y0(${yPlane}) * y0  = ${yPlane * yPlane} - z0(${zPlane}) * z0  = ${zPlane * zPlane} ==> ${be}  <<>> sqrt = ${Math.Sqrt(be)}     ");

                        if (pe == be || pe == -be) {
                            cannonTargetX = pe;
                            gotTarget = true;
                            break;
                        } 
                        }*/

                    if (!gotTarget) {
                        cannonTargetX = 1000;
                        print("Not solved.");
                    } else 
                        cannonTargetX = xPlane + (planeSpeed + planeSpeed + acceleration * t) / 2.0f * t;
                        cannonTargetY = yPlane;
                        cannonTargetZ = zPlane;

                        print(t);
                    }

                    targt = new UnityEngine.Vector3((float)cannonTargetX, (float)cannonTargetY, (float)cannonTargetZ);

                }
                PlayerPrefs.SetFloat("x", (float)targt.x);
                PlayerPrefs.SetFloat("y", (float)targt.y);
                PlayerPrefs.SetFloat("z", (float)targt.z);




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
                target = UnityEngine.Quaternion.Euler(90f - x, y, cannon.transform.rotation.z);

                float difX = cannon.transform.rotation.x - target.x;
                if (difX < -difX) { difX = -difX; }
                float difY = cannon.transform.rotation.y - target.y;
                if (difY < -difY) { difY = -difY; }
                float maxx = difX;
                if (difY > maxx) { maxx = difY; }

                cannon.transform.rotation = UnityEngine.Quaternion.Slerp(cannon.transform.rotation, target, (0.2f * speedCannon) / maxx * Time.deltaTime);


                int cnt = PlayerPrefs.GetInt("cnt");
                int shoot = PlayerPrefs.GetInt("shoot");
                if ((difX <= 0.001f) && (difY <= 0.001f))
                {
                    PlayerPrefs.SetInt("res", 0);
                    PlayerPrefs.SetInt("shoot", 0);
                    Instantiate(bullet, new UnityEngine.Vector3(0, 0, 0), UnityEngine.Quaternion.Euler(-x, y, cannon.transform.rotation.z));
                    PlayerPrefs.SetInt("cnt", cnt + 1);
                    reload = 0f;
                }
            }
        }
        else 
        {
            status.sprite = fail;
            PlayerPrefs.SetInt("res", -1);
        }
    }
}