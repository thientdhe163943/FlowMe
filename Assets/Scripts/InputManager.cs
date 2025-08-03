using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private Game_UI ui;

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
        ui.ChangeStepCount(actionRecord.Count);
    }

    public List<int> GetListInputs()
    {
        if (isRecord)
            return null;
        return actionRecord;
    }
}