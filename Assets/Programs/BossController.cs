using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    GameObject gd;
    GameManager gm;
    PoolManager pm;
    Transform tf;
    float time;
    bool alive = true;
    float spawn_time;
    float attack_time;

    int attack_pattern;
    int attack_data;
    int target;
    Vector3 target_pos;
    int health;
    int P1damage;
    int P2damage;

    //[SerializeField]
    GameObject P1;

    //[SerializeField]
    GameObject P2;

    [SerializeField]
    GameObject pointer_prefab;

    [SerializeField]
    GameObject explosion_large_prefab;
    [SerializeField]
    GameObject explosion_small_prefab;

    GameObject target_obj;

    [SerializeField]
    ParticleSystem charge_particle;

    float deathtimer;

    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        gd = GameObject.FindGameObjectWithTag("GameController");
        gm = gd.GetComponent<GameManager>();
        pm = gm.GetComponent<PoolManager>();

        spawn_time = 1;
        attack_pattern = 0;
        attack_time = 1;
        health = (int)(GameManager.gametime * 3f);

        P1 = GameObject.FindGameObjectWithTag("P1");
        P2 = GameObject.FindGameObjectWithTag("P2");
        P1damage = 0;
        P2damage = 0;

        deathtimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause == true)
        {
            return;
        }

        if (!alive)
        {
            return;
        }

        if (!GameManager.GameNow)
        {
            bossDead();
            return;
        }

        if (spawn_time > 0)
        {
            tf.Translate(0,-7 * Time.deltaTime * spawn_time,0);

            spawn_time -= Time.deltaTime;
        }

        attack_time -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (GameManager.pause == true)
        {
            return;
        }

        if (!alive)
        {
            if (deathtimer < 2)
            {
                if (Random.Range(0, 100)<10)
                {
                    Instantiate(explosion_small_prefab, tf.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                }
                deathtimer += Time.deltaTime;
            }
            else
            {
                GameManager.P1camerashake = 0.5f;
                GameManager.P2camerashake = 0.5f;
                Instantiate(explosion_large_prefab, tf.position, Quaternion.identity);
                Destroy(gameObject);
            }
            return;
        }

        if (attack_time < 0)
        {
            attack_pattern = Random.Range(1, 4);
            switch (attack_pattern)
            {
                case 1:
                    attack_time = 5;
                    break;
                case 2:
                    attack_time = 9;
                    break;
                case 3:
                    attack_time = 7;
                    break;
            }
            //attack_pattern = 2;
            //attack_time = 9;
        }

        switch (attack_pattern)
        {
            case 1:
                if (attack_time == 5)
                {
                    setTarget();
                    target_pos = target_obj.transform.position;

                    Instantiate(charge_particle, tf.position, Quaternion.identity);
                }

                if (attack_time > 1 && attack_time < 3)
                {
                    if (Random.Range(0f, 100f) < 30)
                    {
                        var bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        var dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, Random.Range(7, 9), Random.Range(2f, 3.5f) * GameManager.difficulty_modifier, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles * Random.Range(0.9f, 1.1f));
                    }
                    if (Random.Range(0f, 100f) < 20)
                    {
                        var bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        var dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, Random.Range(1, 2), Random.Range(2f, 3.5f) * GameManager.difficulty_modifier, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles * Random.Range(0.9f, 1.1f));
                    }
                    if (Random.Range(0f, 100f) < 40 + GameManager.gamedifficulty * 20 + GameManager.Timedifficulty * 2)
                    {
                        var bullet2 = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        bullet2.GetComponent<Bullet2Controller>().SetupBullet(0, Random.Range(1, 2), Random.Range(3.5f, 4.5f) * GameManager.difficulty_modifier, 0, Utilities.vec.forward * Random.Range(0, 360));
                    }
                }
                break;
            case 2:
                if (attack_time == 9)
                {
                    setTarget();
                    target_pos = target_obj.transform.position;

                    Instantiate(charge_particle, tf.position, Quaternion.identity);
                    attack_data = 5;
                }

                if (attack_time > 1 && attack_time < 7)
                {
                    if (attack_data < 0)
                    {
                        if (attack_time < 4)
                        {
                            for(int i = 0; i < 2 + (int)(GameManager.Timedifficulty/4); i++)
                            {
                                var bullet2 = pm.GetGameObject(1, tf.position, Quaternion.identity);
                                bullet2.GetComponent<Bullet2Controller>().SetupBullet(0, 3, Random.Range(4.5f,5.5f), 0, Utilities.vec.forward * 180 * (1 - 4/attack_time) * Random.Range(0.9f,1.1f));
                                bullet2 = pm.GetGameObject(1, tf.position, Quaternion.identity);
                                bullet2.GetComponent<Bullet2Controller>().SetupBullet(0, 4, Random.Range(4.5f, 5.5f), 0, Utilities.vec.forward * -180 * (1 - 4 / attack_time) * Random.Range(0.9f, 1.1f));
                                if (Random.Range(0, 100) < 10 + GameManager.Timedifficulty + GameManager.gamedifficulty * 10)
                                {
                                    bullet2 = pm.GetGameObject(1, tf.position, Quaternion.identity);
                                    bullet2.GetComponent<Bullet2Controller>().SetupBullet(0, 1, Random.Range(4.5f, 5.5f), 0, Utilities.vec.forward * 180 * (1 - 4 / attack_time) * Random.Range(0.9f, 1.1f));
                                    bullet2 = pm.GetGameObject(1, tf.position, Quaternion.identity);
                                    bullet2.GetComponent<Bullet2Controller>().SetupBullet(0, 2, Random.Range(4.5f, 5.5f), 0, Utilities.vec.forward * -180 * (1 - 4 / attack_time) * Random.Range(0.9f, 1.1f));
                                }
                            }
                            if (GameManager.gamedifficulty == 3)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    var bullet2 = pm.GetGameObject(1, tf.position, Quaternion.identity);
                                    bullet2.GetComponent<Bullet2Controller>().SetupBullet(0, 3, Random.Range(2.5f, 3.5f), 0, Utilities.vec.forward * 180 * (1 - 4 / attack_time) * Random.Range(0.9f, 1.1f));
                                    bullet2 = pm.GetGameObject(1, tf.position, Quaternion.identity);
                                    bullet2.GetComponent<Bullet2Controller>().SetupBullet(0, 4, Random.Range(2.5f, 3.5f), 0, Utilities.vec.forward * -180 * (1 - 4 / attack_time) * Random.Range(0.9f, 1.1f));
                                }
                            }
                        }

                        //à»â∫ÉèÉCÉìÉ_Å[
                        var bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        var dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 7, 5, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles + Utilities.vec.forward * 20);

                        bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 7, 5, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles - Utilities.vec.forward * 20);

                        bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 7, 5, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles + Utilities.vec.forward * 60);

                        bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 7, 5, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles - Utilities.vec.forward * 60);

                        bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 7, 5, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles + Utilities.vec.forward * 90);

                        bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 7, 5, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles - Utilities.vec.forward * 90);

                        bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 7, 5, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles + Utilities.vec.forward * 120);

                        bullet = pm.GetGameObject(1, tf.position, Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 7, 5, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles - Utilities.vec.forward * 120);


                        attack_data = 5;
                    }
                    attack_data--;
                }
                break;
            case 3:
                if (attack_time == 7)
                {
                    setTarget();
                    //target_pos = target_obj.transform.position;

                    Instantiate(charge_particle, tf.position, Quaternion.identity);
                    attack_data = 1;
                }
                if (attack_data == 1&&attack_time<5f)
                {
                    target_pos = target_obj.transform.position;
                    for (int i = 0; i < 10; i++)
                    {
                        var bullet = pm.GetGameObject(1, new Vector3(0.5f,3,0), Quaternion.identity);
                        var dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 8, 3 + i/2, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles,0.8f);

                        bullet = pm.GetGameObject(1, new Vector3(-0.5f, 3, 0), Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 8, 3 + i / 2, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles,0.8f);
                    }
                    attack_data++;
                }

                if (attack_data == 2 && attack_time < 4.8f)
                {
                    target_pos = target_obj.transform.position;
                    for (int i = 0; i < 10; i++)
                    {
                        var bullet = pm.GetGameObject(1, new Vector3(1f, 3, 0), Quaternion.identity);
                        var dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 8, 3 + i / 2, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles,0.6f);

                        bullet = pm.GetGameObject(1, new Vector3(-1f, 3, 0), Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 8, 3 + i / 2, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles,0.6f);
                    }
                    attack_data++;
                }

                if (attack_data == 3 && attack_time < 4.6f)
                {
                    target_pos = target_obj.transform.position;
                    for (int i = 0; i < 10; i++)
                    {
                        var bullet = pm.GetGameObject(1, new Vector3(1.5f, 3, 0), Quaternion.identity);
                        var dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 8, 3 + i / 2, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles,0.4f);

                        bullet = pm.GetGameObject(1, new Vector3(-1.5f, 3, 0), Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 8, 3 + i / 2, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles,0.4f);
                    }
                    attack_data++;
                }

                if (attack_data == 4 && attack_time < 4.4f)
                {
                    target_pos = target_obj.transform.position;
                    for (int i = 0; i < 10; i++)
                    {
                        var bullet = pm.GetGameObject(1, new Vector3(2f, 3, 0), Quaternion.identity);
                        var dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 8, 3 + i / 2, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles,0.2f);

                        bullet = pm.GetGameObject(1, new Vector3(-2f, 3, 0), Quaternion.identity);
                        dir = target_pos - bullet.transform.position;
                        bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 8, 3 + i / 2, 0, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles,0.2f);
                    }
                    attack_data++;
                }
                break;
        }
    }

    void setTarget()
    {
        if (P1.transform.position.y > P2.transform.position.y)
        {
            target_obj = P1;
            GameObject pointer = Instantiate(pointer_prefab,P1.transform.position,Quaternion.identity);
            pointer.transform.parent = P1.transform;
        }
        else
        {
            target_obj = P2;
            GameObject pointer = Instantiate(pointer_prefab,P2.transform.position, Quaternion.identity);
            pointer.transform.parent = P2.transform;
        }
    }

    void bossDead()
    {
        GameManager.bosscount = 30;
        GameManager.inboss = false;
        alive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alive)
        {
            return;
        }

        if (collision.gameObject.tag == "P1Bullet" || collision.gameObject.tag == "P2Bullet" || collision.gameObject.tag == "P1Laser" || collision.gameObject.tag == "P2Laser")
        {
            if (collision.gameObject.tag == "P1Bullet" || collision.gameObject.tag == "P2Bullet")
            {
                if (collision.GetComponent<BulletController>().HitBullet())
                {
                    health--;
                }

                if (collision.gameObject.tag == "P1Bullet")
                {
                    P1damage++;
                }
                else if (collision.gameObject.tag == "P2Bullet")
                {
                    P2damage++;
                }
            }
            else
            {
                if (collision.gameObject.tag == "P1Laser")
                {
                    health -= 10;
                    P1damage += 10;
                }
                else if (collision.gameObject.tag == "P2Laser")
                {
                    health -= 10;
                    P2damage += 10;
                }
            }

            if (health <= 0)
            {
                alive = false;
                bossDead();
            }
        }
    }
}
