using UnityEngine;
using System.Collections;

public class GameObjectDestroyer : MonoBehaviour {
    GameObject player;
    public float destructTime = 0;
    public bool detachChildren = false;

    void Awake()
    {
        if (destructTime > 0)
        {
            Invoke("DestroyNow", destructTime);
        }
    }


    void DestroyNow()
    {
        if (detachChildren)
        {
            transform.DetachChildren();
        }
        DestroyObject(this.gameObject);
    }

	// Use this for initialization
	void Start () {
        GameObject _p = GameObject.Find("Player");
        if (_p != null)
        {
            player = _p;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (this.transform.position.x <= player.transform.position.x - 20.0f)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
