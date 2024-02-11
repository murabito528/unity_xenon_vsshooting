using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartMenuListController : MonoBehaviour
{
    Vector3 goalpos;

    TextMeshProUGUI tmpgui;
    RectTransform rt;

    float move;
    // Start is called before the first frame update
    void Start()
    {
        move = 0;
        tmpgui = GetComponent<TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
        goalpos = rt.anchoredPosition;
        //tmpgui.color = new Color(1,1,1,0);
        var color = tmpgui.color;
        color.a = 0;
        tmpgui.color = color;
        //menumove();
    }

    // Update is called once per frame
    void Update()
    {
        if (move>0)
        {
            rt.anchoredPosition = goalpos + Utilities.vec.right * Mathf.Min(0.5f,move) * 200;
            var color = tmpgui.color;
            color.a = 1 - Mathf.Min(0.5f, move) / 0.5f;
            tmpgui.color = color;
            move -= Time.deltaTime;
            if (move < 0)
            {
                rt.anchoredPosition = goalpos;
                color = tmpgui.color;
                color.a = 1;
                tmpgui.color = color;
            }
        }
    }

    public void menumove()
    {
        move = 1.5f;
    }
}
