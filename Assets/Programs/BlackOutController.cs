using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOutController : MonoBehaviour
{
    float time;
    bool black;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (black&&time>0)
        {
            var color = sr.color;
            color.a = time / 2;
            sr.color = color;
            time -= Time.deltaTime;
            if (time < 0)
            {
                sr.enabled = false;
            }
        }
        if (!black && time > 0)
        {
            var color = sr.color;
            color.a = time / 2;
            sr.color = color;
            time -= Time.deltaTime;
            if (time < 0)
            {
                sr.enabled = false;
            }
        }
    }

    public void blackout()
    {
        time = 2;
        black = true;
        sr.enabled = true;
    }
    public void whiteout()
    {
        time = 2;
        black = false;
        sr.enabled = true;
    }
}
