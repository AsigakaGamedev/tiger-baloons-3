using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthController health;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource source;

    public LayerMask Enemies;
    public float Radius;
    public float Damage;
    public float AttackDelay;
    public NavMeshAgent agent;
    public Animator animator;

    private bool canAttack;
    public Animal animal;

    public HealthController Health { get => health; }

    private void Start()
    {
        canAttack = true;

        health.onDamage += OnDamage;
        health.onDie += OnDie;
    }

    private void OnDestroy()
    {
        health.onDamage -= OnDamage;
        health.onDie -= OnDie;
    }

    private void OnDamage(float damage)
    {

    }

    private void Update()
    {
        if (!animal)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, Enemies);

            if (colliders.Length > 0)
            {
                animal = colliders[0].GetComponent<Animal>();
            }
            animator.SetBool("move", false);
        }
        else
        {
            animator.SetBool("move", true);
            agent.SetDestination(animal.transform.position);

            if (Vector3.Distance(transform.position, animal.transform.position) <= 3.5f && canAttack)
            {
                source.clip = audioClip;
                source.Play();
                animal.Health.Damage(Damage);
                animator.SetTrigger("attack");
                canAttack = false;
                Invoke(nameof(ResetCanAttack), AttackDelay);
            }
        }
    }

    private void ResetCanAttack()
    {
        canAttack = true;
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }
}
