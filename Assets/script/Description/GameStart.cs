using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    void OnEnable()
    {
        boll_manager.showJudgeScreen = false;
    }

    public void Button()
    {
        //Debug.Log("�X�^�[�g�{�^����������܂���");
        SceneManager.LoadScene("GameScene");
    }
}
