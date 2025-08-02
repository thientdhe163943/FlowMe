using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private PlayerState state;
    [SerializeField] private string idle, moving, jumping, falling;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        state = playerController.currentState;

        switch (state)
        {
            case PlayerState.Idle:
                animator.Play(idle);
                break;
            case PlayerState.Climb:
                animator.Play(idle);
                break;
            case PlayerState.Moving:
                animator.Play(moving);
                break;
            case PlayerState.Jumping:
                animator.Play(jumping);
                break;
            case PlayerState.Falling:
                animator.Play(falling);
                break;
        }
    }
    
}
