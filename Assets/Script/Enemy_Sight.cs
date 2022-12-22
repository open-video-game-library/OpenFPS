using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Sight : MonoBehaviour
{
    Enemy Enemy;
    bool watchOut;
    private RaycastHit hit;
    private NavMeshAgent agent;
    private int destPoint = 0;

    //public Transform en;
    public Transform[] points;
    Transform[] search_points;
    public GameObject ParentObject;
    public GameObject Target;

    public ShotPoint ShotPoint;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
        Enemy = transform.root.gameObject.GetComponent<Enemy>();
        agent = transform.root.gameObject.GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        
        //point��"Point��Transform���u�`����

        ParentObject = GameObject.Find("Apoint").gameObject;
        Target = GameObject.Find("Main Camera").gameObject;
        search_points = new Transform[ParentObject.transform.childCount];

        int a = 0;
        
        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            search_points[i] = ParentObject.transform.GetChild(i).gameObject.transform;
            if(search_points[i].tag == "destination_point")
            {
                //Debug.Log(search_points[i].tag);
                
                a++;
            }
        }
        points = new Transform[a];
        int b = 0;
        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            search_points[i] = ParentObject.transform.GetChild(i).gameObject.transform;
            if (search_points[i].tag == "destination_point")
            {
                //Debug.Log(search_points[i].tag);
                points[b] = ParentObject.transform.GetChild(i).gameObject.transform;
                b++;
            }
        }
        GotoNextPoint();
        //agent.isStopped = true;
        speed = agent.speed;
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        if(points[destPoint] == null)
        {
            destPoint = (destPoint + 1) % points.Length;
            GotoNextPoint();
        }
        agent.destination = points[destPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
        
        destPoint = (destPoint + 1) % points.Length;



    }



// Update is called once per frame
void Update()
    {
        agent.speed = speed;
        if (watchOut == false)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }


    
    }

    Vector3 a;

    /*
    void SetDestination()
        Vector3 randomPos = new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
        //SamplePosition�͐ݒ肵���ꏊ����5�͈̔͂ōł��߂�������Bake���ꂽ�ꏊ��T���B
        NavMesh.SamplePosition(randomPos, out navMeshHit, 5, 1);
        navMeshAgent.destination = navMeshHit.position;
    }
    */



    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {
            //Debug.Log(other.transform.gameObject.tag);
            //GameObject Target = GameObject.Find("Player");
            Target = other.transform.gameObject;
            var diff = Target.transform.position - transform.parent.transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;

            Ray ray = new Ray(transform.parent.position, direction);
            LayerMask layerMask = ~(7 << 8);
            if (Physics.Raycast(transform.parent.position, direction, out hit, distance, layerMask))
            {
                Debug.Log("hit:" + hit.transform.gameObject.tag);
                if (hit.transform.gameObject.tag == "Player")
                {
                    
                    this.transform.root.transform.LookAt(Target.transform.position);
                    //Debug.Log(hit.transform.gameObject.tag);

                    a = Target.transform.position;
                    //this.transform.parent.transform.qulerAngles = 
                    agent.speed = 0;

                    ShotPoint.Fire();

                    agent.destination = hit.transform.position;

                    //break;
                    //
                    //this.transform.parent.gameObject.transform.localScale = new Vector3(2, 1, 3);
                }
                /*
                foreach (RaycastHit hit in Physics.RaycastAll(ray))
                {
                    //Debug.DrawRay(transform.parent.position, direction, Color.red, distance, false);
                    
                    else if (hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Enemy_Sight")
                    {

                    }
                    else
                    {
                        break;
                        agent.destination = a; //�Ō�Ɍ������ʒu�Ɉړ�������A�}�b�v�T�����ĊJ


                        //agent.isStopped = true;
                        //this.transform.parent.gameObject.transform.localScale = new Vector3(1, 1, 1);

                    }
                }
                */
                var pretransform = Target.transform.position;
            }

        }
        else if (other.tag == "destination_point")
        {

            Target = other.transform.gameObject;
            var diff = Target.transform.position - transform.parent.transform.position;
            var distance = diff.magnitude;
            var direction = diff.normalized;
            /*
            if (Physics.Raycast(transform.parent.position, direction, out hit, distance))
            {
                if (hit.transform.gameObject.name != Target.transform.gameObject.name) //�^�[�Q�b�g��ID�ƈႤ�Ȃ�
                {
                    for(int i = 0;i<points.Length; i++)
                    {
                        if(hit.transform.gameObject.name == points[i].transform.gameObject.name)
                        {
                            points[i] = null;
                        }
                    }
                    Debug.Log(hit.transform.gameObject.tag);

                    a = Target.transform.position;

                    agent.isStopped = false;
                    agent.destination = Target.transform.position;
                    //
                    //this.transform.parent.gameObject.transform.localScale = new Vector3(2, 1, 3);
                }
            }
            */
        }
    }
}

//agent.isStopped = true;//����͍��G���~�߂�֐�
//Debug.Log(other.gameObject.name);s
//��������
/*
var aim = other.transform.GetChild(0).transform.position - this.transform.parent.transform.position;
Debug.Log(other.transform.GetChild(0).transform.name + this.transform.parent.transform.name);


var look = Quaternion.LookRotation(aim);
Debug.Log(look.eulerAngles);

//this.transform.GetChild(0).gameObject.transform.eulerAngles = look.eulerAngles;
float distance = 100f; // ��΂�&�\������Ray�̒���
float duration = 3f;
Ray ray = new Ray(transform.parent.transform.position, look.eulerAngles);

//
  // �\�����ԁi�b�j
Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
RaycastHit hit;
//�v���C���[�̕����Ƀ��C���΂��A�����v���C���[�ɓ���������ǂ�������
if (Physics.Raycast(ray, out hit, distance))//10.0f�̕������R�[���̑傫����
    //�v���C���[�̕����Ƀ��C���΂��A�����v���C���[�ɓ���������ǂ�������
{

    //Debug.Log(hit.collider.gameObject.name);
    if (hit.collider.gameObject.tag == "Player")
    {

        //�v���C���[������������`�F�C�X�B�v���C���[�����F�\�̍�
        //�ɂ́A�R�[�����߂��Ⴍ����f�J�����A�Ō�Ɏ��F�����ꏊ�܂ňړ�����
        Enemy.target = hit.collider.gameObject;

    }

}
*/




