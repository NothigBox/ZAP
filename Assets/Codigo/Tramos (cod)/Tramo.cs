using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tramo : Invocable
{
    public Action EnTramoAsesinado;

    private GestorCamino camino;

    private void Awake()
    {
        camino = FindObjectOfType<GestorCamino>();

        EnTramoAsesinado += camino.AparecerTramo;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AsesinoTramo")) 
        {
            EnTramoAsesinado?.Invoke();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AsesinoTramo"))
        {
            transform.SetParent(null);
            gameObject.SetActive(false);
        }
    }
}
