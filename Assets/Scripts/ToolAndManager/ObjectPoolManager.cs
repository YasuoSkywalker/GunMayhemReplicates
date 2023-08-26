using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    //取对象
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnQuaternion)
    {
        //找到该对象对应的池
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        //如果该对象还没被池管理：创建一个池
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        //获取池中的第一个对象
        GameObject spawnableObj = pool.inactiveObjects.FirstOrDefault();

        //如果池中没有对象：创建一个
        //否则取出一个对象
        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnQuaternion);
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnQuaternion;
            pool.inactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    //归还对象
    public static void ReturnObjectToPool(GameObject obj)
    {
        //获取对象名字并处理：如果是clone出来的，去除掉clone的字样
        string goName = obj.name;
        if (goName.EndsWith("(Clone)")) goName = goName.Substring(0, goName.Length - 7);

        //获取对应的池
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        //没有对应的池子：Debuglog错误
        //否则归还对象（入池子即入List）
        if (pool == null)
        {
            Debug.LogWarning(goName + " is not belone to any pool!");
        }
        else
        {
            obj.SetActive(false);
            pool.inactiveObjects.Add(obj);
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString; //对象池的名字
    public List<GameObject> inactiveObjects = new List<GameObject>(); //对象list
}
