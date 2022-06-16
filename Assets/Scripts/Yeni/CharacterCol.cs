using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCol : MonoBehaviour
{
    public GameObject die1;
    public bool die;
    void Start()
    {
        die = false;
        Time.timeScale = 1f;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "water")
        {
            die = true;
            Time.timeScale = 0f;
            die1.SetActive(true);
        }
    }
}
