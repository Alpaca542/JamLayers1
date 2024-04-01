using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menumanager : MonoBehaviour
{
    public GameObject sparks;
    public void StartTheGame(string lvlname)
    {
        SceneManager.LoadScene(lvlname);
    }
    public void OpenLevels()
    {
        Instantiate(sparks, Vector2.zero, Quaternion.identity);
        Invoke(nameof(InvokeOpenScene), 0.6f);
    }
    public void InvokeOpenScene()
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
