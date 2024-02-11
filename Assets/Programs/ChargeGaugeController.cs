using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeGaugeController : MonoBehaviour
{
    [SerializeField]
    bool P1;

    [SerializeField]
    bool P2;

    PlayerController pc;

    Transform tf;


    // Start is called before the first frame update
    void Start()
    {
        if (P1) pc = GameObject.FindGameObjectWithTag("P1").GetComponent<PlayerController>();
        if (P2) pc = GameObject.FindGameObjectWithTag("P2").GetComponent<PlayerController>();
        tf = transform;
    }

    private void Update()
    {
        if (GameManager.pause == true)
        {
            return;
        }

        var vec = tf.localScale;
        vec.x = Mathf.Min(pc.charge / GameManager.P1bombpointmax * 2, 2);
        tf.localScale = vec;
    }
}
