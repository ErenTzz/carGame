using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[AddComponentMenu("UI/Dynamic Progress Bar (DOTween - Slider)")]
public class DynamicProgressBarDOTweenSlider : MonoBehaviour
{
    [Header("UI Elemanları")]
    public Slider progressSlider;
    public Image fillImage;
    public RectTransform fillRect;
    public Image backgroundImage;

    [Header("Konfeti Efekti")]
    public GameObject confettiPrefab;
    public Camera mainCamera;
    public float confettiDistanceFromCamera = 5f;

    [Header("Ses Efektleri")]
    public AudioClip milestone25Sound;
    public AudioClip milestone50Sound;
    public AudioClip milestone75Sound;
    public AudioClip completionSound;

    private AudioSource audioSource;

    [Header("Ayarlar")]
    public float maxValue = 100f;
    public float fillDuration = 0.5f;
    public Ease fillEase = Ease.OutCubic;

    [Header("Renkler")]
    public Color startColor = Color.red;
    public Color midColor = new Color(1f, 0.8f, 0.1f);
    public Color endColor = Color.green;

    private float currentValue = 0f;

    // Milestone kontrolü
    private bool milestone25Triggered = false;
    private bool milestone50Triggered = false;
    private bool milestone75Triggered = false;
    private bool completionTriggered = false;

    private void Awake()
    {
        if (progressSlider != null)
        {
            progressSlider.minValue = 0f;
            progressSlider.maxValue = maxValue;
            progressSlider.value = 0f;
        }

        if (fillImage != null)
            fillImage.color = startColor;

        if (backgroundImage != null)
            backgroundImage.color = new Color(startColor.r * 0.5f, startColor.g * 0.5f, startColor.b * 0.5f);

        if (mainCamera == null)
            mainCamera = Camera.main;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private Color EvaluateColor(float t)
    {
        if (t <= 0.5f)
            return Color.Lerp(startColor, midColor, t / 0.5f);
        else
            return Color.Lerp(midColor, endColor, (t - 0.5f) / 0.5f);
    }

    public void SetProgress(float value)
    {
        AnimateProgress(value);
    }

    public void AddProgress(float amount)
    {
        SetProgress(currentValue + amount);
    }

    public void AnimateProgress(float newValue)
    {
        if (progressSlider == null || fillRect == null)
            return;

        float targetValue = Mathf.Clamp(newValue, 0f, maxValue);
        float startPercent = currentValue / maxValue;
        float endPercent = targetValue / maxValue;

        // Animasyon (yalnızca dolum)
        DOTween.To(() => startPercent, x =>
        {
            progressSlider.value = x * maxValue;

            if (fillImage != null)
                fillImage.color = Color.Lerp(fillImage.color, EvaluateColor(x), 0.3f);

            if (backgroundImage != null)
            {
                Color bgColor = EvaluateColor(x) * 0.5f;
                backgroundImage.color = Color.Lerp(backgroundImage.color, bgColor, 0.3f);
            }

            CheckMilestones(x * maxValue);

        }, endPercent, fillDuration)
        .SetEase(fillEase)
        .OnComplete(() =>
        {
            currentValue = targetValue;
            CheckMilestones(currentValue);

            // Dolum tamamlandıysa konfeti + ses efekti
            if (Mathf.Approximately(currentValue, maxValue) && !completionTriggered)
            {
                completionTriggered = true;
                PlayConfetti();
                PlaySound(completionSound);

                if (fillImage != null)
                    fillImage.DOFade(1f, 0.2f).SetLoops(2, LoopType.Yoyo);
            }
        });
    }

    private void CheckMilestones(float currentVal)
    {
        float progressPercent = (currentVal / maxValue) * 100f;

        if (progressPercent >= 25f && !milestone25Triggered)
        {
            milestone25Triggered = true;
            PlaySound(milestone25Sound);
        }
        else if (progressPercent >= 50f && !milestone50Triggered)
        {
            milestone50Triggered = true;
            PlaySound(milestone50Sound);
        }
        else if (progressPercent >= 75f && !milestone75Triggered)
        {
            milestone75Triggered = true;
            PlaySound(milestone75Sound);
        }
    }

    private void PlayConfetti()
    {
        if (confettiPrefab == null || mainCamera == null || fillRect == null)
            return;

        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, fillRect.position);
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        Vector3 spawnPos = ray.GetPoint(confettiDistanceFromCamera);

        GameObject confetti = Instantiate(confettiPrefab, spawnPos, Quaternion.identity);
        confetti.transform.LookAt(mainCamera.transform);

        var ps = confetti.GetComponent<ParticleSystem>();
        if (ps != null) ps.Play();

        Destroy(confetti, 3f);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null || audioSource == null)
            return;

        audioSource.PlayOneShot(clip);
    }

    public void ForceSetInstant(float value)
    {
        currentValue = Mathf.Clamp(value, 0f, maxValue);
        progressSlider.value = currentValue;
        float t = currentValue / maxValue;
        if (fillImage != null)
            fillImage.color = EvaluateColor(t);
        if (backgroundImage != null)
            backgroundImage.color = EvaluateColor(t) * 0.5f;
    }
}
