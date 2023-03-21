using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    public static int is999;
    //is999 == 0 : config
    //is999 == 1 : shootingRange
    //is999 == 2 : gameScene
    //is999 == 3 : 
    public static bool CursorAvailable;

    public Slider AimSlider;
    public Slider SensSlider;

    public Slider WalkSlider;
    public Slider DashSlider;
    //public Slider CrouchSlider;

    public Slider ZoomSlider;

    public Slider HP;
    //=========================

    public Text aimSlider;
    public Text sensSlider;

    public Text walkSlider;
    public Text dashSlider;
    public int fullCount;

    public Wave Wave;

    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Dropdown enemyDropdown;

    [SerializeField] Transform GunBox;
    [SerializeField] Shot Shot;

    // Start is called before the first frame update
    ShotPoint shotPoint;
    public List<string> optionlist = new List<string>();
    public GameObject[] AllGun;
    public static List <string>[] gunList ;
    public static string[][] listman;
    public ADS_Controller ADS_Controller;
    static bool valueChanged;

    public List<string> enemyOptionlist = new List<string>();
    public List<string> enemyShapelist = new List<string>();

    [SerializeField] InputField SponeEnemy_number;
    public static int sponeEnemy_number;

    public void WaveUpdate()
    {
        //sponeEnemy_number = int.Parse(SponeEnemy_number.text);
        Debug.Log("toggle.isOn" + toggle.isOn);
        Wave.shootingRange = toggle.isOn;
        Debug.Log("Wave.shootingRange" + Wave.shootingRange);
    }

    public static bool playMode;

    [SerializeField] Toggle toggle;

    static int maingun;

    public void GoPlayField()
    {
        if(is999 == 0)
        {
            is999 = 1;
            Wave.shootingRange = false;
        }
        else
        {
            if (is999 == 1)
            {
                is999 = 1;
                Wave.shootingRange = false;
            }
            else
            {
                is999 = 2;
                Wave.shootingRange = true;
            }
        }
        SceneManager.LoadScene("AllInOneScene");
    }

    public void GoShootingRange()
    {
        is999 = 2;
        Wave.shootingRange = true;
        SceneManager.LoadScene("AllInOneScene");
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene("AllInOneScene");
        is999 = 0;
    }

        void Start()
    {
        //toggle.isOn = Wave.shootingRange;
        if(is999 == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (valueChanged == false)
        {
            gunList = new List<string>[GunBox.childCount + 1];
        }
            ADS_Controller.zoomvalue = 1.25f;
            ADS_Controller.aDSSpeed = 0.2f;
            SensSlider.value = PlayerController.Sens;
            AimSlider.value = PlayerController.Aim_Sens;
            ZoomSlider.value = ADS_Controller.fieldofview;

        if (valueChanged == false)
        {
            enemyShapelist = new List<string>();
            for (int i = 0; i < Wave.enemyShape_input.Length; i++)
            {
                enemyShapelist.Add(Wave.enemyShape_input[i].name);
            }
            enemyShape.ClearOptions();
            enemyShape.AddOptions(enemyShapelist);
        }
        else
        {
            Debug.Log("gunList[1][0]" + gunList[1][0]);
        }

    }

    public void DelayMethod()
    {
        Debug.Log("gun" + maingun);
        ChangeGun(dropdown.value);
    }

    private void LateUpdate()
    {

    }

    public static int defaultGun;
    [SerializeField] InputField bulletMany;
    [SerializeField] InputField aDSTime;
    [SerializeField] InputField bulletSpeed;
    [SerializeField] InputField hipShotTime;
    [SerializeField] InputField cycle;
    [SerializeField] InputField damage;
    [SerializeField] InputField reloadtime;
    [SerializeField] Slider gunZoomValue;
    public static bool [] _bulletMode;
    public static bool [] _shootType;
    public static float [] _bulletMany;
    public static float [] _bulletSpeed;
    public static float [] _hipShotTime;
    public static float [] _cycle;
    public static float [] _damage;
    public static float [] _zoomvalue;
    public static float[] _shotinterval;
    public static float[] _reloadtime;
    [SerializeField] Text razer_bullet;
    [SerializeField] Text bullet_Mode;
    int bulletmode;
    int shoottype;

    public void BulletMode() //phsics or not[2]
    {
        bulletmode++;
        gunList[dropdown.value][2] = bulletmode.ToString();
        GunSettingChange();
    }

    public void ShootType() // [1]semi, full
    {
        shoottype++;
        
        gunList[dropdown.value][1] = shoottype.ToString();
        GunSettingChange();
    }

    
    [SerializeField] Dropdown enemyShape;
    public void ChangeShape(int value)
    {
        Wave.enemyShape = enemyShape.value;
        Debug.Log("Wave.enemyShape" + Wave.enemyShape);
    }


    public void ChangeGun(int value)
    {
        for (int i = 0; i < AllGun.Length; i++)
        {
            if(dropdown.value == i)
            {
                //value‚Ée‚Ì’l
                GameObject a = Instantiate(AllGun[dropdown.value],new Vector3(0,0,0), Quaternion.identity);

                foreach (Renderer c in a.GetComponentsInChildren<Renderer>())
                {
                    c.enabled = true;
                }

                a.transform.parent = Shot.transform;
                a.transform.localPosition = new Vector3(0f, 0f, 0f);
                a.transform.localEulerAngles = new Vector3(0, 0, 0);
                a.transform.SetSiblingIndex(1);

                //Shot.GunChange(i);
                bulletmode = int.Parse(gunList[dropdown.value][2]);
                shoottype = int.Parse(gunList[dropdown.value][1]);
                if (shoottype % 2 == 0)
                {
                    razer_bullet.text = "Semi";
                }
                else
                {
                    razer_bullet.text = "Full";
                }
                if (bulletmode % 2 == 0)
                {
                    bullet_Mode.text = "Razer";
                }
                else
                {
                    bullet_Mode.text = "Pythics";
                }

                bulletMany.text = gunList[dropdown.value][3];
                aDSTime.text = gunList[dropdown.value][4];
                bulletSpeed.text = gunList[dropdown.value][5];
                hipShotTime.text = gunList[dropdown.value][6];
                damage.text = gunList[dropdown.value][7];
                cycle.text = gunList[dropdown.value][9];
                reloadtime.text = gunList[dropdown.value][10];
                gunZoomValue.value = float.Parse(gunList[dropdown.value][8]);
                
            }
        }
        
        maingun = dropdown.value;
        Debug.Log("valueChanged" + maingun);
    }
    public void GunSettingChange()
    {
        //gunList[gunNumber][bulletType][shootType][Bullet_Many][ADS_Time]
        //[Bullet_Speed][HipShot_Time][Cycle][Damage][zoom]
        Debug.Log("dropdown.value" + gunList[dropdown.value][1]);
            gunList[dropdown.value][1] = shoottype.ToString();
            gunList[dropdown.value][2] = bulletmode.ToString();
            gunList[dropdown.value][3] = bulletMany.text;
            gunList[dropdown.value][4] = aDSTime.text;
            gunList[dropdown.value][5] = bulletSpeed.text;
            gunList[dropdown.value][6] = hipShotTime.text;
            gunList[dropdown.value][7] = damage.text;
            gunList[dropdown.value][8] = gunZoomValue.value.ToString();
            gunList[dropdown.value][9] = cycle.text;
            gunList[dropdown.value][10] = reloadtime.text;
        GunStatusChange(dropdown.value);
        bulletMany.text = gunList[dropdown.value][3];
        aDSTime.text = gunList[dropdown.value][4];
        bulletSpeed.text = gunList[dropdown.value][5];
        hipShotTime.text = gunList[dropdown.value][6];
        damage.text = gunList[dropdown.value][7];
        gunZoomValue.value = float.Parse(gunList[dropdown.value][8]);
        cycle.text = gunList[dropdown.value][9];
        reloadtime.text = gunList[dropdown.value][10];
        
        Debug.Log("gunList[dropdown.value][9]" + gunList[dropdown.value][9]);
    }

    void GunStatusChange(int value)
    {
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().shoottype = int.Parse(gunList[value][1]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().bulletmode = int.Parse(gunList[value][2]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().fullbullets = int.Parse(gunList[value][3]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().adstime = float.Parse(gunList[value][4]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().shotSpeed = float.Parse(gunList[value][5]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().hipshottime = float.Parse(gunList[value][6]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().bulletdamage = int.Parse(gunList[value][7]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().zoomvalue = float.Parse(gunList[value][8]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().shotInterval = float.Parse(gunList[value][9]);
        GunBox.GetChild(value).GetComponentInChildren<ShotPoint>().reloadtime = float.Parse(gunList[value][10]);
        Shot.ADSandZOOMVlueChange();
        ChangeGun(value);
    }

    // Update is called once per frame
    void Update()
    {
        if(is999 == 0) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        sensSlider.text = ": " + SensSlider.value.ToString();
        aimSlider.text = ": " + AimSlider.value.ToString();
        walkSlider.text = ": " + WalkSlider.value.ToString();
        dashSlider.text = ": " + DashSlider.value.ToString();

        PlayerController.Sens = SensSlider.value;
        PlayerController.Aim_Sens = AimSlider.value;
        ADS_Controller.fieldofview = ZoomSlider.value;
    }

    int shooting_Range_Phase = 3;
    int shooting_Range_Enemy_many = 3;
    int shooting_Range_LeftTime = 5;
    int stage_Range_Phase = 5;
    int stage_Range_Enemy_many = 3;
    int stage_Range_LeftTime = 20;
    int bullets = 20;

    public void Shooting_Range_Phase(string text)
    {
        shooting_Range_Phase = int.Parse(text);
    }
    public void Shooting_Range_Enemy_many(string text)
    {
        shooting_Range_Enemy_many = int.Parse(text);
    }
    public void Shooting_Range_LeftTime(string text)
    {
        shooting_Range_LeftTime = int.Parse(text);
    }

    public void Stage_Range_Phase(string text)
    {
        stage_Range_Phase = int.Parse(text);
    }
    public void Stage_Range_Enemy_many(string text)
    {
        stage_Range_Enemy_many = int.Parse(text);
    }
    public void Stage_Range_LeftTime(string text)
    {
        stage_Range_LeftTime = int.Parse(text);
    }

    public void OnValueChange()
    {
        Shot.transform.root.GetComponentInChildren<ShotPoint>().fullbullets = int.Parse(bulletMany.text);
    }
}
