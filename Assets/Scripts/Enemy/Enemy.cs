using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _destroyTime;
    Transform _target;
    private EnemySpawner enemySpawner;
    // Start is called before the first frame update
    private void Awake()
    {
       enemySpawner= FindAnyObjectByType<EnemySpawner>();
    }
    void Start()
    {
       
    }
    private void OnEnable()
    {
        StartCoroutine(EnemyReturn());
        _target = FindAnyObjectByType<PlayerController>().transform;
    }
    
    IEnumerator EnemyReturn()
    {
        yield return new WaitForSeconds(_destroyTime);
        EnemyPool.ReturnObject(this);
    }
}
