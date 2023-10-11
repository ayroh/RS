using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy {

    [SerializeField] private float arrowSpeed = 2f;
    [SerializeField] private Vector2 arrowDirection;
    [SerializeField] private Transform arrowsParent;


    [SerializeField] private List<Arrow> arrows = new List<Arrow>();
    private Queue<Arrow> arrowQueue = new Queue<Arrow>();

    private new void Start() {
        base.Start();
        for (int i = 0;i < arrows.Count;i++)
            arrowQueue.Enqueue(arrows[i]);
    }

    public void Hit() {

        Arrow arrow = arrowQueue.Dequeue();
        if (arrow == null)
            return;
        arrow.transform.position = attackPoint.position;
        arrow.gameObject.SetActive(true);
        arrow.SetVelocity(arrowDirection.normalized * arrowSpeed);
    }

    public override void ResetElement() {
        base.ResetElement();
        arrowQueue.Clear();
        for (int i = 0;i < arrows.Count;i++) {
            arrows[i].transform.SetParent(arrowsParent);
            arrows[i].ResetElement();
            arrowQueue.Enqueue(arrows[i]);
        }
    }

}
