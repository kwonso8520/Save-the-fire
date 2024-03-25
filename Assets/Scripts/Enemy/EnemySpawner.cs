using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float _respawnTime;
    [SerializeField]
    float _enemySpeed = 5f;
    private Transform _target;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    void OnEnable()
    {
        _target = FindFirstObjectByType<PlayerController>().transform;
    }

    void Update()
    {
       
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int ran = Random.Range(0, 360); //랜덤으로 0~360도
            float x = Mathf.Cos(ran * Mathf.Deg2Rad) * 4.3f; // 정해진 위치에서 5만큼 떨어진 원형 랜덤 방향으로 생성
            float y = Mathf.Sin(ran * Mathf.Deg2Rad) * 4.3f; // 정해진 위치에서 5만큼 떨어진 원형 랜덤 방향으로 생성
            Vector3 pos = transform.position + new Vector3(x, y, 0);
            var EnemyObj = EnemyPool.GetObject();
            EnemyObj.GetComponent<Transform>().position = pos;
            EnemyObj.GetComponent<Rigidbody2D>().AddForce((_target.position - EnemyObj.transform.position).normalized * _enemySpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
}
