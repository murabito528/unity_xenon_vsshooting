using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePointViewController : MonoBehaviour
{
    [SerializeField]
    bool P1;
    [SerializeField]
    bool P2;

    [SerializeField]
    int lifenum;

    bool breaked;

    float time;

    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        breaked = false;
        time = 0;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (breaked)
        {
            if (time > 1)
            {
                Destroy(gameObject);
            }
            transform.Translate(0, -1 * Time.deltaTime, 0);
            var col = sr.color;
            col.a -= Time.deltaTime;
            sr.color = col;
            time += Time.deltaTime;
            return;
        }

        if (P1)
        {
            if (lifenum > GameManager.P1life)
            {
                breaked = true;
                //time = 0;
            }
        }
        if (P2)
        {
            if (lifenum > GameManager.P2life)
            {
                breaked = true;
                //time = 0;
            }
        }
    }
}
