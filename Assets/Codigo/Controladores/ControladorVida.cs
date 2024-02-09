using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControladorVida : MonoBehaviour
{
    [SerializeField] float vidaMaxima;
    float vidaActual;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }


    public virtual void EnHerido(float da�o) 
    {
        vidaActual -= da�o;

        if (vidaActual <= 0) 
        {
            Morir();
        }
    }

    protected abstract void Morir();
}
