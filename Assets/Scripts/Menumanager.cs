using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menumanager : MonoBehaviour
{
    public GameObject sparks;
    public string lvltoopen;
    public void StartTheGame(string lvlname)
    {
        lvltoopen = lvlname;
        Instantiate(sparks, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        Invoke(nameof(InvOpenScn), 0.6f);
    }
    public void InvOpenScn()
    {
        SceneManager.LoadScene(lvltoopen);
    }
    public void OpenLevels()
    {
        Instantiate(sparks, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        Invoke(nameof(InvokeOpenScene), 0.6f);
    }
    public void InvokeOpenScene()
    {
        SceneManager.LoadScene("LvlMenu");
    }
    public void OpenMenu()
    {
        Instantiate(sparks, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        Invoke(nameof(GoToMenu), 0.6f);
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
