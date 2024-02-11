using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float speed;

    GameObject gd;
    GameManager gm;

    PoolManager poolManager;

    Transform tf;

    SpriteRenderer sr;

    bool alive;

    Vector3 default_size;
    // Start is called before the first frame update
    void Awake()
    {
        tf = GetComponent<Transform>();
        gd = GameObject.FindGameObjectWithTag("GameController");
        gm = gd.GetComponent<GameManager>();
        poolManager = gd.GetComponent<PoolManager>();
        sr = GetComponent<SpriteRenderer>();

        default_size = new Vector3(0.5f,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause==true)
        {
            return;
        }

        if (!alive)
        {
            tf.Translate(Utilities.vec.up * speed * Time.deltaTime * 0.1f);
            var color = sr.color;
            color.a -= color.a * Time.deltaTime * 10;
            sr.color = color;
            return;
        }

        tf.Translate(Utilities.vec.up * speed * Time.deltaTime);
        if(tf.position.y > 5)
        {
            RemoveBullet();
        }
    }

    private void OnEnable()
    {
        var color = sr.color;
        color.a = 1;
        sr.color = color;
        tf.localScale = default_size;
        alive = true;
    }

    public bool HitBullet()
    {
        if (alive)
        {
            alive = false;
            tf.localScale = default_size + Utilities.vec.up;
            Invoke("RemoveBullet", 0.2f);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveBullet()
    {
        if (gameObject.activeSelf)
        {
            poolManager.ReleaseGameObject(gameObject, 0);
        }
    }

        public void setupBullet(int parent)
    {
        switch (parent)
        {
            case 1:
                gameObject.tag = "P1Bullet";
                sr.color = Utilities.color.expblue;
                break;
            case 2:
                gameObject.tag = "P2Bullet";
                sr.color = Utilities.color.expred;
                break;
        }
    }
}
