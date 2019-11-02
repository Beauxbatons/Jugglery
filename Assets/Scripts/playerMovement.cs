using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontal = 0;
    float vertical = 0;
    Vector3 vec;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
     horizontal = Input.GetAxis("Horizontal");
     vertical = Input.GetAxis("Vertical");
     vec = new Vector3(horizontal, 0 , vertical);

    rb.velocity = vec* speed;
    }
}
