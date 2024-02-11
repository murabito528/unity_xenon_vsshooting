using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public bool pause = false;

    [SerializeField]
    GameObject wintext;

    [SerializeField]
    GameObject defeattext;

    [SerializeField]
    GameObject pausemenu;

    [SerializeField]
    GameObject P1Camera;

    [SerializeField]
    GameObject P2Camera;

    [SerializeField]
    GameObject P1redplane;

    [SerializeField]
    GameObject P2redplane;

    [SerializeField]
    GameObject P1ready;

    [SerializeField]
    GameObject P2ready;

    [SerializeField]
    List<GameObject> enemy_Prefab;


    public GameObject powerorb_Prefab;

    public GameObject straightlaserP1_Prefab;
    public GameObject straightlaserP2_Prefab;

    public GameObject explosion_small_Prefab;
    public GameObject explosion_large_Prefab;

    public GameObject powerLineP1_Prefab;
    public GameObject powerLineP2_Prefab;

    public GameObject powerorbP1_Prefab;
    public GameObject powerorbP2_Prefab;

    public GameObject exattackorbP1_Prefab;
    public GameObject exattackorbP2_Prefab;

    [SerializeField]
    GameObject boss_prefab;

    [SerializeField]
    int fps;

    PoolManager poolManager;

    static public int P1difficulty;//1~16
    static public int P2difficulty;//1~16
    static public int Timedifficulty;//1~16
    static public bool GameNow;

    float difficultyTimer;

    GameObject P1obj;
    GameObject P2obj;

    float enemySpawnTimer;
    float enemySpawnCoolDown;
    float enemySpawnCoolDownMax;
    int enemySpawnPatternR;
    int enemySpawnPatternL;
    static public float bosscount;

    static public int P1life;
    static public int P2life;

    static public float gametime;

    static public int P1bombpoint;
    static public int P2bombpoint;
    static public int P1bombpointmax;
    static public int P2bombpointmax;
    static public float P1camerashake;
    static public float P2camerashake;
    static public bool inboss;

    Vector3 P1cameradefaultpos;
    Vector3 P2cameradefaultpos;

    static public int gamedifficulty;//1:easy,2:normal.3:hard
    static public float difficulty_modifier;

    float gameendtimer;
    float gamestarttimer;

    int gamephase;//0:準備ok? 1:カウントダウン 2:ゲーム中 3:ゲーム終了

    bool P1ok;
    bool P2ok;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = fps;
        poolManager = GetComponent<PoolManager>();
        P1obj = GameObject.FindWithTag("P1");
        P2obj = GameObject.FindWithTag("P2");

        enemySpawnCoolDown = 5;
        enemySpawnTimer = 0;
        enemySpawnCoolDownMax = 3;

        P1life = 5;
        P2life = 5;

        P1difficulty = 1;
        P2difficulty = 1;
        Timedifficulty = 1;
        difficultyTimer = 15;
        gametime = 0;
        GameNow = false;

        P1bombpoint = 100;
        P2bombpoint = 100;
        P1bombpointmax = 500;
        P2bombpointmax = 500;

        P1cameradefaultpos = P1Camera.transform.position;
        P2cameradefaultpos = P2Camera.transform.position;

        gamedifficulty = 2;

        inboss = false;
        bosscount = 0;

        gamephase = 0;
        P1ok = false;
        P2ok = false;
        gamestarttimer = 3;

        switch (gamedifficulty)
        {
            case 1:
                difficulty_modifier = 0.75f;
                P1difficulty = 1;
                P2difficulty = 1;
                Timedifficulty = 1;
                break;
            case 2:
                difficulty_modifier = 1;
                P1difficulty = 5;
                P2difficulty = 5;
                Timedifficulty = 1;
                break;
            case 3:
                difficulty_modifier = 1.5f;
                P1difficulty = 10;
                P2difficulty = 10;
                Timedifficulty = 10;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                pause = false;
                pausemenu.SetActive(false);
            }
            else
            {
                pause = true;
                pausemenu.SetActive(true);
            }
        }

        if (pause)
        {
            if (Input.GetKeyDown(KeyCode.Z)||Input.GetKeyDown(KeyCode.KeypadEnter))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
            }
        }

        if (pause)
        {
            return;
        }

        cameraShake();

        if (!GameNow)
        {
            switch (gamephase) {
                case 0:
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        P1ready.GetComponent<ReadyCheckController>().ok();
                        P1ok = true;
                        P1camerashake = 0.2f;
                    }
                    if (Input.GetKeyDown(KeyCode.O))
                    {
                        P2ready.GetComponent<ReadyCheckController>().ok();
                        P2ok = true;
                        P2camerashake = 0.2f;
                    }
                    if (P1ok && P2ok)
                    {
                        gamephase = 1;
                        P1ready.GetComponent<ReadyCheckController>().remove_start();
                        P2ready.GetComponent<ReadyCheckController>().remove_start();
                        //GameNow = true;
                    }
                    break;
                case 1:
                    gamestarttimer -= Time.deltaTime;
                    if (gamestarttimer < 0)
                    {
                        gamephase=2;
                        GameNow = true;
                    }
                    break;
                case 3:
                    if (gameendtimer > 0)
                    {
                        gameendtimer -= Time.deltaTime;
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene("GameScene");
                    }
                    break;
            }
            return;
        }

        enemySpawnCoolDown -= Time.deltaTime;
        if (enemySpawnCoolDown <= 0)
        {
            enemySpawnCoolDown = enemySpawnCoolDownMax;
            enemySpawnTimer = 0;
            enemySpawnPatternR = Random.Range(1,8);//Random.Range(1, 3);
            enemySpawnPatternL = Random.Range(1,8);

            enemySpawn();
        }
        
        enemySpawnTimer += Time.deltaTime;
        gametime += Time.deltaTime;

        if (difficultyTimer < 0)
        {
            difficultyTimer = 15;
            if (Timedifficulty < 16)
            {
                Timedifficulty++;
                enemySpawnCoolDownMax -= 0.1f;
            }
        }
        else
        {
            difficultyTimer -= Time.deltaTime;
        }

        P1bombpoint = Mathf.Min(P1bombpoint, P1bombpointmax);
        P2bombpoint = Mathf.Min(P2bombpoint, P2bombpointmax);

        if (!inboss && bosscount<0)
        {
            inboss = true;
            Instantiate(boss_prefab, new Vector3(0, 6, 0), Quaternion.identity);
        }
        if (bosscount > 0)
        {
            bosscount -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {

        if (pause)
        {
            return;
        }

        if (Random.Range(0, 100f) < 0.5f + (P1difficulty+Timedifficulty) / 8)
        {
            var GameObject = poolManager.GetGameObject(1, Utilities.vec.right * Random.Range(-3.8f, 3.8f) + Utilities.vec.up * Random.Range(4f, 4.5f), Quaternion.identity);
            GameObject.GetComponent<Bullet2Controller>().SetupBullet(0, 1, Random.Range(2f,3.5f+(P1difficulty+Timedifficulty)/ 20) * GameManager.difficulty_modifier, 1, Utilities.vec.forward * Random.Range(150f, 210f));
        }
        if (Random.Range(0, 100f) < 0.5f + (P2difficulty+Timedifficulty) / 8)
        {
            var GameObject = poolManager.GetGameObject(1, Utilities.vec.right * Random.Range(-3.8f, 3.8f) + Utilities.vec.up * Random.Range(4f, 4.5f), Quaternion.identity);
            GameObject.GetComponent<Bullet2Controller>().SetupBullet(0, 1, Random.Range(2f, 3.5f + (P2difficulty+Timedifficulty) / 20) * GameManager.difficulty_modifier, 2, Utilities.vec.forward * Random.Range(150f, 210f));
        }
        if (Random.Range(0, 100f) < (0.5f + (P1difficulty+Timedifficulty) / 8)*4)
        {
            var GameObject = poolManager.GetGameObject(1, Utilities.vec.right * Random.Range(-3.8f, 3.8f) + Utilities.vec.up * Random.Range(4f, 4.5f), Quaternion.identity);
            var dir = P1obj.transform.position - GameObject.transform.position;
            GameObject.GetComponent<Bullet2Controller>().SetupBullet(0, 2, Random.Range(2f, 3.5f + (P1difficulty+Timedifficulty) / 20) * GameManager.difficulty_modifier, 1, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles * Random.Range(0.9f, 1.1f));
        }
        if (Random.Range(0, 100f) < (0.5f + (P2difficulty+Timedifficulty) / 8)*4)
        {
            var GameObject = poolManager.GetGameObject(1, Utilities.vec.right * Random.Range(-3.8f, 3.8f) + Utilities.vec.up * Random.Range(4f, 4.5f), Quaternion.identity);
            var dir = P2obj.transform.position - GameObject.transform.position;
            GameObject.GetComponent<Bullet2Controller>().SetupBullet(0, 2, Random.Range(2f, 3.5f + (P2difficulty+Timedifficulty) / 20) * GameManager.difficulty_modifier, 2, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles * Random.Range(0.9f, 1.1f));
        }
        if (Random.Range(0, 100f) < 1)
        {
            var GameObject = Instantiate(enemy_Prefab[1], new Vector3(Random.Range(-2.5f, 2.5f), 5f, 0), Quaternion.identity);
            GameObject.GetComponent<EnemyController>().SetupEnemy(9, 0);
            GameObject.transform.rotation = Quaternion.Euler(Utilities.vec.forward * Random.Range(160f, 200f));
        }
    }

    void enemySpawn()
    {
        GameObject tmpenemy;
        switch (enemySpawnPatternR)
        {

            case 1:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(4.7f, 4, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * 135);
                }
                break;
            case 2:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(4.7f, -4, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * 45);
                }
                break;
            case 3:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(3f, 5.5f, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * 180);
                }
                break;
            case 4:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(3.5f, -5.5f, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.zero);
                }
                break;
            case 5:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(4.9f, 2, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * 90);
                }
                break;
            case 6:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(4.9f, 0, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * 90);
                }
                break;

            case 7:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(2.5f, 5.5f, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * 165);
                }
                break;
        }

        switch (enemySpawnPatternL)
        {
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(-4.7f, 4, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * -135);
                }
                break;
            case 2:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(-4.7f, -4, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * -45);
                }
                break;
            case 3:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(-3f, 5.5f, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * 180);
                }
                break;
            case 4:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(-3.5f, -5.5f, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.zero);
                }
                break;
            case 5:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(-4.9f, 3, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * -90);
                }
                break;
            case 6:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(-4.9f, 1, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * -90);
                }
                break;
            case 7:
                for (int i = 0; i < 5; i++)
                {
                    tmpenemy = Instantiate(enemy_Prefab[getRandomEnemy()], new Vector3(-2.5f, 5.5f, 0), Quaternion.identity);
                    tmpenemy.GetComponent<EnemyController>().SetupEnemy(9, i * 0.5f);
                    tmpenemy.transform.rotation = Quaternion.Euler(Utilities.vec.forward * -165);
                }
                break;
        }
    }

    int getRandomEnemy()
    {
        return Random.Range(0,3);
    }

    public void hit(int player)
    {
        switch (player)
        {
            case 1:
                P1Camera.GetComponent<Camera>().enabled = false;
                P1redplane.SetActive(true);
                Invoke(nameof(delayCameraDownP1),0.15f);
                break;
            case 2:
                P2Camera.GetComponent<Camera>().enabled = false;
                P2redplane.SetActive(true);
                Invoke(nameof(delayCameraDownP2), 0.15f);
                break;
        }
    }

    void delayCameraDownP1()
    {
        P1Camera.GetComponent<Camera>().enabled = true;
        P1redplane.SetActive(false);
    }

    void delayCameraDownP2()
    {
        P2Camera.GetComponent<Camera>().enabled = true;
        P2redplane.SetActive(false);
    }

    public void gameEnd(int looser)
    {
        if (looser == 1)
        {
            wintext.SetActive(true);
            wintext.layer = 7;
            defeattext.SetActive(true);
            defeattext.layer = 6;
        }
        else
        {
            wintext.SetActive(true);
            wintext.layer = 6;
            defeattext.SetActive(true);
            defeattext.layer = 7;
        }
        GameNow = false;
        gameendtimer = 3;
        gamephase = 3;
    }

    void cameraShake()
    {
        if (P1camerashake > 0)
        {
            P1Camera.transform.position = P1cameradefaultpos + new Vector3(Random.Range(-0.5f * P1camerashake,0.5f * P1camerashake), Random.Range(-0.5f * P1camerashake, 0.5f * P1camerashake), 0);
            P1camerashake -= Time.deltaTime;
            if (P1camerashake < 0)
            {
                P1Camera.transform.position = P1cameradefaultpos;
            }
        }

        if (P2camerashake > 0)
        {
            P2Camera.transform.position = P2cameradefaultpos + new Vector3(Random.Range(-0.5f * P2camerashake, 0.5f * P2camerashake), Random.Range(-0.5f * P2camerashake, 0.5f * P2camerashake), 0);
            P2camerashake -= Time.deltaTime;
            if (P2camerashake < 0)
            {
                P2Camera.transform.position = P2cameradefaultpos;
            }
        }
    }
}
