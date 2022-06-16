using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        GameObject Game = GameObject.Find("GameManager");
        Game MainScript = Game.GetComponent<Game>();

        Vector3 targetCamPos = target.position + offset;
        //Follow only in X Position..
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -2f),
        new Vector3(targetCamPos.x, transform.position.y, -2f),
        smoothing * Time.fixedDeltaTime);
        if(MainScript.grounded == false && MainScript.isClimbingUP == true)
        {
            transform.position = target.position + offset;
        }
        if(MainScript.jump == false && MainScript.grounded == true && MainScript.isClimbingUP == false)
        {
            if (target.transform.position.y != gameObject.transform.position.y)
            {
                transform.position = target.position + offset;
            }
        }
    }
}
