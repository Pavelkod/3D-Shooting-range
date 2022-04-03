using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _fireRate = .1f;
    [SerializeField] private float _bulletStartVelocity;
    private ObjectPool<Bullet> _bulletsPool;

    private void Awake()
    {
        _bulletsPool = new ObjectPool<Bullet>(() => Instantiate(_bulletPrefab), x =>
        {
            x.gameObject.SetActive(true);
            x.Init(_bulletStartVelocity, _bulletSpawnPoint, () => _bulletsPool.Release(x));
        }, null, 25);
        GameManager.OnGameStateChange += OnGameState;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameState;
    }

    private void OnGameState(GamesStateId stateId)
    {
        if (stateId == GamesStateId.Lose || stateId == GamesStateId.Win)
            StopAllCoroutines();
    }

    private void OnFire(InputValue inputValue)
    {
        if (inputValue.isPressed) StartCoroutine(Fire());
        else StopAllCoroutines();
    }

    IEnumerator Fire() //Auto fire until stop coroutine
    {
        while (true)
        {
            _bulletsPool.Get();
            yield return new WaitForSeconds(_fireRate);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_bulletSpawnPoint.position, .3f);
        Gizmos.DrawRay(_bulletSpawnPoint.position, _bulletSpawnPoint.forward);
    }
}
