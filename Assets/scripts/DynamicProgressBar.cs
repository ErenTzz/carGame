using UnityEngine;
using UnityEngine.UI;

public class DynamicProgressBar : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public Image fillImage; // Fill kýsmý
    public Image backgroundImage;

    [Header("Ayarlar")]
    [Tooltip("Progress barýn dolum limiti (örnek: 100 = %100)")]
    public float maxValue = 100f;
    [Tooltip("Animasyonun dolum hýzýný kontrol eder")]
    public float fillSpeed = 2f;

    [Header("Renkler")]
    public Color startColor = Color.red;
    public Color endColor = Color.green;

    private float currentValue = 0f;
    private float targetValue = 0f;

    private void Start()
    {
        UpdateFillInstant();
    }

    private void Update()
    {
        if (Mathf.Abs(currentValue - targetValue) > 0.01f)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * fillSpeed);
            UpdateFillInstant();
        }
    }

    public void SetProgress(float value)
    {
        targetValue = Mathf.Clamp(value, 0f, maxValue);
    }

    public void AddProgress(float amount)
    {
        SetProgress(targetValue + amount);
    }

    private void UpdateFillInstant()
    {
        float fillAmount = Mathf.Clamp01(currentValue / maxValue);

        if (fillImage != null)
        {
            fillImage.fillAmount = fillAmount;
            fillImage.color = Color.Lerp(startColor, endColor, fillAmount);
        }
    }
}
