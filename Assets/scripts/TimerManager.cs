using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    [Header("Zaman Ayarlarý")]
    public float timeLimit = 30f;
    public TMP_Text timerText;

    [Header("Baþarý Þartý")]
    public int successThreshold = 3; // Örnek: Kazanmak için 3 farklý türde obje toplanmalý
    public CollectibleManager collectibleManager;

    [Header("Baðlantýlar")]
    public Transform player; // araba objesi
    public CarController carController; // Araba scripti
    public Vector3 offset = new Vector3(0, 2.5f, 0); // dünya üzerindeki offset

    [Header("UI Panelleri")]
    public GameObject successPanel; // Baþarý durumunda gösterilecek panel
    public GameObject failurePanel; // Baþarýsýzlýk durumunda gösterilecek panel
    public GameObject inGameUI;     // Gaz, fren vb. butonlarý tutan panel

    private float currentTime;
    private bool running = true;
    private bool gameEnded = false; // Oyunun bitip bitmediðini kontrol eder

    private void Start()
    {
        currentTime = timeLimit;

        if (collectibleManager == null)
            collectibleManager = FindObjectOfType<CollectibleManager>();

        // YENÝ EKLENDÝ: CarController'ý player objesinden al
        if (player != null && carController == null)
            carController = player.GetComponent<CarController>();

        if (timerText == null)
            Debug.LogWarning("TimerText not assigned!");

        // Panelleri ve oyun içi UI'ý baþlangýçta gizle
        if (successPanel != null) successPanel.SetActive(false);
        if (failurePanel != null) failurePanel.SetActive(false);
        if (inGameUI != null) inGameUI.SetActive(true);
    }

    private void Update()
    {
        // YENÝ EKLENDÝ: Oyun bittiyse veya sayaç çalýþmýyorsa güncelleme yapma
        if (!running || gameEnded) return;

        // Süreyi güncelle
        currentTime -= Time.deltaTime;
        if (currentTime < 0) currentTime = 0;

        UpdateTimerText();

        // YENÝ EKLENDÝ: Baþarý þartýný kontrol et
        if (collectibleManager.GetSuccessCount() >= successThreshold)
        {
            EndGame(true); // Oyunu "Baþarýlý" olarak bitir
        }
        // DEÐÝÞTÝRÝLDÝ: Süre biterse oyunu "Baþarýsýz" olarak bitir
        else if (currentTime <= 0f)
        {
            EndGame(false);
        }
    }

    void UpdateTimerText()
    {
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
    }

    // YENÝ EKLENDÝ: OnTimeUp fonksiyonunu EndGame(bool) olarak güncelledim
    private void EndGame(bool didWin)
    {
        if (gameEnded) return; // Oyun zaten bittiyse tekrar bitirme

        gameEnded = true;
        running = false;
        Debug.Log("Oyun Bitti! Kazandý: " + didWin);

        // 1. Arabayý durdur ve kontrolleri kilitle
        if (carController != null)
        {
            carController.DisableControlsAndBrake();
        }

        // 2. Oyun içi UI'ý (Gaz, Fren butonlarý) gizle
        if (inGameUI != null)
        {
            inGameUI.SetActive(false);
        }

        // 3. Skoru hesapla
        int successCount = collectibleManager.GetSuccessCount();
        int totalCollected = collectibleManager.GetTotalCollected();
        int score = totalCollected * successCount;

        // 4. Doðru paneli göster
        if (didWin)
        {
            if (successPanel != null)
            {
                successPanel.SetActive(true);
                // Panelin içindeki textleri bul ve doldur
                PopulateEndPanel(successPanel, totalCollected, successCount, score);
            }
        }
        else
        {
            if (failurePanel != null)
            {
                failurePanel.SetActive(true);
                // Panelin içindeki textleri bul ve doldur
                PopulateEndPanel(failurePanel, totalCollected, successCount, score);
            }
        }
    }

    // YENÝ EKLENDÝ: Panelleri doldurmak için yardýmcý fonksiyon
    private void PopulateEndPanel(GameObject panel, int totalCollected, int successCount, int score)
    {
        // Orijinal koddaki gibi panelin altýndaki textleri bulur
        var texts = panel.GetComponentsInChildren<TMP_Text>();
        foreach (var t in texts)
        {
            if (t.name == "TotalCollectedText") t.text = $"Toplam Obje: {totalCollected}";
            if (t.name == "SuccessCountText") t.text = $"Tamamlanan Türler: {successCount}";
            if (t.name == "ScoreText") t.text = $"{score}";
        }
    }

    // YENÝ EKLENDÝ: Sayacý durdurmak için genel bir fonksiyon (opsiyonel)
    public void StopTimer()
    {
        running = false;
    }
}