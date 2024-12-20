using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class PlayerControler : MonoBehaviour
{
    public AudioClip Laser_Shoot_ogg;
    public AudioClip hit;
    public AudioClip jump;
    private AudioSource audioSource;
    #region 
    public float velocidad;
    private Rigidbody2D fisica;
    public float arriba;
    private SpriteRenderer giro;
    private Animator AnimacionJugador;
    public GameObject disparao1; // Prefab del disparo
    private bool disparando = false;
    public int Puntuacion;
    public int vidas;

    private bool vulnerable;
    #endregion
    // Nueva variable para controlar el tiempo entre disparos
    private float tiempoUltimoDisparo = 0f;
    public float delayDisparo = 1.5f; // Tiempo de espera entre disparos (en segundos)

    void Awake(){  audioSource = GetComponent<AudioSource>(); }
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        giro = GetComponent<SpriteRenderer>();
        AnimacionJugador = GetComponent<Animator>();
        Puntuacion = 0;
        vidas = 3; // Inicializa vidas si es necesario
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
            audioSource.PlayOneShot(Laser_Shoot_ogg, 0.7F);
            // Instancia el proyectil en la posición del jugador y con su rotación
            GameObject nuevoDisparo = Instantiate(disparao1, transform.position, transform.rotation);
            
            // Establecer la dirección del disparo según la dirección del jugador
            Vector2 direccionDisparo;

            // Comprobar la dirección del jugador
            if (giro.flipX) // Si el sprite está girado hacia la izquierda
            {
                direccionDisparo = Vector2.left; // Dispara hacia la izquierda
            }
            else
            {
                direccionDisparo = Vector2.right; // Dispara hacia la derecha
            }

            nuevoDisparo.GetComponent<Disparo>().SetDireccion(direccionDisparo);
            nuevoDisparo.GetComponent<Disparo>().puntosPorImpacto = 10; // Cambia 10 por el valor que desees

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
            audioSource.PlayOneShot(jump, 0.7F);
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
    // Guardar la puntuación final y el estado del jugador (ganado/perdido)
    PlayerPrefs.SetInt("PuntuacionFinal", Puntuacion);
    PlayerPrefs.SetInt("HaGanado", vidas > 0 ? 1 : 0); // Si tiene vidas restantes, gana, si no, pierde

    // Mostrar la puntuación final en la consola (opcional)
    Debug.Log("Puntuación Final: " + Puntuacion);

    // Pausar el juego y cargar la escena de fin de nivel
    Time.timeScale = 0; // Pausar el juego para dar tiempo de ver el resultado

    // Asegúrate de que el nombre de la escena esté correctamente escrito en Build Settings
    SceneManager.LoadScene("Menu"); // Cambia "Menu" si tu escena tiene otro nombre
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
        Puntuacion += puntos; // Incrementar la puntuación del jugador
    }

    public void QuitarVidas()
    {
        if (vulnerable)
        {
            audioSource.PlayOneShot(hit, 0.7F);

            vulnerable = false;
            vidas--;
            if (vidas <= 0) finDeJuego(); // Si las vidas llegan a 0, termina el juego
            Invoke("HacerVulnerable", 1f); // Reinicia la vulnerabilidad después de 1 segundo
            giro.color = Color.red; // Cambiar color al ser golpeado
        }
    }

    private void HacerVulnerable()
    {
        vulnerable = true; // Restablecer vulnerabilidad
        giro.color = Color.white; // Restaurar color original
    }
}
