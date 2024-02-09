using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GestorJuego : MonoBehaviour
{
    const int MULTIPLICADOR_LAZO = 2;
    const float TIEMPO_INICIO = 1f;
    const int PUNTAJE_PUENTE = 25;
    const int PUNTAJE_LAZO = 5;
    const float AUMENTO_VELOCIDAD = 0.045f;

    [Range(1f, 2f)] public float gameSpeed;

    [Header("Gestores")]
    [SerializeField] GestorUI ui;
    [SerializeField] GestorCamino camino;
    [SerializeField] GestorTecnico tecnico;
    [SerializeField] GestorMusica musica;
    [SerializeField] ControladorConexion conexion;

    [Space]
    [SerializeField] float tiempoPanelFinal;

    bool pausaActiva;
    bool juegoComenzado;
    bool mostrarInicio;
    int puentesGenerados;
    int puentesAlcanzados;
    AudioSource musicaPerder;

    public int LazoUsado => (int)camino.TiempoCorrido * MULTIPLICADOR_LAZO;
    public int PuentesGenerados => puentesGenerados;
    public int PuentesAlcanzados => puentesAlcanzados;
    public bool JuegoComenzado => juegoComenzado;
    public bool PausaActiva => pausaActiva;

    static GestorJuego instancia;
    public static GestorJuego Instancia => instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        SetUp();
    }

    private void Start()
    {
        tecnico.EnParar = (parar) =>
        {
            if (juegoComenzado) PararJuego(parar);
        };

        camino.EnPuenteGenerado = PuenteGenerado;
        ui.EnCuentaAtrasTerminada = EmpezarJuego;

        tecnico.Vida.EnMorir += TerminarJuego;
    }

    private void Update()
    {
        Time.timeScale = gameSpeed;

        if (juegoComenzado || mostrarInicio) return;

        if (Input.anyKeyDown) 
        {
            ComenzarJuego();
        }
    }

    private void SetUp() 
    {
        gameSpeed = 1f;

        musicaPerder = GetComponent<AudioSource>();
        musicaPerder.playOnAwake = false;
        musicaPerder.loop = false;

        mostrarInicio = !(PlayerPrefs.GetInt("Inicio") > 0);

        puentesGenerados = 1;
        puentesAlcanzados = 0;
        juegoComenzado = false;
        ui.OcultarMenu();
        musica.DefinirCancion(Cancion.JuegoElectro);
        PararJuego();

        if (mostrarInicio) 
        {
            ui.MostrarMenu(Menu.Inicio);
            musica.DefinirCancion(Cancion.Menu);
        }
    }

    public void ComenzarJuego()
    {
        musica.DefinirCancion(Cancion.JuegoElectro);

        if (mostrarInicio) 
        {
            StartCoroutine(ComenzarJuegoCorrutina());
        }
        else 
        {
            PararJuego(false);
            juegoComenzado = true;
        }
    }

    IEnumerator ComenzarJuegoCorrutina()
    {
        ui.OcultarMenu();

        yield return new WaitForSeconds(TIEMPO_INICIO);

        ui.ComenzarCuentaAtras();
    }

    private void TerminarJuego()
    {
        StartCoroutine(TerminarJuegoCorrutina());
    }

    IEnumerator TerminarJuegoCorrutina()
    {
        Time.timeScale = 1f;

        musicaPerder.Play();
        musica.Parar();
        PararJuego();

        yield return new WaitForSeconds(tiempoPanelFinal);

        int total = puentesAlcanzados * PUNTAJE_PUENTE + LazoUsado * PUNTAJE_LAZO;
        ui.MostrarPanelFinal(puentesAlcanzados, LazoUsado, total);
        musica.DefinirCancion(Cancion.Menu);
    }

    public void Reiniciar(bool mostrarInicio) 
    {
        PlayerPrefs.SetInt("Inicio", mostrarInicio ? 0 : 1);
        SceneManager.LoadScene("SampleScene");
    }

    void PararJuego(bool parar = true) 
    {
        camino.pararActivo = parar;
    }

    public void PuenteAlcanzado() 
    {
        ++puentesAlcanzados;
        ui.MostrarPuentes(puentesAlcanzados);
    }

    public void MostrarCreditos(bool activados) 
    {
        if (activados) 
        {
            ui.MostrarMenu(Menu.Creditos);
        }
        else 
        {
            ui.MostrarMenu(Menu.Inicio);
        }
    }

    public void DefinirPausa(bool pausaActivada) 
    {
        if (pausaActivada) 
        {
            pausaActiva = true;
            musica.DefinirCancion(Cancion.Menu);
            ui.MostrarMenu(Menu.Pausa);
        }
        else 
        {
            StartCoroutine(ReanudarJuegoCorrutina());            
        }
    }

    IEnumerator ReanudarJuegoCorrutina() 
    {
        musica.DefinirCancion(Cancion.JuegoElectro);
        ui.OcultarMenu();

        yield return new WaitForSeconds(TIEMPO_INICIO);

        ui.ComenzarCuentaAtras();
    }

    public void MatarConexion() 
    {
        conexion.Matar();
        TerminarJuego();
    }

    void PuenteGenerado() 
    {
        puentesGenerados++;
        gameSpeed += AUMENTO_VELOCIDAD; 
    }

    void EmpezarJuego() 
    {
        PararJuego(false);
        juegoComenzado = true;
        pausaActiva = false;
        ui.MostrarPanelJuego();
    }

    public void Salir() 
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Inicio", 1);
    }
}
