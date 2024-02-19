using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionUIController : MonoBehaviour
{
    RectTransform rt;
    Image img;
    float time;
    float rnd;
    int phase;
    private void Start()
    {
        time = 0;
        phase = 1;
        rnd = Random.Range(-0.05f,0.05f);
        rt = GetComponent<RectTransform>();
        rt.localScale = Utilities.vec.zero;
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 1)
        {
            rt.localScale = Utilities.vec.one * time * 15;
            time += Time.deltaTime;
            if (time > 0.1f + rnd)
            {
                phase = 2;
                time = 0.2f;
            }
        }
        if(phase == 2)
        {
            var color = img.color;
            color.a = time / 0.2f;
            img.color = color;
            time -= Time.deltaTime;
            if (time < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
