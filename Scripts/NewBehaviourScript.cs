using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public float speed = 5.0f;
    public static Vector3 dir;
	// Use this for initialization

	void Start () {
        dir = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate((dir - transform.position) * Time.deltaTime * speed);
    }
}
