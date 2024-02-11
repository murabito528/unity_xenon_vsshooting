using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkController : MonoBehaviour
{
    TextMeshProUGUI tmpugui;
    float time;

    [SerializeField]
    float scale;

    [SerializeField]
    GameObject logo;

    bool remove = false;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        tmpugui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (remove)
        {
            var color = tmpugui.color;
            color.a = (Mathf.Sin(time * 50) * 0.5f + 0.5f) * (0.5f-time);
            tmpugui.color = color;
            if (time > 0.5f) {
                Destroy(gameObject);
            }
        }
        else
        {
            var color = tmpugui.color;
            color.a = Mathf.Sin(time * scale) * 0.5f + 0.5f;
            tmpugui.color = color;
        }

        time += Time.deltaTime;
    }

    public void pressed()
    {
        if (remove)
        {
            return;
        }
        time = 0;
        remove = true;
        logo.GetComponent<Animator>().SetBool("presscheck", true);
    }
}
