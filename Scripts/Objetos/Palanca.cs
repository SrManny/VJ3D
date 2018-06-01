using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Palanca : MonoBehaviour {

   
    public GameObject palancaDesctivada, palancaActivada;
    public GameObject MarioBros;

    double distance(Vector3 dist1, Vector3 dist2)
    {
        return Math.Sqrt((dist1.x - dist2.x) * (dist1.x - dist2.x) + (dist1.z - dist2.z) * (dist1.z - dist2.z));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        double dist = distance(MarioBros.transform.position, transform.position);
        Debug.Log(dist);
        if (dist < 12)
        {
            if (gameObject.transform.parent.tag == "palancaActivada")
            {
                gameObject.transform.parent.tag = "palancaDesactivada";
                DestroyObject(gameObject);
                GameObject aux = Instantiate(palancaDesctivada, gameObject.transform.position, palancaDesctivada.transform.rotation);
                aux.GetComponent<Palanca>().MarioBros = MarioBros;
                aux.transform.parent = gameObject.transform.parent;
                DestroyObject(gameObject);
            }
            else
            {
                gameObject.transform.parent.tag = "palancaActivada";
                GameObject aux = Instantiate(palancaActivada, gameObject.transform.position, palancaActivada.transform.rotation);
                aux.GetComponent<Palanca>().MarioBros = MarioBros;
                aux.transform.parent = gameObject.transform.parent;
                DestroyObject(gameObject);
            }
        }
    }
}
