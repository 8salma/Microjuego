using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private int amountPool = 100;

    [SerializeField] private GameObject bulletPrefab;

    public static ObjectPool instance;
    public static ObjectPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeSelf)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }
}
