using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ColisionConEnemigo : MonoBehaviour {

    // Use this for initialization
    private Animator animate;
    public GameObject mainCamara;
    public static bool marioDañado, destransformandose;
    public float timeTrans = 1.0f, TimeToDeath;
    private bool vamosAmorir;
    private GameObject aux;
    public Material pallete, rojoBlanco, MarioHielo, AzulEnRojo;
    public Material[] anterior = new Material[3];
    public Material[] TrajeNormal = new Material[3];
    public AudioClip MarioDestransformacion;
    public AudioClip ohhSound;

    void Start () {
        animate = GetComponent<Animator>();
        marioDañado = false;
        destransformandose = false;
        vamosAmorir = false;
        TimeToDeath = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if (vamosAmorir)
        {
            TimeToDeath -= Time.deltaTime;
            if (TimeToDeath < 0)
            {
                --MarioController.Vidas;
                if (MarioController.Vidas < 0)
                {
                    SceneManager.LoadScene(0, LoadSceneMode.Single);
                }
            }
        }
        else
        {
            if (destransformandose)
            {
                timeTrans -= Time.deltaTime;
                if (timeTrans < 0.0f)
                {
                    destransformandose = false;
                    animate.enabled = true;
                    timeTrans = 1.0f;
                    animate.SetBool("Dañado", true);
                }
                else
                {
                    if (MarioController.poder == 0)
                    {
                        if (timeTrans > 0.6f) transform.localScale = new Vector3(0.13f, 0.05f, 0.13f);
                        else if (timeTrans > 0.4f) transform.localScale = new Vector3(0.13f, 0.11f, 0.13f);
                        else transform.localScale = new Vector3(0.13f, 0.08f, 0.13f);
                    }
                    else
                    {
                        if (timeTrans > 0.6f)
                        {
                            GetComponentInChildren<Renderer>().materials = TrajeNormal;
                        }

                        else if (timeTrans > 0.4f)
                        {
                            GetComponentInChildren<Renderer>().materials = anterior;
                        }
                        else
                        {
                            GetComponentInChildren<Renderer>().materials = TrajeNormal;
                        }

                    }
                }
            }

            else if (marioDañado)
            {
                MarioController.dir = MarioController.anterior;
                BoxCollider bx = aux.GetComponent<BoxCollider>();
                MarioController.dondeMirar = transform.position + bx.center;
                if (aux.tag == "Boomerang") DestroyObject(aux);
                if (MarioController.poder == 0)
                {
                    MarioController.poder = -1;
                    animate.SetInteger("State", -1);
                    vamosAmorir = true;
                    AudioSource.PlayClipAtPoint(MarioDestransformacion, mainCamara.transform.position);
                    AudioSource.PlayClipAtPoint(ohhSound, mainCamara.transform.position);
                }
                else if (MarioController.poder == 1)
                {
                    transform.localScale = new Vector3(0.13f, 0.08f, 0.13f);
                    destransformandose = true;
                    animate.enabled = false;
                    MarioController.poder = 0;
                    AudioSource.PlayClipAtPoint(MarioDestransformacion, mainCamara.transform.position);
                    AudioSource.PlayClipAtPoint(ohhSound, mainCamara.transform.position);
                }
                else if (MarioController.poder == 2 || MarioController.poder == 3)
                {
                    anterior = GetComponentInChildren<Renderer>().materials;
                    TrajeNormal = GetComponentInChildren<Renderer>().materials;
                    anterior.CopyTo(TrajeNormal, 0);
                    TrajeNormal[0] = new Material(pallete);
                    TrajeNormal[1] = new Material(pallete);
                    TrajeNormal[2] = new Material(pallete);
                    if (MarioController.poder == 2)
                    {
                        anterior[0] = new Material(pallete);
                        anterior[1] = new Material(pallete);
                        anterior[2] = new Material(rojoBlanco);

                    }
                    else
                    {
                        anterior[0] = new Material(pallete);
                        anterior[1] = new Material(MarioHielo);
                        anterior[2] = new Material(AzulEnRojo);
                    }

                    //Debug.Log("El primer material se llama " + TrajeNormal[0].name + " El segundo " +TrajeNormal[1].name + " y el tercero " + TrajeNormal[2].name);
                    destransformandose = true;
                    animate.enabled = false;
                    MarioController.poder = 1;
                    AudioSource.PlayClipAtPoint(MarioDestransformacion, transform.position);
                    AudioSource.PlayClipAtPoint(ohhSound, transform.position);
                }
                marioDañado = false;
            }
            else animate.SetBool("Dañado", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Destroy(collision.gameObject);
        // if (collision.gameObject.transform.parent.transform.parent.tag == "Enemy")
        //  {
            
            if (collision.gameObject.tag == "Koopa")
            {
            aux = collision.gameObject;
            marioDañado = true || marioDañado;
            }
            if (collision.gameObject.tag == "Boomerang")
            {
            aux = collision.gameObject;
            marioDañado = true || marioDañado;
            }
            if (collision.gameObject.tag == "KoopaBoomerang")
        {
            aux = collision.gameObject;
            marioDañado = true || marioDañado;
        }
      //  }
        /*else if (collision.gameObject.tag != "Goomba")
        {
            animate.SetBool("Dañado", false);
        }*/
    }
}
