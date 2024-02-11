using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbController : MonoBehaviour
{
    float time;
    float waittime;
    float speed;
    Transform tf;

    [SerializeField]
    bool P1;
    [SerializeField]
    bool P2;

    GameManager gm;

    private void Start()
    {
        tf = transform;
        tf.rotation = Quaternion.Euler(Utilities.vec.forward * Random.Range(0, 360));
        waittime = Random.Range(0.1f,1.5f);
        speed = Random.Range(1f,2f);
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }

        time += Time.deltaTime;
        if (time < waittime)
        {
            tf.Translate(0,speed * Time.deltaTime,0);
            speed *= 0.95f;
        }
        else
        {
            if (P1)
            {
                Instantiate(gm.powerLineP1_Prefab, tf.position, Quaternion.identity);
            }

            if (P2)
            {
                Instantiate(gm.powerLineP2_Prefab, tf.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
