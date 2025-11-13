using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CollectibleManager : MonoBehaviour
{
    [Serializable]
    public class CollectibleType
    {
        public string typeName;
        public int targetAmount = 10;
        [HideInInspector] public int currentAmount = 0;

        [Header("UI Elemanlarý")]
        public TMP_Text amountText; // "x / hedef" yazýsý
        public Image icon; // obje ikonu

        [Header("Ses Efektleri")]
        public AudioClip completionSfx; // Her tür için farklý tamamlanma sesi (opsiyonel)
        [HideInInspector] public bool completed; //  Bir kere çalmasý için
    }

    public CollectibleType[] collectibles;
    public TMP_Text totalText;
    public DynamicProgressBarDOTweenSlider mainProgressBar;

    public event Action OnAllCollected;

    private int totalCollectedCount = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        // Tek bir AudioSource tüm sesleri çalabilir
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateAllUI();
    }

    public void AddCollectible(string collectibleName)
    {
        foreach (var c in collectibles)
        {
            if (c.typeName == collectibleName)
            {
                c.currentAmount++;
                if (c.amountText != null)
                    c.amountText.text = $"{c.currentAmount} / {c.targetAmount}";

                //  Toplam sayaç artýr
                totalCollectedCount++;
                UpdateTotalText();

                //  Eðer bu collectible hedefe ulaþtýysa ve daha önce tamamlanmadýysa
                if (!c.completed && c.currentAmount >= c.targetAmount)
                {
                    c.completed = true;

                    // Sesi çal
                    if (c.completionSfx != null && audioSource != null)
                        audioSource.PlayOneShot(c.completionSfx);
                }

                // Ana progress bar'ý güncelle
                if (mainProgressBar != null)
                    mainProgressBar.SetProgress(GetTotalCollected());

                // Tüm hedefler tamamlandýysa olayý tetikle
                if (AllGoalsReached())
                    OnAllCollected?.Invoke();

                break;
            }
        }
    }

    private void UpdateUI(CollectibleType c)
    {
        if (c.amountText != null)
            c.amountText.text = $"{c.currentAmount} / {c.targetAmount}";
    }

    private void UpdateAllUI()
    {
        foreach (var c in collectibles)
            UpdateUI(c);
        UpdateTotalText();
    }

    private void UpdateTotalText()
    {
        if (totalText != null)
            totalText.text = $"Toplam: {totalCollectedCount}";
    }

    private bool AllGoalsReached()
    {
        foreach (var c in collectibles)
            if (c.currentAmount < c.targetAmount)
                return false;
        return true;
    }

    public int GetSuccessCount()
    {
        int success = 0;
        foreach (var c in collectibles)
            if (c.currentAmount >= c.targetAmount)
                success++;
        return success;
    }

    public int GetTotalCollected() => totalCollectedCount;
}
