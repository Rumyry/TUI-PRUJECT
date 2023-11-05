using UnityEngine;
using System.Collections;

public class CamCntrl : MonoBehaviour
{


    public float speed = 70.0f;
    public float sensitivity = 5.0f;

    private void Start()
    {
        Cursor.visible = true;
    }
    
    void ChangeCursorState()
    {
        
        Cursor.visible = !Cursor.visible;
        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            ChangeCursorState();
        }
        if (Cursor.visible)
        {
            return;
        }
        // Move the camera forward, backward, left, and right
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // Rotate the camera based on the mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);
    }

}