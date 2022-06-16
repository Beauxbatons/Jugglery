using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{
    public static RadialMenuSpawner ins;
    public RadialMenu menuPrefab;
    public GameObject character;
    void Awake()
    {
        ins = this;
    }
    public void SpawnMenu(Intractable obj)
    {
        Camera cam = Camera.main;
        Vector3 Vec = cam.WorldToScreenPoint(character.transform.position);
        RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
        newMenu.transform.SetParent(transform, false);
        //newMenu.transform.position = Input.mousePosition;
        newMenu.transform.position = Vec;
        newMenu.SpawnButtons(obj);
    }
}
