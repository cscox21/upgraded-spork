
using UnityEngine;

public class simpleBullet : MonoBehaviour
{

    public float moveSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, .4f);
    }
}

