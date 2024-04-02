using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurorialManager : MonoBehaviour
{
    public Text Display;
    public Image Display2;
    public string[] sentences;
    public Sprite[] faces;
    private int index;
    public float typingspeed = 0.02f;
    
    IEnumerator Type()
    {
        Display2.sprite = faces[index];
        foreach (char letter1 in sentences[index].ToCharArray())
        {
            Display.text += letter1;
            yield return new WaitForSeconds(typingspeed);
        }
    }
    private void Start()
    {
        StartCoroutine(Type());
    }
}
