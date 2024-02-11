using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAuraController : MonoBehaviour
{
    float time;
    float maxtime;
    int type;
    Transform tf;
    public float movespeed;
    public float scalespeed;

    SpriteRenderer[] childsr;
    // Start is called before the first frame update
    void Awake()
    {
        time = 0;
        childsr = gameObject.GetComponentsInChildren<SpriteRenderer>();
        tf = transform;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeChildColor(new Color(1, 1, 1, 1-(time / maxtime)));
        tf.Translate(0, movespeed * Time.deltaTime, 0);
        tf.localScale += Utilities.vec.one * scalespeed * Time.deltaTime;
        time += Time.deltaTime;
        if (time > maxtime)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int parent,int type)
    {
        this.type = type;
        ChangeChildColor(Utilities.color.white);
        time = 0;

        switch (type)
        {
            case 1:
                tf.localScale = Utilities.vec.one * 3f;
                maxtime = 0.75f;
                movespeed = 0;
                scalespeed = 0.1f;
                break;

            case 2:
                tf.localScale = Utilities.vec.one * 2.5f;
                maxtime = 0.75f;
                movespeed = 1;
                scalespeed = 0;
                break;
            case 3:
                tf.localScale = Utilities.vec.one * 3.5f;
                maxtime = 1.2f;
                movespeed = 0;
                scalespeed = 5;
                break;
            case 4:
                tf.localScale = Utilities.vec.one * 3.5f;
                maxtime = 3;
                movespeed = 0;
                scalespeed = 12;
                break;
        }

    }

    void ChangeChildColor(Color color)
    {
        foreach(SpriteRenderer childsr in childsr)
        {
            childsr.color = color;
        }
    }
}
