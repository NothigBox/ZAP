using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorPanelFinal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI puentesAlcanzados, lazoUsado, total;

    public void DefinirPanelFinal(int puentesAlcanzados, int lazoUsado, int total) 
    {
        this.puentesAlcanzados.text = puentesAlcanzados.ToString("0000");
        this.lazoUsado.text = lazoUsado.ToString("0000");
        this.total.text = total.ToString("0000");
    }
}
