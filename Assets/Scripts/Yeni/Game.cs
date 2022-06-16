using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using FMODUnity;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    GameObject CurrentClone, pauseMenu;
    GameObject[] PowerUpItems, LadderGround, BodyParts, Plants, Flames;
    float horizontal, vertical, defaultValue, defaultSliderValue;
    float cloneLife = 0f;
    bool powerUP, walking;
    public LayerMask whatIsGround;
    public bool facingRight = true;
    public bool jump;
    bool die = false;
    public bool Key;
    Vector3 SpawnLocaiton;
    private Scene scene;
    CinemachineVirtualCamera Cam1;
    public GameObject WaterDoor;
    public bool Button1, Button2;
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 50f;
    [Header("Main Character")]
    public Rigidbody2D c1RB;
    public Animator MainCharacterAnimator;
    public bool grounded;
    public bool isClimbingUP, isClimbindDown;
    [Header("Clone")]
    public bool Water;
    public bool Fire;
    public bool Earth;
    public bool Air;
    public GameObject WaterClone;
    public GameObject FireClone;
    public GameObject EarthClone;
    public GameObject AirClone;
    public float CloneLifeTime = 3f;
    public float ExtendTimeObject = 1f;
    public bool clone;
    [Header("UI")]
    public Slider slider;
    public float SliderMaxValue = 3.019998f;
    public GameObject RadialMenu;
    public Transform RadialMenuCanvas;
    public bool RadialMenuOpen;
    public bool RadialMenuOpened;
    public Sprite FireFill;
    public Sprite WaterFill;
    public Sprite EarthFill;
    public Sprite AirFill;
    [Header("Spawner")]
    public Vector2 SpawnWay;
    [FMODUnity.EventRef]
    public string WalkEvent;
    FMOD.Studio.EventInstance Walk;
    [FMODUnity.EventRef]
    public string CastEvent;
    FMOD.Studio.EventInstance Cast;
    void Start()
    {
        defaultValue = CloneLifeTime;
        Cam1 = (CinemachineVirtualCamera)FindObjectOfType(typeof(CinemachineVirtualCamera));
        LadderGround = GameObject.FindGameObjectsWithTag("LadderGround");
        PowerUpItems = GameObject.FindGameObjectsWithTag("PowerUp");
        Plants = GameObject.FindGameObjectsWithTag("Flower");
        BodyParts = GameObject.FindGameObjectsWithTag("WizardBody");
        Flames = GameObject.FindGameObjectsWithTag("Fire");
        clone = false;
        slider.gameObject.SetActive(false);
        slider.minValue = 0;
        scene = SceneManager.GetActiveScene();
    }
    void Update()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu PauseScript = pauseMenu.GetComponent<pauseMenu>();
        if (die == true)
        {
            Die(MainCharacterAnimator);
        }
        if(RadialMenuOpen)
        {
            Camera cam = Camera.main;
            GameObject rm = GameObject.FindGameObjectWithTag("RadialMenu");
            rm.transform.position = cam.WorldToScreenPoint(GameObject.Find("Character").transform.position);
        }
        if (clone == false && die == false && PauseScript.isPaused == false)
        {
            //Cam1.m_Follow = GameObject.Find("Character").transform;
            Movement(c1RB, MainCharacterAnimator);
            if(Button1 && Button2)
            {
                Key = true;
                foreach (GameObject go in Flames)
                {
                    Destroy(go);
                }
            }
            if (Button1 == true && Button2 == false || Button1 == false && Button2 == true)
            {
                GameObject button1 = GameObject.Find("button");
                GameObject button2 = GameObject.Find("button (1)");
                Animator anim = button1.gameObject.GetComponent<Animator>();
                anim.SetBool("Button", false);
                Animator anim2 = button2.gameObject.GetComponent<Animator>();
                anim2.SetBool("Button2", false);
            }
            if (Key == false)
            {
                foreach (GameObject items in PowerUpItems)
                {
                    items.SetActive(true);
                }
            }
            if(Key)
            {
                foreach(GameObject platns in Plants)
                {
                    platns.SetActive(false);
                }
                if(scene.name == "water level")
                {
                    WaterDoor.SetActive(true);
                }

            }
            slider.gameObject.SetActive(false);
        }
        if (die == false)
        {
            foreach (GameObject parts in BodyParts)
            {
                if (die)
                {
                    //Debug.Log(die);
                    break;
                }
                else
                {
                    CharacterCollision DieScript = parts.GetComponent<CharacterCollision>();
                    die = DieScript.die;
                }
            }
        }
        if (PauseScript.isPaused == false && die == false)
        {
            if (clone)
            {
                if (Earth)
                {
                    RuntimeManager.PlayOneShot(CastEvent);
                    MainCharacterAnimator.SetBool("Magic", true);
                    StartCoroutine("SpawnClone", EarthClone);
                    Earth = false;
                    slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().sprite = EarthFill;
                }
                if (Fire)
                {
                    RuntimeManager.PlayOneShot(CastEvent);
                    MainCharacterAnimator.SetBool("Magic", true);
                    StartCoroutine("SpawnClone", FireClone);
                    Fire = false;
                    slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().sprite = FireFill;
                }
                if (Water)
                {
                    RuntimeManager.PlayOneShot(CastEvent);
                    MainCharacterAnimator.SetBool("Magic", true);
                    StartCoroutine("SpawnClone", WaterClone);
                    Water = false;
                    slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().sprite = WaterFill;
                }
                if (Air)
                {
                    RuntimeManager.PlayOneShot(CastEvent);
                    MainCharacterAnimator.SetBool("Magic", true);
                    StartCoroutine("SpawnClone", AirClone);
                    Air = false;
                    slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().sprite = AirFill;
                }
            }
            if (clone == false)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    Camera cam = Camera.main;
                    GameObject chr = GameObject.FindGameObjectWithTag("Player");
                    Vector3 vec = cam.WorldToScreenPoint(chr.transform.position);
                    GameObject newMenu = Instantiate(RadialMenu) as GameObject;
                    newMenu.transform.SetParent(RadialMenuCanvas, false);
                    newMenu.transform.position = vec;
                    RadialMenuOpened = true;
                    RadialMenuOpen = true;
                }
                if (Input.GetButtonUp("Fire2"))
                {
                    Destroy(GameObject.FindGameObjectWithTag("RadialMenu"));
                    RadialMenuOpen = false;
                }
                if (RadialMenuOpen == false)
                {
                    Destroy(GameObject.FindGameObjectWithTag("RadialMenu"));
                }
            }
            else if (clone == true && RadialMenuOpen == false)
            {
                Destroy(GameObject.FindGameObjectWithTag("RadialMenu"));
            }
        }
        if (clone == true && die == false)
        {
            CurrentClone = GameObject.FindGameObjectWithTag("clone");
            CloneMovement PowerUPScript = CurrentClone.GetComponent<CloneMovement>();
            powerUP = PowerUPScript.PowerUp;
            cloneLife += Time.deltaTime;
            //Debug.Log(cloneLife + " Second Alive");
            slider.value = cloneLife;
            if (powerUP)
            {
                //Debug.Log(powerUP);
                powerUP = false;
                CloneLifeTime += ExtendTimeObject;
                slider.maxValue += 0.999999f;
            }
            if (cloneLife >= CloneLifeTime)
            {
                //Debug.Log("Destroyed Clone");
                clone = false;
                Destroy(GameObject.FindWithTag("clone"));
                cloneLife = 0f;
                CloneLifeTime = defaultValue;
                SliderMaxValue = defaultSliderValue;
                slider.gameObject.SetActive(false);
            }
        }
    }
    void Movement(Rigidbody2D rb, Animator anim)
    {
        //grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        if(c1RB.velocity.x != 0)
        {
            if(walking == false)
            {
                Walk = RuntimeManager.CreateInstance(WalkEvent);
                Walk.start();
                StartCoroutine("StopAudio");
                walking = true;
            }
        }
        else if (c1RB.velocity.x == 0)
        {
            Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            walking = false;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (isClimbingUP)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            rb.gravityScale = 0;
            foreach (GameObject grounds in LadderGround)
            {
                BoxCollider2D bx = grounds.GetComponent<BoxCollider2D>();
                bx.isTrigger = true;
            }
        }
        else if(isClimbindDown)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            rb.gravityScale = 0;
            foreach (GameObject grounds in LadderGround)
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
        if (horizontal > 0 && !facingRight)
        {
            Flip(rb, horizontal);
        }
        if(horizontal < 0 && facingRight)
        {
            Flip(rb, horizontal);
        }
        if (grounded && (Input.GetButtonDown("Jump")))
        {
            jump = true;
            anim.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
    void Flip(Rigidbody2D rb,float horizontal)
    {
        facingRight = !facingRight;
        Vector3 theScale = rb.transform.localScale;
        theScale.x *= -1;
        rb.transform.localScale = theScale;
    }
    void Die(Animator anim)
    {
        anim.SetBool("Dead", true);
        anim.SetFloat("Speed", 0);
    }
    IEnumerator SpawnClone(GameObject clonePrefab)
    {
        yield return new WaitForSeconds(2f);
        if(facingRight)
        {
            SpawnLocaiton = new Vector3(c1RB.transform.position.x + 1, c1RB.transform.position.y, 0);
        }
        if(facingRight == false)
        {
            SpawnLocaiton = new Vector3(c1RB.transform.position.x - 1, c1RB.transform.position.y, 0);
        }
        slider.gameObject.SetActive(true);
        slider.maxValue = SliderMaxValue;
        defaultSliderValue = SliderMaxValue;
        //defaultValue = CloneLifeTime;
        CloneLifeTime = defaultValue;
        clone = true;
        //SpawnLocaiton = new Vector3(c1RB.transform.position.x + 1, c1RB.transform.position.y, 0);
        Instantiate(clonePrefab).transform.position = SpawnLocaiton;
        MainCharacterAnimator.SetFloat("Speed", 0);
        MainCharacterAnimator.SetBool("Ground", true);
        MainCharacterAnimator.SetBool("Magic", false);
        cloneLife = 0f;
    }
    IEnumerator StopAudio()
    {
        yield return new WaitForSeconds(12f);
        Walk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        walking = false;
    }
}
