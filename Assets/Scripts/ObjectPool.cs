using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ObjectPool<T> where T: IPoolable
{
    private List<T> _activeObjects = new List<T>();
    private List<T> _inActiveObjects = new List<T>();

    public T RequestObject()
    {
        if (_inActiveObjects.Count > 0)
        {
            return ActivateObject(_inActiveObjects[0]);
        }
        else
        {
            return ActivateObject(AddNewObject());
        }
    }

    private T AddNewObject()
    {
        T instance = (T)Activator.CreateInstance(typeof(T));
        _inActiveObjects.Add(instance);
        return instance;
    }
    
    public T ActivateObject(T item)
    {
        item.OnEnabled();
        item.Active = true;

        if (_inActiveObjects.Contains(item))
        {
            _inActiveObjects.Remove(item);
        }

        _activeObjects.Add(item);
        return item;
    }

    public void DeactivateObject(T item)
    {
        if (_activeObjects.Contains(item)) _activeObjects.Remove(item);

        item.OnDisabled();
        item.Active = false;
        _inActiveObjects.Add(item);
    }

    public void DeactivateAll()
    {
        foreach (T item in _activeObjects)
        {
            
            item.Active = false;
            item.OnDisabled();
            _inActiveObjects.Add(item);
        }
        _activeObjects.Clear();
    }
}
