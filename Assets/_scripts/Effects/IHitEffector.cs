using UnityEngine;

internal interface IHitEffector
{
    void Run(Transform transform, Collision collision);
}