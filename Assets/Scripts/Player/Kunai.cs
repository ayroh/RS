using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MapElement
{
    public enum KunaiState { Collectible, Throwable}
    public KunaiState kunaistate = KunaiState.Collectible;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private int groundLayer;

    private void Start() {
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    public void SetVelocity(Vector3 vel) => rb.velocity = vel;

    public override void ResetElement() {
        col.enabled = true;
        kunaistate = KunaiState.Collectible;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<Enemy>().Die();
            ResetElement();
        }
        else if (collision.gameObject.layer == groundLayer) {
            ResetElement();
        }
    }
}
