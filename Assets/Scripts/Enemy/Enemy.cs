using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _destroyTime;
    Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        LookatPlayer();
    }
    private void OnEnable()
    {
        StartCoroutine(EnemyReturn());
        _target = FindAnyObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LookatPlayer()
    {
        if (_target != null)
        {
            Vector2 direction = new Vector2(transform.position.x - _target.position.x, transform.position.y - _target.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = angleAxis;
        }
    }
    IEnumerator EnemyReturn()
    {
        yield return new WaitForSeconds(_destroyTime);
        EnemyPool.ReturnObject(this);
    }
}