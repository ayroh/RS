using Cysharp.Threading.Tasks;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState { Running, Sliding, Air}
    public PlayerState playerState = PlayerState.Running;
    

    [Header("Variables")]
    [SerializeField] private float playerSpeed = 5f;

    [Header("References")]
    public Animator animator;
    public BoxCollider2D baseCol;
    public BoxCollider2D slideCol;

    public Rigidbody2D rb;

    private Camera cam;

    private float colLength;
    private float cameraHalfLength;

    private Collider2D[] boxcastHit = new Collider2D[1];

    [HideInInspector] public GameObject currentGround;
    [HideInInspector] public LayerMask groundLayerMask;
    [HideInInspector] public int groundLayer;
    [HideInInspector] public bool changingCollider = false;




    private void Start() {
        cam = Camera.main;
        //GameManager.instance.playerTransform = transform;
        colLength = baseCol.size.x / 2;
        cameraHalfLength = cam.orthographicSize * Screen.width / Screen.height;

        groundLayer =  LayerMask.NameToLayer("Ground");
        groundLayerMask = LayerMask.GetMask("Ground");

        animator.SetBool("isRunning", true);
    }

    private void Update() {
        CheckLeft();
        CheckRight();
        
    }

    private void FixedUpdate() {
        transform.position = new Vector3(transform.position.x + GameManager.gameSpeed, transform.position.y, transform.position.z);
    }

    public void Die() {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    #region Movement


    public bool IsGrounded() {

        if (Physics2D.OverlapBoxNonAlloc(baseCol.bounds.center - new Vector3(0f, 0.1f + (baseCol.size.y / 2), 0f), new Vector2(baseCol.size.x / 2, .09f), 0, boxcastHit, groundLayerMask) > 0) {
            currentGround = boxcastHit[0].gameObject;
            return true;
        }
        else {
            currentGround = null;
            return false;
        }
    }

    private void Land() {
        animator.SetBool("isRunning", true);
        ChangeState(PlayerState.Running);
    }

    #endregion

    #region Movement Checks
    private void CheckLeft() {
        if (Input.GetKey(KeyCode.A) && transform.position.x - colLength > cam.transform.position.x - cameraHalfLength) {
            transform.position = new Vector3(transform.position.x - playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            //newPlayerPosition.x -= playerSpeed;
        }
    }
    private void CheckRight() {
        if (Input.GetKey(KeyCode.D) && transform.position.x + colLength < cam.transform.position.x + cameraHalfLength) {
            transform.position = new Vector3(transform.position.x + playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            //newPlayerPosition.x += playerSpeed;
        }
    }

    #endregion


    public void ChangeState(PlayerState newState) {
        switch (playerState) {
            case PlayerState.Running:
                break;
            case PlayerState.Sliding:
                baseCol.size = new Vector2(1.9f, 2.6f);
                baseCol.offset = new Vector2(0, -1);
                break;
            case PlayerState.Air:
                break;
        }

        playerState = newState;

        switch (newState) {
            case PlayerState.Running:
                break;
            case PlayerState.Sliding:
                baseCol.size = new Vector2(2.427527f, 1.311852f);
                baseCol.offset = new Vector2(0.2637633f, -1.644074f);
                break;
            case PlayerState.Air:
                break;
        }
    }


    public void ChangeCollider(bool toSlide) {
        

    }



    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == groundLayer) {
            if(playerState != PlayerState.Sliding)
                Land();
            currentGround = collision.gameObject;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            if(playerState != PlayerState.Sliding) {
                animator.SetTrigger("Attack");
            }
            collision.GetComponent<Enemy>().Die();
        }
    }


    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer == groundLayer) {
            animator.SetTrigger("Jump");
            ChangeState(PlayerState.Air);
            animator.SetBool("isRunning", false);
            currentGround = null;
        }
    }



}
