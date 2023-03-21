using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyAI : MonoBehaviour
{
    public Wave Wave;
    int MaxHP;
    public int HP;
    public GameObject hpSlider; //Prefab HP bar
    Slider slider; //this HP bar. 

    Animator animator;
    Rigidbody[] ragdollRigidbodies;
    Collider[] ragColliderbodies;
    EnemyAI_Sight EnemyAI_Sight;

    GunChangeValueUpdater gunChangeValueUpdater;

    public bool rigidBodyDown;

    void Start()
    {
        if (GetComponentInChildren<EnemyAI_Sight>() == true)
        {
            EnemyAI_Sight = GetComponentInChildren<EnemyAI_Sight>();
        }
        GameObject defaultNull_Slider = Instantiate(hpSlider, this.transform.position, Quaternion.identity);
        slider = defaultNull_Slider.GetComponentInChildren<Slider>();
        slider.maxValue = HP;
        slider.transform.parent = GameObject.Find("WorldCanvas").transform;
        animator = GetComponent<Animator>();
        if(rigidBodyDown == true)
        {
            ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
            ragColliderbodies = GetComponentsInChildren<Collider>();
            foreach (Rigidbody rigidbody in ragdollRigidbodies)
            {
                rigidbody.isKinematic = true;
            }
            foreach (Collider rigidbody in ragColliderbodies)
            {

            }
        }
        Wave = GameObject.Find("Wave_Maker").GetComponent<Wave>(); //スコアを付けたす用
        hpSlider_ValueChange();
        this.GetComponent<Animator>().SetInteger("hp", HP);
    }
    public ShotPoint shotPoint;

    void Hit() //this is called when enemy ready to attack;
    {
         //this script need to called all time;
        if (EnemyAI_Sight != null)
        {
            EnemyAI_Sight.Fire();
        }
    }

    void PickUp() //this is called when enemy reload animation completed;
    {
        if(shotPoint != null)
        {
            shotPoint.Reloadcomplete();
        }
        if (EnemyAI_Sight != null)
        {
            Debug.Log("moveable");
            EnemyAI_Sight.moveable();
        }
    }

    bool once;

    public void GetDamage(int damage)
    {
        GetComponent<Animator>().SetInteger("damage", damage);
        Invoke("FreshDamage", 0.1f);
        HP -= damage;
        hpSlider_ValueChange();
        this.GetComponent<Animator>().SetInteger("hp", HP);
        if (HP <= 0 && once == false)
        {
            Wave.Score += 100;
            Wave.EnemyMany--;
            Wave.killmany++;
            Invoke("Remove_itself", 1f);
            if (EnemyAI_Sight != null)
            {
                EnemyAI_Sight.Remove();
            }
            if (gunChangeValueUpdater != null)
            {
                gunChangeValueUpdater.DropDownWeapon();
            }
            if (rigidBodyDown == true)
            {
                foreach (Rigidbody rigidbody in ragdollRigidbodies)
                {
                    rigidbody.isKinematic = false;
                }
            }
            once = true;
            slider.gameObject.SetActive(false);
        }
    }

    public void FreshDamage()
    {
        GetComponent<Animator>().SetInteger("damage", 0);
    }

    void Remove_itself()
    {
        //this.GetComponent<NavMeshAgent>().enabled = false;
    }

    void Notice()
    {
        if (EnemyAI_Sight != null)
        {
            EnemyAI_Sight.Notice();
        }
    }

    void hpSlider_ValueChange()
    {
        slider.value = HP;
        slider.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y + 2f,
            this.transform.position.z);

    }
}
