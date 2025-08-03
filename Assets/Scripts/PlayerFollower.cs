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

    }

    protected override void SetInput()
    {

        if (!inputManager.GetIsReplay())
            return;
        inputDatas = inputManager.GetListInputs();

        StartCoroutine(DelayFollower(1));

        if (!isFollower)
            return;
        StartCoroutine(followCo(inputDatas));
    }

    private IEnumerator DelayFollower(float duration)
    {
        yield return new WaitForSeconds(duration);
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
        if (!GameManager.Instance.GetIsPlay())
            return;
        if (!Physics2D.OverlapCircle(transform.position, .1f, endPointLayer))
            return;
        currentState = PlayerState.Idle;

        GameManager.Instance.WinLevel();
    }

}