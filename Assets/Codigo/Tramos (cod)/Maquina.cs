using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Maquina : MonoBehaviour
{
    const float RETRAZO_ACTIVAR = 0.5f;

    public abstract void Activar();
    public abstract void Reiniciar();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Lazo"))
        {
            collision.gameObject.SetActive(false);
            GestorJuego.Instancia.MatarConexion();
        }
        else if (collision.collider.CompareTag("Tecnico")) GetComponent<Rigidbody2D>().simulated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Conexion")) Invoke(nameof(Activar), RETRAZO_ACTIVAR);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tecnico"))
        {
            GestorTecnico tecnico = collision.GetComponent<GestorTecnico>();

            if (tecnico.EstaParado) Activar();
        }
    }
}
