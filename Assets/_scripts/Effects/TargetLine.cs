using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TargetLine : MonoBehaviour
{
    [SerializeField] private List<Vector3> _points;
    [SerializeField] private float _curveStep = .1f;
    [SerializeField] private LineRenderer _renderer;
    private void Init()
    {
        var path = TargetsManager.Instance.GetPath();
        if (path == null) return;

        CameraPriximityFix(path);

        _points = path;
        BuildCurve();
    }

    private void CameraPriximityFix(List<Vector3> points)
    {
        if (points.Count < 2)
        {
            points.Insert(1, Utils.Cam().transform.position + Vector3.up * 10);
            return;
        }

        var cam = Utils.Cam().transform;

        for (int i = 1; i < points.Count; i++)
        {
            var dot = Vector3.Dot((points[i - 1] - cam.position).normalized, (points[i] - cam.position).normalized);
            if (dot < -.5) points.Insert(i, cam.position + Vector3.up * 10);
            i++;
        }
    }

    private void Awake()
    {
        GameManager.OnGameStateChange += OnGameState;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameState;

    }

    private void OnGameState(GamesStateId obj)
    {
        if (GamesStateId.NextTarget == obj)
        {
            Init();
        }
        else
        {
            ClearCurve();
        }
    }

    private void BuildCurve()
    {
        CubicBezierPath path = new CubicBezierPath(_points.ToArray());
        float time = 0;
        List<Vector3> bezierPoints = new List<Vector3>();
        float maxTime = path.GetNumCurveSegments();
        while (time < maxTime)
        {
            var point = path.GetPoint(time);
            bezierPoints.Add(point);
            time = Mathf.MoveTowards(time, maxTime, _curveStep);
        }
        _renderer.positionCount = bezierPoints.Count;
        _renderer.SetPositions(bezierPoints.ToArray());
    }

    private void ClearCurve()
    {
        _renderer.positionCount = 0;
    }


}