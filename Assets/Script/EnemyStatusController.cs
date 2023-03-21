using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStatusController : MonoBehaviour
{
    [SerializeField] Slider Enemy_bulletSpeed_Slider;
    [SerializeField] Slider Enemy_ADS_Time_Slider;
    [SerializeField] Slider Enemy_Speed_Slider;

    float enemy_shoot_Length = 15f;
    float configs_enemy_search_Length = 20f;
    float configs_enemy_Sight = 90f;
    int configs_enemy_bulletMany = 1;

    public static bool enemyBulletMode;
    public static bool enemyShootMode;
    public static float enemy_bulletSpeed;
    public static int enemy_bulletMany;
    public static float enemy_ADS_Time = 1f;
    public static float enemy_Speed = 2f;
    public static float enemy_Sight = 360f;
    public static float enemy_search_Length = 30f;
    public static float enemy_Shoot_Length = 20f;

    public EnemyAI enemyAI;
    public EnemyAI_Sight[] enemyAI_Sight;

    [SerializeField] InputField InputField_enemySight;
    [SerializeField] InputField InputField_shootLength;
    [SerializeField] InputField InputField_searchLength;
    [SerializeField] Toggle Toggle_enemyshoot;
    // Start is called before the first frame update
    void Start()
    {
        //enemyAI_Sight = enemyAI.transform.root.GetComponentsInChildren<EnemyAI_Sight>();
        for (int i = 0; i < enemyAI_Sight.Length; i++)
        {
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().viewingangle = enemy_Sight;
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().shootLength = enemy_Shoot_Length;
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().searchLength = enemy_search_Length;
        }
        if(Enemy_Speed_Slider == true)
        {
            Enemy_Speed_Slider.value = enemy_Speed;
        }
        
        enemyAI.GetComponent<NavMeshAgent>().speed = enemy_Speed;
        if(InputField_enemySight != null)
        {
            InputField_enemySight.text = enemyAI_Sight[0].viewingangle.ToString();
            InputField_shootLength.text = enemyAI_Sight[0].shootLength.ToString();
            InputField_searchLength.text = enemyAI_Sight[0].searchLength.ToString();
        }
    }

    public void Update()
    {
        /*
        enemy_bulletSpeed = Enemy_bulletSpeed_Slider.value;
        enemy_ADS_Time = Enemy_ADS_Time_Slider.value;
        enemy_Speed = Enemy_Speed_Slider.value;
        */

        enemy_Sight = float.Parse(InputField_enemySight.text);
        enemy_Shoot_Length = float.Parse(InputField_shootLength.text);
        enemy_search_Length = float.Parse(InputField_searchLength.text);
        if (Toggle_enemyshoot.isOn == true)
        {
            enemy_bulletMany = 1;
        }
        else
        {
            enemy_bulletMany = 1;
        }
        for (int i = 0; i < enemyAI_Sight.Length; i++)
        {
            /*
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().viewingangle = enemy_Sight;
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().shootLength = enemy_Shoot_Length;
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().searchLength = enemy_search_Length;
            */
        }
    }

        public void SliderValueChange() {
        enemy_bulletSpeed = Enemy_bulletSpeed_Slider.value;
        enemy_ADS_Time = Enemy_ADS_Time_Slider.value;
        enemy_Speed = Enemy_Speed_Slider.value;

        enemy_Sight = float.Parse(InputField_enemySight.text);
        enemy_Shoot_Length = float.Parse(InputField_shootLength.text);
        enemy_search_Length = float.Parse(InputField_searchLength.text);
        if (Toggle_enemyshoot.isOn == true)
        {
            enemy_bulletMany = 1;
        }
        else
        {
            enemy_bulletMany = 1;
        }
        for(int i = 0;i< enemyAI_Sight.Length; i++)
        {
            /*
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().viewingangle = enemy_Sight;
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().shootLength = enemy_Shoot_Length;
            enemyAI_Sight[i].GetComponent<EnemyAI_Sight>().searchLength = enemy_search_Length;
            */
        }
    }
    public void Enemy_Bullet_Mode()
    {
        if (enemyBulletMode == true)
        {
            enemyBulletMode = false;
        }
        else
        {
            enemyBulletMode = true;
        }
    }
    public void Enemy_Shoot_Mode()
    {
        if (enemyShootMode == true)
        {
            enemyShootMode = false;
        }
        else
        {
            enemyShootMode = true;
        }
    }

    public void Enemy_Bullet_Many(string text)
    {
        configs_enemy_bulletMany = int.Parse(text);
    }
    public void Enemy_Sight(string text)
    {
        enemy_search_Length = float.Parse(text);
    }

    public void Enemy_Search_Length(string text)
    {
        enemy_search_Length = float.Parse(text);
    }

    public void Enemy_Shoot_Length(string text)
    {

        enemy_shoot_Length = float.Parse(text);
        Debug.Log("“ü—Í:" + enemy_shoot_Length);
    }

}
