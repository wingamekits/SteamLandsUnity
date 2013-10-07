using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerHandler : MonoBehaviour {
    float speed = 4.0f;
    public GameObject laserPrefab;
    public GameObject textureObject;

    public GameObject systemMalfunction;

    float shootTimer = 0.0f;
    float setShootTimerTo = 0.5f;

    public int Xp = 0;
    public int Level = 0;

    public List<int> levelList = new List<int>();
    PlayerHandler playerHandler;

    public int HP;
    float colorModifier = 1.0f;

    bool isGameOver = false;
    float gameOverTimer = 3.0f;

    public AudioClip shotLaserSFX;

    AnimationHandler animationHandler;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (collision.rigidbody.tag == "Enemy")
            {
                HP -= 1;
                colorModifier = 0.0f;
            }

            if (collision.rigidbody.tag == "PowerUp")
            {
                this.rigidbody.velocity = Vector3.zero;
                systemMalfunction.gameObject.SetActive(false);

                Destroy(collision.gameObject);
                HP += 2;
                if (HP > 10)
                    HP = 10;
            }
        }
    }

	// Use this for initialization
    void Start () {
        int currentXp = 0;
        int xpToIncrease = 50;

        for (int i = 0; i < 20; i++)
        {
            currentXp += xpToIncrease;
            levelList.Add(currentXp);
            xpToIncrease = (int)(xpToIncrease * 1.25);
        }

        playerHandler = this.GetComponent<PlayerHandler>();
        animationHandler = this.GetComponent<AnimationHandler>();

    }
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0)
        {
            isGameOver = true;
            animationHandler.playAnimationSetNumber = 2;
            gameOverTimer -= Time.deltaTime;

            if (gameOverTimer <= 0.0f)
            {
                PlayerPrefs.SetInt("Score", Xp);
                PlayerPrefs.SetInt("Level", Level);

                Application.LoadLevel("GameOver");
            }
        }

        if (rigidbody.velocity.y > 0.1 || rigidbody.velocity.y < -0.1)
        {
            systemMalfunction.gameObject.SetActive(true);
        }

        if (!isGameOver)
        {
            Vector3 movePlayerVector = Vector3.right;
            shootTimer -= Time.deltaTime;

            textureObject.gameObject.renderer.material.color = new Color(1.0f, colorModifier, colorModifier);

            if (colorModifier < 1.0f)
                colorModifier += Time.deltaTime;

            bool hasShot = false;

            if (Input.GetMouseButton(0))
            {
                Vector3 touchWorldPoint = Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x,
                                Input.mousePosition.y,
                                10.0f));

                if (touchWorldPoint.x < this.transform.position.x + 5.0f)
                {
                    if (touchWorldPoint.y > this.transform.position.y)
                    {
                        movePlayerVector.y = 1.0f;
                    }
                    else movePlayerVector.y = -1.0f;
                }
                else
                {
                    if (shootTimer <= 0)
                    {
                        Vector3 shootPos = this.transform.position;
                        shootPos.x += 2;
                        Instantiate(laserPrefab, shootPos, Quaternion.identity);
                        shootTimer = setShootTimerTo;
                        hasShot = true;
                    }
                }
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                movePlayerVector.y = 1.0f;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                movePlayerVector.y = -1.0f;
            }

            if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 0"))
            {
                if (shootTimer <= 0)
                {
                    Vector3 shootPos = this.transform.position;
                    shootPos.x += 2;
                    Instantiate(laserPrefab, shootPos, Quaternion.identity);
                    shootTimer = setShootTimerTo;
                    hasShot = true;
                }
            }

            if (hasShot)
            {
                Camera.main.audio.PlayOneShot(shotLaserSFX);
            }

            float joyY = Input.GetAxis("Vertical");
            if(joyY > 0 || joyY < 0)
                movePlayerVector.y = joyY;

            this.transform.position += movePlayerVector * Time.deltaTime * speed;

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

            if (playerHandler.Xp >= levelList[Level])
            {
                Level++;
            }
        }
	}
}
