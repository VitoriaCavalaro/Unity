using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    
   // public Transform groundCheck;
    public LayerMask layerground;
    public float speed = 10f;
    public float jumpforce = 200f;
    public float radius = 0.2f;
  


    Rigidbody2D rb; 
    SpriteRenderer sprite;
    Animator anim;
    bool isfloor = false;
    bool isJumping = false;
    int extraJumps = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        PlayerAnimation();
        Movimentacao();
        Inputs();
        CheckJump();
    }

    
    void Movimentacao()
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            sprite.flipX = !sprite.flipX;
        }
    }

    void Inputs()
    {
        //if (Input.GetButtonDown("Jump") && isfloor == true) pulo unico
        if(Input.GetButtonDown("Jump") && extraJumps > 0) // ele pode pular mais de uma vez
        {
            isJumping = true;
            extraJumps--;
        }

        
    }

    void CheckJump()
    {
        //isfloor = Physics2D.Linecast(transform.position, groundCheck.position, layerground); // verifica se esta no chao mas é melyhor usar o overlap pq se ficar na beirada nao cai.

        //isfloor = Physics2D.OverlapCircle(groundCheck.position, radius, layerground); // cria o circle de verificacao do chao 

        isfloor = rb.IsTouchingLayers(layerground); //checa diretamente a layer, é bom pra usar se tiver uma parede na layer mas podemos criar outra layer pra parede 

        if (isJumping) // checa se esta no chao
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // adiciono pro pulo duplo (zero a posY para que ele possa pegar impulso novamente no ar)
            rb.AddForce(new Vector2(0, jumpforce));
            isJumping = false;
        }

        if(rb.velocity.y > 0 && !Input.GetButton("Jump")) // pulo curto
        {
            rb.velocity += Vector2.up * -0.8f; // força para que se ele apenas aperatr o botao, ele pula baixo
        }

        if(isfloor)
        {
            extraJumps = 1;
        }
    }

    //private void OnDrawGizmosSelected() // usa junto com o overlapcircle
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(groundCheck.position, radius);
    //}

    void PlayerAnimation()
    {
        if(rb.velocity.x == 0 && rb.velocity.y == 0) // corpo parado
        {
            anim.Play("Idle");
        }
        else if (rb.velocity.x != 0 && rb.velocity.y == 0) // corpo movendo no eixo x
        {
            anim.Play("Walk");
        }
        else if (rb.velocity.y != 0) // corpo movendo no eixo y
        {
            anim.Play("Jump");
        }
    }
}
