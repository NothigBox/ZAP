using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControladorVidaTecnico : ControladorVida
{
    [SerializeField] ParticleSystem chispasMorir;

    public Action EnMorir;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemigo")) 
        {
            EnHerido(1);
        }
    }

    protected override void Morir()
    {
        chispasMorir.transform.SetParent(null);
        chispasMorir.Play();
        chispasMorir.GetComponent<AudioSource>().Play();

        EnMorir?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EnMorir = null;
    }
}
