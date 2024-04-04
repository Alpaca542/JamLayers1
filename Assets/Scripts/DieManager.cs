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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
