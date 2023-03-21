using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RePlayObject : MonoBehaviour
{
    RePlayObjectCollecter RePlayObjectCollecter;
    public AnimationCurve Quaternion_x;
    public AnimationCurve Quaternion_y;
    public AnimationCurve Quaternion_z;
    public AnimationCurve Quaternion_w;

    public AnimationCurve Position_x;
    public AnimationCurve Position_y;
    public AnimationCurve Position_z;

    public AnimationCurve PlayTime; //Record Animation PlayTime

    public Animator this_animator;
    public NavMeshAgent this_navMeshAgent;

    public AnimatorRecorder AnimatorRecorder;

    public bool DontRecordTransform;
    // Start is called before the first frame update
    void Start()
    {
        RePlayObjectCollecter = GameObject.Find("RePlayController").GetComponent<RePlayObjectCollecter>();
        RePlayObjectCollecter.RePlayObjectCollection(this);
        if(GetComponent<Animator>() != null)
        {
            this_animator = GetComponent<Animator>();
        }
        if(GetComponent<NavMeshAgent>() != null)
        {
            this_navMeshAgent = GetComponent<NavMeshAgent>();
        }
        if(AnimatorRecorder == true)
        {
            AnimatorRecorder.StartRecord(RePlayObjectCollecter.world_time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Vector3 pos, Quaternion v, float time)
    {
        Quaternion_x.AddKey(time, v.x);
        Quaternion_y.AddKey(time, v.y);
        Quaternion_z.AddKey(time, v.z);
        Quaternion_w.AddKey(time, v.w);

        Position_x.AddKey(time, pos.x);
        Position_y.AddKey(time, pos.y);
        Position_z.AddKey(time, pos.z);
    }

    public void RecordTransform(float time)
    {
        Add(this.transform.position , this.transform.rotation, time);
    }

    public void IsPlay(float time)
    {
        if (AnimatorRecorder == true)
        {
            this.transform.rotation = new Quaternion(Quaternion_x.Evaluate(time), Quaternion_y.Evaluate(time), Quaternion_z.Evaluate(time), Quaternion_w.Evaluate(time));
            this.transform.position = new Vector3(Position_x.Evaluate(time), Position_y.Evaluate(time), Position_z.Evaluate(time));
        }
        else
        {
            this.transform.rotation = new Quaternion(Quaternion_x.Evaluate(time), Quaternion_y.Evaluate(time), Quaternion_z.Evaluate(time), Quaternion_w.Evaluate(time));
            this.transform.position = new Vector3(Position_x.Evaluate(time), Position_y.Evaluate(time), Position_z.Evaluate(time));
        }
    }
}
