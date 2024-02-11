using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeDispKari : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tmpgui;

    [SerializeField]
    bool P1;

    [SerializeField]
    bool P2;


    // Update is called once per frame
    void Update()
    {
        if (P1) {
            tmpgui.text = "Life:" + GameManager.P1life + "\nDifficulty:" + (GameManager.P1difficulty+GameManager.Timedifficulty);
        }
        if (P2)
        {
            tmpgui.text = GameManager.P2life + ":Life" + "\n" + (GameManager.P2difficulty+GameManager.Timedifficulty) + ":Difficulty";
        }
    }
}
