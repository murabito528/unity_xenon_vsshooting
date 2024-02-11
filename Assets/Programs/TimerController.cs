using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    TextMeshProUGUI tmpgui;
    // Start is called before the first frame update
    void Start()
    {
        tmpgui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmpgui.text = (Mathf.Floor(GameManager.gametime / 60)).ToString() + ":" + Mathf.Floor(GameManager.gametime%60).ToString().PadLeft(2, '0');
    }
}
