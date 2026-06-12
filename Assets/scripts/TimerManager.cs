using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    [Serializable]
    public struct EndGameUIReference
    {
        public string collectibleName;
        public TMP_Text successAmountText;
        public TMP_Text failureAmountText;
    }
    public System.Collections.Generic.List<EndGameUIReference> endGameUIReferences;

    [Header("Zaman Ayarları")]
    public float timeLimit = 30f;
    public TMP_Text timerText;

    [Header("Başarı Şartı")]
    public int successThreshold = 3; // Not used for logic anymore, but kept for Inspector compatibility
    public CollectibleManager collectibleManager;

    [Header("Bağlantılar")]
    public Transform player;
    public CarController carController;
    public Vector3 offset = new Vector3(0, 2.5f, 0);

    [Header("UI Panelleri")]
    public GameObject successPanel;
    public GameObject failurePanel;
    public GameObject inGameUI;

    private float currentTime;
    private bool running = true;
    private bool gameEnded = false;

    private void Start()
    {
        currentTime = timeLimit;

        if (collectibleManager == null)
            collectibleManager = FindObjectOfType<CollectibleManager>();

        if (player != null && carController == null)
            carController = player.GetComponent<CarController>();

        if (timerText == null)
            Debug.LogWarning("TimerText not assigned!");

        if (successPanel != null) successPanel.SetActive(false);
        if (failurePanel != null) failurePanel.SetActive(false);
        if (inGameUI != null) inGameUI.SetActive(true);
    }

    private void Update()
    {
        if (!running || gameEnded) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            
            // Check win condition only when time runs out
            bool success = false;
            if (collectibleManager != null)
            {
                 success = collectibleManager.AllGoalsReached();
            }
            EndGame(success);
        }
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {currentTime:F1}s";
            if (player != null)
            {
                var screenPos = Camera.main.WorldToScreenPoint(player.position + offset);
                timerText.transform.position = screenPos;
            }
        }
    }

    private void EndGame(bool didWin)
    {
        if (gameEnded) return;

        gameEnded = true;
        UpdateEndGameUI(didWin);
        running = false;
        Debug.Log("Oyun Bitti! Kazandı: " + didWin);

        if (carController != null)
            carController.DisableControlsAndBrake();

        if (inGameUI != null)
            inGameUI.SetActive(false);

        int successCount = collectibleManager != null ? collectibleManager.GetSuccessCount() : 0;
        int totalCollected = collectibleManager != null ? collectibleManager.GetTotalCollected() : 0;
        int score = totalCollected * successCount;

        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddGold(score);
        }

        if (didWin)
        {
            if (successPanel != null)
            {
                successPanel.SetActive(true);
                PopulateEndPanel(successPanel, totalCollected, successCount, score);
            }
        }
        else
        {
            if (failurePanel != null)
            {
                failurePanel.SetActive(true);
                PopulateEndPanel(failurePanel, totalCollected, successCount, score);
            }
        }
    }

    private void PopulateEndPanel(GameObject panel, int totalCollected, int successCount, int score)
    {
        var texts = panel.GetComponentsInChildren<TMP_Text>();
        foreach (var t in texts)
        {
            if (t.name == "TotalCollectedText") t.text = $"Toplam Obje: {totalCollected}";
            if (t.name == "SuccessCountText") t.text = $"Tamamlanan Türler: {successCount}";
            if (t.name == "ScoreText") t.text = $"{score}";
        }
    }

    public void StopTimer()
    {
        running = false;
    }

    private void UpdateEndGameUI(bool didWin)
    {
        CollectibleManager cm = collectibleManager;
        if (cm == null) cm = FindObjectOfType<CollectibleManager>();

        if (cm != null && endGameUIReferences != null)
        {
            var collectedList = cm.GetCollectibles();
            foreach (var refUI in endGameUIReferences)
            {
                foreach (var c in collectedList)
                {
                    if (c.typeName == refUI.collectibleName)
                    {
                        if (didWin && refUI.successAmountText != null)
                            refUI.successAmountText.text = c.currentAmount.ToString();
                        else if (!didWin && refUI.failureAmountText != null)
                            refUI.failureAmountText.text = c.currentAmount.ToString();
                    }
                }
            }
        }
    }
}
