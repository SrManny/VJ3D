using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MarioController : MonoBehaviour {

    public float speed = 5.0f;
    public static bool movimiento;
    public static string state = "falling";
    public static int poder = 1, Vidas = 0;
    public static Vector3 dir, anterior, dondeMirar, posMario;
    public static String type;
    public GameObject plataforma1;
    public GameObject[] Goombas = new GameObject[4], Koppas = new GameObject[4], KoopaBoomerangs = new GameObject[4];
    public static bool ocupat = false, tranformandose = false;
    public Animator animate;
    public AudioClip jumpSound;
    private AudioSource source;
    // Use this for initialization

    double distance(Vector3 dist1, Vector3 dist2)
    {
        return Math.Sqrt((dist1.x-dist2.x)*(dist1.x - dist2.x) + (dist1.z- dist2.z)* (dist1.z - dist2.z));
    }

    void Start () {
        BoxCollider bx = plataforma1.GetComponent<BoxCollider>();
        dir = bx.center + plataforma1.transform.position;
        state = "falling";
        animate = GetComponent<Animator>();
        posMario = transform.position;
    }


    // Update is called once per frame
    void Update () {
        //if ((dir.x != transform.position.x) || (dir.z != transform.position.z)) ;
        /*if (0 == distance(dir, transform.position))
        GetComponent<Rigidbody>().
           GetComponent<Rigidbody>().isKinematic = false;*/
        // Quaternion cameraRelativeRotation = Quaternion.FromToRotation(transform.forward, dir);

        if (movimiento)
        {
            movimiento = false;
            for (int i = 0; i < Goombas.Length; ++i)
            { 
                if (Goombas[i] != null)
                {
                    Goombas[i].GetComponent<GoombaController>().movimiento = true;
                }
                if (Koppas[i] != null)
                {
                    Koppas[i].GetComponent<KoopaController>().movimiento = true;
                }
                if (KoopaBoomerangs[i] != null)
                {
                    KoopaBoomerangs[i].GetComponent<KoopaBoomerang>().movimiento = true;
                }

            }

        }
        if (!ColisionConEnemigo.destransformandose && !tranformandose)
        {
            posMario = transform.position;
            double aux = distance(dir, transform.position);
            double jump = dir.y - transform.position.y;
            //Debug.Log("Quiero ir al sitio" + dir + " Y yo estoy en la pos " + transform.position + " y de salto tengo " + jump);
            //Debug.Log("Mi estado es " + state);
            // Ray lookRay = new Ray(transform.position, lookToward);
            // transform.LookAt(lookRay.GetPoint(1));
            transform.LookAt(new Vector3(dondeMirar.x, transform.position.y, dondeMirar.z));

            //dir = Quaternion.cameraRelativeRotation * dir;
            //transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, transform.position.y, dir.z));
            Debug.Log(state + " " + ocupat + " " + Math.Abs(jump) + " " + Input.GetKeyDown(KeyCode.W));
            if (aux < 0.1 && Math.Abs(jump) - 2 <= 1 && ocupat)
            {
                ocupat = false;
            }
            if (Math.Abs(jump) - 2 < 1.1 && aux < 0.01)
            {
                animate.SetInteger("State", 0);
                state = "idle";
                anterior = transform.position;
                if ((state == "Walking" || state == "falling" || state == "idle") && !ocupat && (Math.Abs(jump) < 3) && Input.GetKeyDown(KeyCode.W))
                {
                    animate.SetInteger("State", 2);
                    GetComponent<Rigidbody>().AddForce(0, 30, 0, ForceMode.Impulse);
                    //GetComponent<Rigidbody>().AddForce(new Vector3(3, 60, 3), ForceMode.Impulse);
                    // Debug.Log("Estoy quiero saltar con una fuerza vertical de " + (dir.y - transform.position.y) * 10);
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                    movimiento = true;
                }
            }

            else if (Math.Abs(jump) > 16 || aux > 12)
            {
                ocupat = false;
                state = "idle";
                animate.SetInteger("State", 0);
            }
            else if ((state == "Walking" || state == "falling" || state == "idle") && Math.Abs(jump) < 1.1 + 2 && aux < 12)
            {

                if (type == "Tuberia")
                {
                    type = "null";
                    state = "SaltaATuberia";
                    animate.SetInteger("State", 2);
                    GetComponent<Rigidbody>().AddForce(new Vector3((dir.x - transform.position.x) * 0.65f, 42, (dir.z - transform.position.z) * 0.65f), ForceMode.Impulse);
                    //GetComponent<Rigidbody>().AddForce(new Vector3(3, 60, 3), ForceMode.Impulse);
                    // Debug.Log("Estoy quiero saltar con una fuerza vertical de " + (dir.y - transform.position.y) * 10);
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                }
                else
                {
                    state = "Walking";
                    if (aux > 0.5) animate.SetInteger("State", 1);
                    else animate.SetInteger("State", 0);
                    //transform.Translate((dir.x - transform.position.x) * Time.deltaTime * speed, 0, (dir.z - transform.position.z) * Time.deltaTime * speed, Space.World);
                    transform.Translate((dir.x - transform.position.x) * Time.deltaTime * speed, 0, (dir.z - transform.position.z) * Time.deltaTime * speed, Space.World);
                    if (aux < 0.3) transform.position = new Vector3(dir.x, transform.position.y, dir.z);
                    // Debug.Log("Estoy walking" + jump);
                }
            }
            
            else if ((state == "idle" || state == "Walking") && aux < 12 && jump > 8 - 2 && jump < 12 - 1)
            {
                state = "Jumping";
                animate.SetInteger("State", 2);
                GetComponent<Rigidbody>().AddForce(new Vector3((dir.x - transform.position.x) * 1, 41, (dir.z - transform.position.z) * 1), ForceMode.Impulse);
                //GetComponent<Rigidbody>().AddForce(new Vector3(3, 60, 3), ForceMode.Impulse);
                // Debug.Log("Estoy quiero saltar con una fuerza vertical de " + (dir.y - transform.position.y) * 10);
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }
            else if ((state == "idle" || state == "Walking") && aux < 12 && jump < -8 - 2 && jump > -12 - 2)
            {
                state = "Jumping";
                animate.SetInteger("State", 2);
                GetComponent<Rigidbody>().AddForce(new Vector3((dir.x - transform.position.x) * 1, 21, (dir.z - transform.position.z) * 1), ForceMode.Impulse);
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                // Debug.Log("Estoy quiero saltar");
            }
            else if (state == "Jumping" && jump < -4.3)
            {
                state = "falling";
            }
            else if (state == "SaltaATuberia" && jump < -3)
            {
                state = "falling";
            }
        }
    }
}
