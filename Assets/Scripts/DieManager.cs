using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieManager : MonoBehaviour
{
    public void ToTheMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
