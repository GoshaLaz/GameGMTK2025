using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void ActiveSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void ActiveMainPanel()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
