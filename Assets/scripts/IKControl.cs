using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class IKControl : MonoBehaviour {
    
    protected Animator animator;
    
    Vector3 lFpos;
    Vector3 rFpos;

    Quaternion rFrot;
    Quaternion lFrot;

    float rFWeight;
    float lFWeight;

    Transform leftFoot;
    Transform rightFoot;

    void Start () 
    {
        animator = GetComponent<Animator>();

        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
    }
    
    void Update() {
        RaycastHit leftHit;
        RaycastHit rightHit;

        Vector3 lpos = leftFoot.TransformPoint(Vector3.zero);
        Vector3 rpos = rightFoot.TransformPoint(Vector3.zero);

        if(Physics.Raycast(lpos, -Vector3.up, out leftHit, 1)) {
            lFpos = leftHit.point;
            lFrot = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
        }
        
        if(Physics.Raycast(rpos, -Vector3.up, out rightHit, 1)) {
            rFpos = rightHit.point;
            rFrot = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
        }
    }
    void OnAnimatorIK()
    {
        lFWeight = animator.GetFloat("leftFoot");
        rFWeight = animator.GetFloat("rightFoot");

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,lFWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,rFWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot,lFpos);
        animator.SetIKPosition(AvatarIKGoal.RightFoot,rFpos);
        
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,lFWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,rFWeight);

        animator.SetIKRotation(AvatarIKGoal.LeftFoot,lFrot);
        animator.SetIKRotation(AvatarIKGoal.RightFoot,rFrot);
    }    
}