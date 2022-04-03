using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTargetMover : VerticalTargetMover
{
    protected override void Init()
    {
        var amplitude = Random.Range(_maxAmplitude / 2, _maxAmplitude);
        amplitude *= Random.value > .5 ? -1 : 1;
        var cam = Utils.Cam().transform;

        _targetPositon = _movedObj.position + Vector3.right * amplitude;

        var lookVector = _targetPositon - cam.position;

        _targetPositon += lookVector.normalized * (_distanceFromCamera / lookVector.magnitude);

        _isInitialized = true;
    }
}
