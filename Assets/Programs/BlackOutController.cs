using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOutController : MonoBehaviour
{
    Image img;
    float time;
    bool open;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        time = 1;
        open = false;
        img.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!open)
        {
            var color = img.color;
            color.a = time;
            img.color = color;
            time -= Time.deltaTime;
            if (time < 0)
            {
                img.color = new Color(0, 0, 0, 0);
                open = true;
                time = 1;
                gameObject.SetActive(false);
            }
        }
        else
        {
            var color = img.color;
            color.a = 1 - time;
            img.color = color;
            time -= Time.deltaTime;
            if (time < 0)
            {
                img.color = new Color(0, 0, 0, 1);
                open = false;
                time = 1;
                gameObject.SetActive(false);
            }
        }
    }
}
