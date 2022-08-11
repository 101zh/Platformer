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
    private string currentState="";

    private float dirX= 0f;
    [SerializeField] private float moveSpeed= 7f;
    [SerializeField] private float jumpSpeed=14f;

    private enum MovmentState {Player_idle, Player_run, Player_jump, Player_fall};

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
        dirX= Input.GetAxisRaw("Horizontal");
        rb.velocity= new Vector2(moveSpeed*dirX,rb.velocity.y);
        
        if(Input.GetButtonDown("Jump") && isGrounded()){
            jumpSoundEffect.Play();
            rb.velocity= new Vector3(rb.velocity.x,jumpSpeed,0);

            // Debug.Log("triggered");
        }

        updateAnimation();

    }

    private void updateAnimation(){

        string state;

        if (dirX>0){
            sprite.flipX=false;
            state=nameof(MovmentState.Player_run);
        } else if (dirX<0){
            sprite.flipX=true;
            state=nameof(MovmentState.Player_run);
        } else {
            state=nameof(MovmentState.Player_idle);
        }

        if (rb.velocity.y>0.1f){
            state= nameof(MovmentState.Player_jump);
        } else if (rb.velocity.y<-0.1f){
            state = nameof(MovmentState.Player_fall);
        }

        ChangeAnimationState(state);
    }

    private bool isGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void ChangeAnimationState(string newState)
    {
        if (isPlaying(anim,"Player_death")) return;
        if (currentState==newState) return;

        anim.Play(newState);
        
        currentState=newState;
    }

    private bool isPlaying(Animator anim, string stateName)
    {
    if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        return true;
    else
        return false;
    }
}
