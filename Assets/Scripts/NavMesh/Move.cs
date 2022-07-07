using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;


    void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        var dir = new Vector2(h, v).normalized;

        transform.Translate(dir * Time.deltaTime * speed);
    }
}
