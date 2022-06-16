using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCol : MonoBehaviour
{
    public GameObject log;
    int LogCount;
    public Vector3 Vec1 = new Vector3((float)-1.5, (float)-5.067, 0), Vec2 = new Vector3((float)-0.65, (float)-5.123, 0), Vec3 = new Vector3((float)0.218, (float)-5.133,0);
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "log")
        {
            if(LogCount == 0)
            {
                Instantiate(log).transform.position = Vec1;
                LogCount++;
            }
            else if(LogCount == 1)
            {
                Instantiate(log).transform.position = Vec2;
                LogCount++;
            }
            else if(LogCount == 2)
            {
                Instantiate(log).transform.position = Vec3;
                LogCount++;
            }
        }
    }
}
