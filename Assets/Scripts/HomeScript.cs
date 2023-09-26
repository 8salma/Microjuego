using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HomeScript : MonoBehaviour
{

    public GameObject gameOverScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButton()
    {
        Player.SCORE = 0;
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void exitButton()
    {
        Debug.Log("saliendo...");
        Application.Quit();
    }
}
