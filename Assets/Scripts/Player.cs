using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DeleteDoor());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(DeleteDoor());
            CurrentDoor.GetComponent<Door>().txt.SetActive(false);
        }
    }
    IEnumerator DeleteDoor()
    {
        CurrentDoor.GetComponent<Door>().txt.SetActive(true);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "5";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "4";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "3";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "2";
        yield return new WaitForSeconds(1f);
        CurrentDoor.GetComponent<Door>().txt.GetComponent<TMP_Text>().text = "1";
        CurrentDoor.GetComponent<BoxCollider2D>().enabled = false;
        CurrentDoor.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(CurrentDoor);
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
}
