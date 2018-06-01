using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeEnergia : MonoBehaviour {

    // Use this for initialization
    public Animator animate;
    public float timeToThrow;
    public GameObject boladeFuego, Mario;
    public bool start = false;
    public AudioClip effectSound;
	void Start () {
        animate = GetComponentInParent<Animator>();
        timeToThrow = 50*Time.fixedDeltaTime;
        
    }
	// Update is called once per frame
	void Update () {
        if (MarioController.state == "idle" && Input.GetKeyDown(KeyCode.Q))
        {
            MarioController.state = "Throwing";
            if (MarioController.poder == 2)
            {
                animate.SetInteger("Qpulsada", 1);
                start = true;

            }
            else if (MarioController.poder == 3)
            {
                animate.SetInteger("Qpulsada", 1);
                start = true;
            }
        }
        else if (start)
        {
            timeToThrow -= Time.fixedDeltaTime;
            if (timeToThrow < 0)
            {
                start = false;
                GameObject ball = Instantiate(boladeFuego, transform);
                ball.transform.parent = null;
                ball.GetComponent<MovimientoBolaDeFuego>().direccion = 20 * Mario.transform.forward;
                ball.GetComponent<Rigidbody>().velocity = 20 * Mario.transform.forward;
                AudioSource.PlayClipAtPoint(effectSound, transform.position);
                animate.SetInteger("Qpulsada", 0);
                MarioController.state = "idle";
                timeToThrow = 50 * Time.fixedDeltaTime;
            }
        }
	}
}
