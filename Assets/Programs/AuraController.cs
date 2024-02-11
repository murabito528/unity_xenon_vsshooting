using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour
{
    float scale;
    float time;

    Transform tf;

    [SerializeField]
    float rotate_speed;

    [SerializeField]
    float scalespeed;

    [SerializeField]
    Color defaultcolor;

    SpriteRenderer sr;
    // Start is called before the first frame update
    void Awake()
    {
        tf = transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause == true)
        {
            return;
        }

        tf.Rotate(0,0,rotate_speed * Time.deltaTime);
        if (time < 0.2f)
        {
            scale = Mathf.Lerp(1.5f,10f,time*5);
            tf.localScale = Utilities.vec.one * scale;
        }

        if (time > 0.8f)
        {
            scale = Mathf.Lerp(10f, 12f, (time-0.8f) * 2.5f);
            tf.localScale = Utilities.vec.one * scale;
            var color = sr.color;
            color.a -= (Time.deltaTime*2.5f);
            sr.color = color;
        }

        if (time > 1.2f)
        {
            //this.gameObject.SetActive(false);
            tf.parent.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }

    void OnEnable()
    {
        StartAura();
    }

    public void StartAura()
    {
        scale = 1;
        time = 0;
        tf.localScale = Utilities.vec.one;
        sr.color = defaultcolor;
        //tf.position = targetPlayer.transform.position;
    }
}
