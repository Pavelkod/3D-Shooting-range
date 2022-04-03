using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantSpawner : MonoBehaviour
{
    [SerializeField] GameObject _persistantObjectPrefab;
    private static bool _isSpawned = false;

    private void Awake()
    {
        if (_isSpawned) return;
        var obj = Instantiate(_persistantObjectPrefab);
        DontDestroyOnLoad(obj);
    }
}
