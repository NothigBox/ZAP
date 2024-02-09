using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorFabrica : MonoBehaviour
{
    [SerializeField] ControladorPiscinaControl control;
    [SerializeField] ControladorPiscinaMaquinas maquinas;
    [SerializeField] ControladorPiscinaObstaculos obstaculos;

    public Tramo ConseguirObstaculoAleatorio() 
    {
        int aleatorio = Random.Range(1, obstaculos.CantidadPrefabs);
        
        return obstaculos.ConseguirObjeto(aleatorio);
    }

    public Laboratorio ConseguirMaquinaAleatoria()
    {
        int aleatorio = Random.Range(0, maquinas.CantidadPrefabs);

        return maquinas.ConseguirObjeto(aleatorio);
    }

    public Control ConseguirPuente() 
    {
        return control.ConseguirObjeto(0);
    }

    public Control ConseguirLazoUsado()
    {
        return control.ConseguirObjeto(1);
    }

    public Tramo ConseguirVacio()
    {
        return obstaculos.ConseguirObjeto(0);
    }
}
