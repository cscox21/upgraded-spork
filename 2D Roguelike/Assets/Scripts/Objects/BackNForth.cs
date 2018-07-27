using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackNForth : MonoBehaviour {

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform transformA;
    [SerializeField]
    private Transform transformB;

    private void Start()
    {
        posA = transformA.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transformA.localPosition = Vector3.MoveTowards(transformA.localPosition, nextPos, speed * Time.deltaTime);
        if(Vector3.Distance(transformA.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB;
    }
}
