using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class MagicManager : MonoBehaviour
{
    public Light2D GlobalSun;
    public Texture2D cursor;
    public bool IsInMagicMode = false;
    public int Ducks = 10;

    public GameObject lure;
    public ButtonKostil btnks;
    public Text duckstxt;
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
    private void Start()
    {
        Time.timeScale = 1;
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
        if (Input.GetMouseButtonDown(0) && IsInMagicMode && !btnks.isMouseOver && Ducks > 0)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldMousePosition.z = 0;
            if(Physics2D.OverlapCircle(worldMousePosition, 0.05f))
            {
                Ducks--;
                duckstxt.text = Ducks.ToString();
                Instantiate(lure, worldMousePosition, Quaternion.identity);
            }
        }
    }
}
