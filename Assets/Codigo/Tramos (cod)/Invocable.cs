using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public abstract class Invocable : MonoBehaviour
{
    public Action<Invocable> EnDesaparecido;

    int indice;

    public int Indice => indice;

    public void DefinirIndice(int indice) 
    {
        this.indice = indice;
    }

    private void OnDisable()
    {
        EnDesaparecido?.Invoke(this);
    }
}
