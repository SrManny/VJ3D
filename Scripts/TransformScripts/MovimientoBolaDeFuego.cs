using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MovimientoBolaDeFuego : MonoBehaviour
{
    public float timeToActivatecollider = 0.05f;
    public int bajale;
    public AudioClip deleteAnEnemy;
    public Vector3 origen;
    public Vector3 direccion;
    // Use this for initialization
    void Start()
    {
        bajale = 0;
    }
    double distance(Vector3 dist1, Vector3 dist2)
    {
        return Math.Sqrt((dist1.x - dist2.x) * (dist1.x - dist2.x) + (dist1.z - dist2.z) * (dist1.z - dist2.z) + (dist1.y - dist2.y) * (dist1.y - dist2.y));
    }
    // Update is called once per frame
    void Update()
    {
        if (timeToActivatecollider > 0) origen = transform.position;
        timeToActivatecollider -= Time.deltaTime;
        if (timeToActivatecollider < 0)
        {
            GetComponent<SphereCollider>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision enter)
    {
        if (enter.gameObject.transform.parent != null && enter.gameObject.transform.parent.tag == "Tile")
        {
            if (bajale % 2 == 0)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 30, 0), ForceMode.Impulse);
            }
            else GetComponent<Rigidbody>().AddForce(new Vector3(0, 25, 0), ForceMode.Impulse);
            ++bajale;
            GetComponent<Rigidbody>().AddForce(direccion, ForceMode.Impulse);
        }
        else if (enter.gameObject.tag == "Goomba" || enter.gameObject.tag == "Koopa")
        {
            DestroyObject(enter.gameObject);
            AudioSource.PlayClipAtPoint(deleteAnEnemy, transform.position);
            DestroyObject(gameObject);
        }
        else
        {
            AudioSource.PlayClipAtPoint(deleteAnEnemy, transform.position);
            DestroyObject(gameObject);
        }
    }
}
