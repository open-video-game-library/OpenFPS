using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShotPoint : MonoBehaviour
{
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
    public float siya = 60f;

    public GameObject Bullet;
    
    public int shotCount = 30;
    
    private Camera cam;
    public float FireBuck = 0.1f;
    float yVelocity = 0.0f;
    Shot shot;
    public bool ready = false;
    public Transform gun;
    public GameObject tamera;
    public PlayerController playerController;
    public bool shooting;
    float KoshiTime;
    public shellEject shellEject;

    public Animator EnemyAnimator;

    public void Reloadcomplete()
    {
        leavebullets = fullbullets;
        Animator.SetBool("Reloading", false);
    }

    public Animator Animator;

    public Transform burrel;
    public PlayerController PlayerController;
    public int leavebullets;

    public bool reloading;

    public Text bullets;
    public GameObject magazine;

    public float reloadime;

    public GameObject BulletHole_Prefab;
    public GameObject HitEffect_Prefab;

    Ray ray;
    RaycastHit hit;

    public bool RealBullet;

    public GameObject damage;
    Transform WorldCanvas;

    public GameObject Hiteffect;

    public AudioClip sound1;
    AudioSource audioSource;

    public bool gunholderIsEnemy;

    float m_FieldOfView = 60f;

    public ADS_Controller ADS_Controller;

    [SerializeField]
    ParticleSystem muzzleFlashParticle;

    [SerializeField]
    AudioClip ShotAudioClip;

    [SerializeField]
    GameObject HitEffectParticle;

    [Header("Attach_GetChangeValueUpdater")]
    /// <summary>
    /// This_GameObject_GetChangeValueUpdater
    /// </summary>
    [SerializeField] GunChangeValueUpdater GunChangeValueUpdater;

    [SerializeField] Pool BulletObjectPool;
    [SerializeField] Pool BulletHoleObjectPool;
    [SerializeField] Pool HitEffectObjectPool;
    // Start is called before the first frame update
    void Start()
    {
        //BulletObjectPool = new GameObject("BulletObjectPool").transform.GetComponent<Pool>();
        //BulletHoleObjectPool = new GameObject("BulletHoleObjectPool").transform.GetComponent<Pool>();

        Animator = GameObject.Find("GunManager").GetComponent<Animator>();
        //shotSpeed = PlayerController.bulletSpeed;
        m_FieldOfView = PlayerController.FieldOfView;
        KoshiTime = PlayerController.HipShotTime;
        audioSource = GetComponent<AudioSource>();

        if (this.transform.root.transform.tag == "Enemy")
        {
            gunholderIsEnemy = true;
        }
        if (gunholderIsEnemy == true)
        {
            EnemyAnimator = this.transform.root.GetComponent<Animator>();
        }
        else
        {
            WorldCanvas = GameObject.Find("WorldCanvas").transform;
            cam = tamera.GetComponent<Camera>();
            shot = this.transform.root.GetComponentInChildren<Shot>();
            ADS_Controller = this.transform.root.GetComponentInChildren<ADS_Controller>();
        }
        leavebullets = fullbullets;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    public float timeInterval;
    float sec;
    
    void DelayMethod()
    {
        //Camera.main.fieldOfView = m_FieldOfView;
    }

    public void ShotIntervalReset()
    {
        timeInterval = shotInterval;
    }

    public void ShotIntervalManager()
    {
        if (GunChangeValueUpdater != null)
        {
            GunChangeValueUpdater.GetComponent<Animator>().SetBool("Reloading", false);
        }
        if (shotInterval < timeInterval)//実際発射
        {
            if (leavebullets > 0)
            {
                muzzleFlashParticle.GetComponent<ParticleSystem>().Play();
                GetComponent<AudioSource>().PlayOneShot(ShotAudioClip);
                if(GetComponent<InvokeSound>() == true)
                {
                    GetComponent<InvokeSound>().RecordSound(ShotAudioClip, GetComponent<AudioSource>());
                }
                if (muzzleFlashParticle.GetComponentInChildren<InvokeParticle>() == true)
                {
                    muzzleFlashParticle.GetComponentInChildren<InvokeParticle>().RecordParticle(muzzleFlashParticle.GetComponentInChildren<ParticleSystem>());
                }
                if(GunChangeValueUpdater == true)
                {
                    GunChangeValueUpdater.GetComponent<Animator>().Play("Recoil");
                }
                Wave.shotCount += 1;
                while (shotInterval < timeInterval)
                {
                    timeInterval -= shotInterval;
                }
                if (bulletmode % 2 == 1)
                {
                    PhysicsFire();
                }
                else
                {
                    RayFire();
                }
                leavebullets--;

                if (leavebullets == 0)
                {
                    if (gunholderIsEnemy == true)
                    {
                        EnemyAnimator.SetBool("ReadytoThrow", false);
                    }
                    else
                    {
                        GunChangeValueUpdater.GetComponent<Animator>().SetBool("Reloading", true);
                    }
                }
            }
            else
            {
                if (gunholderIsEnemy == true)
                {
                    EnemyAnimator.SetBool("ReadytoThrow", false);
                    //EnemyAnimator.SetBool("Reloading", true);
                }
                else
                {
                    GunChangeValueUpdater.GetComponent<Animator>().SetBool("Reloading", true);
                }
            }
        }
        timeInterval += Time.deltaTime;
    }

    public void PhysicsFire()
    {
        GameObject bullet = BulletObjectPool.GetObject(Bullet, burrel.transform.position, Quaternion.identity);

        if (bullet.GetComponent<PlayerAttack>() == true)
        {
            bullet.GetComponent<PlayerAttack>().thisParent = this.transform.root.gameObject;
        }
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * shotSpeed);
        Wave.shotmany++;
    }

    public void RayFire()
    {
        int layerMask = ~(1 << 2) & ~(1 << 7);

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, layerMask))
        {
            // ヒットした位置＋面の前方向にずらす
            Vector3 pos = hit.point + (hit.normal * 0.01f);
            // 第一引数：弾痕オブジェクトの正面となる軸
            // 第二引数：向けたい方向
            // オブジェクト作成

            GameObject BulletHole = BulletHoleObjectPool.GetObject(BulletHole_Prefab, pos, Quaternion.identity);
            //GameObject HitEffect = HitEffectObjectPool.GetObject(HitEffect_Prefab, pos, Quaternion.identity);
            BulletHole.GetComponentInChildren<ParticleSystem>().Play();
            BulletHole.GetComponentInChildren<InvokeParticle>().RecordParticle(BulletHole.GetComponentInChildren<ParticleSystem>());

            Quaternion rot = Quaternion.FromToRotation(-1 * BulletHole.transform.up, hit.normal);
            BulletHole.transform.rotation = rot;

            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green, 2, false);
            if (hit.collider.transform.root.GetComponent<EnemyAI>() == true)
            {
                Wave.hitCount += 1;
                //GameObject a = Instantiate(damage, new Vector3(pos.x, pos.y + 1f, pos.z), Quaternion.identity, WorldCanvas);
                float dist = Vector3.Distance(this.transform.position, hit.collider.transform.position);
                if (hit.collider.transform.name.Contains("Head"))
                {
                    hit.collider.transform.root.GetComponent<EnemyAI>().GetDamage(5);
                    Wave.Score += 50;
                    //a.GetComponent<Text>().text = "50";
                }
                else
                {
                    hit.collider.transform.root.GetComponent<EnemyAI>().GetDamage(1);
                    Wave.Score += 10;
                    //a.GetComponent<Text>().text = "10";
                }
            }
            else
            {

            }
            
        }
    }



    public void Fire()
    {
        /*

            audioSource.PlayOneShot(sound1);
        if (gunholderIsEnemy == true && )
        {
                audioSource.PlayOneShot(sound1);
                GameObject bullet = (GameObject)Instantiate(Bullet, burrel.transform.position, Quaternion.identity);

                if (bullet.GetComponent<PlayerAttack>() == true)
                {
                    bullet.GetComponent<PlayerAttack>().thisParent = this.gameObject;
                }
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                bulletRb.AddForce(transform.forward * shotSpeed);
                Destroy(bullet, 3.0f);
                leavebullets -= 1;
            
        }
        shotInterval += timeInterval;
        muzzleFlashParticle.Play();
        */
    }

    void Update()
    {

    }
}
