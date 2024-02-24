using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float defaultspeed;

    float speed;

    [SerializeField]
    bool P1;
    [SerializeField]
    bool P2;

    Rigidbody2D rb;

    [SerializeField]
    GameObject gd;

    PoolManager poolManager;
    [SerializeField]
    GameObject bullet_prefab;
    [SerializeField]
    GameObject auras;
    [SerializeField]
    GameObject bomb_prefab;

    Transform tf;

    GameManager gm;

    Vector3 bullet_pos;

    public float charge;

    float shotcooltime;

    float invinsibletime;

    public bool test_autoattack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = gd.GetComponent<GameManager>();
        tf = GetComponent<Transform>();
        poolManager = gd.GetComponent<PoolManager>();
        shotcooltime = 0;
        bullet_pos = new Vector3(0.2f,0,0);

        charge = 0;
        invinsibletime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause == true)
        {
            return;
        }

        if (GameManager.gamephase >= 4)
        {
            return;
        }

        if (P1)
        {
            if (Input.GetKey(KeyCode.LeftShift)){
                speed = defaultspeed * 0.5f;
            }
            else
            {
                speed = defaultspeed;
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Utilities.vec.up * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Utilities.vec.up * Time.deltaTime * -1 * speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Utilities.vec.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Utilities.vec.right * Time.deltaTime * -1 * speed);
            }

            if (tf.position.x > 3.9f)
            {
                tf.position = new Vector2(3.9f, tf.position.y);
            }
            if (tf.position.x < -3.9f)
            {
                tf.position = new Vector2(-3.9f, tf.position.y);
            }
            if (tf.position.y > 4.6f)
            {
                tf.position = new Vector2(tf.position.x, 4.6f);
            }
            if (tf.position.y < -4.6f)
            {
                tf.position = new Vector2(tf.position.x, -4.6f);
            }

            if (!GameManager.GameNow)
            {
                return;
            }

            if ((Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.V) || test_autoattack) && shotcooltime < 0)
            {
                shotcooltime = 0.1f;
                if (P1)
                {
                    poolManager.GetGameObject(0, tf.position + bullet_pos, Quaternion.identity).GetComponent<BulletController>().setupBullet(1);

                    poolManager.GetGameObject(0, tf.position - bullet_pos, Quaternion.identity).GetComponent<BulletController>().setupBullet(1);
                }
                else if (P2)
                {
                    poolManager.GetGameObject(0, tf.position + bullet_pos, Quaternion.identity).GetComponent<BulletController>().setupBullet(2);

                    poolManager.GetGameObject(0, tf.position - bullet_pos, Quaternion.identity).GetComponent<BulletController>().setupBullet(2);
                }
            }

            if (Input.GetKey(KeyCode.V) && charge<GameManager.P1bombpointmax)
            {
                charge = Mathf.Min(charge + Time.deltaTime * 200, GameManager.P1bombpoint);
            }

            if (Input.GetKeyUp(KeyCode.V))
            {
                //Debug.Log("charge:" + charge);
                if (charge < 30)//C1
                {
                    //Debug.Log("Instant");
                    if (GameManager.P1bombpoint >= 100 && GameManager.P1bombpoint < 200)
                    {
                        if (GameManager.P1difficulty < 16) GameManager.P1difficulty += 1;
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(1, 3);
                        GameManager.P1bombpoint = 0;
                    }
                    if(GameManager.P1bombpoint >= 200)
                    {
                        if (GameManager.P1difficulty < 16) GameManager.P1difficulty += 1;
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(1, 4);
                        GameManager.P1bombpoint = 0;
                    }
                }
                else if (charge < 100)
                {
                    //Debug.Log("charge:" + charge);
                    //•s”­
                }
                else if (charge < 200)//C2 100~199
                {
                    //Debug.Log("C1");
                    Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(1, 2);
                    
                }
                else if (charge < 300)//C3 200~299
                {
                    //Debug.Log("C2");
                    if (GameManager.P1difficulty < 16) GameManager.P1difficulty += 1;
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(1, 3);
                        GameManager.P1bombpoint -= 200;
                    }
                    else
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(1, 1);
                        BombAttack(1);
                        GameManager.P1bombpoint -= 100;
                    }
                    Mathf.Max(GameManager.P1bombpoint, 100);
                }
                else//C4 300
                {
                    //Debug.Log("C3");
                    if (GameManager.P1difficulty < 16) GameManager.P1difficulty += 1;
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(1, 4);
                        GameManager.P1bombpoint -= 300;
                    }
                    else
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(1, 1);
                        BombAttack(2);
                        GameManager.P1bombpoint -= 200;
                    }
                    Mathf.Max(GameManager.P1bombpoint, 100);
                }
                charge = 0;
            }
        }

        if (P2)
        {
            if (Input.GetKey(KeyCode.K))
            {
                speed = defaultspeed * 0.5f;
            }
            else
            {
                speed = defaultspeed;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Utilities.vec.up * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Utilities.vec.up * Time.deltaTime * -1 * speed);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Utilities.vec.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Utilities.vec.right * Time.deltaTime * -1 * speed);
            }

            if (tf.position.x > 3.9f)
            {
                tf.position = new Vector2(3.9f, tf.position.y);
            }
            if (tf.position.x < -3.9f)
            {
                tf.position = new Vector2(-3.9f, tf.position.y);
            }
            if (tf.position.y > 4.6f)
            {
                tf.position = new Vector2(tf.position.x, 4.6f);
            }
            if (tf.position.y < -4.6f)
            {
                tf.position = new Vector2(tf.position.x, -4.6f);
            }

            if (!GameManager.GameNow)
            {
                return;
            }

            if ((Input.GetKey(KeyCode.O) && !Input.GetKey(KeyCode.P)||test_autoattack) && shotcooltime < 0)
            {
                shotcooltime = 0.1f;
                if (P1)
                {
                    poolManager.GetGameObject(0, tf.position + bullet_pos, Quaternion.identity).GetComponent<BulletController>().setupBullet(1);

                    poolManager.GetGameObject(0, tf.position - bullet_pos, Quaternion.identity).GetComponent<BulletController>().setupBullet(1);
                }
                else if (P2)
                {
                    poolManager.GetGameObject(0, tf.position + bullet_pos, Quaternion.identity).GetComponent<BulletController>().setupBullet(2);

                    poolManager.GetGameObject(0, tf.position - bullet_pos, Quaternion.identity).GetComponent<BulletController>().setupBullet(2);
                }
            }

            if (Input.GetKey(KeyCode.P) && charge < GameManager.P2bombpointmax)
            {
                charge = Mathf.Min(charge + Time.deltaTime * 200, GameManager.P2bombpoint);
            }

            if (Input.GetKeyUp(KeyCode.P))
            {
                //Debug.Log("charge:" + charge);
                if (charge < 20)//C1
                {
                    //Debug.Log("C1");
                    if (GameManager.P2bombpoint >= 100 && GameManager.P2bombpoint < 200)
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(2, 3);
                        GameManager.P2bombpoint = 0;
                    }
                    if (GameManager.P2bombpoint >= 200)
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(2, 4);
                        GameManager.P2bombpoint = 0;
                    }
                }
                else if (charge < 100)
                {
                    //•s”­
                }
                else if (charge < 200)//C2 100~199
                {
                    //Debug.Log("C2");
                    Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(2, 2);

                }
                else if (charge < 300)//C3 200~299
                {
                    //Debug.Log("C3");
                    if (GameManager.P2difficulty < 16) GameManager.P2difficulty += 1;
                    if (Input.GetKey(KeyCode.K))
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(2, 3);
                        GameManager.P2bombpoint -= 200;
                    }
                    else
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(2, 1);
                        BombAttack(1);
                        GameManager.P2bombpoint -= 100;
                    }
                    Mathf.Max(GameManager.P2bombpoint, 100);
                }
                else//C4 300
                {
                    //Debug.Log("C4");
                    if (GameManager.P2difficulty < 16) GameManager.P2difficulty += 1;
                    if (Input.GetKey(KeyCode.K))
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(2, 4);
                        GameManager.P2bombpoint -= 300;
                    }
                    else
                    {
                        Instantiate(bomb_prefab, tf.position, Quaternion.identity).GetComponent<BombAuraController>().Setup(2, 1);
                        BombAttack(2);
                        GameManager.P2bombpoint -= 200;
                    }
                    Mathf.Max(GameManager.P2bombpoint, 100);
                }
                charge = 0;
            }
        }

        shotcooltime -= Time.deltaTime;
        if (invinsibletime > 0)
        {
            invinsibletime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (invinsibletime > 0)
        {
            return;
        }

        if (P1)
        {
            if (collision.tag == "targetP1Bullet" || collision.tag == "Bullet")
            {
                //Debug.Log("Hit!");
                //gm.hit(1);
                auras.SetActive(true);
                auras.transform.position = tf.position;
                GameManager.P1life--;
                invinsibletime = 3;

                GameManager.P1camerashake = 0.3f;

                GameManager.P1bombpoint = Mathf.Min(GameManager.P1bombpoint+100*(5-GameManager.P1life),GameManager.P1bombpointmax);

                if (GameManager.P1life <= 0)
                {
                    GameManager.P1camerashake = 1;
                    gm.gameEnd(1);
                }
            }
        }
        if (P2)
        {
            if (collision.tag == "targetP2Bullet" || collision.tag == "Bullet")
            {
                //Debug.Log("Hit!");
                auras.SetActive(true);
                //gm.hit(2);
                auras.transform.position = tf.position;
                GameManager.P2life--;
                invinsibletime = 3;

                GameManager.P2camerashake = 0.3f;

                GameManager.P2bombpoint = Mathf.Min(GameManager.P2bombpoint + 100 * (5 - GameManager.P2life), GameManager.P2bombpointmax);

                if (GameManager.P2life <= 0)
                {
                    GameManager.P2camerashake = 1;
                    gm.gameEnd(2);
                }
            }
        }
    }

    void BombAttack(int type)
    {
        switch (type)
        {
            case 1:
                if (P1)
                {
                    int rand = Random.Range(0,180);
                    for(int i = 0; i < 36; i++)
                    {
                        var bullet = poolManager.GetGameObject(1, Utilities.vec.up * 1, Quaternion.identity);
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 1, Mathf.Abs((i%9)-4f)*0.5f + 1, 2, Utilities.vec.forward * (10 * i + rand));
                    }
                    for (int i = 0; i < 36; i++)
                    {
                        var bullet = poolManager.GetGameObject(1, Utilities.vec.up * 1, Quaternion.identity);
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 2, Mathf.Abs((i % 9) - 4f) * 0.5f + 1, 2, Utilities.vec.forward * (10 * i + 45 + rand));
                    }
                }
                if (P2)
                {
                    int rand = Random.Range(0, 180);
                    for (int i = 0; i < 36; i++)
                    {
                        var bullet = poolManager.GetGameObject(1, Utilities.vec.up * 1, Quaternion.identity);
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 1, Mathf.Abs((i % 9) - 4f) * 0.5f + 1, 1, Utilities.vec.forward * (10 * i + rand));
                    }
                    for (int i = 0; i < 36; i++)
                    {
                        var bullet = poolManager.GetGameObject(1, Utilities.vec.up * 1, Quaternion.identity);
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 2, Mathf.Abs((i % 9) - 4f) * 0.5f + 1, 1, Utilities.vec.forward * (10 * i + 45 + rand));
                    }
                }
                break;
            case 2:
                if (P1)
                {
                    int rand = Random.Range(0, 180);
                    for (int i = 0; i < 36; i++)
                    {
                        var bullet = poolManager.GetGameObject(1, Utilities.vec.up * 1, Quaternion.identity);
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(2, 3, Mathf.Abs((i % 9) - 4f) * 0.5f + 1.5f, 2, Utilities.vec.forward * (10 * i + rand));
                    }
                    for (int i = 0; i < 36; i++)
                    {
                        var bullet = poolManager.GetGameObject(1, Utilities.vec.up * 1, Quaternion.identity);
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(3, 4, Mathf.Abs((i % 9) - 4f) * 0.5f + 1.5f, 2, Utilities.vec.forward * (10 * i + rand));
                    }
                }
                if (P2)
                {
                    int rand = Random.Range(0, 180);
                    for (int i = 0; i < 36; i++)
                    {
                        var bullet = poolManager.GetGameObject(1, Utilities.vec.up * 1, Quaternion.identity);
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(2, 3, Mathf.Abs((i % 9) - 4f) * 0.5f + 1.5f, 1, Utilities.vec.forward * (10 * i + rand));
                    }
                    for (int i = 0; i < 36; i++)
                    {
                        var bullet = poolManager.GetGameObject(1, Utilities.vec.up * 1, Quaternion.identity);
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(3, 4, Mathf.Abs((i % 9) - 4f) * 0.5f + 1.5f, 1, Utilities.vec.forward * (10 * i + rand));
                    }
                }
                break;
        }
    }
}
