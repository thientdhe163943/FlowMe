using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetFollower;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float duration;


    [Header("Settings")]
    [SerializeField] private Vector2 minXY;
    [SerializeField] private Vector2 maxXY;


    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No target has been specified...");
            return;
        }
        Vector3 targetPosition = target.position;
        if (InputManager.Instance.GetIsReplay())
            targetPosition = targetFollower.position;


        targetPosition.z = -10;
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, minXY.x, maxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minXY.y, maxXY.y);

        transform.position = Vector3.Lerp(transform.position,targetPosition + offset,duration*Time.deltaTime);
    }
}
