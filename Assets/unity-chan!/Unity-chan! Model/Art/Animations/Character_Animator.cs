using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Character_Animator : MonoBehaviour
{
    public float m_forward;
    public float m_right;
    public float m_jump;

    public float m_cycle;
    Animator m_animator;

    float prepos_x;
    float prepos_y;
    float prepos_z;

    public float dash_level;
    // Start is called before the first frame update
    void Start()
    {
        prepos_x = 0f;
        prepos_y = 0f;
        prepos_z = 0f;
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
