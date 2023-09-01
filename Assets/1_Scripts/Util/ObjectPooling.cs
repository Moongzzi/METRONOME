using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPooling : MonoBehaviour
{
    public ObjectPoolClass[] poolingObjects;
    public List<GameObject>[] pooledObjects;
    private int defaultPoolAmount = 10;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        pooledObjects = new List<GameObject>[poolingObjects.Length];

        for (int i = 0; i < poolingObjects.Length; i++)
        {
            pooledObjects[i] = new List<GameObject>();

            int poolingAmount;
            if (poolingObjects[i].objectCount > 0) poolingAmount = poolingObjects[i].objectCount;
            else poolingAmount = defaultPoolAmount;

            for (int j = 0; j < poolingAmount; j++)
            {
                GameObject newItem = (GameObject)Instantiate(poolingObjects[i].prefab);
                newItem.SetActive(false);
                pooledObjects[i].Add(newItem);
                newItem.transform.SetParent(transform);
            }
        }
    }

    public GameObject Spawn(string _myObjectName)
    {
        GameObject newObject = GetPooledItem(_myObjectName);

        if (newObject != null)
        {
            newObject.SetActive(true);
            return newObject;
        }
        return null;
    }

    public GameObject Spawn(string _myObjectName, Transform _parentsTransform, Vector3 _setPos)
    {
        GameObject newObject = GetPooledItem(_myObjectName);
        if (_parentsTransform == null)
            newObject.transform.SetParent(transform);
        else
            newObject.transform.SetParent(_parentsTransform);
        newObject.transform.position = _setPos;
        if (newObject != null)
        {
            newObject.SetActive(true);
            return newObject;
        }
        return null;
    }

    public T Spawn<T>(string tag) where T : Component
    {
        GameObject newObject = GetPooledItem(tag);
        if (newObject == null)
        {
            Debug.LogError($"ObjectPool : 해당 오브젝트가 없습니다. [{tag}]");
            return null;
        }
        T component = newObject.GetComponent<T>();
        if (component != null)
        {
            newObject.SetActive(true);
            return component;
        }
        else
        {
            newObject.SetActive(false);
            throw new Exception($"Component not found");
        }
    }

    public T Spawn<T>(string tag, Transform _parentsTransform, Vector3 _setPos) where T : Component
    {
        GameObject newObject = GetPooledItem(tag);
        newObject.transform.SetParent(_parentsTransform);
        newObject.transform.position = _setPos;
        T component = newObject.GetComponent<T>();
        if (component != null)
        {
            newObject.SetActive(true);
            return component;
        }
        else
        {
            newObject.SetActive(false);
            throw new Exception($"Component not found");
        }
    }

    public void Despawn(GameObject myObject)
    {
        myObject.SetActive(false);
        myObject.transform.SetParent(transform);
    }

    private GameObject GetPooledItem(string _myObjectName)
    {
        for (int i = 0; i < poolingObjects.Length; i++)
        {
            if (poolingObjects[i].prefab.name == _myObjectName)
            {
                for (int j = 0; j < pooledObjects[i].Count; j++)
                {
                    if (!pooledObjects[i][j].activeSelf)
                    {
                        return pooledObjects[i][j];
                    }
                }

                GameObject newItem = (GameObject)Instantiate(poolingObjects[i].prefab);
                newItem.SetActive(false);
                pooledObjects[i].Add(newItem);
                newItem.transform.SetParent(transform);
                return newItem;
            }
        }

        return null;
    }
}
