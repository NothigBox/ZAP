using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorEstadisticas : MonoBehaviour
{
    [SerializeField] Image imagen;
    [SerializeField] TextMeshProUGUI cifra;
    //[SerializeField] Texture[] iconos; 
    [SerializeField] Color[] iconos;
    
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MostrarEstadistica(Estadistica estadistica, int cifra) 
    {
        this.cifra.text = cifra.ToString("00");
        imagen.color = iconos[(int) estadistica];

        animator.SetTrigger("Estadistica");
    }
}

public enum Estadistica { Puente = 0, Maquina = 1 }