//using MathNet.Numerics.Statistics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject cannon;
    public GameObject view;
    public GameObject plane;
    public Button view2;
    public Button view3;
    public Camera cam;
    float SpeedCam = 20f;
    public GameObject bullet;
    public Slider slider;

    void speedChange()
    {
        Time.timeScale = slider.value;
    }
    void Start()
    {
        Physics.gravity = new Vector3(0, -PlayerPrefs.GetFloat("g"), 0); ;
        float UseGravity = PlayerPrefs.GetFloat("UseGravity");
        PlayerPrefs.SetInt("view", 2);
        float PositionXPlane = PlayerPrefs.GetFloat("PositionXPlane");
        print(PositionXPlane);
        cannon.transform.position = new Vector3(0, 0, 0);
        view2.onClick.AddListener(delegate { PlayerPrefs.SetInt("view", 2); });
        view3.onClick.AddListener(delegate { Cursor.visible = false; Cursor.lockState = CursorLockMode.Locked; PlayerPrefs.SetInt("view", 3); });
        
    }
    private void Update()
    {
        speedChange();
        if (Input.GetKey(KeyCode.Escape)) { Cursor.visible = true; Cursor.lockState = CursorLockMode.None; SceneManager.LoadScene("Menu"); }
        int view = PlayerPrefs.GetInt("view");
        if (view == 2)
        {
            cam.transform.position = new Vector3(0, 1, -1.19f);
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            /*float speed = 20f;
            if (Input.GetKey(KeyCode.A))
            {
                cam.transform.position += Vector3.left * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                cam.transform.position += Vector3.right * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                cam.transform.position += Vector3.forward * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                cam.transform.position += Vector3.back * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                cam.transform.position += Vector3.up * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                print(22);
                cam.transform.position += Vector3.down * Time.deltaTime * speed;
            }
            /*if (Input.GetKey(KeyCode.W)) { cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + Time.deltaTime * SpeedCam); }
            if (Input.GetKey(KeyCode.S)) { cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z - Time.deltaTime * SpeedCam); }
            if (Input.GetKey(KeyCode.D)) { cam.transform.position = new Vector3(cam.transform.position.x + SpeedCam * Time.deltaTime, cam.transform.position.y, cam.transform.position.z) ; }
            if (Input.GetKey(KeyCode.A)) { cam.transform.position = new Vector3(cam.transform.position.x - SpeedCam * Time.deltaTime, cam.transform.position.y, cam.transform.position.z); }
            if (Input.GetKey(KeyCode.Space)) { cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + SpeedCam * Time.deltaTime, cam.transform.position.z); }
            if (Input.GetKey(KeyCode.LeftShift)) { cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - SpeedCam * Time.deltaTime, cam.transform.position.z); }
            
            float mouseSpeed = 100f;
            float MouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
            float MouseY = -Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
            cam.transform.rotation *= Quaternion.Euler(MouseY, MouseX, 0);
            */
        }
    }
}
