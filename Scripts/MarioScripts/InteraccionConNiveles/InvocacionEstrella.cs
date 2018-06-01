using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocacionEstrella : MonoBehaviour {

    public GameObject[] Koopas = new GameObject[2];
    public GameObject tile, estrella;
    private bool primero = true;
    public AudioClip DesbloquearSecreto;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        for (int i = 0; i < Koopas.Length; ++i)
        {
            if (Koopas[i] == null) ++count;
        }
        if (count == Koopas.Length && primero)
        {
            primero = false;
            Instantiate(estrella, tile.transform.position, estrella.transform.rotation);
            AudioSource.PlayClipAtPoint(DesbloquearSecreto, transform.position);
        }
    }
}
