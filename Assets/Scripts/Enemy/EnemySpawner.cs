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
            int ran = Random.Range(0, 360); //�������� 0~360��
            float x = Mathf.Cos(ran * Mathf.Deg2Rad) * 4.3f; // ������ ��ġ���� 5��ŭ ������ ���� ���� �������� ����
            float y = Mathf.Sin(ran * Mathf.Deg2Rad) * 4.3f; // ������ ��ġ���� 5��ŭ ������ ���� ���� �������� ����
            Vector3 pos = transform.position + new Vector3(x, y, 0);
            var EnemyObj = EnemyPool.GetObject();
            EnemyObj.GetComponent<Transform>().position = pos;
            EnemyObj.GetComponent<Rigidbody2D>().AddForce((_target.position - EnemyObj.transform.position).normalized * _enemySpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
}
