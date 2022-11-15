using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boll_manager : MonoBehaviour
{
    Rigidbody rb;   //リジットボディ変数

    //OnTriggerEnterで使用する変数たち
    Vector3 pos; //ボールの場所を保存する変数
    Vector3 ang; //ボールの角度を保存する変数
    public static float point;　//得点の合計を保存する変数

    //GUIで使用
    public static string labelText; //GUIに表示指せる文字列
    public static int maxPoint = 300;
    public int GameOverPoint = 500;
    public static bool showWinScreen = false;
    public static bool showJudgeScreen = false;


    void Start()
    {
        labelText = (maxPoint) + "点を獲得してクリアを目指そう！";
        rb = GetComponent<Rigidbody>(); //リジットボディ取得
        pos = transform.position; //球の場所取得
        ang = transform.eulerAngles; //球の角度取得
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R)) //"R"キーを押したら初期位置に戻る
        {
            if (showJudgeScreen == false)
            {
                Gostart();
            }
            else
                Debug.Log("Rキーは無効化されています");

        }
    }

    private void OnTriggerEnter(Collider other) //トリガーの中に入った際の処理
    {
        if (showJudgeScreen == false)
        {
            point += int.Parse(other.name);//触れたセンサーの名前を読み取り加算する
            Debug.Log(point);
            if (point >= maxPoint)　//勝ちと判定
            {
                labelText = "おめでとう！ゲームクリア！";
                showWinScreen = true; //判定
                showJudgeScreen = true;  //判定が決まったか判定
            }
            else if ((maxPoint - point) >= GameOverPoint)　//負けと判定
            {
                labelText = "ゲームオーバー…";
                showWinScreen = false;  //判定
                showJudgeScreen = true;　//判定が決まったか判定
            }
            else
            {
                NowPoint();
            }
            Gostart();
        }

    }

    public static void NowPoint() //クリアまでのポイント
    {
        labelText = "あと" + (maxPoint - point) + "でクリア!!";
    }

    private void Gostart() //ボールを初期位置に戻す
    {
        if (showJudgeScreen == false)
        {
            rb.velocity = Vector3.zero; //速さを0にする
            rb.angularVelocity = Vector3.zero; //角度を0にる
            transform.position = pos; //初期位置にセットしている。
            transform.eulerAngles = ang;
            Debug.Log("球のステータスが初期化されました");
        }
        
    }
}