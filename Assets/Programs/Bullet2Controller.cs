using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2Controller : MonoBehaviour
{
    
    float speed;
    int type;
    float timer;

    GameObject gd;
    GameManager gm;

    PoolManager poolManager;

    public List<Sprite> sprites;
    SpriteRenderer spriteRenderer;

    CircleCollider2D circleCollider;

    Vector3 defaultsize;

    int target;//0:—¼•û 1:P1 2:P2

    bool alive;
    float removetime;
    float spawntimer;

    bool canremove;

    float waittime;

    Transform tf;
    // Start is called before the first frame update
    void Awake()
    {
        tf = GetComponent<Transform>();
        gd = GameObject.FindGameObjectWithTag("GameController");
        gm = gd.GetComponent<GameManager>();
        poolManager = gd.GetComponent<PoolManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pause == true)
        {
            return;
        }

        if (!GameManager.GameNow)
        {
            RemoveBullet();
        }

        if (alive)
        {
            if (waittime > 0)
            {
                waittime -= Time.deltaTime;
                return;
            }

            tf.Translate(Utilities.vec.up * speed * Time.deltaTime);
            if (type == 2)
            {
                if (timer < 1.5f) tf.Rotate(0, 0, 30 * Time.deltaTime);
            }

            if (type == 3)
            {
                if (timer < 1.5f) tf.Rotate(0, 0, -30 * Time.deltaTime);
            }

            if (Mathf.Abs(tf.position.y) > 5.5 || Mathf.Abs(tf.position.x) > 4.5f)
            {
                RemoveBullet();
            }

            timer += Time.deltaTime;
        }
        else
        {
            if (spawntimer > 0 && removetime==-1)
            {
                spawntimer -= Time.deltaTime;
                tf.localScale = defaultsize * (1+spawntimer/0.35f);
                var color = spriteRenderer.color;
                color.a = 1 - spawntimer / 0.35f;
                spriteRenderer.color = color;
                if (type == 1)
                {
                    tf.Translate(0, spawntimer * -1 * Time.deltaTime, 0);
                }
                
                if (spawntimer <= 0)
                {
                    color = spriteRenderer.color;
                    color.a = 1;
                    spriteRenderer.color = color;
                    tf.localScale = defaultsize;
                    alive = true;
                }
            }
            else
            {
                removetime += Time.deltaTime;
                if (removetime >= 0.35f)
                {
                    RemoveBullet();
                }
                else
                {
                    tf.localScale += Utilities.vec.one * Time.deltaTime;
                    var color = spriteRenderer.color;
                    color.a = (0.35f - removetime) / 0.35f;
                    spriteRenderer.color = color;
                }
            }
        }
    }

    public void RemoveBullet()
    {
        if (gameObject.activeSelf)
        {
            alive = false;
            poolManager.ReleaseGameObject(this.gameObject, 1);
        }
    }

    public void EraseBullet()
    {
        if (!alive)
        {
            return;
        }
        removetime = 0;
        alive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alive)
        {
            return;
        }

        if (collision.tag == "bullet_remover")
        {
            EraseBullet();
        }

        if (collision.tag == "P1bullet_remover" && target != 2 && canremove == true)
        {
            if(Random.Range(0,100) < 20 + GameManager.P1difficulty * 5)
            {
                Instantiate(gm.powerLineP1_Prefab, tf.position, Quaternion.identity);
            }
            GameManager.P1bombpoint++;
            EraseBullet();
        }
        if (collision.tag == "P2bullet_remover" && target != 1 && canremove == true) 
        {
            if (Random.Range(0, 100) < 20 + GameManager.P2difficulty * 5)
            {
                Instantiate(gm.powerLineP2_Prefab, tf.position, Quaternion.identity);
            }
            GameManager.P2bombpoint++;
            EraseBullet();
        }
    }

    public void SetupBullet(int type,int sprite_type,float speed, int target, Vector3 rotation)
    {
        
        this.speed = speed;
        this.type = type;
        this.target = target;
        //this.alive = true;
        this.timer = 0;

        spriteRenderer.sprite = sprites[sprite_type];
        spriteRenderer.color = Utilities.color.white;

        spawntimer = 0.35f;
        removetime = -1;
        canremove = true;
        switch (sprite_type)
        {
            case 0:
                //tf.localScale = Utilities.vec.one;
                defaultsize = Utilities.vec.one;
                circleCollider.radius = 0.12f;
                break;
            case 1:
                //tf.localScale = Utilities.vec.one * 0.2f;
                defaultsize = Utilities.vec.one * 0.2f;
                circleCollider.radius = 0.3f;
                break;
            case 2:
                //tf.localScale = Utilities.vec.one * 0.2f;
                defaultsize = Utilities.vec.one * 0.2f;
                circleCollider.radius = 0.3f;
                break;
            case 3:
                //tf.localScale = Utilities.vec.one * 0.2f;
                defaultsize = Utilities.vec.one;
                circleCollider.radius = 0.13f;
                canremove = false;
                break;
            case 4:
                //tf.localScale = Utilities.vec.one * 0.2f;
                defaultsize = Utilities.vec.one;
                circleCollider.radius = 0.13f;
                canremove = false;
                break;
            case 5:
                //tf.localScale = Utilities.vec.one * 0.2f;
                defaultsize = Utilities.vec.one * 0.4f;
                circleCollider.radius = 0.3f;
                canremove = false;
                break;
            case 6:
                //tf.localScale = Utilities.vec.one * 0.2f;
                defaultsize = Utilities.vec.one * 0.4f;
                circleCollider.radius = 0.3f;
                canremove = false;
                break;
            case 7:
                defaultsize = Utilities.vec.one * 0.8f;
                circleCollider.radius = 0.1f;
                canremove = false;
                break;
            case 8:
                defaultsize = Utilities.vec.one * 0.8f;
                circleCollider.radius = 0.1f;
                canremove = false;
                break;
            default:
                Debug.LogError("NoTypeError Type:" + sprite_type);
                break;
        }

        switch (target)
        {
            case 0:
                tag = "Bullet";
                gameObject.layer = 0;//Default
                break;
            case 1:
                tag = "targetP1Bullet";
                gameObject.layer = 6;//P1Only
                break;
            case 2:
                tag = "targetP2Bullet";
                gameObject.layer = 7;//P2Only
                break;
        }
        
        tf.rotation = Quaternion.Euler(rotation);
        tf.localScale = defaultsize;
    }

    public void SetupBullet(int type, int sprite_type, float speed, int target, Vector3 rotation, float waittime)
    {
        SetupBullet(type,sprite_type,speed,target,rotation);
        this.waittime = waittime;
    }
}
