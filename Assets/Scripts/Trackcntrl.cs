using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trackcntrl : MonoBehaviour
{
    public GameObject bullet;
    public GameObject plane;
    public TextMeshProUGUI coordsPlane;
    public TextMeshProUGUI coordsBullet;
    public TextMeshProUGUI timetocrash;
    public Image status;
    public Sprite pending;
    public Sprite fail;
    public Sprite success;
    void Start()
    {
        PlayerPrefs.SetInt("res", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float x = plane.transform.position.x;
        float y = plane.transform.position.y;
        float z = plane.transform.position.z;
        coordsPlane.text = "x=" + x.ToString("#.#") + "; " + "y=" + y.ToString("#.#") + "; " + "z=" + z.ToString("#.#") + ";";

        x = PlayerPrefs.GetFloat("bulletX");
        y = PlayerPrefs.GetFloat("bulletY");
        z = PlayerPrefs.GetFloat("bulletZ");
        coordsBullet.text = "x=" + x.ToString("#.#") + "; " + "y=" + y.ToString("#.#") + "; " + "z=" + z.ToString("#.#") + ";";

        int sh = PlayerPrefs.GetInt("shoot");
        if (sh == 0)
        {
            float IsAuto = PlayerPrefs.GetFloat("IsAuto");
            if (IsAuto == 0)
            {
                float speedBullet = PlayerPrefs.GetFloat("speedBullet");
                x = x - PlayerPrefs.GetFloat("x");
                x *= x;
                y = y - PlayerPrefs.GetFloat("y");
                y *= y;
                z = z - PlayerPrefs.GetFloat("z");
                z *= z;
                double ans = Math.Sqrt(x + y + z) / speedBullet;
                ans *= 100;
                ans = Math.Truncate(ans);
                ans /= 100.0f;
                timetocrash.text = (ans).ToString() + " sec";
            }
            if (IsAuto == 1)
            {
                float speedPlane = PlayerPrefs.GetFloat("speedPlane");
                float speedBullet = PlayerPrefs.GetFloat("speedBullet");
                x = x - plane.transform.position.x;
                x *= x;
                y = y - plane.transform.position.y;
                y *= y;
                z = z - -plane.transform.position.z;
                z *= z;
                double ans = Math.Sqrt(x + y + z) / (speedBullet - speedPlane);
                print(speedBullet - speedPlane);
                ans *= 100;
                ans = Math.Truncate(ans);
                ans /= 100.0f;
                timetocrash.text = (ans).ToString() + " sec";
            }
        }
        else { timetocrash.text = "-1 sec"; }

        float res = PlayerPrefs.GetInt("res");
        if (res == 1)
        {
            status.sprite = success;
        }
        if (res == 0)
        {
            status.sprite = pending;
        }
        if (res == -1)
        {
            status.sprite = fail;
        }
    }
}
