//PlayerController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Animator m_Animator;
    [SerializeField] Rigidbody m_Rigidbody;

    //Camera
    [SerializeField] Transform CameraTransform;

    [SerializeField] public static float speed = 3f;
    [SerializeField] public static float DashScale = 1.2f;
    [SerializeField] public static float CrouchScale = 0.9f;
    [SerializeField] public static float jumpSpeed = 3f;
    [SerializeField] public static float jumpPower = 0.25f;
    float jumpCount;
    public float maxSpeed = 3;

    [SerializeField] public static int hp = 5;
    [SerializeField] public static float Sens = 1f;
    [SerializeField] public static float Aim_Sens = 1f;
    [SerializeField] public static float FieldOfView = 60f;
    Rigidbody rb;

    [SerializeField] Text Debug_inputtext;
    [SerializeField] Text Debug_velocity;
    [SerializeField] Text Debug_power;
    [SerializeField] CharacterAnimaterController characterAnimaterController;
    public Wave wave;

    public int maxhp;

    public Rigidbody[] ragdollRigidbodies;
    Collider[] ragColliderbodies;
    // Use this for initialization
    void Start()
    {
        rb = PlayerTransform.GetComponent<Rigidbody>();
    }
    bool ikkai;
    float preY;

    public Transform DeleteHead;

    [SerializeField] RePlayObjectCollecter rePlayObjectCollecter;
    Vector3 PlayerCameraRotate;


    private void Awake()
    {
        hp = maxhp;
    }
    void LateStart()
    {
        speed = 10f;
        PlayerCameraRotate = new Vector3(0f, 0f, 0f);
        preY = CameraTransform.transform.eulerAngles.x;
        CameraTransform.transform.eulerAngles = PlayerCameraRotate;
    }

    void OnAnimatorIK()
    {
        m_Animator.SetLookAtWeight(1);
        m_Animator.SetLookAtPosition(new Vector3(0,0,0));
    }

    private void LateUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //When RePlaying, PlayerContoller can not load;
        if (Time.timeScale == 0)
        {
            return;
        }

        

        if (hp <= 0 && ikkai == false)
        {
            GunChangeValueUpdater[] gunsearcher = GetComponentsInChildren<GunChangeValueUpdater>();
            foreach(GunChangeValueUpdater a in gunsearcher)
            {
                a.DropDownWeapon();
            }
        }

        if (hp <= 0)
        {
            return;
        }

        MathPlayerCamera();
        Vector3 m_GroundNormal = CheckGroundStatus();

        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        if(ver == 0)
        {
            ver = Input.GetAxis("KeyBoardVertical");
        }
        if(hor == 0)
        {
            hor = Input.GetAxis("KeyBoardHorizontal");
        }

        Vector3 velocity = Vector3.zero;
        if(ver != 0f || hor != 0f)
        {
            velocity = new Vector3(CameraTransform.forward.x, 0, CameraTransform.forward.z).normalized;
        }

        Debug_inputtext.text = (velocity.ToString() + speed.ToString() + ver.ToString() + hor.ToString()).ToString();

        //velocity = velocity * ver * speed * Time.deltaTime + CameraTransform.right * hor * speed * Time.deltaTime;
        velocity = velocity * ver + CameraTransform.right * hor;
        velocity = velocity.normalized;
        velocity = new Vector3(velocity.x, 0f, velocity.z);

        //velocity = velocity.normalized - m_GroundNormal; //Math inclosed slope
        Debug.Log("rb.velocity.magnitude" + rb.velocity.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Dash")) //when player is dash;
        {
            if (Input.GetMouseButton(1) == false && Input.GetMouseButton(0) == false) //when dashing, Player can not Fire;
            {
                if(rb.velocity.magnitude < 3f)
                {
                    rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y, velocity.z * speed);
                }
                rb.AddForce(velocity * DashScale, ForceMode.Impulse);

                if (rb.velocity.magnitude > maxSpeed)
                {
                    Vector3 sp = rb.velocity.normalized;
                    rb.velocity = new Vector3(sp.x * maxSpeed * speed, rb.velocity.y, sp.z * maxSpeed * speed);
                }
            }
            
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetButton("Crouch") || Input.GetKey(KeyCode.C)) //when Plyer is chouch;
        {
            m_Crouching = true;
            ScaleCapsuleForCrouching(true);
                rb.velocity = new Vector3(velocity.x * CrouchScale * speed, rb.velocity.y, velocity.z * CrouchScale * speed);
        }
        else
        {
            Debug.Log("velocity:" + velocity + "speed" + speed);
            m_Crouching = false;
                rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y, velocity.z * speed);
            ScaleCapsuleForCrouching(false);
        }
        Debug.Log("mag" + rb.velocity.magnitude);

        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump"))
        {
            if (jumpPower > jumpCount)
            {
                rb.velocity = new Vector3(rb.velocity.x , jumpSpeed, rb.velocity.z);
            }
            jumpCount += Time.deltaTime;
        }
        else
        {
            jumpCount = jumpPower + 1f;
            if (m_IsGrounded == true)
            {
                jumpCount = 0f;
            }
        }
        characterAnimaterController.CharacterUpdater(m_Crouching, m_IsGrounded);
    }

    public float m_forward;
    public float m_right;
    public float m_jump;

    float pos_x;
    float pos_y;
    float pos_z;

    float prepos_x;
    float prepos_y;
    float prepos_z;

    Vector3 player_move;
    Vector3 animatorVector;
    

    [SerializeField] Transform TestTransform;

    public void MathPlayerCamera()
    {
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
        //PlayerTransform.transform.Rotate(0, X_Rotation, 0);
        //limit PlayerCamera sight area
        float ii = PlayerCameraRotate.x - Y_Rotation;
        if (ii > 180f)
        {
            ii = -(360 - ii);
        }
        if (70f < ii)
        {
            Debug.Log("ii" + ii); CameraTransform.transform.eulerAngles = new Vector3(70f, PlayerCameraRotate.y + X_Rotation,  0);
            PlayerCameraRotate = new Vector3(70f, PlayerCameraRotate.y + X_Rotation, 0);
        }
        else if (ii < -70f)
        {
            CameraTransform.transform.eulerAngles = new Vector3(-70f, PlayerCameraRotate.y + X_Rotation, 0);
            PlayerCameraRotate = new Vector3(-70f, PlayerCameraRotate.y + X_Rotation, 0);
        }
        else
        {
            CameraTransform.transform.eulerAngles
            = new Vector3(PlayerCameraRotate.x - Y_Rotation, PlayerCameraRotate.y + X_Rotation, 0);
            PlayerCameraRotate = new Vector3(PlayerCameraRotate.x - Y_Rotation, PlayerCameraRotate.y + X_Rotation, 0);
        }
        //
    }

    Vector3 GetSlope() //Math Player Control of when they standing on slope.
    {
        int layerMask = 7;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, Vector3.down,
              out RaycastHit hit, 1.2f, layerMask))
        {
            return hit.normal;
        }

        return new Vector3(0, 0, 0);
    }

    void ScaleCapsuleForCrouching(bool crouch) //Change Scale of Capsule Scale when Player chouching.
    {
        if (m_IsGrounded && crouch)
        {
            if (m_Crouching) return;
            m_Capsule.height = m_Capsule.height / 2f;
            m_Capsule.center = m_Capsule.center / 2f;
            m_Crouching = true;
        }
    }

    [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_GroundCheckDistance = 0.2f;
    [SerializeField] float runCycle;
    const float k_Half = 0.5f;
    float m_CapsuleHeight;
    Vector3 m_CapsuleCenter;
    CapsuleCollider m_Capsule;
    bool m_Crouching;
    bool m_IsGrounded;
    

    public Animator GunManagerAnimator;
    [SerializeField] Transform CharacterMesh;

    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters

        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        runCycle =
            Mathf.Repeat(
                m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_forward;
        if (m_IsGrounded)
        {
            m_Animator.SetFloat("JumpLeg", jumpLeg);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        m_Animator.speed = m_AnimSpeedMultiplier;
    }

    Vector3 CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(PlayerTransform.transform.position + (Vector3.up * 0.1f), PlayerTransform.transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(PlayerTransform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_IsGrounded = true;
            //m_Animator.applyRootMotion = true;            
            return hitInfo.normal;
        }
        else
        {
            m_IsGrounded = false;
            //m_Animator.applyRootMotion = false;
            return Vector3.up;
        }
    }
}
