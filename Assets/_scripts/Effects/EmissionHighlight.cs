using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EmissionHighlight : MonoBehaviour, ISelectEffector
{
    [SerializeField] private Animator _animator;
    [SerializeField, Space] private bool _autoDisable = false;
    [SerializeField] private float _highlightDuration = 1;
    private int _highlightHash = Animator.StringToHash("Highlight");
    private int _idleHash = Animator.StringToHash("Idle");


    public void Hide()
    {
        StopAllCoroutines();
        if (!_autoDisable) StartCoroutine(AutoDisableAwaiter());
        else InstantHide();
    }

    private void InstantHide()
    {
        _animator.Play(_idleHash);
    }

    public void Show(Transform transform)
    {
        _animator.Play(_highlightHash);

        StopAllCoroutines();
        if (_autoDisable) StartCoroutine(AutoDisableAwaiter());

    }

    IEnumerator AutoDisableAwaiter()
    {
        yield return new WaitForSeconds(_highlightDuration);
        InstantHide();
    }

}
