using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GasMover : MonoBehaviour {

    public float gasSpeed;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * gasSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.tag == "Player")
            {
                SceneManager.LoadScene("Level_01");
            }
            Destroy(other.gameObject);

        }
    }
}
