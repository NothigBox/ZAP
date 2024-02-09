using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GestorUI : MonoBehaviour
{
    [SerializeField] ControladorEstadisticas estadisticas;
    [SerializeField] ControladorPanelFinal panelFinal;
    [SerializeField] ControladorMenus menus;
    [SerializeField] GameObject panelJuego;

    [Header("Cuenta Atras")]
    [SerializeField] float tiempoCuentaAtras;
    [SerializeField] TextMeshProUGUI cuentaAtras;
    [SerializeField] GameObject cuentaAtrasPadre;

    AudioSource audio;

    public Action EnCuentaAtrasTerminada;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();

        panelFinal.gameObject.SetActive(false);
        cuentaAtrasPadre.SetActive(false);
    }

    public void MostrarPuentes(int cifra) 
    {
        estadisticas.MostrarEstadistica(Estadistica.Puente, cifra);
    }

    public void MostrarPanelFinal(int puentesAlcanzados, int lazoUsado, int total) 
    {
        OcultarMenu();

        panelJuego.SetActive(false);
        panelFinal.DefinirPanelFinal(puentesAlcanzados, lazoUsado, total);
        panelFinal.gameObject.SetActive(true);
    }

    public void MostrarMenu(Menu menu) 
    {
        panelJuego.SetActive(false);
        menus.DefinirMenu(menu);
    }

    public void OcultarMenu() 
    {
        menus.QuitarMenu();
    }

    public void MostrarPanelJuego() 
    {
        panelJuego.SetActive(true);
    }

    public void ComenzarCuentaAtras() 
    {
        StartCoroutine(CuentaAtrasCorrutina());
    }

    IEnumerator CuentaAtrasCorrutina() 
    {
        cuentaAtrasPadre.SetActive(true);

        float duracion = tiempoCuentaAtras / 3f;

        for (int i = 3; i > 0; i--) 
        {
            cuentaAtras.text = i.ToString();
            yield return new WaitForSeconds(duracion);
        }

        cuentaAtrasPadre.SetActive(false);

        EnCuentaAtrasTerminada?.Invoke();
    }

    public void SonidoBoton() 
    {
        audio.Play();
    }
}
