using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IHitEffector
{
    [SerializeField] private GameObject _brokenTargetPrefab;
    [SerializeField] float _breakForce = 1f;
    private bool _isBroken = false;

    public void Run(Transform transform, Collision other)
    {
        if (_isBroken) return;
        _isBroken = true;

        var broken = Instantiate(_brokenTargetPrefab, transform.position, transform.rotation);

        var rigidBodies = broken.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in rigidBodies)
        {
            rb.AddExplosionForce(_breakForce, other.contacts[0].point, 3);
        }

        Destroy(gameObject);
    }

}
