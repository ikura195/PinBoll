using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoper_Manager : MonoBehaviour
{
    public float MakeInterval ;
    public static bool desJudge = false; //�I�u�W�F�N�g����
    [SerializeField] GameObject stoper;�@//�I�u�W�F�N�g���o�C�g��(�i����)�ɂ��Ă���

 
    void Update()
    {
        if (desJudge == true)�@//�I�u�W�F�N�g�쐬�BDestry_Stoper�̕Ԃ�l�ɂ��true�ɂȂ�B
        {
            Invoke("Resetst", MakeInterval);
            desJudge = false;
        }
    }

    private void Resetst()
    {
        stoper.SetActive(true); //�V���A�������Ă���̂Ŕ�\���ł��A�N�Z�X�ł���B
        Debug.Log("�X�g�b�p�[������������܂���");
    }
}
