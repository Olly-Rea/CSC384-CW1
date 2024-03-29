using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    // Serialiazble 'Pool' class
    [System.Serializable]
    public class Pool {
        // 'Pool' class attributes
        public string tag;
        public GameObject[] prefab;
        public int size;
        public GameObject parentContainer;
    }

    // Create a reference for this ObjectPooler
    public static ObjectPooler Instance;
    void Awake() {
        Instance = this;
    }

    // SerializeField to store the list of Pools
    [SerializeField] List<Pool> pools;

    // Create a dictionary to contain the object pools
    public Dictionary<string, Queue<GameObject>> poolDict;

    // Create the Dictionary
    void Start() {        
        poolDict = new Dictionary<string, Queue<GameObject>>();
        // Seed the dictionary
        foreach(Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            // Seed the current pool
            for(int i = 0; i < pool.size; i++) {
                // Create the game object and make it inactive
                GameObject obj;
                if (pool.prefab.Length > 1) {
                    obj = Instantiate(pool.prefab[Random.Range(0, pool.prefab.Length)]);
                } else {
                    obj = Instantiate(pool.prefab[0]);
                }
                obj.SetActive(false);
                // Add the Object to it's parent container
                obj.transform.SetParent(pool.parentContainer.transform);
                
                // obj.transform.parent = ;

                // Add the object to the queue
                objectPool.Enqueue(obj);
            }
            // Add the seeded pool to the dictionary
            poolDict.Add(pool.tag, objectPool);
        }
    }

    // Method to spawn an object from one of the pools in the dictionary
    public GameObject spawnFromPool(string key, Vector2 position, Quaternion rotation) {
        // Check to ensure the dictionary contains the key being referenced
        if (!poolDict.ContainsKey(key)) {
            Debug.LogWarning("Pool with key " + key + " does not exist!");
            return null;
        } else {
            // Dequeue the next object from the pool
            GameObject toSpawn = poolDict[key].Dequeue();
            // Check that the item is not already active
            if(!toSpawn.activeSelf) {
                // Set the object as active and assign it a position and rotation 
                toSpawn.SetActive(true);
                toSpawn.transform.position = position;
                toSpawn.transform.rotation = rotation;

                // Call on the OnObjectSpawn method of the IPooledObject Interface
                IPooledObject pooled = toSpawn.GetComponent<IPooledObject>();
                if (pooled != null) {
                    pooled.OnObjectSpawn();
                }
            }
            // Add the dequeued object back to the end of the queue
            poolDict[key].Enqueue(toSpawn);
            // Return the object to spawn
            return toSpawn;
        }
    }

    // Method to return the queue of gameObjects
    public Queue<GameObject> getPool(string key) {
        if (!poolDict.ContainsKey(key)) {
            Debug.LogWarning("Pool with key " + key + " does not exist!");
            return null;
        } else {
            return poolDict[key];
        }
    }
    
}
