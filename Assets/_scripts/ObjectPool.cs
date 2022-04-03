using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _readyObjects = new List<T>();
    private List<T> _notReadyObjects = new List<T>();
    private int _maxSize = 10;

    private Func<T> _onCreate;
    private Action<T> _onGet;
    private Action<T> _onRelease;
    private float _destroyDelay = 1f;

    public ObjectPool(Func<T> onCreate, Action<T> onGet, Action<T> onRelease)
    {
        _onCreate = onCreate;
        _onGet = onGet;
        _onRelease = onRelease;
    }

    public ObjectPool(Func<T> onCreate, Action<T> onGet, Action<T> onRelease, int maxSize) : this(onCreate, onGet, onRelease)
    {
        _maxSize = maxSize;
    }

    public T Get()
    {
        var obj = _readyObjects.Count > 0 ? GetReadyObject() : CreateNewObject();
        _onGet?.Invoke(obj);
        return obj;
    }

    private T CreateNewObject()
    {
        var obj = _onCreate.Invoke();
        if (_notReadyObjects.Count < _maxSize) _notReadyObjects.Add(obj);
        return obj;
    }

    private T GetReadyObject()
    {
        var obj = _readyObjects[0];
        _readyObjects.RemoveAt(0);
        _notReadyObjects.Add(obj);
        return obj;
    }

    public void Release(T obj)
    {
        _onRelease?.Invoke(obj);

        if (_notReadyObjects.Remove(obj)) _readyObjects.Add(obj);
        else GameObject.Destroy(obj.gameObject, _destroyDelay);
    }

}
