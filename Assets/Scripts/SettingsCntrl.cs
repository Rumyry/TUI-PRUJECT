using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsCntrl : MonoBehaviour
{
    public Button ExitBtn;
    public Button SaveBtn;
    public TMP_InputField PositionX;
    public TMP_InputField PositionY;
    public TMP_InputField PositionZ;
    public TMP_InputField length;
    public TMP_InputField height;
    public TMP_InputField width;
    public TMP_InputField speedPlane;
    public TMP_InputField aacceleration;
    public TMP_InputField speedCannon;
    public TMP_InputField speedBullet;
    public Toggle IsAuto;
    public Toggle UseGravity;
    public TMP_InputField Valueg;
    public TMP_InputField accelerationBullet;
    public TMP_InputField distance;
    public TMP_InputField speed;
    public Toggle AutoExp;
    void Start()
    {
        Valueg.text = "9.81";
        accelerationBullet.text = "0";
        distance.text = "0";
        speed.text = "0";
        PositionX.text = PlayerPrefs.GetFloat("PositionXPlane").ToString();
        PositionY.text = PlayerPrefs.GetFloat("PositionYPlane").ToString();
        PositionZ.text = PlayerPrefs.GetFloat("PositionZPlane").ToString();
        length.text = (PlayerPrefs.GetFloat("length") / 50f).ToString();
        height.text = (PlayerPrefs.GetFloat("height") / 50f).ToString();
        width.text = (PlayerPrefs.GetFloat("width") / 50f).ToString();
        speedPlane.text = PlayerPrefs.GetFloat("speedPlane").ToString();
        aacceleration.text = PlayerPrefs.GetFloat("acceleration").ToString();
        speedCannon.text = PlayerPrefs.GetFloat("speedCannon").ToString();
        speedBullet.text = PlayerPrefs.GetFloat("speedBullet").ToString();
        ExitBtn.onClick.AddListener(delegate { SceneManager.LoadScene("Menu"); });
        SaveBtn.onClick.AddListener(delegate { D0(); });
        
        PlayerPrefs.SetInt("cnt", 0);   
    }

    // Update is called once per frame
    void Update()
    {
    }
    void D0()
    {
        PlayerPrefs.SetFloat("speed", float.Parse(speed.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("distance", float.Parse(distance.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("g", float.Parse(Valueg.text.ToString(), CultureInfo.InvariantCulture) / 1000f);
        PlayerPrefs.SetFloat("accelerationBullet", float.Parse(accelerationBullet.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("speedBullet", float.Parse(speedBullet.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("speedCannon", float.Parse(speedCannon.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("speedPlane", float.Parse(speedPlane.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("acceleration", float.Parse(aacceleration.text.ToString(), CultureInfo.InvariantCulture));

        PlayerPrefs.SetFloat("PositionXPlane", float.Parse(PositionX.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("PositionYPlane", float.Parse(PositionY.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("PositionZPlane", float.Parse(PositionZ.text.ToString(), CultureInfo.InvariantCulture));

        PlayerPrefs.SetFloat("length", 50 * float.Parse(length.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("height", 50 * float.Parse(height.text.ToString(), CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("width", 50 * float.Parse(width.text.ToString(), CultureInfo.InvariantCulture));

        float k = 0;
        if (IsAuto.isOn == true)
        {
            k = 1;
        }
        PlayerPrefs.SetFloat("IsAuto", k);
        k = 0;
        if (UseGravity.isOn == true)
        {
            k = 1;
        }
        PlayerPrefs.SetFloat("UseGravity", k);
        k = 0;
        if (AutoExp.isOn == true)
        {
            k = 1;
        }
        PlayerPrefs.SetFloat("AutoExp", k);
    }
}
