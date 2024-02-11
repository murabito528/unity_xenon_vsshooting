using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField]
    float speed;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }

        transform.Translate(0, speed * Time.deltaTime, 0);
        if (transform.position.y < -20)
        {
            transform.Translate(0, 40, 0);
        }
    }
}
