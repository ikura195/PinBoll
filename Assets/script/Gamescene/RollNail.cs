using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollNail : MonoBehaviour
{
    public float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, speed, 0)); //オブジェクトを回転させる
    }
}
