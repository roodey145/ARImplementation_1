using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ProgressAnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private string _goldProgressAnimationName;
    private float _animationDuration;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = 0;
        _AssignAnimationDuration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAnimationProgress(float progress)
    {
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        _animator.Play(_goldProgressAnimationName, 0, progress * _animationDuration);
    }

    private void _AssignAnimationDuration()
    {
        _animationDuration = AnimatorUtility.GetAnimationClip(_animator, _goldProgressAnimationName).length;
    }
}
