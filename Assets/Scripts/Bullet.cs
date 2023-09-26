using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int speed = 10;
    public float maxLifeTime = 3;
    public Vector3 targetVector;
    public GameObject miniAsteroidPrefab;

    public Vector2 offset = new Vector2(0.1f, 0.1f);

    // angulo con el que se separan los meteoritos
    public float angulo = 45;

    void Start()
    {

    }

    void Update()
    {
        // la bala se mueve en la dirección del jugador al disparar
        transform.Translate(targetVector * speed * Time.deltaTime, Space.World);
    }

    public void IncreaseScore()
    {
        // cuando un asteroide es destruido, llama a esta función para darnos puntos.
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // llamamos a esta función cada vez que ganamos puntos para actualizar el marcador
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // colision bala-enmigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // posicion donde colisiona la bala
            Vector2 balaPos = gameObject.transform.position;

            // posicion del meteorito como vector2
            Vector2 enemyPos = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);

            // vector direccion de la bala
            Vector2 direction = (enemyPos - balaPos).normalized;

            // rotacion
            Vector2 rotatedDirection1 = Quaternion.Euler(0, 0, angulo * Mathf.Rad2Deg) * direction;
            Vector2 rotatedDirection2 = Quaternion.Euler(0, 0, -angulo * Mathf.Rad2Deg) * direction;

            // posiones de los mini asteroides
            Vector2 pos1 = balaPos + rotatedDirection1 * 0.5f + offset;
            Vector2 pos2 = balaPos + rotatedDirection2 * 0.5f - offset;

            // creamos los dos mini asteroides
            GameObject mini1 = Instantiate(miniAsteroidPrefab, new Vector3(pos1.x, pos1.y, 0), Quaternion.identity);
            GameObject mini2 = Instantiate(miniAsteroidPrefab, new Vector3(pos2.x, pos2.y, 0), Quaternion.identity);

            // incrementar puntuación
            IncreaseScore();

            // Destruye el enemigo y desactiva la bala
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }

        // colision con un mini-asteroide
        if (collision.gameObject.CompareTag("Mini"))
        {
            IncreaseScore();
            IncreaseScore();
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
