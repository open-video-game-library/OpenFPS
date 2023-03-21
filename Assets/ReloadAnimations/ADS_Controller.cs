using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS_Controller : MonoBehaviour
{
    Animator Animator;
    private AnimatorStateInfo info;
    [SerializeField] Camera camera;
    public float zoomvalue;
    public float aDSSpeed;
    public static float fieldofview = 60f;

    // Start is called before the first frame update
    void Start()
    {
        Animator = this.GetComponent<Animator>();
        info = Animator.GetCurrentAnimatorStateInfo(0);
    }

    float sec;

    public void ZOOMandADSUpdate(float i, float j)
    {
        zoomvalue = i;
        aDSSpeed = j;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("sec" + sec);
            if ((Input.GetMouseButton(1) || Input.GetButton("Aim")))
        {

                Animator.SetFloat("Speed", -1.0f);
                Animator.SetBool("setup", true);
                sec -= Time.deltaTime * (1 / aDSSpeed);

                if (sec < 0f)
                {
                    sec = 0f;
                }
            }
            else
            {
                Animator.SetFloat("Speed", 1.0f);
                //Animator.SetFloat("ADSSpeed", aDSSpeed);
                Animator.SetBool("setup", false);
                sec += Time.deltaTime * (1 / aDSSpeed);

                if (sec >= 1f)
                {
                    sec = 1f;
                }
            }
            Animator.SetFloat("sec", sec);
        float a = fieldofview - fieldofview * (1 - 1 / zoomvalue) * (1 - sec);
        if (a >= 180)
        {
            a = 180;
        }
        camera.fieldOfView = a;
    }
}
