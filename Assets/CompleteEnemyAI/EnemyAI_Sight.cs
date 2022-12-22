using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyAI_Sight : MonoBehaviour
{
    public AICharacterControl AIChanracterControl; //StandardAssets CharacterController
    //Enemy need to be always noticed EnemyPoint;
    //control Enemy by Change aicharavtercontrol target

    [SerializeField] Transform Player_Transform; //GetMainCamera to Search
    [SerializeField] EnemyAI enemyAI;
    private RaycastHit hit;
    private RaycastHit local_hit;

    Transform last_watched_position;
    public GameObject targetcube_Prefab; //Control Enemy by change targetcubePos;
    GameObject targetcubePos;
    public float viewingangle = 90f; //Enemy search hinding object angle

    [SerializeField]  Animator m_Animator;
    public float shootLength = 5f;
    public float searchLength = 15f;

    bool fireing;
    bool reloading;
    public ShotPoint shotpoint;

    bool dead;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] HandIK handIK;

    //GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        AIChanracterControl.SetTarget(null); //Firstly Enemy dont chase Player
        viewingangle = EnemyStatusController.enemy_Sight;
        shootLength = EnemyStatusController.enemy_Shoot_Length;
        targetcubePos = Instantiate(targetcube_Prefab, Player_Transform.position, Quaternion.identity);
        //Player_Transform = GameObject.Find("Player").transform;//Get MainCamera transform to search Player
        if (this.transform.root.GetComponent<NavMeshAgent>() == false)
        {
            AIChanracterControl.enabled = false;
        }
    }

    public void Fire()
    {
        if (shotpoint.leavebullets > 0)
        {
            fireing = true;
            reloading = false;
            enemyAI.GetComponent<Animator>().SetBool("reloading", false);
        }
        else
        {
            fireing = false;
            reloading = true;
        }
    }

    public void Remove()
    {
        dead = true;
    }

    void SearchingCube_Transform_Detecter(float distance, Vector3 direction)
    {
        /*
        LayerMask layerMask = 1 << 0;
        float min = distance;
        if (distance > shootLength)
        {
            min = shootLength;
        }
        float rememberDirection = 1000f;

        this.transform.eulerAngles = direction;
        direction.y = 0f;
        this.transform.Rotate(new Vector3(0f, -viewingangle / 2, 0f));
        float rotateAngle = 5f;
        for (float i = -viewingangle / 2; i < viewingangle / 2; i += rotateAngle)
        {
            Debug.DrawRay(this.transform.position, this.transform.forward * shootLength, new Color(0f, 0f, 1f, 1f));
            this.transform.root.transform.Rotate(new Vector3(0f, rotateAngle, 0f));

            if (Physics.Raycast(this.transform.position, this.transform.forward, out local_hit, shootLength, layerMask))//From y=0f;
            {
                Debug.DrawRay(this.transform.position, this.transform.forward, new Color(0f,1f,0f,1f), shootLength);
                float dist = Vector3.Distance(local_hit.transform.position, this.transform.position);
                if(dist > 1.5f && Mathf.Abs(local_hit.transform.position.y - this.transform.position.y) < 0.25f)
                {
                    if (min > dist)
                    {
                        rememberDirection = i;
                        min = dist;
                    }
                }
            }

        }

        //Decided

        if(rememberDirection == 1000f)
        {

            return;
        }
        else
        {
            this.transform.Rotate(new Vector3(0f, rotateAngle * rememberDirection, 0f));
            if (Physics.Raycast(this.transform.position, this.transform.forward, out local_hit, shootLength, layerMask))//From y=0f;
            {
                targetcubePos.transform.position = local_hit.transform.position;
            }
        }

        */
    }

    Vector3 diff;
    Vector3 direc;
    Vector3 direc2;
    float distance;
    Vector3 direction;
    float angle;
    bool notice;

    void MathDirection()
    {
        diff = Player_Transform.position - this.transform.position; //distance this and "Player_Transform"
        
        direc = this.transform.up;
        direc2 = this.transform.root.transform.forward;
        //Vector3 direc22 = this.transform.root.transform.up;
        //Vector3 direc222 = this.transform.root.transform.right;
        //direc2 = new Vector3(0, this.transform.position.y, 0);

        distance = diff.magnitude;
        direction = diff.normalized;
        Debug.DrawRay(this.transform.position, direction, new Color(1, 1, 1, 1), 0.1f);
        Debug.DrawRay(this.transform.position, direc2, new Color(1, 0, 0, 1), 0.1f);
        //Debug.DrawRay(this.transform.position, direc222, new Color(0, 1, 0, 1), 0.1f);
        //Debug.DrawRay(this.transform.position, direc222, new Color(0, 0, 1, 1), 0.1f);
        angle = Vector3.Angle(direc2, diff); //angle gap of this.lookat and angle to "Player_Transform"
    }

    void Update()
    {
        if (this.transform.root.GetComponent<NavMeshAgent>() == false)
        {
            handIK.ikActive = false;
            AIChanracterControl.enabled = false;
            return;
        }
        else
        {
            if(agent.enabled == true)
            {
                AIChanracterControl.enabled = true;
            }
            else
            {
                return;
            }
        }
        
        if (dead == true)
        {
            handIK.ikActive = false;
            AIChanracterControl.SetTarget(null);
            if (agent == true)
            {
                agent.isStopped = true;
            }
            return;
        }

        if(enemyAI.HP <= 0 || enemyAI == null)
        {
            handIK.ikActive = false;
            AIChanracterControl.SetTarget(null);
            if (agent == true)
            {
                agent.isStopped = true;
            }
            return;
        }

        

        MathDirection();
        if (handIK == true)
        {
            Debug.Log("angle" + angle);
        }
        Ray ray = new Ray(transform.parent.position, direction);
        LayerMask layerMask = 1 << 0 | 1 << 7; //Only default and Player Object
        

        if (reloading == true) //When Already Find and in Range
        {
            m_Animator.SetBool("Reloading", true);
            return;
        }
        else
        {
        }

        if (Physics.Raycast(transform.position, direction, out hit, shootLength, layerMask))
        {
            if (hit.transform.gameObject.tag == "Player")//when range of shoot
            {
                if (Mathf.Abs(angle) < viewingangle / 2) //In This object viewing Angle
                {
                    notice = true;
                    if (handIK == true)
                    {
                        handIK.ikActive = true;
                    }
                    if (agent == true)
                    {
                        agent.isStopped = true;
                    }
                    if (shotpoint.leavebullets > 0)
                    {
                        m_Animator.SetBool("ReadytoThrow", true);
                        if (fireing == true)
                        {
                            shotpoint.ShotIntervalManager();
                        }
                    }
                    else
                    {
                        reloading = true;
                        fireing = false;
                    }
                    }
                else
                {
                    m_Animator.SetBool("ReadytoThrow", false);
                    if (agent == true)
                    {
                        agent.isStopped = false;
                    }
                    if(notice == true)
                    {
                        AIChanracterControl.SetTarget(Player_Transform);
                    }
                    fireing = false;
                    if (handIK == true)
                    {
                        handIK.ikActive = false;
                    }
                }
            }
            else
            {
                m_Animator.SetBool("ReadytoThrow", false);
                if (agent == true)
                {
                    agent.isStopped = false;
                }
                if (notice == true)
                {
                    AIChanracterControl.SetTarget(Player_Transform);
                }
                fireing = false;
                if (handIK == true)
                {
                    handIK.ikActive = false;
                }
            }
        }
        else
        {
            m_Animator.SetBool("ReadytoThrow", false);
            if (agent == true)
            {
                agent.isStopped = false;
            }
            fireing = false;
            if (handIK == true)
            {
                handIK.ikActive = false;
            }
        }

                /*
                    if (!agent.pathPending && agent.remainingDistance >= 0.25f) //When Already Find and in Range
                    {
                        AIChanracterControl.SetTarget(null);
                    }

                    else if (Physics.Raycast(transform.position, direction, out hit, shootLength, layerMask))
                    {
                    if (hit.transform.gameObject.tag == "Player")//when range of shoot
                    {
                        if (Mathf.Abs(angle) < viewingangle / 2) //In This object viewing Angle
                        {
                            if (agent == true)
                            {
                                agent.isStopped = true;
                            }
                            if (reloading == false && shotpoint.leavebullets > 0)
                            {
                                m_Animator.SetBool("ReadytoThrow", true); //Start to Shoot
                                                                          //targetcubePos.transform.position = hit.transform.position;//this target cube searching object to hide
                                if (fireing == true && m_Animator.GetBool("Reloading") == false)
                                {
                                    shotpoint.ShotIntervalManager();



                                    if (handIK == true)
                                    {
                                        handIK.ikActive = true;
                                    }
                                    if (m_Animator.GetBool("ReadytoThrow") == false)
                                    {
                                        reloading = true;
                                        fireing = false;
                                    }
                                }
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            handIK.ikActive = false;
                            fireing = false;
                            AIChanracterControl.SetTarget(Player_Transform);
                            m_Animator.SetBool("ReadytoThrow", false);
                        }
                    }
                        else
                        {
                        handIK.ikActive = false;
                        m_Animator.SetBool("ReadytoThrow", false); //Queit to Attack
                            fireing = false;
                        }

                    }
                    else
                    {
                    handIK.ikActive = false;
                    m_Animator.SetBool("ReadytoThrow", false); //Queit to Attack
                        fireing = false;
                    }
                */
            }
    public void Notice()
    {
        MathDirection();
        /*
        Quaternion targetRotation = Quaternion.LookRotation(Player_Transform.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        */
        //AIChanracterControl.SetTarget(targetcubePos.transform);
    }
    public void moveable()
    {
        SearchingCube_Transform_Detecter(distance, direction);
        //AIChanracterControl.SetTarget(targetcubePos.transform);
        m_Animator.GetComponent<Animator>().SetBool("Reloading", false);
        m_Animator.GetComponent<Animator>().SetBool("ReadytoThrow", false);
        reloading = false;
        fireing = false;
    }
}





