using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public bool tomoudano;
    public GameObject player;
   

    public void TomouDano()
    {
       
            anim.SetTrigger("dano");
        
    }

 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().PerdeVida();
        }
    }
}
