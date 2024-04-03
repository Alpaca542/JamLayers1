using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieManager : MonoBehaviour
{
    public GameObject player;
    public GameObject Tutorialmng;
    public void ToTheMenu()
    {
        if(SceneManager.GetActiveScene().name != "Lvl1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            foreach(GameObject dck in GameObject.FindGameObjectsWithTag("Lure"))
            {
                Destroy(dck);
            }
            player.transform.position = Tutorialmng.transform.position;
        }
    }
}
