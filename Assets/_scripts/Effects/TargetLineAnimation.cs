using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLineAnimation : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private float _animationSpeed = .1f;
    [SerializeField] private float[] _animationPhases = new float[] { 0, .4f, .6f, 1 };
    [SerializeField] private float _animationDuration = 3f;
    private bool _isAnimationStarted = false;
    private float _current = 0;
    private GradientAlphaKey[] _alphaKeys;
    private Gradient _curveGradient;

    private void Awake()
    {
        _alphaKeys = new GradientAlphaKey[4];
        _curveGradient = _renderer.colorGradient;
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
            InitAnimation();
            _isAnimationStarted = true;
        }
        else
            _isAnimationStarted = false;

    }

    private void Start()
    {
        InitAnimation();
    }

    private void InitAnimation()
    {
        _current = 0f;
        _alphaKeys[0] = new GradientAlphaKey(0, 0);
        _alphaKeys[1] = new GradientAlphaKey(1, 0);
        _alphaKeys[2] = new GradientAlphaKey(1, 0);
        _alphaKeys[3] = new GradientAlphaKey(0, 0);
        _curveGradient.alphaKeys = _alphaKeys;
    }

    private float GetNormalizedTime(int phase) => (_current - _animationDuration * _animationPhases[phase]) / (_animationDuration * (_animationPhases[phase + 1] - _animationPhases[phase]));

    private void Update()
    {
        if (!_isAnimationStarted) return;
        if (_current == _animationDuration) _current = 0;
        _current = Mathf.MoveTowards(_current, _animationDuration, _animationSpeed * Time.deltaTime);

        var time = Mathf.Clamp(GetNormalizedTime(0), 0f, 1f);
        _alphaKeys[2].time = time;
        _alphaKeys[3].time = time + .01f;
        _curveGradient.alphaKeys = _alphaKeys;
        _renderer.colorGradient = _curveGradient;

        time = Mathf.Clamp(GetNormalizedTime(2), 0f, 1f);
        _alphaKeys[0].time = time;
        _alphaKeys[1].time = time + .01f;
        _curveGradient.alphaKeys = _alphaKeys;
        _renderer.colorGradient = _curveGradient;
    }
}
