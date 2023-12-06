using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    // Collider2D playerhb;
    // Collider2D terrainhb;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    [SerializeField] private LayerMask jumpableGround;
    private bool canDoubleJump;
    private bool doubleJumped;
    private string currentState="";

    private float dirX= 0f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    Item_Collector item_Collector;

    private enum AnimState {Player_idle, Player_run, Player_jump, Player_fall, Player_death};

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        Debug.Log("Started");
        rb = GetComponent<Rigidbody2D>();
        coll= GetComponent<BoxCollider2D>();
        anim= GetComponent<Animator>();
        sprite= GetComponent<SpriteRenderer>();
        // playerhb=GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        // terrainhb=GameObject.FindGameObjectWithTag("Terrain").GetComponent<Collider2D>();
    }

    // Update is called once per frame 
    private void Update()
    {
        // Double Jump Variable
        canDoubleJump=Item_Collector.canDoubleJump;
        // Can Teleport Variable
        bool canTeleport=Item_Collector.canTeleport;
        // Can Shoot Fire variable
        bool canShootFire=Item_Collector.canShootFire;

        dirX= Input.GetAxisRaw("Horizontal");
        rb.velocity= new Vector2(moveSpeed*dirX,rb.velocity.y);

        if (isGrounded() && !Input.GetButton("Jump")){
            doubleJumped=false;
        }

        if(Input.GetButtonDown("Jump") && !anim.GetBool("isDead")){

            if(isGrounded()){
                jumpSoundEffect.Play();
                rb.velocity= new Vector3(rb.velocity.x,jumpSpeed,0);
            } else if (canDoubleJump&&!doubleJumped){

                jumpSoundEffect.Play();
                rb.velocity= new Vector3(rb.velocity.x,jumpSpeed,0);
                doubleJumped=true;
            }
        }
        
        if(Input.GetButtonDown("Use Ability")){

            float teleportDistance=7f;
            int facingDirection=1;
            if(sprite.flipX) facingDirection=-1;

            if (canTeleport){
                Debug.Log("Teleported");
                teleport(teleportDistance, facingDirection, jumpableGround);
            } else if (canShootFire){
                Debug.Log("Shot Fire");
            }
        }

        if(transform.position.y<=-10) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        updateAnimation();
    }

    private void teleport(float MaxTeleportDistance, int FacingDirection, LayerMask TerrainMask){
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, new Vector2(FacingDirection, 0), MaxTeleportDistance, TerrainMask);
        if (rayHit)
        {
            // RayCastHit2D.fraction will give you the length of the ray minus any collision depth
            transform.Translate(new Vector3(FacingDirection * (MaxTeleportDistance-rayHit.fraction), 0, 0));
        }
        else
        {
            transform.Translate(new Vector3(FacingDirection * MaxTeleportDistance, 0, 0));
        }
    }

    private void updateAnimation(){

        string state;

        if (dirX>0){
            sprite.flipX=false;
            state=nameof(AnimState.Player_run);
        } else if (dirX<0){
            sprite.flipX=true;
            state=nameof(AnimState.Player_run);
        } else {
            state=nameof(AnimState.Player_idle);
        }

        if (rb.velocity.y>0.1f){
            state= nameof(AnimState.Player_jump);
        } else if (rb.velocity.y<-0.1f){
            state = nameof(AnimState.Player_fall);
        }
        ChangeAnimationState(state);
    }

    private bool isGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState==newState || anim.GetBool("isDead")) return;

        anim.Play(newState);
        
        currentState=newState;
    }
}
