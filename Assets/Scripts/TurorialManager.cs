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
    public GameObject btnContinue;
    public float typingspeed = 0.02f;
    
    IEnumerator Type()
    {
        btnContinue.SetActive(false);
        Display.text = "";
        Display2.sprite = faces[index];
        foreach (char letter1 in sentences[index].ToCharArray())
        {
            Display.text += letter1;
            yield return new WaitForSeconds(typingspeed);
        }
        btnContinue.SetActive(true);
    }
    private void Start()
    {
        StartCoroutine(Type());
    }
    public void ContinueTyping()
    {
        index++;
        StartCoroutine(Type());
    }
}
