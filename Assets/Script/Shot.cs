using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;

public class Shot : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint twoBoneIKRight;
    [SerializeField] TwoBoneIKConstraint twoBoneIKLeft;

    [SerializeField] Text bullets;
    public Image image;
    //public float ADSTime = 0.5f;
    public Camera cam;
    public bool kamae;
    public PlayerController playerController;
    public Transform slider;
    
    public ShotPoint shotpoint;
    bool a = false;
    public bool b = false;
    bool c = false;
    int kk;
    bool  dash;

    public Transform gunholder;
    public Transform GunSight;

    public Transform[] shotpointsearcher;

    public Transform maincamera;

    public static int mainWeapon = 0;

    Animator animator;
    public Animator ShotPointAnimator;

    public int shoottype = 0;
    public int bulletmode = 0;
    public int fullbullets = 1;
    public float adstime = 0.2f;
    public float shotSpeed = 500f;
    public float shotInterval = 0.5f;
    public float hipshottime = 0.2f;
    public float zoomvalue = 1.5f;
    public int bulletdamage = 1;
    public float reloadtime = 0.2f;

    public int leavebullets;
    public float timeInterval;

    public GunChangeValueUpdater Latest_GunChangeValueUpdater;
    public static string gunName;

    [SerializeField] GunChangeValueUpdater FirstWeapon;
    [SerializeField] PickUpWeapon pickUpWeapon;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("gunName" + gunName);
        animator = this.GetComponent<Animator>();
        if (gunName == null)
        {
            pickUpWeapon.PickUpWeapon_DoChange(FirstWeapon.gameObject);
        }
        else
        {
            GunChangeValueUpdater prescene_gun = GameObject.Find(gunName).GetComponentInChildren<GunChangeValueUpdater>();
            pickUpWeapon.PickUpWeapon_DoChange(prescene_gun.gameObject);
            if (Latest_GunChangeValueUpdater == null)
            {
                pickUpWeapon.PickUpWeapon_DoChange(FirstWeapon.gameObject);
            }
        }
    }

    public void PickUpGun()
    {
        //Update value of gun status
        if (Latest_GunChangeValueUpdater != null)
        {
            shotpoint = Latest_GunChangeValueUpdater.GetComponentInChildren<ShotPoint>();
            Latest_GunChangeValueUpdater.GunIKUpdate();
            ShotPointAnimator = Latest_GunChangeValueUpdater.GetComponent<GunChangeValueUpdater>().this_ShotPointAnimator;
            ADSandZOOMVlueChange();
        }
        gunName = Latest_GunChangeValueUpdater.gameObject.name;
        Debug.Log("gunName" + gunName);
    }


    public bool shootable;
    public void Shotenable() //switch of enable to shoot
    {
        shootable = true;
    }

    public void Dash() //when Player is running
    {
        shootable = false;
    }

    public void reload()
    {
        ShotPointAnimator.SetBool("Reloading", false);
        shotpoint.leavebullets = shotpoint.fullbullets;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 || shotpoint == null)
        {
            return;
        }

        if (shotpoint == true)
            {
                bullets.text = "<color=#ff0000ff>" + shotpoint.leavebullets + "</color>" + "/" + shotpoint.fullbullets;
            }
            else
            {
                bullets.text = shotpoint.leavebullets + "/" + shotpoint.fullbullets;
            }

            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Reload"))
            {
            Debug.Log("reloadButton");
                ShotPointAnimator.SetBool("Reloading", true);
            }
            bool c = false;
        if ((Input.GetMouseButton(0) || Input.GetButton("Fire")))
        {
            if (this.transform.root.GetComponent<Animator>() == true)
            {
                if (animator.GetBool("Dash") == false)
                {
                    Debug.Log("ShotIntervalManager");
                    shotpoint.ShotIntervalManager();
                }
            }
        }
        else
        {
            shotpoint.ShotIntervalReset();
        }
    }
    public Camera UIcam;
    public Camera bulletCameracam;
    Vector3 sight;
    Vector3 parent;


    public void ADSandZOOMVlueChange()
    {
        this.transform.GetComponent<ADS_Controller>().zoomvalue = zoomvalue;
        this.transform.GetComponent<ADS_Controller>().aDSSpeed = adstime;
    }
    Vector3 preweapon;
}
