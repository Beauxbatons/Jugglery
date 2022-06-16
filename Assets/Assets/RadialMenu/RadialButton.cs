using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /*
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
    public Image circle;
    public Image icon;
    public string title;
    public RadialMenu myMenu;

    Color defaultColor;
    Color color1 = Color.black;
    void Update()
    {
        float DpadH = Input.GetAxis("DPAD-H");
        float DpadV = Input.GetAxis("DPAD-V");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        myMenu.selected = this;
        defaultColor = circle.color;
        color1.a = 0.5f;
        circle.color = color1;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myMenu.selected = null;
        circle.color = defaultColor;
    }
}
