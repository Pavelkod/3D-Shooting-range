using System.Collections.Generic;
using UnityEngine;

public interface ITargetPathBuilder
{
    void Init(Vector3 firstPoint);
    List<Vector3> GetPath(List<Target> targets, int currentIndex, int newIndex);
}

