using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{

    public void startGame(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
