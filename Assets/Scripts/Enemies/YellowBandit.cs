using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBandit : Enemy {

    [SerializeField] private float hitRadius = .4f;

    private LayerMask playerLayer;
    private Collider2D[] playerCol = new Collider2D[1];

    private void Awake() {
        playerLayer = LayerMask.GetMask("Player");
    }

    public void Hit() {
        show = true;
        if (Physics2D.OverlapCircleNonAlloc(attackPoint.position, hitRadius, playerCol, playerLayer) > 0) {
            playerCol[0].GetComponent<Player>().Die();
        }
    }


    bool show = false;
    private void OnDrawGizmos() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        if (show)
            Gizmos.DrawSphere(attackPoint.position, hitRadius);
        show = false;
    }

}
