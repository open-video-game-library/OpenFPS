using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReloadMaker : MonoBehaviour
{
    public Shot shot;
    Animator animator;
    bool isreloading;
    public AudioClip audioclip;
    AudioSource audiosource;
    Animator RootAnimator;
    [SerializeField] Transform this_Gun_Magazine;
    float weight;
    [SerializeField] Transform RightHand_Target;
    [SerializeField] Transform LeftHand_Target;
    // Start is called before the first frame update
    void Start()
    {
        //shotPoint = this.transform.GetComponentInChildren<ShotPoint>();
        animator = GetComponent<Animator>();
        animator.SetLayerWeight(1, 0f);
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.root.GetComponent<Animator>() == true)
        {
            animator.SetFloat("Forward",this.transform.root.GetComponent<Animator>().GetFloat("Forward"));
        }
    }

    public void OnAnimatorIKGunReload()
    {
        if (animator.GetBool("Reloading") == true)
        {
            if (this_Gun_Magazine == null)
            {

            }
            else
            {
                if(weight < 1f)
                {
                    weight += Time.deltaTime;
                }
                else
                {
                    weight = 1f;
                }
            }
        }
        else
        {
            if (weight > 0f)
            {
                weight -= Time.deltaTime;
            }
            else
            {
                weight = 0f;
            }
        }
        Animator ani = transform.root.GetComponentInChildren<ReloadScript>().GetComponent<Animator>();
        if (ani != null)
        {
            /*
            ani.SetIKPosition(AvatarIKGoal.RightHand, RightHand_Target.transform.position);
            ani.SetIKRotation(AvatarIKGoal.RightHand, RightHand_Target.transform.rotation);
            ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            ani.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

            ani.SetIKPosition(AvatarIKGoal.LeftHand, LeftHand_Target.transform.position * (1f - weight) + this_Gun_Magazine.position * weight);
            ani.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(LeftHand_Target.transform.rotation.eulerAngles * (1f - weight) + this_Gun_Magazine.rotation.eulerAngles * weight));
            ani.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            ani.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            */
        }
    }

    void ReloadComplete()
    {
        shot.reload();
        audiosource.PlayOneShot(audioclip);
    }
}
