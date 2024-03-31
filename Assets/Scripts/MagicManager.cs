using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MagicManager : MonoBehaviour
{
    public Light2D GlobalSun;
    public Texture2D cursor;
    public bool IsInMagicMode = false;

    public GameObject lure;
    public ButtonKostil btnks;
    public void MagicMode()
    {
        if (!IsInMagicMode)
        {
            EnterMagicMode();
        }
        else
        {
            ExitMagicMode();
        }
    }
    public void EnterMagicMode()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        IsInMagicMode = true;
        GlobalSun.color = new Color32(120, 233, 255, 255);
        GlobalSun.intensity = 0.5f;
        Time.timeScale = 0;
    }
    public void ExitMagicMode()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        IsInMagicMode = false;
        GlobalSun.color = new Color32(255, 255, 255, 255);
        GlobalSun.intensity = 0.35f;
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsInMagicMode && !btnks.isMouseOver)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldMousePosition.z = 0;
            Instantiate(lure, worldMousePosition, Quaternion.identity);
        }
    }
}
