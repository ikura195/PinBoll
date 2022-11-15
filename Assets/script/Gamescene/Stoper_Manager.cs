using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoper_Manager : MonoBehaviour
{
    public float MakeInterval ;
    public static bool desJudge = false; //オブジェクト判定
    [SerializeField] GameObject stoper;　//オブジェクトをバイト列(永続化)にしている

 
    void Update()
    {
        if (desJudge == true)　//オブジェクト作成。Destry_Stoperの返り値によりtrueになる。
        {
            Invoke("Resetst", MakeInterval);
            desJudge = false;
        }
    }

    private void Resetst()
    {
        stoper.SetActive(true); //シリアル化しているので非表示でもアクセスできる。
        Debug.Log("ストッパーが初期化されました");
    }
}
