using UnityEngine;

public class EffectPrefabSpawner : MonoBehaviour
{
    [SerializeField] GameObject _hitEffectPrefab;

    protected virtual Vector3 GetPosition(Transform transform)
    {
        return transform.position;
    }

    protected virtual Quaternion GetRotation(Transform transform)
    {
        return transform.rotation;
    }

    public virtual void Run(Transform transform)
    {
        var obj = Instantiate(_hitEffectPrefab, GetPosition(transform), GetRotation(transform));

    }

    protected virtual void OnSpawned(GameObject obj) { }

}
