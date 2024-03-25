using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool _instance;

    [SerializeField]
    private GameObject _poolingObjectPrefab;

    Queue<Enemy> _poolingObjectQueue = new Queue<Enemy>();

    private void Awake()
    {
        _instance = this;

        Initialize(50);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            _poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Enemy CreateNewObject()
    {
        
        var newObj = Instantiate(_poolingObjectPrefab).GetComponent<Enemy>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Enemy GetObject()
    {
        if (_instance._poolingObjectQueue.Count > 0)
        {
            var obj = _instance._poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = _instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Enemy obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_instance.transform);
        _instance._poolingObjectQueue.Enqueue(obj);
        obj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
