using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    float time;
    Transform tf;
    float speed;
    float size;
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        speed = Random.Range(1f,2f) * 7.5f;
        size = Random.Range(80,120);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }

        time += Time.deltaTime;
        tf.Translate(0,0,speed * Time.deltaTime);
        if (time > 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
