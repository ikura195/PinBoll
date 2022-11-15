using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin_move : MonoBehaviour
{
    public float speed = 0.5f;  //��������
    public float right_limit = 4;  //���[�̍��W
    public float left_limit = -4;  //�E�[�̍��W
    bool move_R = true; //����
    public GameBehavior gameManager;
    public static bool ItemJudge = false;
   


    void Start()
    {
        //GameBehavior�̃R���|�[�l���g���擾
        gameManager = GameObject.Find("GameBehavior").GetComponent<GameBehavior>();

        if (left_limit > right_limit) //�Đ��{�^��������������ɓ������x�����߂Ă���B
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
        //nail�����E�ɓ��������߂̓��e
        Vector3 pos = transform.position;   //�s���̌��݂̈ʒu���擾
        if (pos.x <= left_limit && !move_R)  // ���̃X�s�[�h
        {
            speed *= -1;
            move_R = true;
        }
        if (pos.x >= right_limit && move_R)�@// �E�̃X�s�[�h
        {
            speed *= -1;
            move_R = false;
        }
        transform.Translate(new Vector3(speed, 0, 0));
    }


    void OnCollisionEnter(Collision collision) //�A�C�e���擾
    {
        if (collision.gameObject.name == "boll")
        {
            GameBehavior.Initialized();
            this.gameObject.SetActive(false); //�X�y�V�����A�C�e��������
            //Destroy(this.transform.parent.gameObject);
            GameBehavior.PrintItemReport();
        }
    }

}
