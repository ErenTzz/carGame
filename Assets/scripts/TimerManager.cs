using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    public float timeLimit = 30f;
    public TMP_Text timerText;
    public Transform player; // araba objesi
    public Vector3 offset = new Vector3(0, 2.5f, 0); // dünya üzerindeki offset
    public CollectibleManager collectibleManager;
    public GameObject endPanel; // result panel UI

    private float currentTime;
    private bool running = true;

    private void Start()
    {
        currentTime = timeLimit;
        if (collectibleManager == null)
            collectibleManager = FindObjectOfType<CollectibleManager>();
        if (timerText == null)
            Debug.LogWarning("TimerText not assigned!");
    }

    private void Update()
    {
        if (!running) return;

        currentTime -= Time.deltaTime;
        if (currentTime < 0) currentTime = 0;

        // Update visual text
        if (timerText != null)
        {
            timerText.text = $"Time: {currentTime:F1}s";
            // world-follow: convert player position to screen
            if (player != null)
            {
                var screenPos = Camera.main.WorldToScreenPoint(player.position + offset);
                timerText.transform.position = screenPos;
            }
        }

        if (currentTime <= 0f)
        {
            running = false;
            OnTimeUp();
        }
    }

    private void OnTimeUp()
    {
        int successCount = collectibleManager.GetSuccessCount();
        int totalCollected = collectibleManager.GetTotalCollected();
        int score = totalCollected * successCount;

        Debug.Log($"Time's up! Total: {totalCollected}, SuccessTypes: {successCount}, Score: {score}");

        // Show end panel with values
        if (endPanel != null)
        {
            endPanel.SetActive(true);
            // assume endPanel has children TMP_Text components - find and assign them as you like
            var texts = endPanel.GetComponentsInChildren<TMP_Text>();
            foreach (var t in texts)
            {
                if (t.name == "TotalCollectedText") t.text = $"Toplam Obje: {totalCollected}";
                if (t.name == "SuccessCountText") t.text = $"Tamamlanan Türler: {successCount}";
                if (t.name == "ScoreText") t.text = $"Skor: {score}";
            }
        }
    }
}
