using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuLaserController : MonoBehaviour
{
    Image img;
    float time;
    int count;

    [SerializeField]
    List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        count = 0;
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.05f)
        {
            img.sprite = sprites[count];
            time = 0;
            count++;

            if (count == 5)
            {
                Destroy(gameObject);
            }
        }
    }
}
