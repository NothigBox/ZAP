using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMenus : MonoBehaviour
{
    [SerializeField] GameObject inicio, pausa;
    [SerializeField] CinemachineVirtualCamera camaraInicio, camaraCreditos;

    public void DefinirMenu(Menu menu)
    {
        QuitarMenu();

        switch (menu)
        {
            case Menu.Inicio:
                inicio.SetActive(true);
                camaraInicio.Priority = 10;
                break;

            case Menu.Creditos:
                camaraCreditos.Priority = 10;
                break;

            case Menu.Pausa:
                pausa.SetActive(true);
                camaraInicio.Priority = 10;
                break;
        }
    }

    public void QuitarMenu() 
    {
        pausa.SetActive(false);
        inicio.SetActive(false);

        camaraInicio.Priority = 1;
        camaraCreditos.Priority = 1;
    }
}

public enum Menu { Inicio , Creditos, Pausa }