using UnityEngine;
using System.Collections;

public class LaserHandler : MonoBehaviour {
    float speed = 16.0f;
    float aliveTimer = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        aliveTimer -= Time.deltaTime;

        if (aliveTimer > 0.0f)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
}
