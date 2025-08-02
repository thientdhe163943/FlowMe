using System.Collections.Generic;
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
    protected InputManager inputManager;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask obstancleLayer;
    [SerializeField] private LayerMask climbLayer;
    [SerializeField] protected LayerMask endPointLayer;
    [Space]
    [Header("Player Setting")]
    [SerializeField] private float stepDistance = 1;
    [SerializeField] protected float speed = 5f;

    private Vector2 targetPosition;
    public PlayerState currentState;

    private bool isMoving;
    private bool canMove;
    private bool facingRight = true;

    protected virtual void Start()
    {
        inputManager = InputManager.Instance;

        targetPosition = transform.position;
        currentState = PlayerState.Idle;

        canMove = true;
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

        EndControl();
        SetInput();
    }

    protected virtual void SetInput()
    {
        if (!canMove)
            return;

        if (Input.GetKeyDown(KeyCode.D)) Move(Vector2.right);
        if (Input.GetKeyDown(KeyCode.A)) Move(Vector2.left);
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentState == PlayerState.Climb) MoveStep(Vector2.down);
    }

    protected void Move(Vector2 dir)
    {
        MoveStep(dir);
        currentState = PlayerState.Moving;
        Flip(dir.x);
    }

    protected void Jump()
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
    protected void MoveStep(Vector2 dir)
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

    protected virtual void EndControl()
    {
        if (canMove == false)
            return;

        if (!Physics2D.OverlapCircle(transform.position, .1f, endPointLayer))
            return;

        currentState = PlayerState.Idle;
        canMove = false;

        inputManager.StopRecord();
    }

    public void Flip(float moveInput)
    {
        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}