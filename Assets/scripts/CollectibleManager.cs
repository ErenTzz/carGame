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
    }

    public CollectibleType[] collectibles;
    public TMP_Text totalText; // "Toplam: x / y" gibi göstermek için
    public DynamicProgressBarDOTween mainProgressBar;



    public event Action OnAllCollected;

    private int TotalTarget => collectibles.Length * collectibles[0].targetAmount;
    private int TotalCurrent
    {
        get
        {
            int total = 0;
            foreach (var c in collectibles)
                total += c.currentAmount;
            return total;
        }
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
                // Toplama miktarýný artýr
                c.currentAmount++;
                if (c.currentAmount > c.targetAmount)
                    c.currentAmount = c.targetAmount;

                // Ýlgili paneldeki miktar metnini güncelle
                if (c.amountText != null)
                    c.amountText.text = $"{c.currentAmount} / {c.targetAmount}";

                // Genel toplam metnini güncelle
                UpdateTotalText();

                // Ana progress bar'ý güncelle (varsa)
                if (mainProgressBar != null)
                    mainProgressBar.SetProgress(GetTotalCollected());

                // Eðer tüm objeler hedefe ulaþtýysa olayý tetikle
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
            totalText.text = $"Toplam: {TotalCurrent} / {TotalTarget}";
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

    public int GetTotalCollected() => TotalCurrent;
}
