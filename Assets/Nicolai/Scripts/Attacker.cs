using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [Header("Attacking Settings")]
    public bool canAttack;

    public int health;
    public float range;
    public int damage;
    public float attackSpeed;
    public float timer;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _attackSound;

    [Header("Targets")]
    public GameObject currentTarget;
    public List<GameObject> targetList = new List<GameObject>();

    // Start is called before the first frame update
    protected void Start()
    {
        
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
        if (other.gameObject.tag == "Enemy")
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
        health -= damages;
        if (health <= 0) Destroy(gameObject);
    }

}
