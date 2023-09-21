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

    public GameObject gun, bulletPrefab;

    public static int SCORE = 0; // Define la variable SCORE como estática.

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
        


        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet balaScript = bullet.GetComponent<Bullet>();

            balaScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }
}
