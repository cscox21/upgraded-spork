using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackNForth : MonoBehaviour {

    public GameObject movingFirePos;

    public float moveSpeed;

    private Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    // Use this for initialization
    void Start()
    {
        currentPoint = points[pointSelection];
    }

    // Update is called once per frame
    void Update()
    {
        movingFirePos.transform.position = Vector3.MoveTowards(movingFirePos.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

        if (movingFirePos.transform.position == currentPoint.position)
        {
            pointSelection++;

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }
    }
}
