using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectPrefabSpawner : EffectPrefabSpawner, ISpawnEffector
{
    [SerializeField] private float _shift;
    protected override Vector3 GetPosition(Transform transform)
    {
        var pos = transform.forward * _shift + transform.position;
        return pos;
    }
}
