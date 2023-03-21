using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRecorder : MonoBehaviour
{

    private bool isPlayBack;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator recorderAnimator;

    private Vector3 defaultPos;
    private Quaternion defaultRot;

    public float startRecording_time;
    void Start()
    {
        //recorderAnimator.SetFloat("Speed", 1f);
    }

    public void StartRecord(float time)
    {
        Debug.Log("StartRecordCount");
        if (isPlayBack)
        {
            animator.StopPlayback();
            recorderAnimator.StopPlayback();
        }
        //　キャラクターの初期位置を保存
        defaultPos = animator.transform.position;
        defaultRot = animator.transform.rotation;
        animator.StartRecording(0);
        recorderAnimator.StartRecording(0);
        animator.recorderStartTime = time;
        recorderAnimator.recorderStartTime = time;
        startRecording_time = time;
        isPlayBack = false;
        Debug.Log("アニメーションの録画開始");
    }

    public void StopRecord()
    {
        Debug.Log("アニメーションの録画停止");
        isPlayBack = false;
        animator.StopRecording();
        recorderAnimator.StopRecording();
    }

    public void PlayBack()
    {
        //　録画してなければ何もしない
        if (animator.recorderStopTime <= 0)
        {
            Debug.Log("アニメーションなし"+ animator.recorderStopTime);
            return;
        }
        if (animator && recorderAnimator && !isPlayBack)
        {
            animator.Rebind();
            //recorderAnimator.Rebind();
            animator.StartPlayback();
            recorderAnimator.StartPlayback();
            animator.playbackTime = 0f;
            //animator.transform.position = defaultPos;
            //animator.transform.rotation = defaultRot;
            isPlayBack = true;
            Debug.Log("アニメーションの再生");
        }
    }

    public void StopPlayBack()
    {
        isPlayBack = false;
        animator.StopPlayback();
        recorderAnimator.StopPlayback();
        Debug.Log("アニメーションの停止");
    }
    public float playBackTime;
    void Update()
    {
        if (isPlayBack)
        {
            playBackTime = animator.playbackTime + recorderAnimator.GetFloat("Speed") * Time.deltaTime;
            if (RePlayObjectCollecter.world_time < startRecording_time)
            {
                return;
            }
            /*
            if (playBackTime >= recorderAnimator.recorderStopTime)
            {
                playBackTime = RePlayObjectCollecter.world_time;
                //animator.transform.position = defaultPos;
                //animator.transform.rotation = defaultRot;
            }
            */
            recorderAnimator.playbackTime = RePlayObjectCollecter.world_time;
            animator.playbackTime = RePlayObjectCollecter.world_time;
        }
    }
}