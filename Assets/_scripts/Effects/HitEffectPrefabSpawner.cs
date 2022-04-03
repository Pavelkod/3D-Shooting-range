using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectPrefabSpawner : EffectPrefabSpawner, IHitEffector
{
    public void Run(Transform transform, Collision collision)
    {
        Run(transform);
    }
}
