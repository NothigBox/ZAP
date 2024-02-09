using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GestorMusica : MonoBehaviour
{
    [SerializeField] AudioClip[] canciones;
    AudioSource fuente;

    private void Awake()
    {
        fuente = GetComponent<AudioSource>();
        fuente.loop = true;
        fuente.playOnAwake = false;
        fuente.Stop();
    }

    public void DefinirCancion(Cancion cancion) 
    {
        AudioClip resultado = null;

        switch (cancion)
        {
            case Cancion.Menu:
                resultado = canciones[0];
                break;

            case Cancion.JuegoElectro:
                resultado = canciones[1];
                break;

            case Cancion.JuegoJazz:
                resultado = canciones[2];
                break;
        }

        if (fuente.clip == resultado) return;

        fuente.Stop();
        fuente.clip = resultado;
        fuente.Play();
    }

    public void Parar() 
    {
        fuente.Stop();
    }
}

public enum Cancion { Menu, JuegoElectro, JuegoJazz }