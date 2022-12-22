using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    /*
    public NavMeshAgent player;
    public GameObject target;
    //public GameObject Ene;
    //public Transform muki;
    //public GameObject Player;
    public int MaxHP;
    public int HP;
    //public Transform s;
    Rigidbody[] ragdollRigidbodies;
    Collider[] ragColliderbodies;
    Enemy_Sight Enemy_Sight;
    private NavMeshAgent agent;
    public GameObject hpSlider;
    public GameObject a;
    public Slider slider;

    public float downtime;

    Animator animator;
    Wave Wave;
    void Start()
    {
        

        target = GameObject.Find("Player");
        a = (GameObject)Instantiate(hpSlider, this.transform.position, Quaternion.identity, GameObject.Find("WorldCanvas").transform);
        slider = a.GetComponent<Slider>();
        slider.maxValue = MaxHP;
        

        player = gameObject.GetComponent<NavMeshAgent>();
        //Ene = this.transform.parent.gameObject;

        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragColliderbodies = GetComponentsInChildren<Collider>();

        animator = GetComponent<Animator>();

        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        foreach (Collider rigidbody in ragColliderbodies)
        {
            //rigidbody.isTrigger = true;

        }
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = EnemyStatusController.enemy_Speed;
        preHP = HP;

        //StartCoroutine(Test());
        Wave = GameObject.Find("Wave_Maker").GetComponent<Wave>();
    }

    void SetRagdoll(bool isEnabled)
    {

        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.AddForce(new Vector3(0f, 0f, 10f));
            //rigidbody.isKinematic = !isEnabled;
            animator.enabled = !isEnabled;
            agent.speed = 0;

        }
        foreach (Collider rigidbody in ragColliderbodies)
        {
            rigidbody.gameObject.layer = 7;
        }
    }

    IEnumerator Test()
    {
        SetRagdoll(false);
        yield return new WaitForSeconds(3f);
        SetRagdoll(true);
    }

    float die;
    int preHP;
    void Update()
    {
        if (slider == null)
        {
            return;
        }
        slider.value = HP;
        slider.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y + 2f,
            this.transform.position.z);
        if (preHP != HP)
        {
            agent.destination = target.transform.position;
            Debug.Log(agent.destination);
        }
        preHP = HP;

        if (HP <= 0)
        {

            if (die <= 0)
            {
                SetRagdoll(true);
                die+=0.01f;
                Wave.EnemyMany--;
                Wave.killmany++;
            }
            else
            {
                player.isStopped = true;
                Destroy(this.transform.root.gameObject, downtime);
                Destroy(a);
            }
            
        }
        else
        {
            //var aim = this.Player.transform.position - this.transform.position;
            //var look = Quaternion.LookRotation(aim);
            //Vector3 a = look.eulerAngles;

            //this.transform.eulerAngles = muki.eulerAngles;
            
            if (target != null)
            {
                //player.destination = target.transform.position;
            }
            else
            {
                //ƒ^ƒQ‚ª‚È‚¢

            }
            
        }
        

    }
    //public float bounce = 10f;
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Test");
        if (collision.transform.root.tag == "Player")
        {
            
            //collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(3f,4f,3f);
            //PlayerController.hp -= 1;
            //collision.gameObject.GetComponent<PlayerController>().hp -= 1;
            //agent.isStopped = true;
            //Invoke("DelayMethod", 1f);
        }
        
    }
    void DelayMethod()
    {
        agent.isStopped = false;
    }

    */
}
