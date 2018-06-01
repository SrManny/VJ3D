using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoombaController : MonoBehaviour {

    // Use this for initialization
    public bool movimiento;
    public Vector3 anterior;

    public int GirarYAvanzar = 0;
    public bool NoHaLlegado = false, starts = false;
    public static bool marioDañado = false;
    public Vector3 objetivo;
    public float timeToTransform = 0.5f;

    public AudioClip GoombaDead;

    double distance(Vector3 dist1, Vector3 dist2)
    {
        return Math.Sqrt((dist1.x - dist2.x) * (dist1.x - dist2.x) + (dist1.z - dist2.z) * (dist1.z - dist2.z));
    }
    void Start () {
        movimiento = false;
        anterior = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (starts) timeToTransform -= Time.deltaTime;
        if (timeToTransform < 0.0f)
        {
            DestroyObject(gameObject);
        }
        if (movimiento) 
        {
            movimiento = false;
            NoHaLlegado = true;
            if (GirarYAvanzar % 2 == 0) transform.Rotate(0, 90, 0);
            anterior = transform.position;
            objetivo = transform.forward * 10 + transform.position;
            ++GirarYAvanzar;
        }
        if (NoHaLlegado)
        {
            transform.Translate((objetivo.x-transform.position.x) * Time.deltaTime * 5, 0, (objetivo.z - transform.position.z) * Time.deltaTime * 5, Space.World);
            if (distance(objetivo, transform.position) < 0.1)
            {
                NoHaLlegado = false;
            }
        }
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MarioBros" && Math.Abs(collision.gameObject.transform.position.y - transform.position.y)<3.5 && !starts)
        {
            --GirarYAvanzar;
            if (GirarYAvanzar % 2 == 0) transform.Rotate(0, -90, 0);
            objetivo = anterior;
            ColisionConEnemigo.marioDañado = true;

        }
        else if (collision.gameObject.tag == "MarioBros" && collision.contacts[0].point.y >= 3)
        {
            starts = true;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,20,0), ForceMode.Impulse);
            AudioSource.PlayClipAtPoint(GoombaDead, transform.position);
            transform.localScale = new Vector3(0.25f, 0.0625f ,0.25f);
        }
    }
}
