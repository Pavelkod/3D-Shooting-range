using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class DotProductTargetSelector : MonoBehaviour, ITargetSelector
{
    [SerializeField] float _threshold = .7f;
    public Target GetTarget(List<Target> targets)
    {
        if (targets.Count == 0) return null;
        var ray = Utils.Cam().ScreenPointToRay(Mouse.current.position.ReadValue());
        var item = targets.Where(x =>
        {
            return Vector3.Dot((x.transform.position - ray.origin).normalized, ray.direction.normalized) < _threshold;
        }).OrderBy(x => Random.value).FirstOrDefault();

        if (item == null)
            item = targets.OrderBy(x => Random.value).FirstOrDefault();

        return item;
    }

}

