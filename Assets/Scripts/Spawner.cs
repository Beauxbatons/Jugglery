using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Vector3 vec;
    void Start()
    {
        InvokeRepeating("SpawnBullet", 0, 1);
        vec = gameObject.transform.position;
    }
    void Update()
    {
        //SpawnBullet();
    }
    void SpawnBullet()
    {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject(0);
        bullet.SetActive(true);
        bullet.transform.position = vec;
        Debug.Log(bullet);
    }
}
