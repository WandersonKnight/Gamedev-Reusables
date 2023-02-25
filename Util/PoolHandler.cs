using System.Collections.Generic;
using UnityEngine;

public class PoolHandler : MonoBehaviour
{
    public List<GameObject> CreatePool(GameObject originalObject, int size)
    {
        List<GameObject> objectPool = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject newObjectInstance = Instantiate(originalObject);
            newObjectInstance.SetActive(false);
            objectPool.Add(newObjectInstance);

        }

        return objectPool;
    }

    public GameObject GetPool(List<GameObject> objectPool)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeSelf)
            {
                return objectPool[i];
            }
        }

        return null;
    }
}
