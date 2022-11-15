using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boll_manager : MonoBehaviour
{
    Rigidbody rb;   //���W�b�g�{�f�B�ϐ�

    //OnTriggerEnter�Ŏg�p����ϐ�����
    Vector3 pos; //�{�[���̏ꏊ��ۑ�����ϐ�
    Vector3 ang; //�{�[���̊p�x��ۑ�����ϐ�
    public static float point;�@//���_�̍��v��ۑ�����ϐ�

    //GUI�Ŏg�p
    public static string labelText; //GUI�ɕ\���w���镶����
    public static int maxPoint = 300;
    public int GameOverPoint = 500;
    public static bool showWinScreen = false;
    public static bool showJudgeScreen = false;


    void Start()
    {
        labelText = (maxPoint) + "�_���l�����ăN���A��ڎw�����I";
        rb = GetComponent<Rigidbody>(); //���W�b�g�{�f�B�擾
        pos = transform.position; //���̏ꏊ�擾
        ang = transform.eulerAngles; //���̊p�x�擾
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R)) //"R"�L�[���������珉���ʒu�ɖ߂�
        {
            if (showJudgeScreen == false)
            {
                Gostart();
            }
            else
                Debug.Log("R�L�[�͖���������Ă��܂�");

        }
    }

    private void OnTriggerEnter(Collider other) //�g���K�[�̒��ɓ������ۂ̏���
    {
        if (showJudgeScreen == false)
        {
            point += int.Parse(other.name);//�G�ꂽ�Z���T�[�̖��O��ǂݎ����Z����
            Debug.Log(point);
            if (point >= maxPoint)�@//�����Ɣ���
            {
                labelText = "���߂łƂ��I�Q�[���N���A�I";
                showWinScreen = true; //����
                showJudgeScreen = true;  //���肪���܂���������
            }
            else if ((maxPoint - point) >= GameOverPoint)�@//�����Ɣ���
            {
                labelText = "�Q�[���I�[�o�[�c";
                showWinScreen = false;  //����
                showJudgeScreen = true;�@//���肪���܂���������
            }
            else
            {
                NowPoint();
            }
            Gostart();
        }

    }

    public static void NowPoint() //�N���A�܂ł̃|�C���g
    {
        labelText = "����" + (maxPoint - point) + "�ŃN���A!!";
    }

    private void Gostart() //�{�[���������ʒu�ɖ߂�
    {
        if (showJudgeScreen == false)
        {
            rb.velocity = Vector3.zero; //������0�ɂ���
            rb.angularVelocity = Vector3.zero; //�p�x��0�ɂ�
            transform.position = pos; //�����ʒu�ɃZ�b�g���Ă���B
            transform.eulerAngles = ang;
            Debug.Log("���̃X�e�[�^�X������������܂���");
        }
        
    }
}