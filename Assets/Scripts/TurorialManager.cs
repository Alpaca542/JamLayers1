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
    public int index;
    public GameObject btnContinue;
    public GameObject Panel;
    public GameObject cnv;
    public GameObject rock;
    public float typingspeed = 0.02f;
    public GameObject knight;
    public GameObject robber;
    public Sprite dieknight;
    public Sprite dierobber;

    public GameObject player;


    IEnumerator Type()
    {
        btnContinue.SetActive(false);
        Display.text = "";
        //knight = 488 269
        //bandit = 300 300
        //wizard = 250 250
        //statue = 150 250
        if (faces[index].name == "HeavyBandit_0")
        {
            Display2.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            Display2.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (faces[index].name == "HeroKnight_0")
        {
            Display2.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(488, 269);
            Display2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (faces[index].name == "Untitled 03-30-2024 07-07-39")
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
        if (index == 4)
        {
            rock.SetActive(true);
            knight.GetComponent<SpriteRenderer>().sprite = dieknight;
            robber.GetComponent<SpriteRenderer>().sprite = dierobber;

        }
        if (index == 14 || index == 18)
        {
            cnv.SetActive(true);
            Camera.main.GetComponent<CameraFolllllllow>().enabled = true;
            player.GetComponent<Player>().enabled = true;
            Panel.SetActive(false);
        }
        else
        {
            index++;
            StartCoroutine(Type());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            index++;
            cnv.SetActive(false);
            Panel.SetActive(true);
            StartCoroutine(Type());
        }
    }
}
