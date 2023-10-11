using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MapElement
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    public void SetVelocity(Vector3 vel) => rb.velocity = vel;

    private void Update() {
        float angle = Vector3.Angle(Vector3.right, rb.velocity);
        if (rb.velocity.y < 0)
            angle = -angle;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public override void ResetElement() {
        enabled = true;
        col.enabled = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            col.enabled = false;
            rb.velocity = Vector3.zero;
            transform.SetParent(collision.transform);
            collision.GetComponent<Player>().Die();
            enabled = false;
        }
    }


}
