using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject character1, character2, character3, tree, logs, MovableLogs, pauseMenu;
    public LayerMask treeMask, logMask, CloneMask1, CloneMask2, whatIsGround;
    int logCount;
    public TextMeshProUGUI LogText;
    float horizontal, vertical;
    Vector3 vec;
    public float speed = 5;
    public float jumpForce = 50;
    bool grounded;
    public Rigidbody2D character1RB, character2RB, character3RB;
    public Transform groundCheck1, groundCheck2, groundCheck3;
    float groundRadius = 0.2f;
    int character = 1;
    bool clone1, clone2, p1, p2;
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(p1)
            {
                Player2();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(p2)
            {
                Player3();
            }
        }
        if(character == 1)
        {
            Movement(character1RB, groundCheck1);
        }
        if (character == 2)
        {
                Movement(character2RB, groundCheck2);
        }
        if (character == 3)
        {
            if(p2)
            Movement(character3RB, groundCheck3);
        }
    }
    void Update()
    {
        CharacterCol DieScript = character1.GetComponent<CharacterCol>();
        pauseMenu PauseScript = pauseMenu.GetComponent<pauseMenu>();

        if (DieScript.die == false && PauseScript.isPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, treeMask);

                if (hit)
                {
                    Debug.Log("Tree");
                    Vector3 LastTreePos = new Vector3(tree.transform.position.x, tree.transform.position.y, tree.transform.position.z);
                    Destroy(tree);
                    SpawnTree(logs, LastTreePos);
                    SpawnTree(logs, LastTreePos);
                    SpawnTree(logs, LastTreePos);
                }
                RaycastHit2D hit2 = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, logMask);
                if (hit2)
                {
                    Debug.Log("Log");
                    Destroy(hit2.transform.gameObject);
                    logCount++;
                    LogText.text = "Objects to spawn: " + logCount.ToString();
                }
                RaycastHit2D hit3 = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, CloneMask1);
                if (hit3)
                {
                    clone1 = false;
                    if (character == 2)
                    { character = 1; }
                    hit3.transform.gameObject.SetActive(false);
                    p1 = false;
                }
                RaycastHit2D hit4 = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, CloneMask2);
                if (hit4)
                {
                    if (character == 3)
                    { character = 1; }
                    clone2 = false;
                    hit4.transform.gameObject.SetActive(false);
                    p2 = false;
                }
            }
            //if (Input.GetKeyDown(KeyCode.Mouse2))
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (logCount > 0)
                {
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
                    Vector3 SpawnLocaiton = new Vector3(worldPoint.x, worldPoint.y, 0);

                    SpawnTree(MovableLogs, SpawnLocaiton);
                    Debug.Log("Spawned Log");
                    logCount--;
                    LogText.text = "Objects to spawn: " + logCount.ToString();
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (clone1 == false)
                {
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);

                    Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, character1.transform.position.z);

                    SpawnCharacter(character2, adjustZ);

                    Debug.Log("Spawned Clone1");

                    clone1 = true;

                    p1 = true;
                }
                else if (clone2 == false)
                {
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);

                    Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, character1.transform.position.z);

                    SpawnCharacter(character3, adjustZ);

                    Debug.Log("Spawned Clone2");

                    clone2 = true;

                    p2 = true;
                }
            }
        }
    }
    public void Player1()
    {
        character = 1;
    }
    public void Player2()
    {
        character = 2;
    }
    public void Player3()
    {
        character = 3;
    }
    void Movement(Rigidbody2D rb, Transform groundCheck)
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        vec = new Vector3(horizontal, 0, vertical);

        rb.velocity = vec * speed;

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (grounded && (Input.GetButtonDown("Jump")))
        {
            rb.velocity = (new Vector2(0, jumpForce));
        }
    }
    private void SpawnTree(GameObject gameObject, Vector3 position)
    {
        Instantiate(gameObject).transform.position = position;
    }
    public void SpawnCharacter(GameObject character, Vector3 position)
    {
        character.SetActive(true);
        character.transform.position = position;
        Debug.Log("Spawned" + character);
    }
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
