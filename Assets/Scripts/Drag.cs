using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 offset;
    public GameObject MovableObject, character1, pauseMenu;

    void OnMouseDown()
    {
        pauseMenu PauseScript = pauseMenu.GetComponent<pauseMenu>();
        CharacterCol DieScript = character1.GetComponent<CharacterCol>();
        if (DieScript.die == false && PauseScript.isPaused == false)
        {
            offset = gameObject.transform.position -
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        }
    }
    void OnMouseDrag()
    {
        CharacterCol DieScript = character1.GetComponent<CharacterCol>();
        pauseMenu PauseScript = pauseMenu.GetComponent<pauseMenu>();
        if (DieScript.die == false && PauseScript.isPaused == false)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "water")
        {
            //gameObject.transform.position = new Vector3(col.transform.position.x, col.transform.position.y);
            gameObject.SetActive(false);
            Debug.Log(col.gameObject);
        }
    }
}
