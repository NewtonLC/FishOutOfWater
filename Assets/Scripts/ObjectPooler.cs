using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;        // Singleton instance

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;            // Assign the singleton instance to this script
        }
        else
        {
            Destroy(gameObject);        // Destroy any other instances
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools){
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++){
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public void ReturnObjectToPool(GameObject obj){
        obj.SetActive(false);
        poolDictionary[obj.tag].Enqueue(obj);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation){
        if (!poolDictionary.ContainsKey(tag)){
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // Find an inactive object in the pool
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            // If the object is inactive, it's safe to use
            if (!objectToSpawn.activeInHierarchy)
            {
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                poolDictionary[tag].Enqueue(objectToSpawn); // Put it back in the queue
                return objectToSpawn;
            }

            // If it's still active, put it back at the end of the queue
            poolDictionary[tag].Enqueue(objectToSpawn);
        }

        // If no inactive objects are found, return null or handle it (e.g., expand the pool)
        Debug.LogWarning("No available objects in the pool for tag: " + tag);
        return null;
    }
}
