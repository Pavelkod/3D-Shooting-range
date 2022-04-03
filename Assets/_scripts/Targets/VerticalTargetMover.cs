using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTargetMover : MonoBehaviour, ITargetMover
{
    [SerializeField] protected float _moveSpeed = 1f;
    [SerializeField] protected AnimationCurve _curve;
    [SerializeField] protected Transform _movedObj;
    protected bool _isActive = false;
    protected float _current = 0f, _target = 1f;
    protected float _maxAmplitude = 10f;
    protected float _distanceFromCamera = 0f;
    protected Vector3 _startPosition, _targetPositon;
    protected bool _isInitialized = false;

    private void Start()
    {
        _distanceFromCamera = (_movedObj.position - Utils.Cam().transform.position).magnitude;
        _startPosition = _movedObj.position;
    }


    private void Update()
    {
        if (!_isActive) return;
        _current = Mathf.MoveTowards(_current, _target, _moveSpeed * Time.deltaTime);
        if (_target == _current) _target = _current == 0 ? 1 : 0;
        var curveTime = _curve.Evaluate(_current);
        _movedObj.position = Vector3.Lerp(_startPosition, _targetPositon, curveTime);
        _movedObj.rotation = Quaternion.LookRotation(_movedObj.position - Utils.Cam().transform.position);
    }

    public void Activate(bool acivate)
    {
        if (!_isInitialized) Init();
        _isActive = acivate;
    }

    protected virtual void Init()
    {
        var amplitude = Random.Range(_maxAmplitude / 2, _maxAmplitude);
        amplitude *= _movedObj.position.y > _maxAmplitude ? -1 : 1;
        var cam = Utils.Cam().transform;

        _targetPositon = _movedObj.position + Vector3.up * amplitude;

        var lookVector = _targetPositon - cam.position;

        _targetPositon += lookVector.normalized * (_distanceFromCamera / lookVector.magnitude);

        _isInitialized = true;
    }
}
