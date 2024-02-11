using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    Transform tf;

    Vector3 default_rotation;
    int count;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        count = Random.Range(0,8);
        default_rotation = new Vector3(0,0,45);

        tf.rotation = Quaternion.Euler(default_rotation * count * -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }

        time += Time.deltaTime;
        if (time > 0.5f) {
            count++;
            tf.rotation = Quaternion.Euler(default_rotation * count * -1);
            if (count > 7)
            {
                count = 0;
            }
            time = 0;
        }
    }
}
