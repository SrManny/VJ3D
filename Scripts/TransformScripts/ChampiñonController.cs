using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampiñonController : MonoBehaviour {

    // Use this for initialization
    public float timeTrans = 1.0f;
    public AudioClip powerTransform;
    public GameObject mainCamara, aux;
    private Animator animate;
    private bool tocado;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (MarioController.tranformandose && tocado)
        {
            Debug.Log("Aqui deberia entrar");
            timeTrans -= Time.deltaTime;
            if (timeTrans < 0)
            {
                Debug.Log("Si esta entrando aqui no va la vida");
                MarioController.tranformandose = false;
                timeTrans = 1.0f;
                MarioController.poder = 1;
                animate.enabled = true;
                DestroyObject(gameObject);
            }
            else
            {
                Debug.Log("OLA? "+ timeTrans);
                if (timeTrans > 0.6f) aux.transform.localScale = new Vector3(0.13f, 0.09f, 0.13f);
                else if (timeTrans > 0.4f) aux.transform.localScale = new Vector3(0.13f, 0.15f, 0.13f);
                else aux.transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
            }
        }
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MarioBros")
        {
            aux = collision.gameObject;
            if (MarioController.poder == 0)
            {

                animate = collision.gameObject.GetComponent<Animator>();
                animate.enabled = false;
                AudioSource.PlayClipAtPoint(powerTransform, mainCamara.transform.position);
                MarioController.tranformandose = true;
                aux.transform.localScale = new Vector3(0.13f, 0.18f, 0.13f);
                tocado = true;
            }
            else
            {
                DestroyObject(gameObject);
                AudioSource.PlayClipAtPoint(powerTransform, mainCamara.transform.position);
                tocado = true;
            }
        }

    }
}
