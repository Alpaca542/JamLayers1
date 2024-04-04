using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondDialogueManager : MonoBehaviour
{
    public Text Display;
    public Image Display2;
    public string[] sentences;
    public Sprite[] faces;
    public int index;
    public GameObject btnContinue;
    public float typingspeed = 0.02f;

    public GameObject cnv;
    public GameObject Panel;

    IEnumerator Type()
    {
        btnContinue.SetActive(false);
        Display.text = "";
        if (faces[index].name == "Untitled 03-30-2024 07-07-39")
        {
            Display2.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
            Display2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (faces[index].name == "TX Props Statue")
        {
            Display2.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 250);
            Display2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
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
        if (index == 9)
        {
            cnv.SetActive(true);
            Camera.main.GetComponent<CameraFolllllllow>().enabled = true;
            Panel.SetActive(false);
        }
        else
        {
            index++;
            StartCoroutine(Type());
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            StopCoroutine(Type());
            Display.text = sentences[index];
            btnContinue.SetActive(true);
        }
    }
}
