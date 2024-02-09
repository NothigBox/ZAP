using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sierra : Maquina
{
    [SerializeField] float velocidadRotacion;
    [SerializeField] float velocidadMovimiento;
    [SerializeField] GameObject apariencia;

    bool activado;
    Vector3 posicionInicial;

    private void Awake()
    {
       posicionInicial = apariencia.transform.localPosition;
    }

    void FixedUpdate()
    {
        apariencia.transform.Rotate(0, 0, velocidadRotacion * (activado? 2 : 1), Space.Self);

        if(activado) apariencia.transform.localPosition += Vector3.up * (velocidadMovimiento * Time.fixedDeltaTime);
    }

    public override void Activar() 
    {
        activado = true;
    }

    public override void Reiniciar()
    {
        activado = false;
        apariencia.transform.localPosition = posicionInicial;
    }
}
