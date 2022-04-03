using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITarget
{
    public bool IsActive => _isActive;
    private ISpawnEffector _spawnEffector;
    private IHitEffector _hitEffector;
    private ISelectEffector _selectEffector;
    private ITargetMover _mover;
    private float _visibilityThreshold = .8f;
    private bool _isActive = false;
    private void Awake()
    {
        _spawnEffector = GetComponent<ISpawnEffector>();
        _hitEffector = GetComponent<IHitEffector>();
        _selectEffector = GetComponent<ISelectEffector>();
        _mover = GetComponent<ITargetMover>();
    }


    private void OnEnable()
    {
        _spawnEffector?.Run(transform);
    }

    private IEnumerator VisibilityCheck()
    {
        float dot;
        var camTransform = Utils.Cam().transform;
        var targetVector = transform.position - camTransform.position;
        do
        {
            dot = Vector3.Dot(camTransform.forward, targetVector.normalized);
            yield return null;
        } while (dot < _visibilityThreshold);
        _selectEffector?.Hide();
        GameManager.Next();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (_isActive)
        {
            _hitEffector?.Run(transform, other);
            TargetsManager.Instance.RemoveTarget(this);
            GameManager.HitTarget();
        }

    }

    public void Select()
    {
        _isActive = true;
        _selectEffector?.Show(transform);
        StartCoroutine(VisibilityCheck());
    }

    public void Deselect()
    {
        _isActive = false;
        _selectEffector?.Hide();
        StopAllCoroutines();
    }

    public void ActivateMovement(bool activate) => _mover?.Activate(activate);

}