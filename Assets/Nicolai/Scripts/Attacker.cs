using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attacker : Stats
{


    private Animator _animator;
    float deathAnimationDuration = 0;
    public bool isDead = false;
    public string deathAnimationClipName = "Death";

    

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _attackSound;

    [Header("Targets")]
    public GameObject currentTarget;
    public List<GameObject> targetList = new List<GameObject>();

    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    protected void Update()
    {
        // Ensure the attacker does not attack when its health is equal or below zero
        if (health <= 0) return;

    }


    #region Trigger enter/exit detecting targets
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<Attacker>().CanTakeDamage(damageType))
        {

            targetList.Add(other.gameObject);
        }

    }
    
    protected void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider has the tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Remove the game object from the target list
            targetList.Remove(other.gameObject);
        }
    }
    #endregion



    protected void attack(int damages)
    {
        timer += Time.deltaTime;
        if (timer >= attackSpeed)
        {
            currentTarget.GetComponent<Attacker>().takeDamages(damages);
            timer = 0;
            //get the heath from enemy

            // TODO: Play audio

        }
    }

    protected void takeDamages(int damages)
    {
        if (isDead) return;
      // add defence
        health -= damages;
        if (health <= 0)
        {
            isDead = true;
            _animator.SetTrigger("DeadTrigger");
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name.ToLower() == deathAnimationClipName.ToLower())
                {
                    deathAnimationDuration = clip.length / _animator.speed;
                }
            }
            Destroy(this.gameObject, deathAnimationDuration);
            

        }
    }

    internal bool CanTakeDamage(DamageType damageType)
    {
        // Check if the character can take damage based on its character type and the damage type.
        switch (characterType)
        {
            case CharacterType.GroundAlly:
                return (damageType == DamageType.GroundAttack || damageType == DamageType.Both);
            case CharacterType.AerialAlly:
                return (damageType == DamageType.RangedAttack || damageType == DamageType.Both);
            case CharacterType.GroundEnemy:
                return (damageType == DamageType.GroundAttack || damageType == DamageType.Both);
            case CharacterType.AerialEnemy:
                return (damageType == DamageType.RangedAttack || damageType == DamageType.Both);
            default:
                return true; // Allow all other combinations by default
        }
    }

    void OnDrawGizmosSelected()
    {
        // Ensure the radius is not negative
        range = Mathf.Max(0, range);

        // Draw a wireframe sphere to represent the radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
