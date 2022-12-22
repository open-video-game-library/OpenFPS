using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunChangeValueUpdater : MonoBehaviour
{
    //this script for Shot to Setting Gun to set IK;
    [Header("Should be changed IK")]
    /// <summary>
    /// Target RightHandIK
    /// </summary>
    [SerializeField] TwoBoneIKConstraint twoBoneIKRight;

    /// <summary>
    /// Target  LeftHandIK
    /// </summary>
    [SerializeField] TwoBoneIKConstraint twoBoneIKLeft;

    [Header("Transform of above IK's target")]
    /// <summary>
    /// RightHandTarget
    /// </summary>
    [SerializeField] Transform rightHandObj;
    /// <summary>
    /// LeftHandTarget
    /// </summary>
    [SerializeField] Transform leftHandObj;

    [Header("This Gun Animator")]
    public Animator this_ShotPointAnimator;

    [Header("This Guns Sight Changer")]
    /// <summary>
    /// attach object named "sight"
    /// </summary>
    [SerializeField] Transform GunSight;
    /// <summary>
    /// attach object named guns true name
    /// </summary>
    [SerializeField] Transform Gun;

    public bool isIK;

    public void GunIKUpdate()
    {
        if(twoBoneIKRight.data.target != null)
        {
            twoBoneIKRight.data.target = rightHandObj;
            twoBoneIKLeft.data.target = leftHandObj;
            this.transform.root.GetComponent<RigBuilder>().Build();
            isIK = true;
        }
        PositionUpdate();
    }

    public void PositionUpdate()
    {
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            Gun.transform.localPosition = -GunSight.localPosition;
    }

    public void DropDownWeapon()
    {
        
        this.transform.parent = null;
        Debug.Log("Drop : " + this.transform.root.name);
        RaycastHit hit;
        int layerMask = 1 << 0;

        MeshRenderer[] meshrenderers = this.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer a in meshrenderers)
        {
            a.gameObject.layer = 0;
        }
        BoxCollider[] a_colliders = this.GetComponentsInChildren<BoxCollider>(); ;
        foreach (BoxCollider b in a_colliders)
        {
            b.enabled = true;
            if (b.GetComponent<Rigidbody>())
            {
                b.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        /*
        if (Physics.Raycast(a.transform.position, -new Vector3(0, -10f, 0), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(a.transform.position, -new Vector3(0, -10f, 0), new Color(1f, 1f, 1f, 1f));
            this.transform.position = hit.transform.position;
        }
        */
    }
    
}
