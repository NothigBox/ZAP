using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laboratorio : Tramo
{
    [SerializeField] Maquina maquina;

    private void OnEnable()
    {
        maquina.Reiniciar();
    }
}
