using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    float time;
    [SerializeField]
    float speed;
    Transform tf;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tf = transform;
        if(tag== "P1bullet_remover")
        {
            sr.color = Utilities.color.expblue;
        }else if(tag == "P2bullet_remover")
        {
            sr.color = Utilities.color.expred;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }

        if (time < 0.2f)
        {
            tf.localScale = Utilities.vec.one * 0.1f + (Utilities.vec.one * time * speed);
        }
        else {
            var color = sr.color;
            color.a = (0.3f - (time-0.2f)) / 0.3f;
            sr.color = color;
        }
        time += Time.deltaTime;
        if (time > 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
