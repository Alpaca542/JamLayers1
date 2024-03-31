using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MagicManager : MonoBehaviour
{
    public Light2D GlobalSun;
    public bool IsInMagicMode = false;
    public void EnterMagicMode()
    {
        if (!IsInMagicMode)
        {
            IsInMagicMode = true;
            GlobalSun.color = new Color32(120, 233, 255, 255);
            GlobalSun.intensity = 0.5f;
            Time.timeScale = 0;
        }
        else
        {
            GlobalSun.color = new Color32(255, 255, 255, 255);
            GlobalSun.intensity = 0.35f;
            Time.timeScale = 1;
        }
    }
}
