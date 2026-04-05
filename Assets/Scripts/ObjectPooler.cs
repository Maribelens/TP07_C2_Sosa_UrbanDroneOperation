using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool = 20;
}

public class ObjectPooler : MonoBehaviour
{
    //[SerializeField] private List<ObjectPoolItem> itemsToPool;
    //[SerializeField] private List<GameObject> instancedObjects = new List<GameObject>();

    //private void Start()
    //{
    //    foreach (ObjectPoolItem item in itemsToPool)
    //    {
    //        for (int i = 0; i < item.amountToPool; i++)
    //        {
    //            GameObject obj = Instantiate(item.objectToPool);
    //            obj.SetActive(false);
    //            instancedObjects.Add(obj);
    //        }
    //    }
    //}

    //public GameObject GetPoolObject(string tag)
    //{

    //    for (int i = 0; i < instancedObjects.Count; i++)
    //    {
    //        if (!instancedObjects[i].activeInHierarchy && instancedObjects[i].CompareTag(tag))
    //            instancedObjects[i].SetActive(true);
    //            return instancedObjects[i];
    //    }

    //    foreach (ObjectPoolItem item in itemsToPool)
    //    {
    //        if (item.objectToPool.CompareTag(tag))
    //        {
    //            GameObject obj = Instantiate(item.objectToPool);
    //            obj.SetActive(true);
    //            instancedObjects.Add(obj);
    //            return obj;
    //        }
    //    }
    //    return null;
    //}
    //public void ReturnToPool(GameObject obj)
    //{
    //    obj.SetActive(false);
    //}
}
