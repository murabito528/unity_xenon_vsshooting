using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCheckController : MonoBehaviour
{
    [SerializeField]
    Sprite check_no_ok;
    [SerializeField]
    Sprite check_ok;

    SpriteRenderer sr;

    float timer;
    bool remove;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        remove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (remove)
        {
            timer -= Time.deltaTime;
            var color = sr.color;
            color.a = timer;
            sr.color = color;
            if (timer < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ok()
    {
        sr.sprite = check_ok;
    }
    public void remove_start()
    {
        remove = true;
        timer = 1;
    }
}
