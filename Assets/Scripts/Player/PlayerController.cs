using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 5;
    private float _circleRadius = 4f; // 원의 반지름

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.isDead = false;
    }

    // Update is called once per frame
    void Update() 
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);
        Vector3 moveAmount = dir * _speed * Time.deltaTime;
        Vector3 newPosition = transform.position + moveAmount;

        float distanceFromCenter = Vector3.Distance(Vector3.zero, newPosition);

        if (distanceFromCenter <= _circleRadius)
        {
            transform.position = newPosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !GameManager.instance.isDead)
        {
            GameManager.instance.GameEnd_Panel_On();
            gameObject.SetActive(false);
            GameManager.instance.isDead = true;
        }
    }
}