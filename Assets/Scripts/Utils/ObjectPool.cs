using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly List<T> _pool = new List<T>();
    private readonly T _prefab;

    public ObjectPool(int counter, T prefab, Transform parent)
    {
        _prefab = prefab;
        for (var i = 0; i < counter; i++)
        {
            AddObjectToPool(prefab, parent);
        }
    }

    public T GetObject()
    {
        foreach (var prefab in _pool)
        {
            if (!prefab.gameObject.activeSelf)
            {
                prefab.gameObject.SetActive(true);
                return prefab;
            }
        }

        return AddObjectToPool(_prefab, _pool[0].transform.parent);
    }

    private T AddObjectToPool(T prefab, Transform parent)
    {
        var inst = Object.Instantiate(prefab, parent);
        inst.gameObject.SetActive(false);
        _pool.Add(inst);
        return inst;
    }
}