using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEntry<T>
{
    public T Value;
}

public class PoolGeneric<T> : MonoBehaviour
{
    private List<PoolEntry<T>> availables = new List<PoolEntry<T>>();
    private List<PoolEntry<T>> inUse = new List<PoolEntry<T>>();

    public int AvailablesCount => availables.Count;

    public PoolEntry<T> GetorCreate()
    {
        if (availables.Count > 0)
        {
            var obj = availables[0];
            availables.RemoveAt(0);
            inUse.Add(obj);
            return obj;
        }

        var newObj = new PoolEntry<T>();
        inUse.Add(newObj);
        return newObj;
    }

    public void AddPool(PoolEntry<T> poolEntry)
    {
        inUse.Remove(poolEntry);
        availables.Add(poolEntry);
    }
}
