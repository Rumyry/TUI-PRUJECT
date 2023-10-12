using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject cannon;
    public GameObject view;
    public GameObject plane;
    public float cannonX, cannonY, cannonZ;

    void Start()
    {
        PlayerPrefs.SetInt("cnt", 0);
        cannon.transform.position = new Vector3(cannonX, cannonY, cannonZ);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { SceneManager.LoadScene("Menu"); }
    }
}
