using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Wave : MonoBehaviour
{
    public static int killmany;
    public static int SponeEnemyMany = 1;
    
    public static int shotmany;
    public static int hitmany;
    public static int Wave_number = 5;
    public static float LeftTime = 20f;
    public static bool shootingRange;
    public static int shotCount; //撃った数
    public static int hitCount; //当たった数
    public static int Score;
    public static int enemyShape;
    public int EnemyMany = 1;
    public int now_Wave_number;

    public GameObject Prefab_Target;
    public Text timeManager;
    float timer;
    
    public float[] hitRate;
    public string[] EnemyLeave;

    bool end;
    //[SerializeField] RePlayObjectCollecter rePlayObjectCollecter;

    float m;
    float n;

    public Text scoreText;
    Transform[] SponePoint;
    bool SponeAlreadyDeside;

    public GameObject[] enemyShape_input;

    public GameObject[] EnemySponePoint;

    public GameObject ShootingRange_ParentObject;

    public bool isCountDown = false;
    // Start is called before the first frame update

    public GameObject[] EnemyPackage;
    public GameObject ShootingRange_EnemyPackage;
    public int [] Wave_number_EnemyCount;
    public GameObject[] EnemyBox;
    int deadEnemyCount = 0;

    public static bool enemySponePoint_isRandom;

    int premany;
    bool once;

    [SerializeField] CanvasController canvasController;
    public DataManager dataManager;
    private void Awake()
    {
        Prefab_Target = enemyShape_input[enemyShape];

        //When isnt countdown, count sponepoint to math "Wave_number"
        if (shootingRange == true)
        {
            Debug.Log("Wave_number" + Wave_number);
        }
        else if (isCountDown == false)
        {
            Wave_number = EnemyPackage.Length; //When Wave_number == ParentObject, new Enemy will load
        }
        else
        {

        }

        Wave_number_EnemyCount = new int[Wave_number+4];

        //UnPack AllWave Enemy Package;
        for (int i = 0; i< Wave_number; i++)
        {
            if (shootingRange == true)
            {
                for (int j = 0; j < SponeEnemyMany; j++)
                {
                    GameObject target = Instantiate
                        (Prefab_Target, new Vector3(15f, 0f, -15f), Quaternion.Euler(0, 0, 0));
                    if(target.GetComponent<NavMeshAgent>() == true)
                    {
                        target.GetComponent<NavMeshAgent>().enabled = false;
                    }
                    target.tag = "Enemy";
                }
                Wave_number_EnemyCount[i] = SponeEnemyMany;
            }
            else
            {
                Debug.Log("IsNotShootingRange" + EnemyPackage[i].transform.childCount);
                int childcount = EnemyPackage[i].transform.childCount;
                Wave_number_EnemyCount[i] = childcount;
                for (int j = 0; j < childcount; j++)
                {
                    GameObject target = Instantiate
                        (Prefab_Target, new Vector3(15f, 0f, -15f), Quaternion.Euler(0, 0, 0));
                    if (target.GetComponent<NavMeshAgent>() == true)
                    {
                        target.GetComponent<NavMeshAgent>().enabled = false;
                    }
                    target.tag = "Enemy";
                }
            }
        }

        EnemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        Initialisation();
    }

    void Initialisation()
    {
        timer = 1f;
        deadEnemyCount = 0;
        Score = 0;
        killmany = 0;
        end = false; //All task is done
        ScoreBox.text = null;
        RoundBox.text = null;
        killBox.text = null;
        now_Wave_number = -1;
        shotCount = 0;
        hitCount = 0;
        EnemyMany = 0;
        hitRate = new float[Wave_number+2];
        EnemyLeave = new string[Wave_number+2];
        for (int i = 0; i <= Wave_number; i++)
        {
            hitRate[i] = 0f;
            EnemyLeave[i] = "";
        }
    }

    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();

        int a = 0;

        for (int j = 0; j < enemyShape_input.Length; j++)
        {
            if (j != enemyShape)
            {
                enemyShape_input[j].SetActive(false);
            }

        }

        if(CursorController.is999 == 0)
        {
            timer = 99999;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Score.ToString();
        if((PlayerController.hp <= 0 || end == true) && once == false)
        {
            Time.timeScale = 0;
            RecordStatus();
            for (int i = 0; i < Wave_number; i++)
            {
                if (dataManager != null)
                {
                    dataManager.postData(Score, i, hitRate[i], EnemyLeave[i], Wave_number_EnemyCount[now_Wave_number]);
                }
            }
            canvasController.WhenPlayerDie(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("カーソル使えます");
            once = true;
        }
        else if(isCountDown == true)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = RePlayObjectCollecter.world_time;
        }

        
        if(now_Wave_number >= 0)
        {
            if(now_Wave_number <= Wave_number)
            {
                Debug.Log("killmany" + killmany + "now_Wave_number" + Wave_number_EnemyCount[now_Wave_number] + "CursorController.is999" + CursorController.is999);

                if (Wave_number_EnemyCount[now_Wave_number] <= killmany && CursorController.is999 == 2)
                {
                    WaveUpdate();
                }
            }
        }

        if(now_Wave_number < 0)
        {
            timeManager.GetComponent<Text>().text = "";
        }
        else
        {
            timeManager.GetComponent<Text>().text = Mathf.Floor(timer + 1f - 3f).ToString();
        }
    }

    public Text ScoreBox;
    public Text RoundBox;
    public Text killBox;

    public void download()
    {

    }

    public void GoBackConfig()
    {
        SceneManager.LoadScene("AllInOneScene");
        CursorController.is999 = 0;
    }
    public void TryAgain()
    {
        if(shootingRange == true) //go shooting range
        {
            CursorController.is999 = 1;
            SceneManager.LoadScene("AllInOneScene");
        }
        else
        {
            CursorController.is999 = 2;
            SceneManager.LoadScene("AllInOneScene");
        }
        
    }

    public void WaveUpdate()
    {
        if(isCountDown == true)
        {
            timer = LeftTime;
            m = Random.value;
            n = Random.value;
            if (m > 0.5f)
            {
                m = 1f;
            }
            else
            {
                m = -1f;
            }
            if (n > 0.5f)
            {
                n = 1f;
            }
            else
            {
                n = -1f;
            }
        }
        else
        {
            if (now_Wave_number <= Wave_number)
            {
                RecordStatus();
            }

            now_Wave_number++;

            if (now_Wave_number >= Wave_number)
            {
                end = true;
            }
            else
            {
                if(shootingRange == true)
                {
                    Change_SponeTarget_Position();

                }
                else
                {
                    Static_SponeTarget_Position();
                }
                EnemyMany += Wave_number_EnemyCount[now_Wave_number];
                premany = EnemyMany;
            }

            
        }
    }


    void Change_SponeTarget_Position()
    {
        for (int i = 0; i < Wave_number_EnemyCount[now_Wave_number]; i++)
        {
            EnemyBox[sponeCount].transform.position = ShootingRange_ParentObject.transform.GetChild
                (Random.Range(0, ShootingRange_ParentObject.transform.childCount)).transform.position;
            Transform a = EnemyBox[sponeCount].transform;
            if (a.GetComponent<NavMeshAgent>() == true)
            {
                a.GetComponent<NavMeshAgent>().enabled = true;
                if (a.GetComponent<AICharacterControl>() == true)
                {
                    a.GetComponent<AICharacterControl>().enabled = true;
                }
            }
            a.transform.parent = null;
            sponeCount++;
        }
    }

    int sponeCount;

    void Static_SponeTarget_Position()
    {
        for (int i = 0; i < Wave_number_EnemyCount[now_Wave_number]; i++)
        {
            EnemyBox[sponeCount].transform.position = EnemyPackage[now_Wave_number].transform.GetChild(i).transform.position;
            EnemyBox[sponeCount].transform.eulerAngles = EnemyPackage[now_Wave_number].transform.GetChild(i).transform.eulerAngles;
            Transform a = EnemyBox[sponeCount].transform;
            if (a.GetComponent<NavMeshAgent>() == true)
            {
                 a.GetComponent<NavMeshAgent>().enabled = true;
                if (a.GetComponent<AICharacterControl>() == true)
                {
                    a.GetComponent<AICharacterControl>().enabled = true;
                }
            }
            a.transform.parent = null;
            sponeCount++;
        }
    }

    void RecordStatus()
    {
        if(now_Wave_number >= 0 && now_Wave_number<=Wave_number)
        {
            if (hitCount > 0)
            {
                hitRate[now_Wave_number] = (float)hitCount / (float)shotCount;
            }
            else
            {
                hitRate[now_Wave_number] = 0;
            }

            EnemyLeave[now_Wave_number] = killmany.ToString() + "/" + Wave_number_EnemyCount[now_Wave_number].ToString();

            Debug.Log(now_Wave_number + "hitRate: " + hitRate[now_Wave_number]);
            ScoreBox.text = null;
            RoundBox.text = null;
            killBox.text = null;
            for (int i = 0; i < Wave_number; i++)
            {
                ScoreBox.text += (i).ToString() + "\n";
                RoundBox.text += hitRate[i].ToString() + "\n";
                killBox.text += EnemyLeave[i] + "\n";
            }

        }
        hitCount = 0;
        shotCount = 0;
        killmany = 0;
    }
    public Transform[] AlreadyDecidedSponePoint;
}
