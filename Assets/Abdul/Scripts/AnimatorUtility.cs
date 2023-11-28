using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUtility
{
    public static void AdjustAnimatorSpeed(Animator animator, float speed)
    {
        animator.speed = speed;
    }

    public static AnimationClip GetAnimationClip(Animator animator, string clipName)
    {
        AnimationClip clip = null;

        clipName = clipName.ToLower();

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach(AnimationClip animationClip in clips)
        {
            if(animationClip.name.ToLower() == clipName)
            {
                clip = animationClip;
                break;
            }
        }

        if(clip == null)
        {
            throw new NullReferenceException($"The animation clip with the name ({clipName}) was not found!");
        }

        return clip;
    }
}
