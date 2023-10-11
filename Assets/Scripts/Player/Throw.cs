using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Transform kunaiParent;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwSpeed;

    [SerializeField] private List<Kunai> kunais = new List<Kunai>();
    private Queue<Kunai> kunaiQueue = new Queue<Kunai>();


    private void Start() {
        //for (int i = 0;i < kunais.Count;i++)
        //    kunaiQueue.Enqueue(kunais[i]);
    }


    private void Update() {
        CheckThrow();
    }
    private void CheckThrow() {
        if (Input.GetKeyDown(KeyCode.Space) && kunaiQueue.Count > 0) {
            ThrowKunai();
        }
    }

    private void ThrowKunai() {
        Kunai kunai = kunaiQueue.Dequeue();
        kunai.transform.SetPositionAndRotation(throwPoint.position, Quaternion.identity);
        kunai.kunaistate = Kunai.KunaiState.Throwable;
        kunai.gameObject.SetActive(true);
        kunai.SetVelocity(Vector2.right * throwSpeed);
    }


    private Kunai GetAvailableKunai() {
        for(int i = 0;i < kunais.Count;++i) {
            if (!kunais[i].gameObject.activeSelf)
                return kunais[i];
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Kunai") && collision.GetComponent<Kunai>().kunaistate == Kunai.KunaiState.Collectible) {
            collision.gameObject.SetActive(false);
            kunaiQueue.Enqueue(GetAvailableKunai());
        }
    }
}
