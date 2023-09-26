using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce = 10f;
    public float rotationSpeed = 120f;
    Vector3 thrustDirection;
    Rigidbody _rigidbody;

    float xBorderLimit = 18f;
    float yBorderLimit = 9f;

    public GameObject gameOverScene;
    public GameObject pauseMenu;

    public GameObject gun;

    public static int SCORE = 0;

    private AudioSource sonidoDisparo;
    public AudioSource sonidoGameOver;

    public GameObject gameManager;

    void Start()
    {
        // rigidbody nos permite aplicar fuerzas en el jugador
        _rigidbody = GetComponent<Rigidbody>();
        sonidoDisparo = GetComponent<AudioSource>();
        
    }

    private void FixedUpdate()
    {
        // obtenemos las pulsaciones de teclado
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * thrustForce * Time.deltaTime;

        // la dirección de empuje por defecto es .right (el eje X positivo)
        thrustDirection = transform.right;

        // añadimos la fuerza capturada arriba a la nave del jugador
        _rigidbody.AddForce(thrustForce * thrust * thrustDirection);

        // rotamos con el eje "Rotate" negativo para que la dirección sea correcta
        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);
    }

    private void Update()
    {
        // espacio infinito
        var newPos = transform.position;

        if (newPos.x > xBorderLimit)
        {
            newPos.x = -xBorderLimit + 1;
        }
        else if (newPos.x < -xBorderLimit)
        {
            newPos.x = xBorderLimit - 1;
        }
        else if (newPos.y > yBorderLimit)
        {
            newPos.y = -yBorderLimit + 1;
        }
        else if (newPos.y < -yBorderLimit)
        {
            newPos.y = yBorderLimit - 1;
        }
        transform.position = newPos;

        // disparo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sonidoDisparo.Play();
            GameObject bullet = ObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = gun.transform.position;

                bullet.SetActive(true);

                Bullet balaScript = bullet.GetComponent<Bullet>();

                balaScript.targetVector = transform.right;
            }
        }

        // pausar con escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        // reanudar con escape desde la pantalla de pausa
        /*
        if (pauseMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
        */

    }

    private void OnCollisionEnter(Collision collision)
    {
        // muerte
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Mini"))
        {
            AudioSource collisionAudio = collision.gameObject.GetComponent<AudioSource>();
            collisionAudio.Play();
            SCORE = 0;
            Time.timeScale = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameOverScene.SetActive(true);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }
}