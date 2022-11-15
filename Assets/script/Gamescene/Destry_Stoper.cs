using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destry_Stoper : MonoBehaviour
{
    void Update()//オブジェクトを消す
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.SetActive(false);
            Stoper_Manager.desJudge = true;
        }
    }
}
