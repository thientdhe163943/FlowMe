using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private Sprite spikeUp;
    [SerializeField] private Sprite spikeDown;
    [SerializeField] private bool isActive = true;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        UpdateState();
    }

    public void ToggleState()
    {
        isActive = !isActive;
        UpdateState();
    }

    private void UpdateState()
    {
        spriteRenderer.sprite = isActive ? spikeUp : spikeDown;
        boxCollider.enabled = isActive;
    }
}
