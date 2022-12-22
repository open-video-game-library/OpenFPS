using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class HandIK : MonoBehaviour
{


    protected Animator animator;

    public bool ikActive = false;
    public bool lefthand_ikActive = false;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform lookObj = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // IK ���v�Z���邽�߂̃R�[���o�b�N
    void OnAnimatorIK()
    {
        if (animator)
        {

            // IK ���L���Ȃ�΁A�ʒu�Ɖ�]�𒼐ڐݒ肵�܂�
            if (ikActive)
            {

                // ���łɎw�肳��Ă���ꍇ�́A�����̃^�[�Q�b�g�ʒu��ݒ肵�܂�
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1f, 1f ,0.5f, 0.5f, 0.75f);
                    animator.SetLookAtPosition(lookObj.position);
                }
                // �w�肳��Ă���ꍇ�́A�E��̃^�[�Q�b�g�ʒu�Ɖ�]��ݒ肵�܂�
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
                
                if (leftHandObj != null && lefthand_ikActive== true)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }
            }

            //IK ���L���łȂ���΁A��Ɠ��̈ʒu�Ɖ�]�����̈ʒu�ɖ߂��܂�
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}
