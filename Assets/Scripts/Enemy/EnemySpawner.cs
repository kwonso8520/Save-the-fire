using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float _enemySpeed = 5f;
    private Transform _target;
    public bool isOrganization;
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    void OnEnable()
    {
        _target = FindFirstObjectByType<PlayerController>().transform;
    }

    IEnumerator SpawnEnemy()
    {
        int patternNum = Random.Range(0, 4);
        switch (patternNum)
        {
            case 0:
                isOrganization = false;
                StartCoroutine(PatternOne());
                break;
            case 1:
                isOrganization = false;
                StartCoroutine(PatternTwo());
                break;
            case 2:
                isOrganization = false;
                StartCoroutine(PatternThree());
                break;
            case 3:
                isOrganization = true;
                StartCoroutine(PatternFour());
                break;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator PatternOne()
    {
        int ran = Random.Range(0, 360); //랜덤으로 0~360도
        float x = Mathf.Cos(ran * Mathf.Deg2Rad) * 4.3f; // 정해진 위치에서 5만큼 떨어진 원형 랜덤 방향으로 생성
        float y = Mathf.Sin(ran * Mathf.Deg2Rad) * 4.3f; // 정해진 위치에서 5만큼 떨어진 원형 랜덤 방향으로 생성
        Vector3 pos = transform.position + new Vector3(x, y, 0);

        var EnemyObj = EnemyPool.GetObject();
        EnemyObj.GetComponent<Transform>().position = pos;
        Vector2 direction = _target.position - EnemyObj.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        EnemyObj.transform.rotation = angleAxis;

        EnemyObj.GetComponent<Rigidbody2D>().AddForce((_target.position - EnemyObj.transform.position).normalized * _enemySpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
    }
    IEnumerator PatternTwo()
    {
        int ran = Random.Range(0, 360); //랜덤으로 0~360도
        float x = Mathf.Cos(ran * Mathf.Deg2Rad) * 4.3f; // 정해진 위치에서 5만큼 떨어진 원형 랜덤 방향으로 생성
        float y = Mathf.Sin(ran * Mathf.Deg2Rad) * 4.3f; // 정해진 위치에서 5만큼 떨어진 원형 랜덤 방향으로 생성
        Vector3 pos = transform.position + new Vector3(x, y, 0);

        for (int i = 0; i < 5; i++)
        {
            var EnemyObj = EnemyPool.GetObject();
            EnemyObj.GetComponent<Transform>().position = pos;

            Vector2 direction = _target.position - EnemyObj.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            EnemyObj.transform.rotation = angleAxis;

            EnemyObj.GetComponent<Rigidbody2D>().AddForce((_target.position - EnemyObj.transform.position).normalized * _enemySpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator PatternThree()
    {
        for (int i = 1; i <= 8; i++)
        {
            float x = Mathf.Cos((360 / i) * Mathf.Deg2Rad) * 4.3f; // 정해진 위치에서 5만큼 떨어진 원형 랜덤 방향으로 생성
            float y = Mathf.Sin((360 / i) * Mathf.Deg2Rad) * 4.3f; // 정해진 위치에서 5만큼 떨어진 원형 랜덤 방향으로 생성
            Vector3 pos = transform.position + new Vector3(x, y, 0);
            var EnemyObj = EnemyPool.GetObject();
            EnemyObj.GetComponent<Transform>().position = pos;

            Vector2 direction = _target.position - EnemyObj.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            EnemyObj.transform.rotation = angleAxis;

            EnemyObj.GetComponent<Rigidbody2D>().AddForce((_target.position - EnemyObj.transform.position).normalized * _enemySpeed, ForceMode2D.Impulse);
        }
        yield return null;
    }
    IEnumerator PatternFour()
    {
        int x;
        int y;
        do
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        } while(x ==0  && y ==0);
        
        Vector3 pos = transform.position + new Vector3(x, y, 0).normalized * 4.3f;
       
        Vector2 direction = new Vector2(pos.x - 0, pos.y - 0);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        for(int i = -3; i < 4; i++)
        {
            var EnemyObj = EnemyPool.GetObject();
            EnemyObj.transform.rotation = angleAxis;
            if (x == 0 || y == 0)
            {
                EnemyObj.transform.position = pos + new Vector3(y, x, 0).normalized * i * 0.5f;
            }
            else if(x == y)
            {
                EnemyObj.transform.position = pos + new Vector3(-x, y, 0).normalized * i * 0.5f;
            }
            else if (x != y)
            {
                EnemyObj.transform.position = pos + new Vector3(x, -y, 0).normalized * i * 0.5f;
            }
            EnemyObj.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * _enemySpeed, ForceMode2D.Impulse);
        }
        yield return null;
    }
}
