using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Jumping,
    Falling,
    Climb
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask obstancleLayer;
    [SerializeField] private LayerMask climbLayer;

    [SerializeField] private float stepDistance = 1;
    [SerializeField] private float speed = 5f;
    private Vector2 targetPosition;

    private bool isMoving;
    [SerializeField] private PlayerState currentState;

    private void Start()
    {
        targetPosition = transform.position;
        currentState = PlayerState.Idle;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
            return;
        }

        if (currentState == PlayerState.Falling)
        {
            MoveStep(Vector2.down);
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) Move(Vector2.right);
        if (Input.GetKeyDown(KeyCode.A)) Move(Vector2.left);
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentState == PlayerState.Climb) MoveStep(Vector2.down);
    }

    private void Move(Vector2 dir)
    {
        MoveStep(dir);
        currentState = PlayerState.Moving;
    }

    private void Jump()
    {
        MoveStep(Vector2.up);
        if (currentState != PlayerState.Jumping)
            currentState = PlayerState.Jumping;

        CheckFalling(PlayerState.Jumping);
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < .01f)
        {
            transform.position = targetPosition;
            CheckFalling(PlayerState.Moving);
            CheckClimp();
            isMoving = false;
        }
    }
    private void MoveStep(Vector2 dir)
    {
        if (Physics2D.Raycast(transform.position, dir, stepDistance, obstancleLayer))
        {
            currentState = PlayerState.Idle;
            return;
        }

        targetPosition = (Vector2)transform.position + dir * stepDistance;
        isMoving = true;
    }

    private void CheckFalling(PlayerState state)
    {
        if (currentState == state)
        {
            if (CheckOnGround())
            {
                currentState = PlayerState.Idle;
                return;
            }

            currentState = PlayerState.Falling;
        }
    }

    private bool CheckOnGround()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, stepDistance, obstancleLayer))
            return true;
        return false;
    }

    private void CheckClimp()
    {
        if (Physics2D.OverlapCircle(transform.position, .1f, climbLayer))
            currentState = PlayerState.Climb;
    }
}