using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    public static Stack<string> ItemStack = new Stack<string>(); //アイテムのスタック
    public float Interval;　//アイテムの出現時間
    private float StartInterval;
    public TextMeshProUGUI timerText; //スペシャルアイテムのカウンター
    private int Seconds;

    private GUIStyle style;
    private GUIStyleState styleState;
    //表示非表示にするための定義
    [SerializeField] GameObject Liprotect;
    [SerializeField] GameObject Riprotect;
    [SerializeField] GameObject CountText;
    [SerializeField] GameObject luckyObject;

    void Start()
    {
        StartInterval = Interval;
        ResetProtect(); //非表示にしている
        GameObject.Find("TimerText").SetActive(false); //テキストを消す
        StartCoroutine(Protect()); //繰り返し処理行う

        style = new GUIStyle();
        style.fontSize = 20; //GUIのフォントサイズ
        styleState = new GUIStyleState();　//GUIのフォントカラー
    }

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        Interval -= Time.deltaTime; //スペシャルアイテムの残り時間
        Seconds = (int)Interval;
        timerText.text = Seconds.ToString();
        if (Interval <= 0.0f)   //カウントが0になったら以下を実行
        {
            CountText.SetActive(false); //カウンターの非表示
        }
    }

    //ここから繰り返し-----------------------------
    IEnumerator Protect()
    {
        while (boll_manager.showJudgeScreen == false)//ゲームがクリアしていなければ以下をループ
        {
            Debug.Log("アイテム機能は有効です");
            yield return new WaitUntil(() => ItemStack.Count == 2);  //スタックにアイテムを所持している場合のみOK
            boll_manager.labelText = "スペシャルアイテムを獲得したよ!! Qキーを押してみよう";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q));//Aキーを押す
            boll_manager.labelText = "スペシャルタイム!!";
            Interval = StartInterval;      //Timerを30秒にセット
            yield return LightProtect();   //左側の壁出現
            yield return RightProtect();   //右側の壁出現
            yield return CountTime();
            yield return CloseProtect();   //左右の壁消滅
            boll_manager.labelText = "スペシャルタイム終了!!";
            yield return new WaitForSeconds(4f);
            luckyObject.SetActive(true);   //スペシャルアイテムを出現させる
            boll_manager.NowPoint(); //現在のポイントを表示
        }
        Debug.Log("ゲームクリア…アイテムは機能は無効です");
    }

    IEnumerator LightProtect()  //左側の壁出現
    {
        var litemfound = ItemStack.Contains("Protect Light Wall");
        if (litemfound == true)　//スタックに左アイテムがあるか確認
        {
            Liprotect.SetActive(true);
            yield return new WaitForSeconds(0f);
        }

    }

    IEnumerator RightProtect()  //右側の壁出現
    {
        var LitemFound = ItemStack.Contains("Protect Right Wall");
        if (LitemFound == true) //スタックに右アイテムがあるか確認
        {
            Riprotect.SetActive(true);
            yield return new WaitForSeconds(0f);
        }
    }

    IEnumerator CountTime()
    {
        CountText.SetActive(true); //カウンター表示
        Update();
        yield return new WaitForSeconds(0f);
    } //スペシャルアイテムの残り時間

    IEnumerator CloseProtect()  //左右の壁消滅
    {
        yield return new WaitForSeconds(Interval); //Interval待ったのち
        ResetProtect(); //左右の壁を消す
        Debug.Log("スペシャルアイテム消失");
    }
    //ここmまで繰り返し-----------------------------

    private void ResetProtect() //オブジェクト非表示
    {
        GameObject.Find("Lprotect").SetActive(false); //左側の壁を消す
        GameObject.Find("Rprotect").SetActive(false); //左側の壁を消す
        ItemStack.Clear();
        Debug.LogFormat("スタック要素をクリア。現在の要素は {0} です。", ItemStack.Count);
    }

    public static void ResTart() //スタート画面に戻る
    {
        //Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
        boll_manager.point = 0;
    }

    public static void Initialized()//取得するアイテム
    {
        ItemStack.Push("Protect Light Wall"); //左の壁
        ItemStack.Push("Protect Right Wall"); //右の壁
    }

    public static void PrintItemReport() //アイテムの所持数を確認
    {
        Debug.LogFormat("アイテムを {0} つ所持しています", ItemStack.Count);
    }

    private void OnGUI()  //GUIの表記
    {
        styleState.textColor = Color.white;
        style.normal = styleState;

        GUI.Box(new Rect(50, 50, 180, 25), "現在の得点:" + boll_manager.point); //左画面上部
        GUI.Box(new Rect(50, 80, 180, 25), "-200点でゲームオーバー"); //左画面上部
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 300, 50), boll_manager.labelText, style);　//中央下部

        //勝敗が決まった時に表示する画面
        if (boll_manager.showJudgeScreen)
        {
            switch (boll_manager.showWinScreen)
            {
                case true:　//勝利
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "ゲームクリアおめでとう!!"))
                    {
                        GameBehavior.ResTart();
                    }
                    break;
                case false:　//敗北
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "ゲームオーバー..."))
                    {
                        GameBehavior.ResTart();
                    }
                    break;
            }
        }

    }
}