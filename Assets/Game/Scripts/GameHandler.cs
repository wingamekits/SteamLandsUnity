using UnityEngine;
using System.Collections;
public class GameHandler : MonoBehaviour
{
    public int level;

    public GameObject enemy;
    public GameObject enemy2;

    public GameObject player;

    public PlayerHandler playerHandler;

    public GUIText levelLabel;
    public GUIText xpLabel;
    public GUIText hpLabel;
    public GUIText scoreLabel;

    public GameObject hpPowerUp;

    public GameObject building1;
    public GameObject building2;

    float colorModifier = 1.0f;

    float spawnNewEnemyTimer = 2;

    // Use this for initialization
    void Start()
    {
        playerHandler = player.GetComponent<PlayerHandler>();
    }

    void HandleBackbutton()
    {
        Application.LoadLevel("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        // Handle the back button on Windows Phone 8
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleBackbutton();
        }

        spawnNewEnemyTimer -= Time.deltaTime;
        if (spawnNewEnemyTimer <= 0)
        {
            spawnNewEnemyTimer = 5;


            int spawnNumberOfEnemies = 1 + (level/2);

            for (int i = 0; i < spawnNumberOfEnemies; i++)
            {
                GameObject enemyToSpawn;
                enemyToSpawn = enemy;

                if (level > 2)
                {
                    float rndEnemy = Random.Range(0.0f, 1.0f);
                    if (rndEnemy > 0.5)
                    {
                        enemyToSpawn = enemy;
                    }
                    else
                    {
                        enemyToSpawn = enemy2;
                    }
                }

                float modifier = Random.Range(-1.0f, 1.0f) * 3;

                Instantiate(enemyToSpawn, new Vector3(player.transform.position.x + 20.0f + i * 3,
                       player.transform.position.y + modifier, 0.0f), Quaternion.identity);
            }

            float rndPowerupHp = Random.Range(0.0f, 1.0f);
            if (rndPowerupHp < 0.1)
            {
                Instantiate(hpPowerUp, new Vector3(player.transform.position.x + 30.0f,
                       player.transform.position.y, 0.0f), Quaternion.identity);
            }

            float rndBuilding = Random.Range(0.0f, 1.0f);
            if (rndPowerupHp < 0.5)
            {
                GameObject whatBuilding = building1;

                float rndWhatBulding = Random.Range(0.0f, 1.0f);

                if (rndWhatBulding > 0.5)
                {
                    whatBuilding = building1;
                }
                else
                {
                    whatBuilding = building2;
                }

                Instantiate(whatBuilding, new Vector3(player.transform.position.x + 30.0f,
                        0.0f, 0.005f), Quaternion.identity);
            }
        }

        if (playerHandler != null)
        {
            level = playerHandler.Level;
            levelLabel.text = "Level " + (level+1);
            scoreLabel.text = "Score: " + playerHandler.Xp;

            int xpInLevel = playerHandler.Xp;
            if (level > 0)
            {
                xpInLevel = playerHandler.Xp - playerHandler.levelList[level - 1];
            }

            int xpForNextLevel = (playerHandler.levelList[level]);
            if(level > 0)
                xpForNextLevel = playerHandler.levelList[level] - playerHandler.levelList[level - 1];

            xpLabel.text = "Xp: " + xpInLevel + "/" + xpForNextLevel;

            hpLabel.text = "[ ";
            for (int i = 0; i < playerHandler.HP; i++)
            {
                hpLabel.text += "|";
            }
            hpLabel.text += " ] ";
            if (playerHandler.HP <= 3)
                hpLabel.color = Color.red;
            else hpLabel.color = new Color(88.0f / 255.0f, 82.0f / 255.0f, 75.0f / 255.0f);
        }
    }
}