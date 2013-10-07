using UnityEngine;
using System.Collections;

public static class Windows8Handler {
    public static void PauseGame(bool p)
    {
        if (p)
        {
            Time.timeScale = 0.0f;
        }
        else Time.timeScale = 1.0f;
    }
}
