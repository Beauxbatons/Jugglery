using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using FMODUnity;

public class CloneMovement : MonoBehaviour
{
    bool walking;
    GameObject[] LadderGround;
    public GameObject GameManager;
    bool facingRight = true;
    float horizontal, vertical;
    float groundRadius = 0.2f;
    public bool grounded;
    public LayerMask whatIsGround;
    Rigidbody2D rb;
    public Transform groundCheck;
    public bool PowerUp;
    Animator anim;
    private Scene scene;
    public LayerMask WhatIsLadder;
    public float distance = 5f;
    public bool isClimbing;
    CinemachineVirtualCamera Cam1;
    [FMODUnity.EventRef]
    public string WalkEvent;
    FMOD.Studio.EventInstance Walk;
    [FMODUnity.EventRef]
    public string EarthPick;
    FMOD.Studio.EventInstance Earth;
    [FMODUnity.EventRef]
    public string FirePick;
    FMOD.Studio.EventInstance Fire;
    [FMODUnity.EventRef]
    public string WaterPick;
    FMOD.Studio.EventInstance Water;
    [FMODUnity.EventRef]
    public string AirPick;
    FMOD.Studio.EventInstance Air;
    [FMODUnity.EventRef]
    public string KeyPick;
    FMOD.Studio.EventInstance Key;
    void Start()
    {
        LadderGround = GameObject.FindGameObjectsWithTag("LadderGround");
        GameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
        Cam1 = (CinemachineVirtualCamera)FindObjectOfType(typeof(CinemachineVirtualCamera));
    }
    void Update()
    {
        //Cam1.m_Follow = gameObject.transform;
        PowerUp = false;
        Game SpeedValues = GameManager.GetComponent<Game>();
        //grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        rb.velocity = new Vector2(horizontal * SpeedValues.speed, rb.velocity.y);
        RaycastHit2D hitInfo = Physics2D.Raycast(gameObject.transform.position, Vector2.up, distance, WhatIsLadder);
        if (rb.velocity.x != 0)
        {
            if (walking == false)
            {
                Walk = RuntimeManager.CreateInstance(WalkEvent);
                Walk.start();
                StartCoroutine("StopAudio");
                walking = true;
            }
        }
        else if (rb.velocity.x == 0)
        {
            Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            walking = false;
        }
        if (hitInfo.collider != null)
        {
            if(vertical > 0)
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }
        else
        {
            isClimbing = false;
        }
        if(isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * SpeedValues.speed);
            rb.gravityScale = 0;
            foreach(GameObject grounds in LadderGround)
            {
                BoxCollider2D bx = grounds.GetComponent<BoxCollider2D>();
                bx.isTrigger = true;
            }
        }
        else
        {
            rb.gravityScale = 10;
            foreach (GameObject grounds in LadderGround)
            {
                BoxCollider2D bx = grounds.GetComponent<BoxCollider2D>();
                bx.isTrigger = false;
            }
        }
        if (scene.name == "air level" && gameObject.name == "AirClone(Clone)")
        {
            rb.mass = 0.5f;
        }
        if (horizontal > 0 && !facingRight)
        {
            Flip(rb, horizontal);
        }
        if (horizontal < 0 && facingRight)
        {
            Flip(rb, horizontal);
        }
        if (grounded && Input.GetButtonDown("Jump"))
        {
            //rb.velocity = (new Vector2(0, jumpForce));
            anim.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, SpeedValues.jumpForce));
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Game MainScript = GameManager.GetComponent<Game>();
        if (col.gameObject.tag == "PowerUp")
        {
            if (scene.name == "earth level" && gameObject.name == "EarthClone(Clone)")
            {
                RuntimeManager.PlayOneShot(EarthPick);
                Power(col);
            }
            if (scene.name == "water level" && gameObject.name == "WaterClone(Clone)")
            {
                RuntimeManager.PlayOneShot(WaterPick);
                Power(col);
            }
            if (scene.name == "fire level" && gameObject.name == "FireClone(Clone)")
            {
                RuntimeManager.PlayOneShot(FirePick);
                Power(col);
            }
            if (scene.name == "air level" && gameObject.name == "AirClone(Clone)")
            {
                RuntimeManager.PlayOneShot(AirPick);
                Power(col);
            }
        }
        if (col.gameObject.tag == "Flower")
        {
            if((gameObject.name != "EarthClone(Clone)"))
            {
                Die();
            }
        }
        if(col.gameObject.tag == "Fire")
        {
            if(gameObject.name != "FireClone(Clone)")
            {
                Die();
            }
        }
        if (col.gameObject.tag == "Die")
        {
            Die();
        }
        if(col.gameObject.tag == "Finish")
        {
            if (scene.name == "air level" && gameObject.name == "AirClone(Clone)")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if(col.gameObject.tag == "Key")
        {
            MainScript.Key = true;
            col.gameObject.SetActive(false);
            RuntimeManager.PlayOneShot(KeyPick);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {    
        Game MainScript = GameManager.GetComponent<Game>();
        if(collision.gameObject.tag == "Water")
        {
            if(gameObject.name != "WaterClone(Clone)")
            {
                Die();
            }
        }
        if(collision.gameObject.tag == "Button1")
        {
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetBool("Button", true);
            MainScript.Button1 = true;
        }
        if(collision.gameObject.tag == "Button2")
        {
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetBool("Button2", true);
            MainScript.Button2 = true;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        if(collision.gameObject.tag == "LadderGround")
        {
            grounded = true;
        }
        if(collision.gameObject.tag == "Water")
        {
            if(gameObject.name == "WaterClone(Clone)")
            {
                grounded = true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        if (collision.gameObject.tag == "LadderGround")
        {
            grounded = false;
        }
        if (collision.gameObject.tag == "Water")
        {
            if (gameObject.name == "WaterClone(Clone)")
            {
                grounded = false;
            }
        }
    }
    void Flip(Rigidbody2D rb, float horizontal)
    {
        facingRight = !facingRight;
        Vector3 theScale = rb.transform.localScale;
        theScale.x *= -1;
        rb.transform.localScale = theScale;
    }
    void Power(Collider2D col)
    {
        PowerUp = true;
        col.gameObject.SetActive(false);
    }
    void Die()
    {
        Game MainScript = GameManager.GetComponent<Game>();
        MainScript.clone = false;
        Destroy(gameObject);
    }
    IEnumerator StopAudio()
    {
        yield return new WaitForSeconds(12f);
        Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        walking = false;
    }
}
