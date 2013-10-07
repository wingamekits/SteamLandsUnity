using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.3f;
    private Vector2 velocity;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float x = Mathf.SmoothDamp(this.transform.position.x, target.position.x, ref velocity.x, smoothTime);
        float y = -0.5f;

        this.transform.position = new Vector3(x, y, this.transform.position.z);
	}
}
