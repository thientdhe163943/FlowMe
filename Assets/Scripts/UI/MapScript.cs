using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapViewController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Camera Settings")]
    public Camera mainCamera;

    [Header("Normal View")]
    public Transform normalTarget;
    public float normalSize = 5f;

    [Header("Map View")]
    public Vector3 mapViewPosition;
    public float mapViewSize = 25f;

    [Header("Transition")]
    public float moveSpeed = 5f;
    public float zoomSpeed = 5f;

    private bool isMapView = false;
    private bool isHoldingButton = false;

    void Update()
    {
        if (mainCamera == null || normalTarget == null) return;

        // Nếu giữ phím M hoặc giữ nút UI
        isMapView = Input.GetKey(KeyCode.M) || isHoldingButton;

        Vector3 targetPos = isMapView ? mapViewPosition : normalTarget.position;
        float targetSize = isMapView ? mapViewSize : normalSize;

        // Di chuyển mượt
        mainCamera.transform.position = Vector3.Lerp(
            mainCamera.transform.position,
            new Vector3(targetPos.x, targetPos.y, mainCamera.transform.position.z),
            Time.deltaTime * moveSpeed
        );

        // Zoom mượt
        mainCamera.orthographicSize = Mathf.Lerp(
            mainCamera.orthographicSize,
            targetSize,
            Time.deltaTime * zoomSpeed
        );
    }

    // ✅ Chỉ cần 1 nút UI, tự nhận biết nhấn/thả
    public void OnPointerDown(PointerEventData eventData)
    {
        isHoldingButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHoldingButton = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        float aspect = 16f / 9f;
        float halfHeight = mapViewSize;
        float halfWidth = mapViewSize * aspect;

        Gizmos.DrawWireCube(mapViewPosition, new Vector3(halfWidth * 2, halfHeight * 2, 0));
    }
}
