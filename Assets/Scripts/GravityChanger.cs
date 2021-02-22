using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGDDPlatformer
{
    public class GravityChanger : MonoBehaviour
    {
        // flip player model
        
        PlayerController playerController;
        ParticleSystem particleSystem;
        bool isChanging = false;
        public bool isOnline = true;
        


        void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
            
        }

        private void Update()
        {
                
            
                if (playerController)
                {
                
                    if (playerController.isGrounded && isChanging)
                    {

                        playerController.gravityShift = false;
                        isChanging = false;
                        isOnline = true;
                    }
                }

            if (isOnline)
            {
                var main = particleSystem.main;
                main.startColor = Color.green;
            }
            else
            {
                var main = particleSystem.main;
                main.startColor = Color.red;
            }
            
            
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isOnline)
            {
                var main = particleSystem.main;
                playerController = collision.GetComponentInParent<PlayerController>();
                playerController.gravityModifier *= -1;
                playerController.gravityShift = true;
                isChanging = true;
                isOnline = false;
                playerController.flipped = !(playerController.flipped);
                main.startColor = Color.red;
                playerController.isGrounded = false;
                playerController.spriteRenderer.flipY = !(playerController.spriteRenderer.flipY);
                
            }
            
        }
    }
}
