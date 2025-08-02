using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private List<int> actionRecord = new List<int>();

    private bool isRecord;
    private bool isReplay;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartRecord();
    }

    private void Update()
    {
        Recording();
    }

    private void Recording()
    {
        if (!isRecord)
            return;

        // 0: Left - 1: Right - 2: Jump/Climb Up - 3: Climb Down 
        if (Input.GetKeyDown(KeyCode.D))
            actionRecord.Add(0);
        if (Input.GetKeyDown(KeyCode.A))
            actionRecord.Add(1);
        if (Input.GetKeyDown(KeyCode.Space))
            actionRecord.Add(2);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            actionRecord.Add(3);
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

    public List<int> GetListInputs()
    {
        if (isRecord)
            return null;
        return actionRecord;
    }
}