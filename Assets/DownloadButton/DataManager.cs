using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    // もしWebGL版の場合は、JavaScript側で動く関数を取得する
    // addData → 1試行ごとにjsにデータを送信する関数
    // downloadData → 任意の試行が終わったのち、csvデータをダウンロードする関数
#if UNITY_WEBGL && !UNITY_EDITOR
     [DllImport("__Internal")]
     private static extern void addData(string jsonData);
 
     [DllImport("__Internal")]
     private static extern void downloadData();
#endif
    // 取得するデータのクラスを定義
    // Escape-Fishの場合は「スコアscore」「獲得魚数」「ダメージ数」
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

    // 試行が終わったときに呼び出す関数
    // Hunter-Chameleonでは、ゲーム終了のときに「スコア」「ヒット数」「トリガーを引いた数」を引数としてこの関数を呼び出しています
    // ゲームに応じて分かりやすい変数名にしてください
    public void postData(int _score, int _wave_num, float _hit_rate, string _kill_num, int _enemy_num)
    {
        Data data = new Data(); // クラスを生成
        data.score = _score; // スコア
        data.wave_num = _wave_num; // ヒット数
        data.hit_rate = _hit_rate; // ヒット数
        data.kill_num = _kill_num; // キル数
        data.enemy_num = _enemy_num; //敵の数

        /*
        string[] s1 = { _score.ToString(), _wave_num.ToString(), _hit_rate.ToString(), _kill_num, _enemy_num.ToString() };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        */

        string json = JsonUtility.ToJson(data); // json形式に変換してjsに渡す
#if UNITY_WEBGL && !UNITY_EDITOR
         addData(json);
#endif
    }

    // ダウンロードボタンを押したときに呼び出す関数
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