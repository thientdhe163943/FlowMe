using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : PlayerController
{
    [Header("Following Details")]
    [SerializeField] private float duration = 10f;

    private List<int> inputDatas;
    private int currentAction = 0;
    private bool isFollower;

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.P))
            StartFollow();

    }

    protected override void SetInput()
    {

        if (!inputManager.GetIsReplay())
            return;
        inputDatas = inputManager.GetListInputs();

        if (!isFollower)
            return;
        StartCoroutine(followCo(inputDatas));
    }

    private void StartFollow()
    {
        isFollower = true;
    }

    private IEnumerator followCo(List<int> inputDatas)
    {
        while (currentAction < inputDatas.Count)
        {
            Following(inputDatas[currentAction]);
            currentAction++;
            yield return new WaitForSeconds(duration);
        }
    }

    private void Following(int action)
    {
        if (action == 0) Move(Vector2.right);
        if (action == 1) Move(Vector2.left);
        if (action == 2) Jump();
        if (action == 3 && currentState == PlayerState.Climb) MoveStep(Vector2.down);
    }

    protected override void EndControl()
    {
        if (!Physics2D.OverlapCircle(transform.position, .1f, endPointLayer))
            return;
        currentState = PlayerState.Idle;

        Debug.Log("You win");
    }

}