using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ControladorPiscina<T> : MonoBehaviour where T : Invocable
{
    [SerializeField] T[] prefabs;

    List<T>[] usados;
    Queue<T>[] disponibles;

    public int CantidadPrefabs => prefabs.Length;

    private void Awake()
    {
        int cantidadPrefabs = prefabs.Length;

        usados = new List<T>[cantidadPrefabs];
        disponibles = new Queue<T>[cantidadPrefabs];

        for (int i = 0; i < cantidadPrefabs; i++) 
        {
            usados[i] = new List<T>();
            disponibles[i] = new Queue<T>();
        }
    }

    public T ConseguirObjeto(int indice) 
    {
        if (indice >= prefabs.Length) 
        {
            return null;
        }

        if (disponibles[indice].Count <= 0)
        {
            T nuveoObjeto = Instantiate(prefabs[indice]);
            disponibles[indice].Enqueue(nuveoObjeto);
        }

        T resultado = disponibles[indice].Dequeue();

        resultado.DefinirIndice(indice);
        resultado.EnDesaparecido = DevolverObjeto;
        resultado.gameObject.SetActive(true);

        usados[indice].Add(resultado);

        return resultado;
    }

    void DevolverObjeto(Invocable invocable) 
    {
        int indice = invocable.Indice;
        usados[indice].Remove(invocable as T);
        disponibles[indice].Enqueue(invocable as T);
    }
}
