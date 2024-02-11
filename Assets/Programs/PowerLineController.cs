using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLineController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sr;
    float time;
    int phase;
    [SerializeField]
    bool P1;
    [SerializeField]
    bool P2;

    GameObject gd;
    PoolManager pm;
    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(0.1f,0.2f);
        phase = 0;
        sr.enabled = false;

        gd = GameObject.FindWithTag("GameController");
        pm = gd.GetComponent<PoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }

        time -= Time.deltaTime;
        switch (phase)
        {
            case 0:
                if (time < 0)
                {
                    phase++;
                    time = 0.3f;
                    setLine();
                    sr.enabled = true;
                }
                break;
            case 1:
                if (time < 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    var color = sr.color;
                    color.a = time/0.3f;
                    sr.color = color;
                }
                break;
        }
    }

    void setLine()
    {
        if (P1)
        {
            Vector3 target_pos = new Vector3(Random.Range(-3.8f, 3.8f) + 104.3f, Random.Range(2f, 4.2f), 0);//
            transform.Translate(95.7f, 0, 0);
            float dist = Vector2.Distance(transform.position, target_pos);
            transform.localScale = new Vector2(dist * 1.55f, 0.2f);
            var dir = target_pos - transform.position;
            transform.localRotation = Quaternion.FromToRotation(Utilities.vec.right, dir);

            if (Random.Range(0, 10) < 6)
            {
                if (Random.Range(0, 7) == 0)
                {
                    //var bullet = pm.GetGameObject(1, Utilities.vec.right * Random.Range(-3f, 3f) + Utilities.vec.up * Random.Range(4f, 4.5f), Quaternion.identity);
                    var bullet = pm.GetGameObject(1, target_pos - Utilities.vec.right * 104.3f, Quaternion.identity);
                    bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 3, Random.Range(1.5f, 3f + (GameManager.P1difficulty+GameManager.Timedifficulty) / 20), 2, Utilities.vec.forward * Random.Range(150f, 210f));
                }
                else
                {
                    var bullet = pm.GetGameObject(1, target_pos - Utilities.vec.right * 104.3f, Quaternion.identity);
                    bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 1, Random.Range(2f, 3.5f + (GameManager.P1difficulty + GameManager.Timedifficulty) / 20), 2, Utilities.vec.forward * Random.Range(150f, 210f));
                }
            }
            else
            {
                //var bullet = pm.GetGameObject(1, Utilities.vec.right * Random.Range(-3f, 3f) + Utilities.vec.up * Random.Range(4f, 4.5f), Quaternion.identity);
                var bullet = pm.GetGameObject(1, target_pos - Utilities.vec.right * 104.3f, Quaternion.identity);
                dir = GameObject.FindWithTag("P2").transform.position - bullet.transform.position;
                bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 2, Random.Range(2f, 3.5f + (GameManager.P1difficulty + GameManager.Timedifficulty) / 20), 2, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles * Random.Range(0.9f, 1.1f));
            }
        }

        if (P2)
        {
            Vector3 target_pos = new Vector3(Random.Range(-3.8f, 3.8f) + 95.7f, Random.Range(2f, 4.2f), 0);
            transform.Translate(104.3f, 0, 0);//
            float dist = Vector2.Distance(transform.position, target_pos);
            transform.localScale = new Vector2(dist * 1.55f, 0.2f);
            var dir = target_pos - transform.position;
            transform.localRotation = Quaternion.FromToRotation(Utilities.vec.right, dir);

            if (Random.Range(0, 10) < 6)
            {
                if (Random.Range(0, 7) == 0)
                {
                    //var bullet = pm.GetGameObject(1, Utilities.vec.right * Random.Range(-3f, 3f) + Utilities.vec.up * Random.Range(4f, 4.5f), Quaternion.identity);
                    var bullet = pm.GetGameObject(1, target_pos - Utilities.vec.right * 95.7f, Quaternion.identity);
                    bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 3, Random.Range(1.5f, 3f + (GameManager.P2difficulty + GameManager.Timedifficulty) / 20), 1, Utilities.vec.forward * Random.Range(150f, 210f));
                }
                else
                {
                    var bullet = pm.GetGameObject(1, target_pos - Utilities.vec.right * 104.3f, Quaternion.identity);
                    bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 1, Random.Range(2f, 3.5f + (GameManager.P2difficulty + GameManager.Timedifficulty) / 20), 1, Utilities.vec.forward * Random.Range(150f, 210f));
                }
            }
            else
            {
                //var bullet = pm.GetGameObject(1, Utilities.vec.right * Random.Range(-3f, 3f) + Utilities.vec.up * Random.Range(4f, 4.5f), Quaternion.identity);
                var bullet = pm.GetGameObject(1, target_pos - Utilities.vec.right * 95.7f, Quaternion.identity);
                dir = GameObject.FindWithTag("P1").transform.position - bullet.transform.position;
                bullet.GetComponent<Bullet2Controller>().SetupBullet(0, 2, Random.Range(2f, 3.5f + (GameManager.P2difficulty + GameManager.Timedifficulty) / 20), 1, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles * Random.Range(0.9f, 1.1f));
            }
        }
    }
}
