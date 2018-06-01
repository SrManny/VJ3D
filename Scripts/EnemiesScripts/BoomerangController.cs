using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : MonoBehaviour {

    // Use this for initialization
    public Animator animate;
    public float timeToThrow, TimeToRespawnBoomer;
    public GameObject Boomerang, KoopaBoomer;
    public bool start = false, tenemosBoomer;
    public AudioClip effectSound;
    void Start()
    {
        start = false;
        animate = GetComponentInParent<Animator>();
        timeToThrow = 50 * Time.fixedDeltaTime;
        TimeToRespawnBoomer = 10.0f;
        tenemosBoomer = true;
        Boomerang.GetComponent<MeshRenderer>().enabled = true;
        Boomerang.GetComponent<BoxCollider>().enabled = false;
        Boomerang.GetComponent<MovimientoBoomerang>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (KoopaBoomerang.state == "idle" && KoopaBoomer.GetComponent<KoopaBoomerang>().VoyALanzar && tenemosBoomer)
        {
            KoopaBoomer.GetComponent<KoopaBoomerang>().VoyALanzar = false;
            tenemosBoomer = false;
            start = true;
            animate.SetBool("Lanzado", true);
        }
        else if (KoopaBoomerang.state == "idle" && KoopaBoomer.GetComponent<KoopaBoomerang>().VoyALanzar && !tenemosBoomer)
        {
            KoopaBoomer.GetComponent<KoopaBoomerang>().VoyALanzar = false;
        }
        else if (start)
        {
            timeToThrow -= Time.fixedDeltaTime;
            if (timeToThrow < 0)
            {
                start = false;
                GameObject Boomerang2 = Instantiate(Boomerang, transform);
                Boomerang.GetComponent<MeshRenderer>().enabled = false;
                Boomerang2.transform.parent = null;
                //Boomerang2.GetComponent<Rigidbody>().velocity = 20 * KoopaBoomer.transform.forward;
                Boomerang2.GetComponent<MovimientoBoomerang>().origen = KoopaBoomer.transform.position;
                Boomerang2.GetComponent<MovimientoBoomerang>().enabled = true;
                AudioSource.PlayClipAtPoint(effectSound, transform.position);
                animate.SetBool("Lanzado", false);
                KoopaBoomerang.state = "idle";
                timeToThrow = 50 * Time.fixedDeltaTime;
            }
        }
        else if (!tenemosBoomer)
        {
            TimeToRespawnBoomer -= Time.fixedDeltaTime;
            if (TimeToRespawnBoomer < 0)
            {
                TimeToRespawnBoomer = 10.0f;
                tenemosBoomer = true;
                Boomerang.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
