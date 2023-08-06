using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 4;
    public Rigidbody2D player;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        animator.SetBool("moving", false);
        if (change != Vector3.zero)
        {
            MovePlayer();
            AnimatePlayerWalk();
        }
    }

    void MovePlayer()
    { 
        player.MovePosition(transform.position + speed * Time.deltaTime * change.normalized);
    }

    void AnimatePlayerWalk()
    {
        animator.SetBool("moving", true);
        animator.SetFloat("moveX", change.x);
        animator.SetFloat("moveY", change.y);
    }
}
