using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimaterController : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Transform CameraTransform;
    [SerializeField] Transform BodyRotate;
    public Animator m_Animator;
    public CapsuleCollider col;
    float prepos_x;
    float prepos_y;
    float prepos_z;

    float m_forward;
    float m_right;
    float m_jump;

    [SerializeField] float runCycle;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform CharacterRotate;
    [SerializeField] Transform CharacterSpineRotate;
    [SerializeField] Transform SpineRotate;

    private Animator anim;
    private AnimatorStateInfo Spineinfo;
    private AnimatorStateInfo info;

    [SerializeField] Text dirman;

    public Transform RightHand;
    public Transform LeftHand;
    // Start is called before the first frame update
    void Start()
    {
        anim = m_Animator;
        info = anim.GetCurrentAnimatorStateInfo(2);
        Spineinfo = anim.GetCurrentAnimatorStateInfo(3);
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {

    }

    [SerializeField] Transform LookAtTransform;

    public Transform right;
    public Transform left;



    private void OnAnimatorIK(int layerIndex)
    {
        if (Time.timeScale != 0)
        {
            float weight = 1f;
            //transform.root.GetComponentInChildren<GunReloadMaker>().OnAnimatorIKGunReload();
        }
    }

    // Update is called once per frame
    public void CharacterUpdater(bool crounch, bool isGround)
    {
        MathPlayerMove();
        //m_forward is player forward velocity
        AnimatorUpdater(m_forward, m_jump, m_right, crounch, isGround);
    }

    public void MathPlayerMove()
    {
        // Get difference of Player position.
        float move_x = (PlayerTransform.transform.position.x - prepos_x);
        float move_y = (PlayerTransform.transform.position.y - prepos_y);
        float move_z = (PlayerTransform.transform.position.z - prepos_z);
        Vector3 player_move = new Vector3(move_x, move_y, move_z);

        float dir = Vector3.SignedAngle(
            new Vector3(move_x, 0f, move_z).normalized,
            new Vector3(CameraTransform.forward.x, 0f, CameraTransform.forward.z).normalized, Vector3.up);

        float str = Mathf.Sqrt(move_x * move_x + move_z * move_z);
        dir = dir * Mathf.PI / 180f;

        m_right = str * Mathf.Cos(dir) / Time.deltaTime;
        m_forward = -str * Mathf.Sin(dir) / Time.deltaTime;
        m_jump = player_move.y / Time.deltaTime;

        dirman.text = dir.ToString("N2") + "," + m_right.ToString("N2") + "," + m_forward.ToString("N2");
        Debug.Log("m_forward" + m_forward + ", m_right" + m_right);
        Debug.Log("dir" + dir);

        prepos_x = PlayerTransform.transform.position.x;
        prepos_y = PlayerTransform.transform.position.y;
        prepos_z = PlayerTransform.transform.position.z;

        /*
        float a = (Quaternion.LookRotation(new Vector3(m_forward, 0f, m_right)).eulerAngles.y);
        if(a < 0) { a += 360f; }
        a /= 360f;
        anim.Play(info.shortNameHash, 2, a) ;
        anim.Play(Spineinfo.shortNameHash, 3, Quaternion.LookRotation(CameraTransform.forward).eulerAngles.y / 360f);
        anim.Play(Spineinfo.shortNameHash, 4, Quaternion.LookRotation(CameraTransform.forward).eulerAngles.x / (180f - 40f));
        Debug.Log("player_move:" + Quaternion.LookRotation(CameraTransform.forward).eulerAngles.y / 360f);
        */
    }

    public void AnimatorUpdater(float x, float y, float z, bool crounch, bool isGround)
    {
        float dir = Vector3.SignedAngle(
           new Vector3(x, 0, z).normalized,
           new Vector3(CameraTransform.transform.forward.x, 0f, CameraTransform.transform.forward.z).normalized, Vector3.up);
        m_Animator.SetFloat("Forward", Mathf.Abs(m_forward) + Mathf.Abs(m_right), 0.1f, Time.deltaTime);
        if(m_forward < 0)
        {
            m_Animator.SetFloat("Forward", -(Mathf.Abs(m_forward) + Mathf.Abs(m_right)), 0.1f, Time.deltaTime);
        }
        m_Animator.SetFloat("Turn",
            Vector3.SignedAngle(this.transform.eulerAngles, this.transform.root.transform.eulerAngles, this.transform.up) / -180f, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", crounch);
        m_Animator.SetBool("OnGround", isGround);
        if (!isGround)
        {
            m_Animator.SetFloat("Jump", y);
            anim.SetLayerWeight(2, 0f);
        }
        else
        {
            anim.SetLayerWeight(2, 1f);
        }
        AnimatorClipInfo[] clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
        string clipName = clipInfo[0].clip.name;

    }
}
