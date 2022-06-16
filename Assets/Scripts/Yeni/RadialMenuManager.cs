using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RadialMenuManager : MonoBehaviour
{    /*
        float DpadH = Input.GetAxis("DPAD-H");
        float DpadV = Input.GetAxis("DPAD-V");
        if (DpadH == 1)
        {
            Debug.Log("Right");
        }
        if (DpadH == -1)
        {
            Debug.Log("Left");
        }
        if(DpadV == 1)
        {
            Debug.Log("Up");
        }
        if(DpadV == -1)
        {
            Debug.Log("Down");
        }
     */
    bool selected;
    public void Update()
    {
        float DpadH = Input.GetAxis("DPAD-H");
        float DpadV = Input.GetAxis("DPAD-V");
        GameObject Event = GameObject.Find("EventSystem");
        EventSystem EventManager = Event.GetComponent<EventSystem>();
        //EventManager.SetSelectedGameObject(GameObject.Find("Play"));
        GameObject Game = GameObject.Find("GameManager");
        Game GameManager = Game.GetComponent<Game>();
        if(GameManager.RadialMenuOpen)
        {
            if(selected == false)
            {
                if (DpadH == 1)
                {
                    selected = true;
                    EventManager.SetSelectedGameObject(GameObject.Find("Right"));
                    GameManager.clone = true;
                    Debug.Log("Right");
                    FireClone();
                    Invoke("Close", 0.3f);
                }
                if (DpadH == -1)
                {
                    selected = true;
                    EventManager.SetSelectedGameObject(GameObject.Find("Left"));
                    GameManager.clone = true;
                    Debug.Log("Left");
                    AirClone();
                    Invoke("Close", 0.3f);
                }
                if (DpadV == 1)
                {
                    selected = true;
                    EventManager.SetSelectedGameObject(GameObject.Find("Up"));
                    GameManager.clone = true;
                    Debug.Log("Up");
                    WaterClone();
                    Invoke("Close", 0.3f);
                }
                if (DpadV == -1)
                {
                    selected = true;
                    EventManager.SetSelectedGameObject(GameObject.Find("Down"));
                    GameManager.clone = true;
                    Debug.Log("Down");
                    EarthClone();
                    Invoke("Close", 0.3f);
                }
            }
        }
    }
    void Close()
    {
        GameObject Game = GameObject.Find("GameManager");
        Game GameManager = Game.GetComponent<Game>();
        GameManager.RadialMenuOpen = false;
        selected = false;
    }
    public void WaterClone()
    {
        GameObject Game = GameObject.Find("GameManager");
        Game GameManager = Game.GetComponent<Game>();
        GameManager.Water = true;
    }
    public void EarthClone()
    {
        GameObject Game = GameObject.Find("GameManager");
        Game GameManager = Game.GetComponent<Game>();
        GameManager.Earth = true;
    }
    public void FireClone()
    {
        GameObject Game = GameObject.Find("GameManager");
        Game GameManager = Game.GetComponent<Game>();
        GameManager.Fire = true;
    }
    public void AirClone()
    {
        GameObject Game = GameObject.Find("GameManager");
        Game GameManager = Game.GetComponent<Game>();
        GameManager.Air = true;
    }
}
