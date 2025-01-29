using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public HealthController Health;
    public LayerMask Enemies;
    public float Radius;
    public float Damage;
    public float AttackDelay;
    public NavMeshAgent agent;
    public Animator animator;

    [Space]
    [SerializeField] private GameObject type1;
    [SerializeField] private GameObject type2;

    private bool canAttack;
    public Enemy enemy;

    private void Start()
    {
        GameManager.Instance.AddAnimals();
        canAttack = true;
        Health.onDie += OnDie;
    }

    private void OnDestroy()
    {
        Health.onDie -= OnDie;
    }

    private void OnDie()
    {
        GameManager.Instance.RemoveAnimals();
        Destroy(gameObject);
    }

    public void SetType(int type)
    {
        type1.SetActive(type == 1);
        type2.SetActive(type == 2);
        print(type == 1);
        print(type == 2);
    }

    private void Update()
    {
        if (!enemy)
        {
            animator.SetBool("move", false);
            Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, Enemies);

            if (colliders.Length > 0)
            {
                enemy = colliders[0].GetComponent<Enemy>();
            }
        }
        else
        {
            Vector3 dir = enemy.transform.position - transform.position;
            dir.Normalize();
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
            agent.SetDestination(enemy.transform.position);

            if (Vector3.Distance(transform.position, enemy.transform.position) <= 3.5f)
            {
                animator.SetBool("move", false);

                if (!canAttack) return;
                enemy.Health.Damage(Damage);
                animator.SetTrigger("attack");
                canAttack = false;
                Invoke(nameof(ResetCanAttack), AttackDelay);
            }
            else
            {
                animator.SetBool("move", true);
            }
        }
    }

    private void ResetCanAttack()
    {
        canAttack = true;
    }
}
