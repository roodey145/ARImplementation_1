using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class EnemyController : Attacker
{
    NavMeshAgent agent;
    EnemyNav enemyNav;
    SphereCollider rangeColider;
    bool startAttack = false;
    private Animator _animator;

    [Header("Death Animation Data")]
    public string deathAnimationClipName = "Death";
    float deathAnimationDuration = 0;
    float delayBeforeRemovingCorops = 1f;


    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        enemyNav = GetComponent<EnemyNav>();
        rangeColider = GetComponent<SphereCollider>();
        _animator = GetComponent<Animator>();
        rangeColider.radius = range;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        // Ensure the attacker does not attack when its health is equal or below zero
        if (health <= 0) return;

        //when we are close enugh we should attack
        if (enemyNav.target != null)
        {
            //used to know what object to damages
            currentTarget = enemyNav.target.gameObject;

            if (startAttack) {
                attack(attackDamage);
                _animator.SetTrigger("AttackTrigger");
            }
        }
        else
        {
            agent.stoppingDistance = 0;
            startAttack = false;
            _animator.SetTrigger("WalkTrigger");
        }
    }

    // avoid interception with target
    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(enemyNav.target != null)
        {
            print(other.gameObject + ", " + enemyNav.target.gameObject);
            if (other.gameObject == enemyNav.target.gameObject)
            {
                agent.stoppingDistance = Vector3.Distance(transform.position, enemyNav.target.position);
                startAttack = true;
            }
        }
        

    }



    protected override void takeDamages(int damages)
    {
        base.takeDamages(damages);

        if(isDead)
        {
            _animator.SetBool("Dead", true);
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name.ToLower() == deathAnimationClipName.ToLower())
                {
                    deathAnimationDuration = clip.length / _animator.speed;
                }
            }
            Destroy(gameObject, deathAnimationDuration + delayBeforeRemovingCorops);
        }
    }

}
