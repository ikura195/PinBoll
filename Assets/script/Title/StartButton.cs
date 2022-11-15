using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    void OnEnable()
    {
        boll_manager.showJudgeScreen = false;
    }

    public void Button()
    {
        //Debug.Log("スタートボタンが押されました");
        SceneManager.LoadScene("Description");
    }
}
