using UnityEngine;

public interface IPooledObject {
    // Required method for classes that inherit from this interface
    void OnObjectSpawn();
}
