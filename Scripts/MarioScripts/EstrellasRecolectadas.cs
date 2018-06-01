using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrellasRecolectadas : MonoBehaviour {

    public int estrellasRecolectadas;
	// Use this for initialization
	void Start () {
        estrellasRecolectadas = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Estrella")
        {
            ++estrellasRecolectadas;
        }
    }
}
