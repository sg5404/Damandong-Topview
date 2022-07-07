using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressTextCtrl : MonoBehaviour
{
    [SerializeField]
    private float moveMax;
    [SerializeField]
    private float speed;
    [SerializeField]
    private TextMesh pressText;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = pressText.transform.position;
        pressText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirPos = pos;
        dirPos.y = pos.y + moveMax * Mathf.Sin(Time.time * speed);
        pressText.transform.position = dirPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressText.gameObject.SetActive(false);
        }
    }
}
