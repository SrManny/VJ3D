using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ganar : MonoBehaviour {

    public float danceTime, timeToGoCenter;
    public bool start, noSeHaHecho, primero;
    public Animator animate;
    public GameObject TileAlQueMirar;
	// Use this for initialization
	void Start () {
        start = false;
        animate = GetComponent<Animator>();
        danceTime = 9.0f;
        timeToGoCenter = 0.4f;
        noSeHaHecho = true;
        primero = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            timeToGoCenter -= Time.deltaTime;
            if (timeToGoCenter < 0)
            {
                danceTime -= Time.deltaTime;
                animate.SetBool("Win", true);
                MarioController.dir = (TileAlQueMirar.transform.position + TileAlQueMirar.GetComponent<BoxCollider>().center);
                MarioController.tranformandose = true;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<CapsuleCollider>().enabled = false;
                if (!primero && noSeHaHecho)
                {
                    transform.Translate(0, -2, 0);
                    noSeHaHecho = false;
                }
                else primero = false;
                if (danceTime < 0) SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%4);
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FINAL")
        {
            start = true;
        }
    }
}
