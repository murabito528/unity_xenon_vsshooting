using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject gd;
    GameManager gm;
    PoolManager poolManager;
    Transform tf;

    float shotcool;
    float angle;
    float speed;
    float waittime;
    int health;

    int type;
    [SerializeField]
    int enemy_type;
    float time;
    bool alive = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        tf = GetComponent<Transform>();
        gd = GameObject.FindGameObjectWithTag("GameController");
        gm = gd.GetComponent<GameManager>();
        poolManager = gd.GetComponent<PoolManager>();

        shotcool = 0;
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }

        if (!GameManager.GameNow)
        {
            RemoveEnemy();
        }

        if (waittime > 0)
        {
            waittime -= Time.deltaTime;
            return;
        }

        switch (type)
        {
            case 1:
                if (time < 0.5f)
                {
                    tf.position = Vector3.Lerp(new Vector3(3.5f, 4f, 0), new Vector3(2f, 3f, 0),Mathf.Min(time*2.5f,1));
                    break;
                }

                if(time < 1.7f)
                {
                    break;
                }

                tf.Translate(-0.7f * Time.deltaTime * speed, -1 * Time.deltaTime * speed, 0);
                
                //tf.position -= Utilities.vec.right * Time.deltaTime * speed;
                break;
            case 2:
                if(time < 1)
                {
                    break;
                }
                if (time < 1.5f)
                {
                    tf.position = Vector3.Lerp(new Vector3(3.5f, 2.2f, 0), new Vector3(1.7f, 2.2f, 0), Mathf.Min((time-1) * 2.5f, 1));
                    break;
                }

                if (time < 1.7f)
                {
                    break;
                }

                tf.Translate(-0.7f * Time.deltaTime * speed, -1 * Time.deltaTime * speed, 0);
                break;
            case 3:
                if (time < 1)
                {
                    break;
                }
                if (time < 1.5f)
                {
                    tf.position = Vector3.Lerp(new Vector3(3.5f, 1.9f, 0), new Vector3(1.4f, 1.4f, 0), Mathf.Min((time - 1) * 2.5f, 1));
                    break;
                }

                if (time < 1.7f)
                {
                    break;
                }

                tf.Translate(-0.7f * Time.deltaTime * speed, -1 * Time.deltaTime * speed, 0);
                break;
            case 4:
                if (time < 1)
                {
                    break;
                }
                if (time < 1.5f)
                {
                    tf.position = Vector3.Lerp(new Vector3(3.5f, 1.6f, 0), new Vector3(1.1f, 0.5f, 0), Mathf.Min((time - 1) * 2.5f, 1));
                    break;
                }

                if (time < 1.7f)
                {
                    break;
                }

                tf.Translate(-0.7f * Time.deltaTime * speed, -1 * Time.deltaTime * speed, 0);
                break;

            case 5:
                if (time < 0.5f)
                {
                    tf.position = Vector3.Lerp(new Vector3(-3.5f, 4f, 0), new Vector3(-2f, 3f, 0), Mathf.Min(time * 2.5f, 1));
                    break;
                }

                if (time < 1.7f)
                {
                    break;
                }

                tf.Translate(0.7f * Time.deltaTime * speed, -1 * Time.deltaTime * speed, 0);

                //tf.position -= Utilities.vec.right * Time.deltaTime * speed;
                break;
            case 6:
                if (time < 1)
                {
                    break;
                }
                if (time < 1.5f)
                {
                    tf.position = Vector3.Lerp(new Vector3(-3.5f, 2.2f, 0), new Vector3(-1.7f, 2.2f, 0), Mathf.Min((time - 1) * 2.5f, 1));
                    break;
                }

                if (time < 1.7f)
                {
                    break;
                }

                tf.Translate(0.7f * Time.deltaTime * speed, -1 * Time.deltaTime * speed, 0);
                break;
            case 7:
                if (time < 1)
                {
                    break;
                }
                if (time < 1.5f)
                {
                    tf.position = Vector3.Lerp(new Vector3(-3.5f, 1.9f, 0), new Vector3(-1.4f, 1.4f, 0), Mathf.Min((time - 1) * 2.5f, 1));
                    break;
                }

                if (time < 1.7f)
                {
                    break;
                }

                tf.Translate(0.7f * Time.deltaTime * speed, -1 * Time.deltaTime * speed, 0);
                break;
            case 8:
                if (time < 1)
                {
                    break;
                }
                if (time < 1.5f)
                {
                    tf.position = Vector3.Lerp(new Vector3(-3.5f, 1.6f, 0), new Vector3(-1.1f, 0.5f, 0), Mathf.Min((time - 1) * 2.5f, 1));
                    break;
                }

                if (time < 1.7f)
                {
                    break;
                }

                tf.Translate(0.7f * Time.deltaTime * speed, -1 * Time.deltaTime * speed, 0);
                break;
            case 9:
                tf.Translate(0,Time.deltaTime * speed, 0);
                break;
        }
        time+=Time.deltaTime;

        if (Mathf.Abs(tf.position.y) > 5.5 || Mathf.Abs(tf.position.x) > 5)
        {
            RemoveEnemy();
        }
    }

    void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void SetupEnemy(int type,float waittime)
    {
        this.type = type;
        this.time = 0;
        this.health = 3;
        this.speed = 2;
        this.waittime = waittime;

        if (enemy_type == 3)
        {
            health = 20;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alive)
        {
            return;
        }
        if(waittime > 0)
        {
            return;
        }
        /*
        if(collision.gameObject.tag == "bullet_remover")
        {
            health--;
            Debug.Log("bombhit");
        }*/

        if (collision.gameObject.tag == "P1Bullet"|| collision.gameObject.tag == "P2Bullet" || collision.gameObject.tag == "P1Laser" || collision.gameObject.tag == "P2Laser")
        {
            if (collision.gameObject.tag == "P1Bullet" || collision.gameObject.tag == "P2Bullet")
            {
                if (collision.GetComponent<BulletController>().HitBullet())
                {
                    health--;
                }
            }
            else
            {
                if (collision.gameObject.tag == "P1Laser")
                {
                    var exppre = Instantiate(gm.explosion_large_Prefab, tf.position, Quaternion.identity);
                    exppre.tag = "P1bullet_remover";
                    if (enemy_type == 2)
                    {
                        var laser = Instantiate(gm.straightlaserP1_Prefab, tf.position, Quaternion.identity);
                        laser.transform.rotation = transform.GetChild(0).rotation;
                    }

                    if (enemy_type == 3)
                    {
                        for (int i = 0; i < 5 + Mathf.Floor(GameManager.P1difficulty / 4); i++)
                        {
                            Instantiate(gm.powerorbP1_Prefab, tf.position, Quaternion.identity);
                        }
                        if (Random.Range(0, 100) < 20 + (GameManager.P1difficulty + GameManager.Timedifficulty) * 2)//20~84
                        {
                            Instantiate(gm.exattackorbP1_Prefab, tf.position, Quaternion.identity);
                        }

                        GameManager.P1bombpoint += 4;
                    }
                    else
                    {
                        for (int i = 0; i < 2 + Mathf.Floor(GameManager.P1difficulty / 4); i++)
                        {
                            Instantiate(gm.powerorbP1_Prefab, tf.position, Quaternion.identity);
                        }
                        GameManager.P1bombpoint += 2;
                    }

                }
                else if (collision.gameObject.tag == "P2Laser")
                {
                    var exppre = Instantiate(gm.explosion_large_Prefab, tf.position, Quaternion.identity);
                    exppre.tag = "P2bullet_remover";
                    
                    if (enemy_type == 2)
                    {
                        var laser = Instantiate(gm.straightlaserP2_Prefab, tf.position, Quaternion.identity);
                        laser.transform.rotation = transform.GetChild(0).rotation;
                    }

                    if (enemy_type == 3)
                    {
                        for (int i = 0; i < 5 + Mathf.Floor(GameManager.P2difficulty / 4); i++)
                        {
                            Instantiate(gm.powerorbP2_Prefab, tf.position, Quaternion.identity);
                        }
                        if (Random.Range(0, 100) < 20 + (GameManager.P2difficulty + GameManager.Timedifficulty) * 2)//20~84
                        {
                            Instantiate(gm.exattackorbP2_Prefab, tf.position, Quaternion.identity);
                        }
                        GameManager.P2bombpoint += 4;
                    }
                    else
                    {
                        for (int i = 0; i < 2 + Mathf.Floor(GameManager.P2difficulty / 4); i++)
                        {
                            Instantiate(gm.powerorbP2_Prefab, tf.position, Quaternion.identity);
                        }
                        GameManager.P1bombpoint += 2;
                    }
                }

                alive = false;
                RemoveEnemy();
                return;
            }

            if (health <= 0)
            {   
                if (collision.gameObject.tag == "P1Bullet")
                {
                    if (enemy_type == 2)
                    {
                        var gameObject = Instantiate(gm.straightlaserP1_Prefab, tf.position, Quaternion.identity);
                        gameObject.transform.rotation = transform.GetChild(0).rotation;
                    }

                    if(enemy_type == 3)
                    {
                        for (int i = 0; i < 3 + Mathf.Floor(GameManager.P1difficulty/5); i++)
                        {
                            Instantiate(gm.powerorbP1_Prefab, tf.position, Quaternion.identity);
                        }
                        GameManager.P1bombpoint+=2;
                    }
                    else
                    {
                        for (int i = 0; i < 1 + Mathf.Floor(GameManager.P1difficulty / 5); i++)
                        {
                            Instantiate(gm.powerorbP1_Prefab, tf.position, Quaternion.identity);
                        }
                        GameManager.P1bombpoint++;
                    }

                    var exppre = Instantiate(gm.explosion_small_Prefab, tf.position, Quaternion.identity);
                    exppre.tag = "P1bullet_remover";
                }
                else if(collision.gameObject.tag == "P2Bullet")
                {
                    if (enemy_type == 2)
                    {
                        var gameObject = Instantiate(gm.straightlaserP2_Prefab, tf.position, Quaternion.identity);
                        gameObject.transform.rotation = transform.GetChild(0).rotation;
                    }

                    if (enemy_type == 3)
                    {
                        for (int i = 0; i < 3 + Mathf.Floor(GameManager.P2difficulty / 5); i++)
                        {
                            Instantiate(gm.powerorbP2_Prefab, tf.position, Quaternion.identity);
                        }
                        GameManager.P2bombpoint += 2;
                    }
                    else
                    {
                        for (int i = 0; i < 1 + Mathf.Floor(GameManager.P2difficulty / 5); i++)
                        {
                            Instantiate(gm.powerorbP2_Prefab, tf.position, Quaternion.identity);
                        }
                        GameManager.P2bombpoint++;
                    }

                    var exppre = Instantiate(gm.explosion_small_Prefab, tf.position, Quaternion.identity);
                    exppre.tag = "P2bullet_remover";
                }
                /*else if (collision.gameObject.tag == "bullet_remover")
                {
                    var exppre = Instantiate(gm.explosion_small_Prefab, tf.position, Quaternion.identity);
                    exppre.tag = "bullet_remover";
                }*/

                alive = false;

                RemoveEnemy();
            }
        }
    }
}
