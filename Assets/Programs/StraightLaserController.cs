using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLaserController : MonoBehaviour
{
    SpriteRenderer sr;

    [SerializeField]
    List<Sprite> sprites;

    float time;
    int count;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.05f)
        {
            sr.sprite = sprites[count];
            time = 0;
            count++;
            if(count == 2)
            {
                tag = "Untagged";
            }

            if (count == 5)
            {
                Destroy(gameObject);
            }
        }
    }
}
