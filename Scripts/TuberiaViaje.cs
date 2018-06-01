using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuberiaViaje : MonoBehaviour {

    // Use this for initialization
    public GameObject LugarAViajar;
    public GameObject LugarASaltar;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MarioBros")
        {
            BoxCollider bx = LugarASaltar.GetComponent<BoxCollider>();
            BoxCollider bx2 = LugarAViajar.GetComponent<BoxCollider>();
            MarioController.dir = LugarASaltar.transform.position + bx.center;
            MarioController.dondeMirar = MarioController.dir;
            collision.gameObject.transform.position = new Vector3(LugarAViajar.transform.position.x + bx2.center.x, LugarAViajar.transform.position.y + bx2.center.y + 3, LugarAViajar.transform.position.z + bx2.center.z);
            collision.gameObject.GetComponent<MarioController>().animate.SetInteger("State", 2);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3((MarioController.dir.x - collision.gameObject.transform.position.x) * 0.65f, 30f, (MarioController.dir.z - collision.gameObject.transform.position.z) * 0.65f), ForceMode.Impulse);
            
        }
    }
}
