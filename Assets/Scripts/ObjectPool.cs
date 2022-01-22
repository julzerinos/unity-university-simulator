using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    List<T> Pool = new List<T>();
    T Prefab;

    public ObjectPool(int counter, T prefab)
    {
        Prefab = prefab;
        for (int i = 0; i < counter; i++)
        {
            AddObjectToPool(prefab);
        }
    }

    public T GetObject()
    {
        foreach (var prefab in Pool)
        {
            if (!prefab.gameObject.activeSelf)
            {
                return prefab;
            }
        }
        return AddObjectToPool(Prefab);
    }

    private T AddObjectToPool(T prefab)
    {
        var inst = GameObject.Instantiate(prefab);
        inst.gameObject.SetActive(false);
        Pool.Add(inst);
        return inst;
    }
}
