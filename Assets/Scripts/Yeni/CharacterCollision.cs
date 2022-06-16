using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class CharacterCollision : MonoBehaviour
{
    //public GameObject DieScreen;
    public bool die, grounded;
    public LayerMask WhatIsLadder;
    public float distance = 5f;
    public bool isClimbing;
    float vertical;
    [FMODUnity.EventRef]
    public string DoorOpen;
    FMOD.Studio.EventInstance Door;
    int i;
    void Start()
    {
        die = false;
        Time.timeScale = 1f;
    }
    void Update()
    {
        GameObject obj = GameObject.Find("GameManager");
        Game MainScript = obj.GetComponent<Game>();
        vertical = Input.GetAxis("Vertical");
        RaycastHit2D hitInfo = Physics2D.Raycast(gameObject.transform.position, Vector2.up, distance, WhatIsLadder);
        if (hitInfo.collider != null)
        {
            if (vertical > 0)
            {
                MainScript.isClimbingUP = true;
            }
            else
            {
                MainScript.isClimbingUP = false;
            }
        }
        else
        {
            MainScript.isClimbingUP = false;
        }
        RaycastHit2D hitinfo2 = Physics2D.Raycast(gameObject.transform.position, Vector2.down, distance, WhatIsLadder);
        if(hitinfo2.collider != null)
        {
            if(vertical < 0)
            {
                MainScript.isClimbindDown = true;
            }
            else
            {
                MainScript.isClimbindDown = false;
            }
        }
        else
        {
            MainScript.isClimbindDown = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        int a = SceneManager.GetActiveScene().buildIndex;
        GameObject obj = GameObject.Find("GameManager");
        Game MainScript = obj.GetComponent<Game>();
        if (MainScript.Key)
        {
            if (collision.gameObject.tag == "Door")
            {
                if(i == 0)
                {
                    RuntimeManager.PlayOneShot(DoorOpen);
                    i++;
                }
                StartCoroutine("LoadNextLevel");
            }
        }
        if(collision.gameObject.tag == "Ground")
        {
            MainScript.jump = false;
        }
        if (collision.gameObject.tag == "Flower")
        {
            die = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(collision.gameObject.tag == "Fire")
        {
            die = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }        
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            //DieScreen.SetActive(true);
            //Time.timeScale = 0f;
            die = true;
        }
        if (collision.gameObject.tag == "Die")
        {
            die = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(collision.gameObject.tag =="Water")
        {
            die = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject obj = GameObject.Find("GameManager");
        Game MainScript = obj.GetComponent<Game>();
        if (collision.gameObject.tag == "Ground")
        {
            MainScript.grounded = true;
        }
        if (collision.gameObject.tag == "LadderGround")
        {
            MainScript.grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        GameObject obj = GameObject.Find("GameManager");
        Game MainScript = obj.GetComponent<Game>();
        if (collision.gameObject.tag == "Ground")
        {
            MainScript.grounded = false;
        }
        if (collision.gameObject.tag == "LadderGround")
        {
            MainScript.grounded = false;
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
