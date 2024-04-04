using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [Header("Parameters")]
    public float speed = 5f;
    bool AmIDead = false;

    [Header("Debug")]
    public Rigidbody2D rb;
    public Animator anim1;
    public AnimationClip clipDie;
    public DieManager diemng;
    public GameObject CurrentDoor;
    private IEnumerator coroutine;
    public Light2D sun;
    public GameObject panel;
    public GameObject btn;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float MovementX = Input.GetAxis("Horizontal");
        float MovementY = Input.GetAxis("Vertical");
        if(MovementX < 0 && transform.rotation != Quaternion.Euler(0, 180, 0) && transform.rotation != Quaternion.Euler(0, -180, 0))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (MovementX > 0 && transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rb.velocity = new Vector2(MovementX, MovementY).normalized * speed;

        if (Input.GetKeyDown(KeyCode.Space) && CurrentDoor != null)
        {
            coroutine = DeleteDoor();
            StartCoroutine(coroutine);
        }
        if (Input.GetKeyUp(KeyCode.Space) && CurrentDoor != null)
        {
            StopCoroutine(coroutine);
            CurrentDoor.GetComponent<Door>().Canvas.SetActive(false);
        }
    }
    IEnumerator DeleteDoor()
    {
        CurrentDoor.GetComponent<Door>().Canvas.SetActive(true);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "5";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "4";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "3";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "2";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "1";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().Canvas.SetActive(false);
        CurrentDoor.GetComponent<SpriteRenderer>().enabled = false;
        if (CurrentDoor.GetComponent<Door>().AmIFinal)
        {
            Time.timeScale = 0;
            sun.intensity = 1f;
            panel.SetActive(true);
            btn.SetActive(false);
            PlayerPrefs.SetInt("Open", System.Convert.ToInt32(SceneManager.GetActiveScene().name.Replace("Lvl", ""))+1);
        }
        foreach(BoxCollider2D col in CurrentDoor.GetComponents<BoxCollider2D>())
        {
            col.enabled = false;
        }
    }
    public void die()
    {
        if (!AmIDead)
        {
            AmIDead = true;
            diemng.ToTheMenu();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            CurrentDoor = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            CurrentDoor.GetComponent<Door>().Canvas.SetActive(false);
            StopCoroutine(coroutine);
            CurrentDoor = null;
        }
    }
}
