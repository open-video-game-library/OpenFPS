//PlayerController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioClip audioclip;
    
    float sita;
    public GameObject Player;
    public GameObject Camera;
    public PhysicMaterial pm;
    
    private Transform PlayerTransform;
    private Transform CameraTransform;
    bool a = false;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;

    public static float speed = 50f;
    public static float jumpHeight = 0.13f;
    public static float DashSpeed = 2f;
    public static float bairitsu = 1f ;
    public static int hp;
    public static float Sens = 1f;
    public static float Aim_Sens = 1f;
    public static float FieldOfView = 60f;
    public static float HipShotTime = 1.5f;

    float shit = 0.4f;
    public bool dash = false;
    Rigidbody rb;
    float angleDir;
    public bool jumpAble;
    public float power;
    float defy;
    public bool smooth;
    bool shitting;
    public bool AutoAim;
    public bool AutoAim_burrel;
    public Vector3 AutoAim_transform;
    bool crouch;
    
    public Animator m_Animator;
    float m_OrigGroundCheckDistance;
    public Collider rightReg;
    public Collider leftReg;
    public Vector3 inputVelocity;
    public bool climb;

    //public float HP;
    private RaycastHit hit;

    


    public Wave wave;
    public Slider hpSlider;
    public Text hpvalue;

    public int maxhp;

    public Rigidbody[] ragdollRigidbodies;
    Collider[] ragColliderbodies;

    [SerializeField] GameObject head;

    [SerializeField] Image ImageUpdate;
    void SetRagdoll(bool isEnabled)
    {
        m_Animator.enabled = false;

        foreach (Rigidbody rigidbody in ragdollRigidbodies)
        {
            rigidbody.useGravity = true;
            rigidbody.velocity = new Vector3(0, 2f, 0);
            rigidbody.isKinematic = !isEnabled;
        }
        foreach (Collider rigidbody in ragColliderbodies)
        {
            rigidbody.isTrigger = false;
        }
        this.transform.root.transform.parent = null;
        transform.root.transform.tag = "Enemy";
    }

    // Use this for initialization
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        PlayerTransform = transform.parent;
        CameraTransform = GetComponent<Transform>();

        angleDir = 180f;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;

        hp = preHP;
        maxhp = hp;
        if(hpvalue != null)
        {
            hpvalue.text = hp.ToString();
            hpSlider.value = hp;
            image.color = new Color(0, 0, 0, 0);
        }
        m_Animator = this.transform.root.GetComponent<Animator>();
    }
    Animator MainCamera;
    private float ii;
    float k;
    public int preHP = 5;
    public Image image;
    public SpriteRenderer DamageEffect;

    bool ikkai;
    float preY;

    public Transform DeleteHead;

    [SerializeField] RePlayObjectCollecter rePlayObjectCollecter;

    void LateStart()
    {
        preY = CameraTransform.transform.eulerAngles.x;
        CameraTransform.transform.eulerAngles = new Vector3(0f, CameraTransform.transform.eulerAngles.y, CameraTransform.transform.eulerAngles.z);
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 || rePlayObjectCollecter.once_Count != 0)
        {
            return;
        }

        if(hp <= 0 && ikkai == false)
        {
            GunChangeValueUpdater[] gunsearcher = GetComponentsInChildren<GunChangeValueUpdater>();
            foreach(GunChangeValueUpdater a in gunsearcher)
            {
                a.DropDownWeapon();
            }
        }
        if (Input.GetKey(KeyCode.Return))
        {

        }
        if(preHP != hp)
        {
            this.transform.root.GetComponent<AudioSource>().PlayOneShot(audioclip);
            if (hpvalue != null)
            {
                image.color = new Color(1, 1, 1, 1);
                //image.DOFade(endValue: 0f, duration: 1f);

                hpvalue.text = hp.ToString();
                hpSlider.value = (float)hp/ (float)maxhp;
            }
            //DamageEffect.color = new Color(1, 1, 1, 1);
        }
        preHP = hp;
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        if (0 != Input.GetAxis("Horizontal2"))
        {
            X_Rotation = Input.GetAxis("Horizontal2");
        }    
        if (0 != Input.GetAxis("Vertical2"))
        {
            Y_Rotation = Input.GetAxis("Vertical2");
        }
        

        X_Rotation *= Sens;
        Y_Rotation *= Sens;
        if (Input.GetMouseButton(1))
        {
            X_Rotation *= Aim_Sens;
            Y_Rotation *= Aim_Sens;
        }

        //ÉIÅ[ÉgÉGÉCÉÄÇÃé¿ëï
        if (AutoAim == true && AutoAim_burrel==false)
        {
            

            //CameraTransform.transform.eulerAngles = look.eulerAngles;
        }
        
        else
        {
            PlayerTransform.transform.Rotate(0, X_Rotation, 0);

            //ii< 70Ç©Ç¬ii > 290
            ii = CameraTransform.transform.localEulerAngles.x - Y_Rotation;
            if (ii < 290 && ii > 70f)
            {
                
            }
            else
            {
                CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
            }
        }

        Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));
        Vector3 dir2 = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir));
        //dash = false;
        a = false;
        //if (this.transform.root.GetComponent<Animator>().GetBool("setup") == true) bairitsu = ADSSpeed; //ç\Ç¶ÇΩÇÁî{ó¶ï™íxÇ≠Ç∑ÇÈ

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Dash"))
        {
            if(Input.GetMouseButton(1) == false && Input.GetMouseButton(0) == false)
            {
                bairitsu = DashSpeed;//dash
                shitting = false;
                m_Crouching = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetButton("Crouch") || Input.GetKey(KeyCode.C))
        {
            shitting = true;
            bairitsu = 0.5f;
            m_Crouching = true;

        }
        else
        {
            shitting = false;
            m_Crouching = false;
        }
        float slope = GetSlope();

        inputVelocity = new Vector3(0, 0, 0);

        int layerMask = 7;
        layerMask = ~layerMask;
        //Debug.Log(this.transform.root.name);
        Debug.DrawRay(this.transform.root.transform.position, Vector3.down, Color.red, 0.2f, false);

        if (Physics.Raycast(this.transform.root.transform.position, Vector3.down,
              out hit, jumpHeight))
        {
            jumpAble = true;
        }
        else
        {
            jumpAble = false;
        }

        Debug.Log("Input.GetAxis(Horizontal)" + Input.GetAxis("Horizontal"));
        Debug.Log("Input.GetAxis(Vertical)" + Input.GetAxis("Vertical"));

        if (Input.GetAxis("Vertical")<0)
        {
            inputVelocity += Player.transform.forward;
        }

        else if (Input.GetKey(KeyCode.W) )
        {
            
            if (smooth == true && jumpAble == true)
            {
                inputVelocity += Player.transform.forward;
            }
            else if (smooth == true && jumpAble == false)
            {
                inputVelocity += Player.transform.forward * 1f;

            }
            //PlayerTransform.transform.position += dir1 * speed* bairitsu * Time.deltaTime;
            a = true;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            inputVelocity -= Player.transform.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {

            if (smooth == true && jumpAble == true)
            {
                inputVelocity -= Player.transform.right * 0.85f;
            }
            else if (smooth == true && jumpAble == false)
            {
                inputVelocity -= Player.transform.right * 0.85f;

            }
            a = true;
            //PlayerTransform.transform.position += dir2 * speed * bairitsu * Time.deltaTime;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            inputVelocity += Player.transform.right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (smooth == true && jumpAble == true)
            {
                inputVelocity += Player.transform.right * 0.85f;
            }
            else if (smooth == true && jumpAble == false)
            {
                inputVelocity += Player.transform.right * 0.85f;

            }
            a = true;
            //PlayerTransform.transform.position += -dir2 * speed * bairitsu * Time.deltaTime;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            inputVelocity -= Player.transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (smooth == true && jumpAble == true)
            {
                inputVelocity -= Player.transform.forward * 0.5f;
            }
            else if (smooth == true && jumpAble == false)
            {
                inputVelocity -= Player.transform.forward * 0.5f;

            }
            a = true;
        }
        //inputVelocity.Normalize(); //inputVelocity is normalized in this prossess;
        float defaultSpeed = 3f;

        if (inputVelocity == new Vector3(0f, 0f, 0f)) //Player Completely stopped in this prosess;
        {
            //Debug.Log("TTest");
            pm.dynamicFriction = 4f;
        }
        else
        {
            pm.dynamicFriction = 0.2f;
        }

        float mas = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z); //Horizontal Speed

        if (Mathf.Abs(rb.velocity.y) > 0f)
        {
            shitting = false;
        }

        if (bairitsu >= DashSpeed) //ëñÇ¡ÇƒÇ¢ÇÈ
        {
            if (mas > 1f / 1.7f)
            {
                rb.AddForce(inputVelocity * bairitsu * Time.deltaTime * defaultSpeed, ForceMode.Impulse);
                if (mas > DashSpeed)
                {
                    rb.velocity = new Vector3(inputVelocity.x * DashSpeed * defaultSpeed, rb.velocity.y, inputVelocity.z * DashSpeed * defaultSpeed);

                }
                dash = true;
            }
            else
            {
                rb.velocity = new Vector3(inputVelocity.x * defaultSpeed, rb.velocity.y, inputVelocity.z * defaultSpeed);
                dash = false;
            }
            StateText.text = "ëñÇÈ";
            //Dash();

        }
        else if (bairitsu < DashSpeed) //ï‡Ç¢ÇƒÇ¢ÇÈ
        {
            if (rb.velocity.magnitude == 0)
            {
                StateText.text = "óßÇøé~Ç‹ÇÈ";
            }
            StateText.text = "ï‡Ç≠";
            rb.velocity = new Vector3(inputVelocity.x * defaultSpeed, rb.velocity.y, inputVelocity.z * defaultSpeed);
            //ADS();
            dash = false;
        }
        else
        {
            dash = false;
        }

        if (jumpAble == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Jump"))
            {
                rb.velocity += new Vector3(0, power, 0);
            }
        }


        if (shitting == true)
        {
            rb.velocity = new Vector3(inputVelocity.x * 0.7f, rb.velocity.y, inputVelocity.z * 0.7f);
            m_Crouching = true;
        }

        Vector3 move = rb.velocity;
        //if (move.magnitude > 1f) move.Normalize();
        //move = transform.InverseTransformDirection(move);
        //m_ForwardAmount = move.z;
        //m_TurnAmount = Mathf.Atan2(move.x, move.z);
        
        m_IsGrounded = jumpAble;
        crouch = false;

        UpdateAnimator(move);

        bairitsu = 1f;
        //pm.dynamicFriction = 0.7f;

            AnimatorClipInfo[] clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
            string clipName = clipInfo[0].clip.name;

    }
    public Text StateText;
    

    void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
    }

    float GetSlope()
    {
        int layerMask = 7;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, Vector3.down,
              out RaycastHit hit, 1.2f, layerMask))
        {
            return Vector3.Angle(Vector3.up, hit.normal);
            //Moving(hit.normal);
        }

        return -1f;
    }


    private void Moving(Vector3 a)
    {
        Player.transform.root.transform.position = a 
            +new Vector3(0, 0.15f,0);
        //rb.velocity = 
    }
    void HandleGroundedMovement(bool crouch, bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            // jump!
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
            m_IsGrounded = false;
            m_Animator.applyRootMotion = false;
            m_GroundCheckDistance = 0.1f;
        }
    }

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_JumpPower = 12f;
    [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
    [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;

    Rigidbody m_Rigidbody;
    //bool m_IsGrounded;
    //float m_OrigGroundCheckDistance;
    const float k_Half = 0.5f;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    float m_CapsuleHeight;
    Vector3 m_CapsuleCenter;
    CapsuleCollider m_Capsule;
    bool m_Crouching;
    bool m_IsGrounded;
    public float runCycle;

    public Animator GunManagerAnimator;

    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        GunManagerAnimator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        GunManagerAnimator.SetBool("Dash",dash);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            //m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        runCycle =
            Mathf.Repeat(
                m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        if (m_IsGrounded)
        {
            m_Animator.SetFloat("JumpLeg", jumpLeg);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
            
            
            //dash = true;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
            //dash = false;
        }
    }

    public void GroundTap(bool a)
    {
        //jumpAble = a;
    }

    public void Climb(bool a)
    {
        /*
        climb = a;
        if(climb == false)
        {
            //transform.parent.position += new Vector3(0, 0.2f, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
        pm.dynamicFriction = 0f;
        */
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            m_Animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            m_Animator.applyRootMotion = false;
        }
    }

    void ADS()
    {

    }

}
