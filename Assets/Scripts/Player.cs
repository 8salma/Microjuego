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



    public GameObject gun, bulletPrefab;

    public static int SCORE = 0;

    void Start()
    {
        // rigidbody nos permite aplicar fuerzas en el jugador
        _rigidbody = GetComponent<Rigidbody>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet balaScript = bullet.GetComponent<Bullet>();

            balaScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }
}
