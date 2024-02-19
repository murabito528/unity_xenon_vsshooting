using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUController : MonoBehaviour
{
    PlayerController pc;

    Vector2 move;
    Transform tf;

    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
        move = new Vector2(0, 0);
        tf = transform;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Update()
    {
        tf.Translate(move * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Random.Range(0, 100f) < 10f)
        {
            pc.test_autoattack = !pc.test_autoattack;
        }
        if (Random.Range(0, 100f) < 10f)
        {
            move.x = Random.Range(-1, 1);
            move.y = Random.Range(-1, 1);
            if (tf.position.x < -1 && move.x < 0)
            {
                move.x *= -1;
            }
            if (tf.position.x > 1 && move.x > 0)
            {
                move.x *= -1;
            }
            if (tf.position.y < -2 && move.y < 0)
            {
                move.y *= -1;
            }
            if (tf.position.y > 2 && move.y > 0)
            {
                move.y *= -1;
            }
        }
    }
}
