using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    float startTimer = 2.0f;
	// Use this for initialization
	void Start () {
	
	}

    void HandleBackbutton()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle the back button on Windows Phone 8
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleBackbutton();
        }

        startTimer -= Time.deltaTime;

        if (startTimer <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("Level");
            }
        }
	}
}
