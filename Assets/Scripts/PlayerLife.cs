using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    public Transform respawnPoint;
    
    [SerializeField] private AudioSource deathSoundEffect;
    
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        anim.SetTrigger("death");
        deathSoundEffect.Play();
        transform.position = respawnPoint.position;
        anim.SetTrigger("create");
    }
}
