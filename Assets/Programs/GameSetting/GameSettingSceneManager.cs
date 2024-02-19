using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingSceneManager : MonoBehaviour
{
    int select_num;
    [SerializeField]
    GameObject select_arrow;
    [SerializeField]
    GameObject select_laser;

    [SerializeField]
    GameObject blackoutui;

    [SerializeField]
    GameObject loading;

    int phase;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        select_num = 1;
        phase = 0;
        time = 1.5f;
    }

    // Update is called once per frame 127 377
    void Update()
    {
        switch (phase)
        {
            case 0:
                time -= Time.deltaTime;
                if (time < 0)
                {
                    phase = 1;
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    select_num = Mathf.Max(1, select_num - 1);

                    select_arrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-450,-250 * select_num + 123, 0);
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    select_num = Mathf.Min(3, select_num + 1);
                    select_arrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-450,-250 * select_num + 123, 0);
                }
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.O))
                {
                    GameManager.gamedifficulty = select_num;
                    select_laser.SetActive(true);
                    blackoutui.SetActive(true);
                    Invoke(nameof(ChangeGameScene), 2);
                    loading.SetActive(true);
                    blackoutui.GetComponent<BlackoutUIController>().enabled = true;
                    phase = 2;
                }
                if (Input.GetKeyDown(KeyCode.V)|| Input.GetKeyDown(KeyCode.P))
                {
                    blackoutui.SetActive(true);
                    Invoke(nameof(ChangeTitleScene), 2);
                    loading.SetActive(true);
                    blackoutui.GetComponent<BlackoutUIController>().enabled = true;
                    phase = 2;
                }
                break;
            case 2:
                break;
        }
    }
    void ChangeGameScene()
    {
        GetComponent<LoadingSceneBarController>().LoadNextScene("GameScene");
    }
    void ChangeTitleScene()
    {
        GetComponent<LoadingSceneBarController>().LoadNextScene("TitleScene");
    }
}
