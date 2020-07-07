using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb; // para podermos aplicar velo
    public float speed = 10f;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2 (move * speed, rb.velocity.y); 

        if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            Flip();
        }
    }

    void Flip()
    {
        sprite.flipX = !sprite.flipX;
        
    }
}
