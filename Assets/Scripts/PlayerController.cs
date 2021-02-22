using UnityEngine;

namespace AGDDPlatformer
{
    public class PlayerController : KinematicObject
    {
        [Header("Movement")]
        public float maxSpeed = 7;
        public float jumpSpeed = 7;
        public float jumpDeceleration = 0.5f; // Upwards slow after releasing jump button
        public float cayoteTime = 0.1f; // Lets player jump just after leaving ground
        public float jumpBufferTime = 0.1f; // Lets the player input a jump just before becoming grounded

        [Header("Audio")]
        public AudioSource source;
        public AudioClip jumpSound;

        Vector2 startPosition;
        bool startOrientation;

        float lastJumpTime;
        float lastGroundedTime;
        public bool canJump;
        bool jumpReleased;
        public Vector2 move;
        public bool gravityShift = false;
        public bool getGravity = true;
        public float orignalGravity;
        public bool flipped;

        public SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            lastJumpTime = -jumpBufferTime * 2;
            startPosition = transform.position;
            if (getGravity)
            {
                orignalGravity = gravityModifier;
                getGravity = false;
            }
            startOrientation = spriteRenderer.flipX;
        }

        public void setGravity()
        {
            gravityModifier = orignalGravity;
        }

        void Update()
        {
            isFrozen = GameManager.instance.timeStopped;

            /* --- Read Input --- */

            move.x = Input.GetAxisRaw("Horizontal");
            if (gravityModifier < 0)
            {
                move.x *= -1;
            }

            if (Input.GetButtonDown("Jump"))
            {
                // Store jump time so that we can buffer the input
                lastJumpTime = Time.time;
            }

            if (Input.GetButtonUp("Jump"))
            {
                jumpReleased = true;
            }

            /* --- Compute Velocity --- */

            // Store grounded time to allow for late jumps
            if (isGrounded)
            {
                lastGroundedTime = Time.time;
                canJump = true;
            }

            // Allow for buffered jumps and late jumps
            float timeSinceJumpInput = Time.time - lastJumpTime;
            float timeSinceLastGrounded = Time.time - lastGroundedTime;

            if (canJump && timeSinceJumpInput <= jumpBufferTime && timeSinceLastGrounded <= cayoteTime)
            {
                velocity.y = Mathf.Sign(gravityModifier) * jumpSpeed;
                canJump = false;
                isGrounded = false;

                source.PlayOneShot(jumpSound);
            }
            else if (jumpReleased)
            {
                // Decelerate upwards velocity when jump button is released
                if ((gravityModifier >= 0 && velocity.y > 0) ||
                    (gravityModifier < 0 && velocity.y < 0))
                {
                    velocity.y *= jumpDeceleration;
                }
                jumpReleased = false;
            }
            
            

            velocity.x = move.x * maxSpeed;

            if (gravityShift)
            {

                velocity.x *= -1;

            }

            /* --- Adjust Sprite --- */

            // Assume the sprite is facing right, flip it if moving left
            if (move.x > 0.01f)
            {
                if (flipped)
                {
                    spriteRenderer.flipX = true;
                }
                else 
                {
                    spriteRenderer.flipX = false;
                }
                
            }
            else if (move.x < -0.01f)
            {
                if (flipped)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
            }

            
        }

        public void ResetPlayer()
        {
            gravityModifier = orignalGravity;
            transform.position = startPosition;
            if (spriteRenderer.flipY == startOrientation)
            {
                spriteRenderer.flipX = startOrientation;
            }
            else
            {
                spriteRenderer.flipY = startOrientation;
                spriteRenderer.flipX = startOrientation;
            }
            
            
            

            lastJumpTime = -jumpBufferTime * 2;

            velocity = Vector2.zero;
        }
    }
}
