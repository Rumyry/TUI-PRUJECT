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

    void Start()
    {
        cannon.transform.position = new Vector3(0, 0, 0);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { SceneManager.LoadScene("Menu"); }
    }
}
