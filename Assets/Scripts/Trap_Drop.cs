using UnityEngine;

public class Trap_Drop : MonoBehaviour
{
    [SerializeField] private LayerMask obstancleLayer;

    [SerializeField] private float stepDistance = 1;
    [SerializeField] protected float speed = 10f;

    [SerializeField] private Vector3 bottomOffset;
    private Vector2 targetPosition;

    private bool isMoving;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!InputManager.Instance.GetIsReplay())
            return;

        if (isMoving)
        {
            MoveToTarget();
            return;
        }

        FallingStep();

    }

    private bool CheckOnGround()
    {
        if (Physics2D.OverlapCircle(transform.position + bottomOffset,.1f, obstancleLayer))
            return true;
        return false;
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < .01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
    protected void FallingStep()
    {
        if (CheckOnGround())
        {
            return;
        }

        targetPosition = (Vector2)transform.position + Vector2.down * stepDistance;
        isMoving = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + bottomOffset,.2f);
    }
}
