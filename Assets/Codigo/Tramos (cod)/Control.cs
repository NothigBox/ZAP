using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class Control : Tramo
{
    [SerializeField] TextMeshProUGUI cifra;

    public void DefinirTextoCifra(string cifra) 
    {
        this.cifra.text = cifra;
    }
}
