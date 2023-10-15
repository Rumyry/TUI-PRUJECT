using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menucntrl : MonoBehaviour
{
    public Button PlayBtn;
    public Button QuitBtn;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("cnt", 0);
        PlayerPrefs.SetFloat("speedBullet", 100f);
        PlayerPrefs.SetFloat("speedCannon", 3f);
        PlayerPrefs.SetFloat("speedPlane", 20f);
        print("Start");
        PlayBtn.onClick.AddListener(delegate { LoadScene("Game"); });
        QuitBtn.onClick.AddListener(delegate { quit(); });
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void quit()
    {
        print("quit");
        Application.Quit();
    }
    void LoadScene(string n)
    {
        print(n);
        SceneManager.LoadScene(n);
    }
}
