using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 100f;
    [SerializeField] private float _autoRotationSpeed = 1f;
    [SerializeField] private float _autoRotationThreshold = .97f;
    private Vector3 _target = Vector3.zero;
    private Quaternion _autoLookTarget;

    private void Awake()
    {
        GameManager.OnGameStateChange += OnGameState;

    }

    private void OnGameState(GamesStateId obj)
    {
        if (GamesStateId.StartGame == obj)
            StartCoroutine(ShowTarget());
    }
    
    private IEnumerator ShowTarget() // Show first target coroutine
    {
        var target = TargetsManager.Instance.GetCurrentTarget().transform;
        var camTransform = Utils.Cam().transform;
        _autoLookTarget = Quaternion.LookRotation(target.position - camTransform.position);
        do
        {
            var rotation = Quaternion.RotateTowards(transform.rotation, _autoLookTarget, _autoRotationSpeed * Time.deltaTime).eulerAngles;
            _target = rotation;
            _target.z = 0;

            yield return null;
        } while (Vector3.Dot(camTransform.forward, (target.position - camTransform.position).normalized) < _autoRotationThreshold);


        _target = NormalizeRotation(_target);

        GameManager.StartGame();
    }

    private Vector3 NormalizeRotation(Vector3 rotation)
    {
        if (rotation.x > 20) rotation.x -= 360;
        return rotation;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameState;

    }

    private void OnMenu()
    {
        CanvasSwitcher.Instance.GameMenu();
    }

    private void OnLook(InputValue value)
    {
        var mouseCoords = value.Get<Vector2>() * _sensitivity;

        _target.x += mouseCoords.y;
        _target.y += mouseCoords.x;
        _target.x = Mathf.Clamp(_target.x, -90, 20);
    }

    private void Update() => transform.rotation = Quaternion.Euler(_target);


}