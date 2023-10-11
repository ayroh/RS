using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : MapElement
{
    // Variables
    [SerializeField] protected float attackStartingDistance = 3f;
    [SerializeField] protected float secondsBetweenAttacks = 1f;

    // References
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Collider2D col;
    protected Transform playerTransform;

    // ENUM
    public enum EnemyState { Idle, Attack }
    protected EnemyState enemyState = EnemyState.Idle;


    public void Start() {
        playerTransform = GameManager.instance.playerTransform;
    }

    public void Die() {
        animator.SetTrigger("Die");
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Die();
        }
    }

    protected IEnumerator AttackCoroutine() {
        enemyState = EnemyState.Attack;
        while(enemyState == EnemyState.Attack) {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(secondsBetweenAttacks);
        }
    }


    private void Update() {
        if (enemyState == EnemyState.Idle &&
            transform.position.x - playerTransform.position.x < attackStartingDistance)
            StartCoroutine(AttackCoroutine());
    }

    public override void ResetElement() {
        col.enabled = true;
        enemyState = EnemyState.Idle;
        animator.Rebind();
        animator.Update(0f);
    }
}
