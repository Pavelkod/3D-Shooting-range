using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] float _keepAliveTimeout = 5f;
    IHitEffector _hitEffect;
    private Action _onDestroyAction;
    private void Awake()
    {
        _hitEffect = GetComponent<IHitEffector>();
    }

    public void Init(float velocity, Transform spawnTransform, Action onDestroyAction)
    {
        _onDestroyAction = onDestroyAction;
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;

        _rb.velocity = transform.forward * velocity;

        StopAllCoroutines();
        StartCoroutine(KeepAlive());
    }
    private void OnCollisionEnter(Collision other)
    {
        _hitEffect?.Run(transform, other);
        Release();
    }

    private void Release()
    {
        _onDestroyAction?.Invoke();
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    IEnumerator KeepAlive()
    {
        yield return new WaitForSeconds(_keepAliveTimeout);
        Release();
    }
}
