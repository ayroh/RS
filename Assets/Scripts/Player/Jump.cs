using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField] private float jumpForce = 22f;

    [SerializeField] private Player player;


    private void Update() {
        CheckJump();
    }

    private void JumpPlayer() {
        player.rb.velocity = new Vector2(0f, jumpForce);
    }



    private void CheckJump() {
        if (Input.GetKeyDown(KeyCode.W) && player.IsGrounded()) {
            JumpPlayer();
        }
    }

}
