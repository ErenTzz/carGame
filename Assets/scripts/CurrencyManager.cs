using UnityEngine;

using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public event Action<int> OnGoldChanged;
    public event Action<int> OnDiamondsChanged;

    private int gold;
    private int diamonds;

    private const string GOLD_KEY = "PlayerGold";
    private const string DIAMOND_KEY = "PlayerDiamonds";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCurrency();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        SaveCurrency();
        OnGoldChanged?.Invoke(gold);
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        SaveCurrency();
        OnDiamondsChanged?.Invoke(diamonds);
    }

    public int GetGold() => gold;
    public int GetDiamonds() => diamonds;

    private void SaveCurrency()
    {
        PlayerPrefs.SetInt(GOLD_KEY, gold);
        PlayerPrefs.SetInt(DIAMOND_KEY, diamonds);
        PlayerPrefs.Save();
    }

    private void LoadCurrency()
    {
        gold = PlayerPrefs.GetInt(GOLD_KEY, 0);
        diamonds = PlayerPrefs.GetInt(DIAMOND_KEY, 0);
    }
}
