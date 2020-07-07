using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    
    public Transform groundCheck;
    public LayerMask layerground;
    public float speed = 10f;
    public float jumpforce = 200f;


    Rigidbody2D rb; 
    SpriteRenderer sprite;
    bool isfloor = false;
    bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isfloor = Physics2D.Linecast(transform.position, groundCheck.position, layerground);     

        if(Input.GetButtonDown("Jump") && isfloor == true)
        {
            isJumping = true;
        }
    }

    void FixedUpdate() 
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2 (move * speed, rb.velocity.y); 

        if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            Flip();
        }

        if(isJumping)
        {
            rb.AddForce(new Vector2(0, jumpforce));
            isJumping = false;
        }
    }

    void Flip()
    {
        sprite.flipX = !sprite.flipX;
        
    }

    void Movimentacao()
    {

    }

    void Inputs()
    {

    }
}
