using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private Game_UI ui;
    [SerializeField] public int maxStep;

    private AudioSource audioSource;

    private List<int> actionRecord = new List<int>();

    private bool isRecord;
    private bool isReplay;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        StartRecord();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void StartRecord()
    {
        isRecord = true;
    }

    public void StopRecord()
    {
        isRecord = false;
        isReplay = true;
    }

    public bool GetIsReplay() => isReplay;

    public void AddAction(int action)
    {
        actionRecord.Add(action);

        if (actionRecord.Count > maxStep)
        {
            GameManager.Instance.LostLevel();
            return; 
        }
        int currentStep = maxStep - actionRecord.Count;
        ui.ChangeStepCount(currentStep);

        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.pitch = Random.Range(.9f, 1.1f);
        audioSource.Play();
    }

    public List<int> GetListInputs()
    {
        if (isRecord)
            return null;
        return actionRecord;
    }
}