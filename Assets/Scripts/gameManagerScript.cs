using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject homeMenu;
    public GameObject gameOverScene;
    private AudioSource sonidoGameOver;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(pauseMenu.activeSelf);
    }

    public void pauseButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void restartButton()
    {
        pauseMenu.SetActive(false);
        //gameOverScene.SetActive(false);
        Player.SCORE = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void reintentarButton()
    {
        gameOverScene.SetActive(false);
        Player.SCORE = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void homeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void playButton()
    {
        Debug.Log("caracol");
        homeMenu.SetActive(false);
        Player.SCORE = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void exitButton()
    {
        Debug.Log("me meti");
        Application.Quit();
    }
}