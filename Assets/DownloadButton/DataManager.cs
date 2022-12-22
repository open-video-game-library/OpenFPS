using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    // ����WebGL�ł̏ꍇ�́AJavaScript���œ����֐����擾����
    // addData �� 1���s���Ƃ�js�Ƀf�[�^�𑗐M����֐�
    // downloadData �� �C�ӂ̎��s���I������̂��Acsv�f�[�^���_�E�����[�h����֐�
#if UNITY_WEBGL && !UNITY_EDITOR
     [DllImport("__Internal")]
     private static extern void addData(string jsonData);
 
     [DllImport("__Internal")]
     private static extern void downloadData();
#endif
    // �擾����f�[�^�̃N���X���`
    // Escape-Fish�̏ꍇ�́u�X�R�Ascore�v�u�l�������v�u�_���[�W���v
    //private StreamWriter sw;
    [System.Serializable]
    public class Data
    {
        public int score;
        public int wave_num;
        public float hit_rate;
        public string kill_num;
        public int enemy_num;
    }

    int count;

    private void Start()
    {
        /*
        sw = new StreamWriter(@"SaveData.csv", false, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "score", "wave_num", "hit_rate" , "kill_num", "enemy_num" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        */
        count = 0;
    }

    // ���s���I������Ƃ��ɌĂяo���֐�
    // Hunter-Chameleon�ł́A�Q�[���I���̂Ƃ��Ɂu�X�R�A�v�u�q�b�g���v�u�g���K�[�����������v�������Ƃ��Ă��̊֐����Ăяo���Ă��܂�
    // �Q�[���ɉ����ĕ�����₷���ϐ����ɂ��Ă�������
    public void postData(int _score, int _wave_num, float _hit_rate, string _kill_num, int _enemy_num)
    {
        Data data = new Data(); // �N���X�𐶐�
        data.score = _score; // �X�R�A
        data.wave_num = _wave_num; // �q�b�g��
        data.hit_rate = _hit_rate; // �q�b�g��
        data.kill_num = _kill_num; // �L����
        data.enemy_num = _enemy_num; //�G�̐�

        /*
        string[] s1 = { _score.ToString(), _wave_num.ToString(), _hit_rate.ToString(), _kill_num, _enemy_num.ToString() };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        */

        string json = JsonUtility.ToJson(data); // json�`���ɕϊ�����js�ɓn��
#if UNITY_WEBGL && !UNITY_EDITOR
         addData(json);
#endif
    }

    // �_�E�����[�h�{�^�����������Ƃ��ɌĂяo���֐�
    public void getData()
    {
        /*
        sw.Close();
        */

#if UNITY_WEBGL && !UNITY_EDITOR
         downloadData();
#endif
    }
}