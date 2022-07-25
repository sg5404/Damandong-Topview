using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float time;

    void Start()
    {
                
    }

    private void OnEnable()
    {
        time = 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        time -= Time.deltaTime;
        if (time <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
