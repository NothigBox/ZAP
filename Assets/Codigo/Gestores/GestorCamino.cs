using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorCamino : MonoBehaviour
{
    const int REPETICIONES = 2;

    [SerializeField] float velocidad;
    [SerializeField] Transform padreTramos;
    [SerializeField] Transform pivoteCamino;
    [SerializeField] GestorFabrica fabrica;
    [SerializeField] string secuenciaActual;
    [SerializeField] string[] secuencias;

    [HideInInspector] public bool pararActivo;

    public Action EnPuenteGenerado;

    int repeticionActual;
    int indiceSecuencia;
    int indiceSecuenciaActual;
    float tiempoCorrido;

    public float TiempoCorrido => tiempoCorrido;

    private void Awake()
    {
        repeticionActual = 0;
        indiceSecuencia = 0;
        indiceSecuenciaActual = 0;

        secuenciaActual = secuencias[indiceSecuencia];
    }

    private void FixedUpdate()
    {
        if (pararActivo || GestorJuego.Instancia.PausaActiva) return;

        for (int i = 0; i < padreTramos.childCount; i++) padreTramos.GetChild(i).transform.position += Vector3.left * velocidad;

        tiempoCorrido += Time.fixedDeltaTime;
    }

    public void AparecerTramo()
    {
        GameObject tramo = null;
        switch (secuenciaActual[indiceSecuenciaActual]) 
        {
            case 'O':
                tramo = fabrica.ConseguirObstaculoAleatorio().gameObject;
                break;

            case 'V':
                tramo = fabrica.ConseguirVacio().gameObject;
                break;

            case 'P':
                Control p = fabrica.ConseguirPuente();
                p.DefinirTextoCifra(GestorJuego.Instancia.PuentesGenerados.ToString("00"));
                tramo = p.gameObject;

                EnPuenteGenerado?.Invoke();
                break;

            case 'L':
                Control l = fabrica.ConseguirLazoUsado();
                l.DefinirTextoCifra((GestorJuego.Instancia.LazoUsado).ToString("0000 m"));
                tramo = l.gameObject;
                break;

            case 'M':
                tramo = fabrica.ConseguirMaquinaAleatoria().gameObject;
                break;

            default:
                tramo = fabrica.ConseguirVacio().gameObject;
                break;
        }

        tramo.transform.position = pivoteCamino.position;
        tramo.transform.SetParent(padreTramos);

        indiceSecuenciaActual++;

        if (indiceSecuenciaActual >= secuenciaActual.Length)
        {
            indiceSecuenciaActual = 0;
            repeticionActual++;

            if(repeticionActual >= REPETICIONES) 
            {
                repeticionActual = 0;
                indiceSecuencia++;

                if (indiceSecuencia >= secuencias.Length) 
                {
                    indiceSecuencia = secuencias.Length - 1;
                }

                secuenciaActual = secuencias[indiceSecuencia];
            }
        }
    }
}
