using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcCurve : MonoBehaviour {

    public AnimationCurve curve;
    public Transform target;

    private Vector3 start;
    private Coroutine coroutine;

    private void Awake()
    {
        start = transform.position;
    }

    private void Update()
    {
        if (coroutine == null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) == true)
            {
                coroutine = StartCoroutine(Curve());
            }
        }
    }

    IEnumerator Curve()
    {
        float duration = 2f;
        float time = 0f;

        Vector3 end = target.position - (target.forward * 0.55f); // lead the target a bit to account for travel time, your math will vary

        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, 3.0f, heightT); // change 3 to however tall you want the arc to be

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }

        // impact

        coroutine = null;
    }
}

