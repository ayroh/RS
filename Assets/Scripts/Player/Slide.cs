using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{

    [SerializeField] private float slideForce = 5f;

    [SerializeField] private Player player;
    [SerializeField] private AnimationClip slideClip;

    private bool willSlide = false;
    private bool isInsideEffector = false;




    private void Update() {
        CheckSlide();
    }

    private void SlidePlayer() {
        player.animator.SetTrigger("Slide");
        _ = SlideControl();
        //StartCoroutine(SlideControl());
    }

    private void CheckSlide() {
        if (Input.GetKeyDown(KeyCode.S)) {
            PlatformEffector2D groundEffector = null;
            player.ChangeState(Player.PlayerState.Sliding);
            if (player.IsGrounded() && !player.currentGround.TryGetComponent<PlatformEffector2D>(out groundEffector))
                SlidePlayer();
            else {
                if (groundEffector != null)
                    _ = EffectorColliderToggle();
                player.rb.velocity = new Vector2(0f, -slideForce);
                player.animator.SetTrigger("WillSlide");
                willSlide = true;
            }
        }
    }

    //private IEnumerator SlideControl() {
    //    //player.baseCol.enabled = false;
    //    //player.slideCol.enabled = true;
    //    player.ChangeCollider(true);
    //    yield return new WaitUntil(() => player.playerState != Player.PlayerState.Sliding);
    //    player.ChangeCollider(false);
    //    //player.slideCol.enabled = false;
    //    //player.baseCol.enabled = true;
    //}

    private async UniTask SlideControl() {
        await UniTask.WaitUntil(() => player.playerState != Player.PlayerState.Sliding);
    }

    public void EndSlide() => player.ChangeState(Player.PlayerState.Running);

    private async UniTask EffectorColliderToggle() {
        Collider2D currentGroundCol = player.currentGround.GetComponent<Collider2D>();
        isInsideEffector = true;
        currentGroundCol.isTrigger = true;
        await UniTask.WaitUntil(() => isInsideEffector == false);
        currentGroundCol.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == player.groundLayer) {
            if (collision.gameObject.CompareTag("Platform"))
                isInsideEffector = false;
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == player.groundLayer) {
            if (willSlide) {
                SlidePlayer();
                willSlide = false;
            }
        }

    }

}
