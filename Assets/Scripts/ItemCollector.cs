using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private int cherries;

    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private float startTimer = 20f;
    private static float currentTimer;

    private Rigidbody2D rb;
    //private Animator anim;
    private GameObject pl;
    private GameObject go;
    private GameObject bg;

    [SerializeField] private Text cherriesText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text gameOverMessage;

    [SerializeField] private AudioSource collectCherry;
    [SerializeField] private AudioSource collectBanana;
    [SerializeField] private AudioSource collectKiwi;
    [SerializeField] private AudioSource collectOrange;
    [SerializeField] private AudioSource collectPineapple;

    [SerializeField] private AudioSource outOfTimeSound;


    private void Start()
    {
        pl = GameObject.Find("Player");
        go = GameObject.Find("Panel");
        bg = GameObject.Find("BG Music");
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        currentTimer = startTimer;
        //gameOverMessage.text = "";
        go.SetActive(false);
    }

    private void Update()
    {
        cherriesText.text = cherries.ToString("Cherries: " + cherries);
  
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else if (currentTimer < 0)
        {
            Die();
        }


        timeText.text = "Time: " + Mathf.Round(currentTimer);
    }


    private void Die()
    {
            outOfTimeSound.Play();
            rb.bodyType = RigidbodyType2D.Static;
            pl.SetActive(false);
            Invoke ("RestartLevel",  4f);
            //gameOverMessage.text = "Out of Time :(";
            go.SetActive(true);
            bg.SetActive(false);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectCherry.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
        else if (collision.gameObject.CompareTag("Orange"))
        {
            collectOrange.Play();
            Destroy(collision.gameObject);
            cherries--;
            cherriesText.text = "Cherries: " + cherries;
        }

        if (collision.gameObject.CompareTag("Kiwi"))
        {
            collectKiwi.Play();
            Destroy(collision.gameObject);
            currentTimer +=2;
        }
        else if (collision.gameObject.CompareTag("Pineapple"))
        {
            collectPineapple.Play();
            Destroy(collision.gameObject);
            currentTimer -=2;
        }

        if (collision.gameObject.CompareTag("Banana"))
        {
            collectBanana.Play();
            Destroy(collision.gameObject);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

    }


}
