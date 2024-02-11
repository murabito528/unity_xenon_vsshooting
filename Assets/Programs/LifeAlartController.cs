using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeAlartController : MonoBehaviour
{
    [SerializeField]
    bool P1;

    [SerializeField]
    bool P2;

    SpriteRenderer sr;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (P1 && GameManager.P1life == 1)
        {
            var color = sr.color;
            color.a = 0.5f + Mathf.Sin(time * 5) / 4;
            sr.color = color;
        }
        if (P2 && GameManager.P2life == 1)
        {
            var color = sr.color;
            color.a = 0.5f + Mathf.Sin(time * 5) / 4;
            sr.color = color;
        }
        time += Time.deltaTime;
    }
}
