using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGaugeController : MonoBehaviour
{

    [SerializeField]
    bool P1;

    [SerializeField]
    bool P2;

    Transform tf;


    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
    }

    private void Update()
    {
        var vec = tf.localScale;
        if (P1) vec.x = Mathf.Min((float)GameManager.P1bombpoint / GameManager.P1bombpointmax * 2, 2);
        if (P2) vec.x = Mathf.Min((float)GameManager.P2bombpoint / GameManager.P2bombpointmax * 2, 2);
        tf.localScale = vec;
    }
}
