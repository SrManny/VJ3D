using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaBoomerang : MonoBehaviour {

    // Use this for initialization
    public bool VoyALanzar;
    public GameObject mano;
    public static string state;
    public bool movimiento;
    public Animator animate;

	void Start () {
        VoyALanzar = false;
        state = "idle";
        animate = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if (movimiento)
        {
            movimiento = false;
            VoyALanzar = true;

        }
        animate.SetBool("Recogido", false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boomerang")
        {
            DestroyObject(collision.gameObject);
            mano.GetComponent<BoomerangController>().Boomerang.GetComponent<MeshRenderer>().enabled = true;
            mano.GetComponent<BoomerangController>().tenemosBoomer = true;
            animate.SetBool("Recogido", true);
        }
    }
}
