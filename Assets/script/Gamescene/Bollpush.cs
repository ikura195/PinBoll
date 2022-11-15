using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bollpush : MonoBehaviour
{

    public float downPosition = -55.67f;
    public float upPosition = -36.64f;
    public float downSpeed = -0.8f;
    public float upSpeed = 2.5f;

    

    public float pushFoce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //�t���[�����[�g�̌Œ�60FPS
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            if(this.transform.position.y > downPosition) //�X�y�[�X�������Ă��鎞�̓���
            {
                this.transform.position += new Vector3(0, downSpeed, 0);
            }
        }
        else
        {
            if (this.transform.position.y < upPosition)�@//�X�y�[�X�������Ă��Ȃ��Ƃ��̓���
            {
                this.transform.position += new Vector3(0, upSpeed, 0);
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        float positionAvalage = (downPosition - upPosition)/2;

        //�{�[���̃I�u�W�F�N�g���͂����o�����Ƃ��Ă���ꍇ
        if (collision.gameObject.name == "boll")
        {
             collision.gameObject.GetComponent<Rigidbody>().AddForce(0, pushFoce, 0, ForceMode.Impulse);
             Debug.Log("�ˏo");
        }

    }
}
