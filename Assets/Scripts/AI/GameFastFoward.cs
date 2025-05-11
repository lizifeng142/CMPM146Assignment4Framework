using UnityEngine;

public class GameFastForward : MonoBehaviour
{
    [Header("Time Settings")]
    public float normalTimeScale = 1f;
    public float fastForwardTimeScale = 3f;

    private bool isFastForwarding = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) // Press F1 to toggle fast forward
        {
            ToggleFastForward();
        }
    }

    void ToggleFastForward()
    {
        isFastForwarding = !isFastForwarding;
        Time.timeScale = isFastForwarding ? fastForwardTimeScale : normalTimeScale;
        Debug.Log("Time scale set to: " + Time.timeScale);
    }

    void OnDisable()
    {
        Time.timeScale = normalTimeScale; // Reset on disable just in case
    }
}