using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrellaController : MonoBehaviour {

    // Use this for initialization
    public float timeToDesapair;
    public bool start;
	void Start () {
        timeToDesapair = 1.0f;

    }
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(Vector3.up * Time.deltaTime*32, Space.Self);
        if (start)
        {
            timeToDesapair -= Time.deltaTime;
            Vector3 aux = transform.localScale;
            transform.localScale = aux*0.8f;
            if (timeToDesapair < 0)
            {
                gameObject.transform.parent.GetComponent<ParticleSystem>().enableEmission = false;
                DestroyObject(gameObject);

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        start = true;
        //gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
