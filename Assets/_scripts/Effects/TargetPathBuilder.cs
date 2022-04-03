using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetPathBuilder : MonoBehaviour, ITargetPathBuilder
{
    [SerializeField] float _shiftFromTarget = 2f;
    private List<Vector3> _pathPoints = new List<Vector3>();
    public void Init(Vector3 firstPoint)
    {
        _pathPoints.Add(firstPoint);
    }
    public List<Vector3> GetPath(List<Target> targets, int currentIndex, int newIndex)
    {
        if (targets.Count == 0) return _pathPoints;
        _pathPoints.RemoveRange(0, _pathPoints.Count - 1);  // Remove all but not last point
        var min = Mathf.Min(currentIndex, newIndex);
        var max = Mathf.Max(currentIndex, newIndex);
        var len = max - min;
        var _hops = targets.GetRange(min, len + (currentIndex == min ? 1 : 0));
        if (currentIndex == max) _hops.Reverse();
        PopulatePathPoints(_hops);
        return _pathPoints;
    }
    private void PopulatePathPoints(List<Target> points)
    {
        foreach (var point in points)
            _pathPoints.Add(point.transform.position + point.transform.forward * _shiftFromTarget);
    }
}

