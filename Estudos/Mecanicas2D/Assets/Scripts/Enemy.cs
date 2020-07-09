using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public bool tomoudano;

   

    public void TomouDano()
    {
       
            anim.SetTrigger("dano");
        
    }
   
}
