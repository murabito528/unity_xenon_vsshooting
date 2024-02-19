using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartSceneManager : MonoBehaviour
{
    public int phase;//0:press key 1:‘JˆÚ’† 2:select play 

    [SerializeField]
    GameObject pressstart;

    [SerializeField]
    GameObject select_arrow;

    [SerializeField]
    GameObject select_laser;

    [SerializeField]
    GameObject selector;

    [SerializeField]
    GameObject blackoutui;

    [SerializeField]
    GameObject loading;

    [SerializeField]
    List<GameObject> MenuList;

    float time;

    int select_num;

    GameInputs _gameInputs;

    private void Awake()
    {
        _gameInputs = new GameInputs();

        _gameInputs.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
        time = 0;
        select_num = 1;
#if UNITY_EDITOR
        Cursor.visible = true;
#else
        Cursor.visible = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case 0:
                if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
                {
                    pressstart.GetComponent<BlinkController>().pressed();
                    selector.SetActive(true);
                    phase = 1;
                    time = 1;
                    foreach (GameObject go in MenuList)
                    {
                        go.GetComponent<StartMenuListController>().menumove();
                    }
                }
                break;
            case 1:
                time -= Time.deltaTime;
                if (time < 0)
                {
                    phase = 2;
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
                {
                    select_num = Mathf.Max(1,select_num - 1);

                    select_arrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(360,-15 + (-60 * select_num + 60),0);
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    select_num = Mathf.Min(5, select_num + 1);
                    select_arrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(360, -15 + (-60 * select_num + 60), 0);
                }
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.O))
                {
                    switch (select_num)
                    {
                        case 1:
                            //select_laser.SetActive(true);
                            break;
                        case 2:
                            select_laser.SetActive(true);
                            blackoutui.SetActive(true);
                            Invoke(nameof(ChangeGameSettingScene), 2);
                            loading.SetActive(true);
                            phase = 1234;
                            break;
                        case 3:
                            //select_laser.SetActive(true);
                            //phase = 1234;
                            break;
                        case 4:
                            select_laser.SetActive(true);
                            phase = 1234;
                            break;
                        case 5:
                            select_laser.SetActive(true);
                            phase = 1234;
                            blackoutui.SetActive(true);
                            Invoke(nameof(GameEnd),1);
                            break;
                    }
                }
                break;
        }
    }

    private void OnDestroy()
    {
        _gameInputs.Dispose();
    }

    void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    void ChangeGameScene()
    {
        GetComponent<LoadingSceneBarController>().LoadNextScene("GameScene");
    }
    void ChangeGameSettingScene()
    {
        GetComponent<LoadingSceneBarController>().LoadNextScene("GameSettingScene");
    }
}
