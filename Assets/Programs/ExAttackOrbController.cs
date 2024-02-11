using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAttackOrbController : MonoBehaviour
{
    float speed;
    Transform tf;

    [SerializeField]
    bool P1;
    [SerializeField]
    bool P2;

    GameManager gm;
    PoolManager pm;

    Vector3 target_pos;
    Vector3 start_pos;
    float rate;


    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        speed = Random.Range(0.4f, 0.7f);
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        pm = gm.GetComponent<PoolManager>();
        if (P1)
        {
            tf.Translate(95.7f, 0, 0);
            target_pos = new Vector3(Random.Range(-3.8f, 3.8f) + 104.3f, Random.Range(2f, 4.2f), 0);
        }
        if (P2)
        {
            tf.Translate(104.3f, 0, 0);
            target_pos = new Vector3(Random.Range(-3.8f, 3.8f) + 95.7f, Random.Range(2f, 4.2f), 0);
        }
        var dir = target_pos - tf.position;
        start_pos = tf.position;
        tf.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
        rate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause)
        {
            return;
        }
        tf.position = Vector3.Lerp(start_pos, target_pos, rate);
        rate += ((1 - rate) * speed + 0.1f) *Time.deltaTime;

        if (rate > 0.95f)
        {
            if (P1)
            {
                var P2pos = GameObject.FindWithTag("P2").transform.position;

                var bullet = pm.GetGameObject(1, tf.position - Utilities.vec.right * 104.3f, Quaternion.identity);
                var dir = P2pos - bullet.transform.position;
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                //bullet.transform.Rotate(0,0,10);
                bullet.transform.Translate(0, -0.5f, 0);
                dir = P2pos - bullet.transform.position;
                bullet.GetComponent<Bullet2Controller>().SetupBullet(1, 5, 8f, 2, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles);

                bullet = pm.GetGameObject(1, tf.position - Utilities.vec.right * 104.3f, Quaternion.identity);
                dir = P2pos - bullet.transform.position;
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                bullet.transform.Rotate(0, 0, 70);
                bullet.transform.Translate(0, -0.5f, 0);
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                dir = P2pos - bullet.transform.position;
                bullet.GetComponent<Bullet2Controller>().SetupBullet(1, 5, 8f, 2, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles);

                bullet = pm.GetGameObject(1, tf.position - Utilities.vec.right * 104.3f, Quaternion.identity);
                dir = P2pos - bullet.transform.position;
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                bullet.transform.Rotate(0, 0, -70);
                bullet.transform.Translate(0, -0.5f, 0);
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                dir = P2pos - bullet.transform.position;
                bullet.GetComponent<Bullet2Controller>().SetupBullet(1, 5, 8f, 2, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles);
            }

            if (P2)
            {
                var P1pos = GameObject.FindWithTag("P1").transform.position;

                var bullet = pm.GetGameObject(1, tf.position - Utilities.vec.right * 95.7f, Quaternion.identity);
                var dir = P1pos - bullet.transform.position;
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                //bullet.transform.Rotate(0,0,10);
                bullet.transform.Translate(0, -0.5f, 0);
                dir = P1pos - bullet.transform.position;
                bullet.GetComponent<Bullet2Controller>().SetupBullet(1, 5, 8f, 1, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles);

                bullet = pm.GetGameObject(1, tf.position - Utilities.vec.right * 95.7f, Quaternion.identity);
                dir = P1pos - bullet.transform.position;
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                bullet.transform.Rotate(0, 0, 70);
                bullet.transform.Translate(0, -0.5f, 0);
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                dir = P1pos - bullet.transform.position;
                bullet.GetComponent<Bullet2Controller>().SetupBullet(1, 5, 8f, 1, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles);

                bullet = pm.GetGameObject(1, tf.position - Utilities.vec.right * 95.7f, Quaternion.identity);
                dir = P1pos - bullet.transform.position;
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                bullet.transform.Rotate(0, 0, -70);
                bullet.transform.Translate(0, -0.5f, 0);
                bullet.transform.rotation = Quaternion.FromToRotation(Utilities.vec.up, dir);
                dir = P1pos - bullet.transform.position;
                bullet.GetComponent<Bullet2Controller>().SetupBullet(1, 5, 8f, 1, Quaternion.FromToRotation(Utilities.vec.up, dir).eulerAngles);
            }
            Destroy(gameObject);
        }
    }
}
