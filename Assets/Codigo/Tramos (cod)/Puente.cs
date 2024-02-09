using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puente : MonoBehaviour
{
    [SerializeField] private SpriteRenderer indicador;
    [SerializeField] private Color colorActivado, colorDesactivado;
    [SerializeField] private ParticleSystem chispas;

    bool estaActivado;
    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        estaActivado = false;
        indicador.color = colorDesactivado; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Conexion")) 
        {
            if(!estaActivado) PuenteAlcanzado();
        }
    }

    void PuenteAlcanzado() 
    {
        estaActivado = true;
        indicador.color = colorActivado;
        chispas.Play();
        audio.Play();
        GestorJuego.Instancia.PuenteAlcanzado();
    }
}
