using UnityEngine;
using TMPro;

public class CurrencyUIDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text goldText;
    public TMP_Text diamondText;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnGoldChanged += UpdateGold;
            CurrencyManager.Instance.OnDiamondsChanged += UpdateDiamonds;
            UpdateUI(); // Ensure UI is up to date when enabled
        }
    }

    private void OnDisable()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnGoldChanged -= UpdateGold;
            CurrencyManager.Instance.OnDiamondsChanged -= UpdateDiamonds;
        }
    }

    private void UpdateUI()
    {
        if (CurrencyManager.Instance != null)
        {
            UpdateGold(CurrencyManager.Instance.GetGold());
            UpdateDiamonds(CurrencyManager.Instance.GetDiamonds());
        }
    }

    private void UpdateGold(int amount)
    {
        if (goldText != null) goldText.text = amount.ToString();
    }

    private void UpdateDiamonds(int amount)
    {
        if (diamondText != null) diamondText.text = amount.ToString();
    }
}
