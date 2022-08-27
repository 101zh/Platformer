using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool doubleJump;
    private string currentState="";

    private float dirX= 0f;
    [SerializeField] private float moveSpeed= 7f;
    [SerializeField] private float jumpSpeed=14f;

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
        bool canDoubleJump=Item_Collector.canDoubleJump;
        doubleJump=doubleJump && canDoubleJump;

        dirX= Input.GetAxisRaw("Horizontal");
        rb.velocity= new Vector2(moveSpeed*dirX,rb.velocity.y);

        if (isGrounded() && !Input.GetButton("Jump")){
            doubleJump=false;
        }

        if(Input.GetButtonDown("Jump")){

            if((isGrounded() || doubleJump) && !anim.GetBool("isDead")){
                jumpSoundEffect.Play();
                rb.velocity= new Vector3(rb.velocity.x,jumpSpeed,0);
                doubleJump=!doubleJump;

                // Debug.Log("triggered");
            }
        }
        
        updateAnimation();
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
        Debug.Log("state is: "+state);
        ChangeAnimationState(state);
    }

    private bool isGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState==newState || anim.GetBool("isDead")) return;

        anim.Play(newState);
        Debug.Log("changed animation to: "+newState);
        
        currentState=newState;
    }
}
