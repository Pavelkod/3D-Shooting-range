using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTargetMoverComposite : MonoBehaviour, ITargetMover
{
    [SerializeField] private GameObject _targetMoversObject;
    private ITargetMover[] _movers;
    private int _activaMoverIndex = -1;

    public void Activate(bool acivate)
    {
        if (_activaMoverIndex > -1) _movers[_activaMoverIndex].Activate(acivate);
    }

    private void Start()
    {
        _movers = _targetMoversObject.GetComponents<ITargetMover>();
        _activaMoverIndex = Random.Range(0, _movers.Length);
    }
}
