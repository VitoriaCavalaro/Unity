using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Caracteristicas do Player")]
    public bool IsAlive = true;
    public int health = 5;
    public Transform posInicial;
    

    [Header("Variaveis de Movimentação")]
    public Transform groundCheck;
    public LayerMask layerground;
    public float speed = 10f;
    public float jumpforce = 200f;
    public float radius = 0.2f;

    [Header("Variaveis de Ataque")]
    public Transform atkCheck;
    public float radiusAtk;
    public LayerMask layerEnemies; //layerobjetos dps criar
    float timeNextAtk;
    public Enemy scriptenemy;


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

        transform.position = posInicial.position;
        health = 5;
    }


    void Update()
    {
        if(IsAlive == true)
        {
            PlayerAnimation();
            Movimentacao();
            Inputs();
            CheckJump();
        }
        else
        {
            PlayerMorreu();
        }
        
    }

    
    void Movimentacao()
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            sprite.flipX = !sprite.flipX;
            atkCheck.localPosition = new Vector2(-atkCheck.localPosition.x, atkCheck.localPosition.y);
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

        if(timeNextAtk <= 0)
        {
            if (Input.GetButtonDown("Fire1") && rb.velocity == new Vector2 (0,0))
            {
                anim.SetTrigger("Atack");
                timeNextAtk = 0.2f;
                
            }
        }
        else
        {
            timeNextAtk -= Time.deltaTime;
        }
         
    }

    public void PerdeVida()
    {
        health--;
        if (health <= 0)
        {
            IsAlive = false;
        }
    }

    public void Attack()
    {
        Collider2D [] enemiesAttack = Physics2D.OverlapCircleAll(atkCheck.position, radiusAtk, layerEnemies);
        for(int i = 0; i < enemiesAttack.Length; i++)
        {
            scriptenemy.TomouDano();
            Debug.Log(enemiesAttack[i].name);
        }
        
    }

    void CheckJump()
    {
        //isfloor = Physics2D.Linecast(transform.position, groundCheck.position, layerground); // verifica se esta no chao mas é melyhor usar o overlap pq se ficar na beirada nao cai.

        //isfloor = rb.IsTouchingLayers(layerground); //checa diretamente a layer, é bom pra usar se tiver uma parede na layer mas podemos criar outra layer pra parede 

        isfloor = Physics2D.OverlapCircle(groundCheck.position, radius, layerground); // cria o circle de verificacao do chao . Melhor forma

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

    private void OnDrawGizmosSelected() // usa junto com o overlapcircle
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.DrawWireSphere(atkCheck.position, radiusAtk);
    }

    void PlayerAnimation()
    {
        anim.SetFloat("VelX",  Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VelY", Mathf.Abs(rb.velocity.y));
    }

    public void PlayerMorreu()
    {
        if(IsAlive == false)
        {
            Invoke("ReloadLevel", 0.5f);
        }
        

    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
