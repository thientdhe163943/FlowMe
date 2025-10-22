using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    public float hoverScale = 1.1f;
    public float scaleSpeed = 8f;

    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public Image buttonImage;
    public Color normalColor = new Color32(0, 0, 0, 76);
    public Color hoverColor = new Color32(142, 151, 255, 100);

    private bool isHovering = false;

    void Start()
    {
        originalScale = transform.localScale;
        if (buttonImage == null) buttonImage = GetComponent<Image>();
            buttonImage.color = normalColor;
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 targetScale = isHovering ? originalScale * hoverScale : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        buttonImage.color = hoverColor;
        if (hoverSound && audioSource)
            audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        buttonImage.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound && audioSource)
            audioSource.PlayOneShot(clickSound);
        isHovering = false;
        buttonImage.color = normalColor;
    }
}
