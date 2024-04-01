using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUnlocker : MonoBehaviour
{
    public int LvlName;
    public GameObject locker;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Open"))
        {
            if(PlayerPrefs.GetInt("Open") == LvlName)
            {
                locker.SetActive(false);
                GetComponent<Button>().enabled = true;
                //animation
            }
            else if (PlayerPrefs.GetInt("Open") > LvlName)
            {
                locker.SetActive(false);
                GetComponent<Button>().enabled = true;
            }
        }
    }
}
