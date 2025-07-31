using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public int indexNextLevel;

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(indexNextLevel - 1);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(indexNextLevel);
    }
}