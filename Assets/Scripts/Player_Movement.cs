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

    private float dirX= 0f;
    [SerializeField] private float moveSpeed= 7f;
    [SerializeField] private float jumpSpeed=14f;

    private enum MovmentState {idle, running, jumping, falling};

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
            rb.velocity= new Vector3(rb.velocity.x,jumpSpeed,0);
            // Debug.Log("triggered");
        }

        updateAnimation();

    }

    private void updateAnimation(){

        MovmentState state;

        if (dirX>0){
            sprite.flipX=false;
            state=MovmentState.running;
        } else if (dirX<0){
            sprite.flipX=true;
            state=MovmentState.running;
        } else {
            state=MovmentState.idle;
        }

        if (rb.velocity.y>0.1f){
            state= MovmentState.jumping;
        } else if (rb.velocity.y<-0.1f){
            state = MovmentState.falling;
        }

        anim.SetInteger("PlayerState", (int)state);
    }

    private bool isGrounded(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
