using System.Collections.Generic;
using UnityEngine;

public delegate T CreatePooledMonobehaviourDelegate<T>() where T : MonoBehaviour;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;
    private CreatePooledMonobehaviourDelegate<T> _createDelegate;

    public ObjectPool(CreatePooledMonobehaviourDelegate<T> createDelegate)
    {
        _pool = new List<T>();
        _createDelegate = createDelegate;
    }

    public T GetFromPool()
    {
        if(!TryGetFromPool(out T pooled))
        {
            T obj = _createDelegate?.Invoke();
            _pool.Add(obj);
            return obj;
        }

        return pooled;
    }

    private bool TryGetFromPool(out T obj)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].gameObject.activeSelf)
            {
                obj = _pool[i];
                obj.gameObject.SetActive(true);
                return true;
            }
        }

        obj = null;
        return false;
    }
}