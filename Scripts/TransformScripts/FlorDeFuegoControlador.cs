using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorDeFuegoControlador : MonoBehaviour
{

    // Use this for initialization
    public float timeTrans = 1.0f;
    public AudioClip powerTransform;
    public GameObject mainCamara, aux;
    public Material pallete, rojoBlanco, AzulRojo;
    public Material[] anterior = new Material[3];
    public Material[] trajeFuego = new Material[3];
    private Animator animate;
    int typeOfTrans;
    public bool tocado;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MarioController.tranformandose && tocado)
        {
            timeTrans -= Time.deltaTime;
            if (timeTrans < 0)
            {
                MarioController.tranformandose = false;
                timeTrans = 1.0f;
                MarioController.poder = 2;
                animate.enabled = true;
                DestroyObject(gameObject);
            }
            else
            {
                if (timeTrans > 0.6f)
                {
                    if (typeOfTrans == 1) aux.transform.localScale = new Vector3(0.13f, 0.09f, 0.13f);
                    aux.GetComponentInChildren<Renderer>().materials = trajeFuego;
                }
                else if (timeTrans > 0.4f)
                {
                    if (typeOfTrans == 1) aux.transform.localScale = new Vector3(0.13f, 0.15f, 0.13f);
                    aux.GetComponentInChildren<Renderer>().materials = anterior;
                }
                else
                {
                    if (typeOfTrans == 1) aux.transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
                    aux.GetComponentInChildren<Renderer>().materials = trajeFuego;
                }
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
                AudioSource.PlayClipAtPoint(powerTransform, mainCamara.transform.position);
                aux.transform.localScale = new Vector3(0.13f, 0.18f, 0.13f);
                typeOfTrans = 1;
            }
            else
            {
                typeOfTrans = 2;
                AudioSource.PlayClipAtPoint(powerTransform, mainCamara.transform.position);
            }
            MarioController.tranformandose = true;
            animate = collision.gameObject.GetComponent<Animator>();
            animate.enabled = false;
            anterior = collision.gameObject.GetComponentInChildren<Renderer>().materials;
            trajeFuego = collision.gameObject.GetComponentInChildren<Renderer>().materials;
            trajeFuego[0] = new Material(pallete);
            trajeFuego[1] = new Material(rojoBlanco);
            trajeFuego[2] = new Material(AzulRojo);
            tocado = true;
        }

    }
}
