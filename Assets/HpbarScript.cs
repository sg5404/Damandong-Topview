using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpbarScript : MonoBehaviour
{
    public GameObject hpbarObj;
    public GameObject canvas;

    RectTransform hpbar;

    Vector3 hpbarPos;
    private float height = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
        hpbar = hpbarObj.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        hpbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpbar.position = hpbarPos;
    }
}
