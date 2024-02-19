using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorController : MonoBehaviour
{
    float time;
    Image img;
    [SerializeField]
    bool active;
    private void Start()
    {
        time = 1.5f;
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        if (!active)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        var color = img.color;
        color.a = 1 - Mathf.Min(time, 1);
        img.color = color;
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(this);
        }
    }
}
