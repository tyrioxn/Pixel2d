using System.Collections;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float velocidad;
    private Rigidbody2D fisica;
    public float arriba;
    private SpriteRenderer giro;
    private Animator AnimacionJugador;
    public GameObject disparao1;
    private bool disparando = false;
    public int Puntuacion;
    public int vidas;

    private bool vulnerable;

    // Nueva variable para controlar el tiempo entre disparos
    private float tiempoUltimoDisparo = 0f;
    public float delayDisparo = 1.5f; // Tiempo de espera entre disparos (en segundos)

    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        giro = GetComponent<SpriteRenderer>();
        AnimacionJugador = GetComponent<Animator>();
        Puntuacion = 0;
        vidas = 3; // Inicializa las vidas
        vulnerable = true;
    }

    // Todas las físicas del juego aquí
    void FixedUpdate()
    {
        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        transform.Translate(move, 0f, 0f);
        fisica.velocity = new Vector2(move * velocidad, fisica.velocity.y);
    }

    void Update()
    {
        // Lógica de disparo con delay
        if (Input.GetKey(KeyCode.Space) && Time.time >= tiempoUltimoDisparo + delayDisparo) // Comprobar si ha pasado el tiempo de espera
        {
            // Instancia el proyectil en la posición del jugador y con su rotación
            Instantiate(disparao1, transform.position, transform.rotation);
            disparando = true; // Marcar como disparando
            AnimacionJugador.Play("jugador-disparo"); // Reproducir la animación de disparo

            // Actualiza el tiempo del último disparo
            tiempoUltimoDisparo = Time.time;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            disparando = false; // Dejar de disparar
        }

        // ASIGNAMOS TECLA PARA SALTAR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // AÑADIMOS FUERZA DE SALTO
            if (Suelo()) fisica.AddForce(Vector2.up * arriba, ForceMode2D.Impulse);
        }

        // Hace flip en el eje x para dar la vuelta al sprite
        if (fisica.velocity.x < 0f) giro.flipX = true;
        else if (fisica.velocity.x > 0f) giro.flipX = false;

        AnimarJugador();
    }

    public void TerminarDisparo()
    {
        disparando = false; // Restablecer el estado de disparo
    }

    // Mira si el jugador toca el suelo para el siguiente salto
    private bool Suelo()
    {
        RaycastHit2D siToca = Physics2D.Raycast(transform.position + new Vector3(0f, -2f, 0f), Vector2.down, 0.2f);
        return siToca.collider != null;
    }

    public void finDeJuego()
    {
        Debug.Log("Puntuación Final: " + Puntuacion);

        Time.timeScale = 0; 

    }

    private void AnimarJugador()
    {
        // Solo ejecuta la animación de parado si no se está disparando
        if (fisica.velocity.y == 0f && fisica.velocity.x == 0f && !disparando)
        {
            AnimacionJugador.Play("jugador-parao");
        }
        else if (fisica.velocity.y == 0f && fisica.velocity.x != 0f && !disparando)
        {
            AnimacionJugador.Play("jugador-correr");
        }
        else if (!Suelo() && !disparando)
        {
            AnimacionJugador.Play("jugador-salto");
        }
    }

    public void IncrementarPuntos(int puntos)
    {
        Puntuacion += puntos;
    }

    public void QuitarVidas()
    {
        if (vulnerable)
        {
            vulnerable = false;
            vidas--;
            if (vidas <= 0) finDeJuego();
            Invoke("HacerVulnerable", 1f);
            giro.color = Color.red;
        }
    }

    private void HacerVulnerable()
    {
        vulnerable = true;
        giro.color = Color.white;
    }
}