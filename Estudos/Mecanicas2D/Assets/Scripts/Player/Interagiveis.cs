using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interagiveis : MonoBehaviour
{
    public GameObject player;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "morte")
        {
            player.GetComponent<PlayerController>().IsAlive = false;
        }
    }
}
