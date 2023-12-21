using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerSpeed;
    [SerializeField] int jumpForce;
    [SerializeField] float lineLength = 1f;
    [SerializeField] float offset = 1f;
    [SerializeField] ParticleSystem particles;

    [SerializeField] bool isJumping = false;

    void Start()
    {

    }

    void Update()
    {

        GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed * Input.GetAxisRaw("Horizontal"), GetComponent<Rigidbody2D>().velocity.y);
        if (Input.GetAxisRaw("Horizontal") == 1) GetComponent<SpriteRenderer>().flipX = false;
        if (Input.GetAxisRaw("Horizontal") == -1) GetComponent<SpriteRenderer>().flipX = true;



        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            AudioManager.instance.PlaySFX("Jump");
            particles.Play();

        }


        Vector2 origin = new Vector2(transform.position.x, transform.position.y - offset);
        Vector2 target = new Vector2(transform.position.x, transform.position.y - offset - lineLength);
        Debug.DrawLine(origin, target, Color.black);

        RaycastHit2D raycast = Physics2D.Raycast(origin, Vector2.down, lineLength);

        if (raycast.collider == null)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        // ------------------------------------------------------------------------------------------
        // APLICACI�N AL JUEGO DE PLATAFORMAS QUE UTILIZA RAYCAST PARA DETECTAR QUE EST� EN EL SUELO
        // ------------------------------------------------------------------------------------------
        // Si el raycast no toca con nada el personaje est� en el aire
        if (raycast.collider == null)
        {
            SetAnimation("Jump");
        }
        else
        {
            // Si est� sobre una superficie pero se mueve lateralmente
            if (GetComponent<Rigidbody2D>().velocity.x != 0) SetAnimation("Run");
            else SetAnimation("Idle"); // Si est� sobre una superficie pero no se mueve
        }

    }

    void SetAnimation(string name)
    {

        // Obtenemos todos los par�metros del Animator
        AnimatorControllerParameter[] parametros = GetComponent<Animator>().parameters;

        // Recorremos todos los par�metros y los ponemos a false
        foreach (var item in parametros) GetComponent<Animator>().SetBool(item.name, false);

        // Activamos el pasado por par�metro
        GetComponent<Animator>().SetBool(name, true);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision != null)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                AudioManager.instance.PlaySFX("Hit");
                AudioManager.instance.PlayMusic("LoseALife");
                SceneController.instance.LoadScene("Gameover");

            }
        }
    }
}
