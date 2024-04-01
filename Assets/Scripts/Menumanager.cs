using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menumanager : MonoBehaviour
{
    public void StartTheGame(string lvlname)
    {
        SceneManager.LoadScene(lvlname);
    }
    public void OpenLevels()
    {
        SceneManager.LoadScene("LvlMenu");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
