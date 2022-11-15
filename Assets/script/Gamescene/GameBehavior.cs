using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    public static Stack<string> ItemStack = new Stack<string>(); //�A�C�e���̃X�^�b�N
    public float Interval;�@//�A�C�e���̏o������
    private float StartInterval;
    public TextMeshProUGUI timerText; //�X�y�V�����A�C�e���̃J�E���^�[
    private int Seconds;

    private GUIStyle style;
    private GUIStyleState styleState;
    //�\����\���ɂ��邽�߂̒�`
    [SerializeField] GameObject Liprotect;
    [SerializeField] GameObject Riprotect;
    [SerializeField] GameObject CountText;
    [SerializeField] GameObject luckyObject;

    void Start()
    {
        StartInterval = Interval;
        ResetProtect(); //��\���ɂ��Ă���
        GameObject.Find("TimerText").SetActive(false); //�e�L�X�g������
        StartCoroutine(Protect()); //�J��Ԃ������s��

        style = new GUIStyle();
        style.fontSize = 20; //GUI�̃t�H���g�T�C�Y
        styleState = new GUIStyleState();�@//GUI�̃t�H���g�J���[
    }

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        Interval -= Time.deltaTime; //�X�y�V�����A�C�e���̎c�莞��
        Seconds = (int)Interval;
        timerText.text = Seconds.ToString();
        if (Interval <= 0.0f)   //�J�E���g��0�ɂȂ�����ȉ������s
        {
            CountText.SetActive(false); //�J�E���^�[�̔�\��
        }
    }

    //��������J��Ԃ�-----------------------------
    IEnumerator Protect()
    {
        while (boll_manager.showJudgeScreen == false)//�Q�[�����N���A���Ă��Ȃ���Έȉ������[�v
        {
            Debug.Log("�A�C�e���@�\�͗L���ł�");
            yield return new WaitUntil(() => ItemStack.Count == 2);  //�X�^�b�N�ɃA�C�e�����������Ă���ꍇ�̂�OK
            boll_manager.labelText = "�X�y�V�����A�C�e�����l��������!! Q�L�[�������Ă݂悤";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q));//A�L�[������
            boll_manager.labelText = "�X�y�V�����^�C��!!";
            Interval = StartInterval;      //Timer��30�b�ɃZ�b�g
            yield return LightProtect();   //�����̕Ǐo��
            yield return RightProtect();   //�E���̕Ǐo��
            yield return CountTime();
            yield return CloseProtect();   //���E�̕Ǐ���
            boll_manager.labelText = "�X�y�V�����^�C���I��!!";
            yield return new WaitForSeconds(4f);
            luckyObject.SetActive(true);   //�X�y�V�����A�C�e�����o��������
            boll_manager.NowPoint(); //���݂̃|�C���g��\��
        }
        Debug.Log("�Q�[���N���A�c�A�C�e���͋@�\�͖����ł�");
    }

    IEnumerator LightProtect()  //�����̕Ǐo��
    {
        var litemfound = ItemStack.Contains("Protect Light Wall");
        if (litemfound == true)�@//�X�^�b�N�ɍ��A�C�e�������邩�m�F
        {
            Liprotect.SetActive(true);
            yield return new WaitForSeconds(0f);
        }

    }

    IEnumerator RightProtect()  //�E���̕Ǐo��
    {
        var LitemFound = ItemStack.Contains("Protect Right Wall");
        if (LitemFound == true) //�X�^�b�N�ɉE�A�C�e�������邩�m�F
        {
            Riprotect.SetActive(true);
            yield return new WaitForSeconds(0f);
        }
    }

    IEnumerator CountTime()
    {
        CountText.SetActive(true); //�J�E���^�[�\��
        Update();
        yield return new WaitForSeconds(0f);
    } //�X�y�V�����A�C�e���̎c�莞��

    IEnumerator CloseProtect()  //���E�̕Ǐ���
    {
        yield return new WaitForSeconds(Interval); //Interval�҂����̂�
        ResetProtect(); //���E�̕ǂ�����
        Debug.Log("�X�y�V�����A�C�e������");
    }
    //����m�܂ŌJ��Ԃ�-----------------------------

    private void ResetProtect() //�I�u�W�F�N�g��\��
    {
        GameObject.Find("Lprotect").SetActive(false); //�����̕ǂ�����
        GameObject.Find("Rprotect").SetActive(false); //�����̕ǂ�����
        ItemStack.Clear();
        Debug.LogFormat("�X�^�b�N�v�f���N���A�B���݂̗v�f�� {0} �ł��B", ItemStack.Count);
    }

    public static void ResTart() //�X�^�[�g��ʂɖ߂�
    {
        //Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
        boll_manager.point = 0;
    }

    public static void Initialized()//�擾����A�C�e��
    {
        ItemStack.Push("Protect Light Wall"); //���̕�
        ItemStack.Push("Protect Right Wall"); //�E�̕�
    }

    public static void PrintItemReport() //�A�C�e���̏��������m�F
    {
        Debug.LogFormat("�A�C�e���� {0} �������Ă��܂�", ItemStack.Count);
    }

    private void OnGUI()  //GUI�̕\�L
    {
        styleState.textColor = Color.white;
        style.normal = styleState;

        GUI.Box(new Rect(50, 50, 180, 25), "���݂̓��_:" + boll_manager.point); //����ʏ㕔
        GUI.Box(new Rect(50, 80, 180, 25), "-200�_�ŃQ�[���I�[�o�["); //����ʏ㕔
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 300, 50), boll_manager.labelText, style);�@//��������

        //���s�����܂������ɕ\��������
        if (boll_manager.showJudgeScreen)
        {
            switch (boll_manager.showWinScreen)
            {
                case true:�@//����
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "�Q�[���N���A���߂łƂ�!!"))
                    {
                        GameBehavior.ResTart();
                    }
                    break;
                case false:�@//�s�k
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "�Q�[���I�[�o�[..."))
                    {
                        GameBehavior.ResTart();
                    }
                    break;
            }
        }

    }
}