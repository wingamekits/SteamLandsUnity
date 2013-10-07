using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour
{
    public GameObject textureObject;
    public int Hp;
    public GameObject explodePrefab;
    public GameObject smokePrefab;

    public AudioClip impactSFX;
    public AudioClip hitLaserSFX;

    float colorModifier = 1.0f;
    bool isDead = false;
    float isDeadTimer = 0.3f;
    public int enemyDifficulty;
    float speed = 3.0f;
    GameObject player;

    AnimationHandler animationHandler;
    PlayerHandler playerHandler;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (collision.rigidbody.name == "Player")
            {
                Instantiate(explodePrefab, this.transform.position, Quaternion.identity);
                Instantiate(smokePrefab, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                Camera.main.audio.PlayOneShot(impactSFX);
                
            }

            if (collision.rigidbody.tag == "Laser")
            {
                Hp -= 1;

                Camera.main.audio.PlayOneShot(hitLaserSFX);

                Destroy(collision.gameObject);
                Instantiate(smokePrefab, this.transform.position, Quaternion.identity);

                if (Hp <= 0)
                {
                    isDead = true;

                    if (animationHandler != null)
                    {
                        animationHandler.playAnimationSetNumber = 2;
                    }
                }
                else
                {
                    colorModifier = 0.0f;
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        GameObject _p = GameObject.Find("Player");
        if (_p != null)
        {
            player = _p;

            playerHandler = player.GetComponent<PlayerHandler>();
        }

        animationHandler = this.GetComponent<AnimationHandler>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            isDeadTimer -= Time.deltaTime;
        } else 
        {
            Vector3 moveVector = Vector3.left;
            transform.position += moveVector * speed * Time.deltaTime;
        }


        textureObject.gameObject.renderer.material.color = new Color(1.0f, colorModifier, colorModifier);

        if (colorModifier < 1.0f)
            colorModifier += Time.deltaTime;

        if (player != null)
        {
            if (this.transform.position.x <= player.transform.position.x - 10.0f)
            {
                Destroy(this.gameObject);
            }
        }

        if (isDeadTimer <= 0.0f)
        {
            Camera.main.audio.PlayOneShot(impactSFX);
            Instantiate(explodePrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            playerHandler.Xp += enemyDifficulty * 10;
        }

        if (transform.position.y > -2.0)
        {
            transform.position = new Vector3(transform.position.x,
                                            -2.0f,
                                            transform.position.z);
        }

        if (transform.position.y < -5.5)
        {
            transform.position = new Vector3(transform.position.x,
                                            -5.5f,
                                            transform.position.z);
        }
    }
}


