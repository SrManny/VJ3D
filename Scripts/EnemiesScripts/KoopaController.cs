using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KoopaController : MonoBehaviour {

    // Use this for initialization
    public bool movimiento;
    public GameObject tile, MarioBros;
    public Vector3 objetivo, anterior;
    public bool NoHaLlegado = false;

    double distance(Vector3 dist1, Vector3 dist2)
    {
        return Math.Sqrt((dist1.x - dist2.x) * (dist1.x - dist2.x) + (dist1.z - dist2.z) * (dist1.z - dist2.z));
    }
    void Start () {
        movimiento = false;
        anterior = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
      /*  if (starts) timeToTransform -= Time.deltaTime;
        if (timeToTransform < 0.0f)
        {
            DestroyObject(gameObject);
        }*/
        if (movimiento)
        {
            movimiento = false;
            NoHaLlegado = true;
            GameObject[] listaAdj = tile.GetComponent<Desplazar>().Adj;
            int indiceMin = -1;
            float minDist = 99999999999999;
            Vector3 dondeMirar = new Vector3(0f,0f,0f);
            for (int i = 0; i < listaAdj.Length; ++i)
            {
                if (listaAdj[i] != null && listaAdj[i].tag != "Tuberia")
                {
                    if (listaAdj[i] == null) break;
                    Vector3 pos = listaAdj[i].transform.position;
                    BoxCollider bx = listaAdj[i].GetComponent<BoxCollider>();
                    Vector3 objectiv = pos + bx.center;
                    double aux = distance(objectiv, MarioBros.transform.position);
                    if (aux < minDist)
                    {
                        minDist = (float)aux;
                        indiceMin = i;
                        objetivo = objectiv;
                    }
                }
            }
            if (indiceMin == -1)
            {
                NoHaLlegado = false;
            }
            else
            {
                anterior = transform.position;
                transform.LookAt(new Vector3(objetivo.x, transform.position.y, objetivo.z));
            }
            
        }
        if (NoHaLlegado)
        {
            transform.Translate((objetivo.x - transform.position.x) * Time.deltaTime * 5, 0, (objetivo.z - transform.position.z) * Time.deltaTime * 5, Space.World);
            if (distance(objetivo, transform.position) < 0.1)
            {
                NoHaLlegado = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.tag == "Tile")
        {
            tile = collision.gameObject;
        }
        else
        {
            objetivo = anterior;
            NoHaLlegado = true;
        }

    }
}
