using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ControladorVidaTecnico))]
public class GestorTecnico : MonoBehaviour
{
    [SerializeField] float fuerzaSaltar;
    [SerializeField] float tiempoParar;

    [Header("Brazo")]
    [SerializeField] Transform brazo;
    [SerializeField] Transform pivoteBrazo;

    public bool useKeyboard;

    bool estaParado;
    bool puedeSaltar;
    Vector2 escalaInicial;
    new Rigidbody2D rigidbody;
    ControladorVidaTecnico vida;
    Coroutine barrer, parar;

    public Action<bool> EnParar;

    public bool EstaParado => estaParado;
    public ControladorVidaTecnico Vida => vida;    

    private void Awake()
    {
        parar = null;
        barrer = null;

        rigidbody = GetComponent<Rigidbody2D>();
        vida = GetComponent<ControladorVidaTecnico>();

        escalaInicial = transform.localScale;

        vida.EnMorir += () => brazo.gameObject.SetActive(false);
    }

    Vector2 posicionToqueInicial;

    void Update()
    {
        if (!GestorJuego.Instancia.JuegoComenzado || GestorJuego.Instancia.PausaActiva) return;

        if (useKeyboard) 
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                Saltar();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Barrer();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Parar();
            }

            return;
        }

        Touch touch = Input.GetTouch(0);
        TouchPhase touchPhase = touch.phase;

        if(touchPhase == TouchPhase.Began) 
        {
            posicionToqueInicial = touch.position;
        }
        else if(touchPhase == TouchPhase.Ended) 
        {
            Vector2 direccionToque = (touch.position - posicionToqueInicial).normalized;

            float y = direccionToque.y;
            float x = direccionToque.x;

            if (Mathf.Abs(y) > Mathf.Abs(x)) 
            {
                if (y > 0) Saltar();
                else if (y < 0) Barrer();
            }
            else if(Mathf.Abs(x) > Mathf.Abs(y)) 
            {
                if(x < 0) Parar();
            }
            else Saltar();
        }
    }

    private void LateUpdate()
    {
        brazo.position = pivoteBrazo.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Piso")) 
        {
            puedeSaltar = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Piso"))
        {
            puedeSaltar = false;
        }
    }

    void Saltar()
    {
        if(!puedeSaltar) return;
        puedeSaltar = false;

        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(Vector2.up * fuerzaSaltar, ForceMode2D.Impulse);

        EnParar?.Invoke(false);
    }

    void Barrer()
    {
        if(barrer == null) barrer = StartCoroutine(BarrerCorrutina());

        EnParar?.Invoke(false);
    }

    void Parar()
    {
        if (!puedeSaltar) return;

        if (parar == null) parar = StartCoroutine(PararCorrutina());
    }

    IEnumerator PararCorrutina() 
    {
        EnParar?.Invoke(true);
        estaParado = true;
        
        yield return new WaitForSeconds(tiempoParar);

        EnParar?.Invoke(false);
        estaParado = false;

        parar = null;
    }

    IEnumerator BarrerCorrutina()
    {
        transform.localScale = new Vector2(escalaInicial.x * 1.2f, escalaInicial.y / 2f);

        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(Vector2.down * (fuerzaSaltar / 2f), ForceMode2D.Impulse);

        yield return new WaitForSeconds(tiempoParar);

        transform.localScale = new Vector2(escalaInicial.x, escalaInicial.y);

        barrer = null;
    }
}