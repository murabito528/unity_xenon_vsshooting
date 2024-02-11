using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    //ObjectPool<GameObject> pool;
    List<ObjectPool<GameObject>> pools;

    public GameObject Prefab { get; private set; }

    public List<GameObject> bullet_Prefabs;
    //public GameObject bullet1_Prefab;
    //public GameObject bullet2_Prefab;

    void Awake()
    {
        //pool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
        pools = new List<ObjectPool<GameObject>>();

        for(int i = 0; i < bullet_Prefabs.Count; i++)
        {
            pools.Add(new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject));
        }
    }

    GameObject OnCreatePooledObject()
    {
        var gameObject = Instantiate(Prefab);
        return gameObject;
    }

    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetGameObject(int bullet_id, Vector3 position, Quaternion rotation)
    {
        Prefab = bullet_Prefabs[bullet_id];
        GameObject obj = pools[bullet_id].Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;

        return obj;
    }

    public void ReleaseGameObject(GameObject obj,int bullet_id)
    {
        pools[bullet_id].Release(obj);
    }
}
