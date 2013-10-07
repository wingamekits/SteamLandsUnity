using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    float gameOverTimer = 5.0f;
    public GUIText scoreLabel;

    int score = 0;
    int level = 0;
    int highscore = 0;

    // Use this for initialization
	void Start () {
        score = PlayerPrefs.GetInt("Score", 0);
        level = PlayerPrefs.GetInt("Level", 1);
        highscore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highscore)
        {
            highscore = score;
        }

        scoreLabel.text = "Level " + level + " / Score: " + score + "\nHigh score: " + highscore;
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

        gameOverTimer -= Time.deltaTime;
        if (gameOverTimer <= 0)
        {
            scoreLabel.text = "Tap to restart";
            if(Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("MainMenu");
            }
        }
	}
}
