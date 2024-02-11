using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField]
    float speed;
    Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause == true)
        {
            return;
        }
        tf.Rotate(0,0,Time.deltaTime * speed);
    }
}
