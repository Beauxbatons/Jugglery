using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject obj = GameObject.Find("GameManager");
        Game MainScript = obj.GetComponent<Game>();
        if(collision.gameObject.tag == "Ground")
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
    }
}
