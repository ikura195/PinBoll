using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin_move : MonoBehaviour
{
    public float speed = 0.5f;  //動く速さ
    public float right_limit = 4;  //左端の座標
    public float left_limit = -4;  //右端の座標
    bool move_R = true; //方向
    public GameBehavior gameManager;
    public static bool ItemJudge = false;
   


    void Start()
    {
        //GameBehaviorのコンポーネントを取得
        gameManager = GameObject.Find("GameBehavior").GetComponent<GameBehavior>();

        if (left_limit > right_limit) //再生ボタンを押した直後に動く速度を決めている。
        {
            float temp = left_limit;
            left_limit = right_limit;
            right_limit = temp;
        }
        if (speed < 0)
        {
            move_R = false;
        }
    }

    
    void Update()
    {
        //nailが左右に動かすための内容
        Vector3 pos = transform.position;   //ピンの現在の位置を取得
        if (pos.x <= left_limit && !move_R)  // 左のスピード
        {
            speed *= -1;
            move_R = true;
        }
        if (pos.x >= right_limit && move_R)　// 右のスピード
        {
            speed *= -1;
            move_R = false;
        }
        transform.Translate(new Vector3(speed, 0, 0));
    }


    void OnCollisionEnter(Collision collision) //アイテム取得
    {
        if (collision.gameObject.name == "boll")
        {
            GameBehavior.Initialized();
            this.gameObject.SetActive(false); //スペシャルアイテムを消す
            //Destroy(this.transform.parent.gameObject);
            GameBehavior.PrintItemReport();
        }
    }

}
