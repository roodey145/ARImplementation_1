using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MinerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private string _animationTriggerName;
    [SerializeField] private float _animationDelayInSeconds = 10; // Play the animation every 10 sec
    private Vector3 _startPosition; // In case the animation is not in place.

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _animator = GetComponent<Animator>();
        StartCoroutine(_Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator _Animate()
    {
        transform.position = _startPosition;

        //print(_animationTriggerName);

        _animator.SetTrigger(_animationTriggerName);

        yield return new WaitForSeconds(_animationDelayInSeconds);

        StartCoroutine(_Animate());
    }
}
