using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
[AddComponentMenu("UI/Dynamic Progress Bar (DOTween)")]
public class DynamicProgressBarDOTween : MonoBehaviour
{
    [Header(" UI Elemanlarý")]
    public Image fillImage;          // Dolu kýsým (Image Type = Filled olmalý)
    public Image backgroundImage;    // Opsiyonel

    [Header(" Ayarlar")]
    [Tooltip("Progress barýn maksimum deðeri (örneðin 30 = 30 obje toplamak)")]
    public float maxValue = 100f;
    [Tooltip("Progress dolum animasyon süresi (saniye)")]
    public float fillDuration = 0.5f;
    [Tooltip("Animasyon eðrisi (Ease tipi)")]
    public Ease fillEase = Ease.OutCubic;

    [Header(" Renkler")]
    public Color startColor = Color.red;
    public Color midColor = new Color(1f, 0.8f, 0.1f);
    public Color endColor = Color.green;

    [Header(" Efektler")]
    [Tooltip("Dolum sýrasýnda küçük sýçrama efekti")]
    public float popScale = 1.1f;
    public float popDuration = 0.25f;
    public Ease popEase = Ease.OutBack;

    [Tooltip("Dolum sýrasýnda hafif sarsma efekti")]
    public float shakeStrength = 8f;
    public float shakeDuration = 0.2f;
    public int shakeVibrato = 10;

    [Header(" Tamamlanma Efektleri")]
    public ParticleSystem completionParticles;
    public AudioClip completionSfx;
    public float completionScale = 1.25f;
    public float completionAnimDuration = 0.6f;

    private float currentValue = 0f;
    private RectTransform rectTransform;
    private AudioSource audioSource;

    private Tween fillTween;
    private Tween popTween;

    public ParticleSystem completionConfetti; // Progress bar dolunca fýþkýracak konfeti


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
            fillImage.color = startColor;
        }
    }

    //  Renk geçiþini hesapla
    private Color EvaluateColor(float t)
    {
        if (t <= 0.5f)
            return Color.Lerp(startColor, midColor, t / 0.5f);
        else
            return Color.Lerp(midColor, endColor, (t - 0.5f) / 0.5f);
    }

    //  Hedef deðeri ayarla
    public void SetProgress(float value)
    {
        float targetValue = Mathf.Clamp(value, 0f, maxValue);
        AnimateProgress(targetValue);
    }

    //  Belirli miktar ekle
    public void AddProgress(float amount)
    {
        SetProgress(currentValue + amount);
    }

    //  DOTween animasyonu ile dolum
    private void AnimateProgress(float targetValue)
    {
        // Eðer ayný deðerse animasyon yapma
        if (Mathf.Approximately(targetValue, currentValue))
            return;

        // Önceki animasyonlarý temizle
        fillTween?.Kill();
        popTween?.Kill();

        float startPercent = Mathf.Clamp01(currentValue / maxValue);
        float endPercent = Mathf.Clamp01(targetValue / maxValue);

        // Dolum animasyonu
        fillTween = DOTween.To(() => startPercent, x =>
        {
            if (fillImage != null)
            {
                fillImage.fillAmount = x;
                fillImage.color = EvaluateColor(x);
            }
        }, endPercent, fillDuration)
        .SetEase(fillEase)
        .OnComplete(() =>
        {
            currentValue = targetValue;

            if (Mathf.Approximately(currentValue, maxValue))
                PlayCompletionEffect();
        });

        // Pop efekti (küçük zýplama)
        popTween = rectTransform.DOPunchScale(Vector3.one * (popScale - 1f), popDuration, 1, 0.5f)
            .SetEase(popEase);

        // Shake efekti (hafif sarsma)
        rectTransform.DOShakeAnchorPos(shakeDuration, shakeStrength, shakeVibrato, 90f, false);

        currentValue = targetValue;
    }

    // Tamamlandýðýnda yapýlan efektler
    private void PlayCompletionEffect()
    {
        DOTween.Kill(rectTransform);

        rectTransform.DOScale(completionScale, completionAnimDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                rectTransform.DOScale(1f, 0.3f).SetEase(Ease.InBack);
            });

        if (completionParticles != null)
            completionParticles.Play();

        if (completionSfx != null)
            audioSource.PlayOneShot(completionSfx);
    }

    //  Ýstenirse anýnda set et (örnek: sahne baþlangýcýnda)
    public void ForceSetInstant(float value)
    {
        currentValue = Mathf.Clamp(value, 0f, maxValue);
        float fillAmount = Mathf.Clamp01(currentValue / maxValue);

        if (fillImage != null)
        {
            fillImage.fillAmount = fillAmount;
            fillImage.color = EvaluateColor(fillAmount);
        }
    }
}
