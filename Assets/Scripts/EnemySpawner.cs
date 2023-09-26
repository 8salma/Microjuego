using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    
    float spawnRatePerMinute = 30f;
    float spawnRateIncrement = 1f;

    float xBorderLimit = 14f;
    float yBorderLimit;
    
    private float spawnNext = 0;

    public float maxLifeTime = 4f;

    void Update()
    {
        // instanciamos enemigos sólo si ha pasado tiempo suficiente desde el último.
        if (Time.time > spawnNext)
        {
            // indicamos cuándo podremos volver a instanciar otro enemigo
            spawnNext = Time.time + 60 / spawnRatePerMinute;

            // con cada spawn hay mas asteroides por minuto para incrementar la dificultad
            spawnRatePerMinute += spawnRateIncrement;

            // guardamos un punto aleatorio entre las esquinas superiores de la pantalla
            float rand = Random.Range(-xBorderLimit, xBorderLimit);
            Vector2 spawnPosition = new Vector2(rand, 13f);

            // instanciamos el asteroide en el punto y con el ángulo aleatorios
            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            Destroy(meteor, maxLifeTime);
        }
    }
}
