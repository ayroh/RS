using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBandit : Enemy {

    [SerializeField] private Vector2 hitSize = new Vector2(.2f, 1f);

    private LayerMask playerLayer;
    private Collider2D[] playerCol = new Collider2D[1];

    private void Awake() {
        playerLayer = LayerMask.GetMask("Player");
    }

    public void Hit() {
        if (Physics2D.OverlapCapsuleNonAlloc(attackPoint.position, hitSize, CapsuleDirection2D.Horizontal, 0, playerCol, playerLayer) > 0) {
            playerCol[0].GetComponent<Player>().Die();
        }
    }

}
