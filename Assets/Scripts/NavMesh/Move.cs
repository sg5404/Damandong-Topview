using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //1 : 0.5625

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private Camera mainCamera;

    Vector3 playerDir;

    void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        if (Fade.Instance.isFade) return;

        playerDir = transform.position;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed;

        playerDir.x = Mathf.Clamp(transform.position.x, -12f, 12f);
        playerDir.y = Mathf.Clamp(transform.position.y, -6.5f, 6.5f);

        transform.position = playerDir;
    }
}
