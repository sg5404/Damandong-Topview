using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    Vector3 playerDir;

    void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        playerDir = transform.position;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed;

        playerDir.x = Mathf.Clamp(transform.position.x, -8.8f, 8.8f);
        playerDir.y = Mathf.Clamp(transform.position.y, -4.8f, 4.8f);

        transform.position = playerDir;
    }
}
