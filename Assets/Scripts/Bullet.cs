using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject GameManager;
    int speed = 5;
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        Game MainScript = GameManager.GetComponent<Game>();
        //this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = MainScript.SpawnWay * speed; 
    }
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
