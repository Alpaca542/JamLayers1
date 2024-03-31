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
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = new RaycastHit2D();
            if(hit.collider != null)
            {
                if (!hit.collider.gameObject.name.Contains("Button"))
                {
                    Vector2 WhereClicked = hit.point;
                    Instantiate(lure, WhereClicked, Quaternion.identity);
                }
            }
            else
            {
                Vector2 WhereClicked = hit.point;
                Instantiate(lure, WhereClicked, Quaternion.identity);
            }
        }
    }
}
