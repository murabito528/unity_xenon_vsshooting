using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointerController : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }

        transform.Rotate(0,0,speed*Time.deltaTime);
        transform.localScale -= Utilities.vec.one * Time.deltaTime *1.2f;

        var color = sr.color;
        color.a -= Time.deltaTime;
        sr.color = color;

        if (color.a < 0)
        {
            Destroy(gameObject);
        }
    }
}
