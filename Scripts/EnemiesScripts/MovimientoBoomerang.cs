using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovimientoBoomerang : MonoBehaviour {

    // Use this for initialization
    public float TimeToActiveBoxCollider;
    public Vector3 origen;
    public GameObject KoopaBoomerang;
    public bool primero;
    public float TimeToDelete;

    double distance(Vector3 dist1, Vector3 dist2)
    {
        return Math.Sqrt((dist1.x - dist2.x) * (dist1.x - dist2.x) + (dist1.z - dist2.z) * (dist1.z - dist2.z));
    }

    void Start () {
        TimeToActiveBoxCollider = 0.3f;
        primero = true;
        TimeToDelete = 10.0f;
    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(KoopaBoomerang.transform.up * Time.deltaTime * 300, Space.World);
        TimeToActiveBoxCollider -= Time.deltaTime;
        TimeToDelete -= Time.deltaTime;
        if (TimeToDelete < 0) DestroyObject(gameObject);
        if (TimeToActiveBoxCollider < 0)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        double dist = distance(origen, transform.position);
        if (dist > 20) primero = false;
        if (!primero) transform.Translate(-KoopaBoomerang.transform.forward * 20 * Time.deltaTime, Space.World);
        else if (primero) transform.Translate(KoopaBoomerang.transform.forward*20*Time.deltaTime, Space.World);
    }

    public void OnCollisionEnter(Collision collision)
    {
    }
}
