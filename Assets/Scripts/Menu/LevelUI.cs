using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    [SerializeField] int indexLevel;

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(indexLevel);
    }

    public void Undo()
    {
        Debug.Log("Undo");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GoMenu();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Undo();
        }
    }
}