using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocacionTuberia : MonoBehaviour {

    // Use this for initialization
    public GameObject[] Koopas = new GameObject[2];
    public GameObject Tuberia;
    public GameObject tile86, tile96, tile84, tile94;
    //public float TimeToSpawn = 1.0f;
    private bool primero;
	void Start () {
        primero = true;
    }
	
	// Update is called once per frame
	void Update () {
        int count = 0;
        for (int i = 0; i < 2; ++i)
        {
            if (Koopas[i] == null) ++count;
        }
        if (count == 2 && primero)
        {
            primero = false;
            Instantiate(Tuberia, tile86.transform.position, Tuberia.transform.rotation);
            Instantiate(Tuberia, tile96.transform.position, Tuberia.transform.rotation);
            tile86.GetComponent<TuberiaViaje>().enabled = true;
            tile96.GetComponent<TuberiaViaje>().enabled = true;
            tile86.GetComponent<TuberiaViaje>().LugarAViajar = tile96;
            tile86.GetComponent<TuberiaViaje>().LugarASaltar = tile94;
            tile96.GetComponent<TuberiaViaje>().LugarAViajar = tile86;
            tile96.GetComponent<TuberiaViaje>().LugarASaltar = tile84;
            tile86.tag = "Tuberia";
            tile96.tag = "Tuberia";
        }
	}
}
//-4.4, 79.5 139.73 86 84
//-14.5 119.5 199.8 96 94