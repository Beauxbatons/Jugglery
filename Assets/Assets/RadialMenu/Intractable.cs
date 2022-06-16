using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intractable : MonoBehaviour
{
    [System.Serializable]
    public class Action
    {
        public Color color;
        public Sprite sprite;
        public string title;
    }

    public Action[] options;

    /*void OnMouseDown()
    {
        RadialMenuSpawner.ins.SpawnMenu(this);
    }*/
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            RadialMenuSpawner.ins.SpawnMenu(this);
        }
    }
}
