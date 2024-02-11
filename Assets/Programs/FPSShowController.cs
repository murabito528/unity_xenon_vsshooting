using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSShowController : MonoBehaviour
{
    TextMeshProUGUI tmpgui;

    int frameCount;
    float prevTime;

    // Start is called before the first frame update
    void Start()
    {
        tmpgui = GetComponent<TextMeshProUGUI>();
        frameCount = 0;
        prevTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ++frameCount;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            tmpgui.text = "FPS:" + ((int)Mathf.Ceil(frameCount/time)).ToString();

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }
}
