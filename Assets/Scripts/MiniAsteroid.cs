using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAsteroid : MonoBehaviour
{
    public float maxLifeTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
