using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public string typeName; // inspector'da örn: "TypeA"
    public GameObject pickupVFX;    // opsiyonel
    public AudioClip pickupSound;   // opsiyonel
    public float disableDelay = 0.2f; // ses/VFX için kýsa delay

    [SerializeField] private CollectibleManager collectibleManager; // Inspector'a sürükle (önerilen)
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Eðer collectibleManager Inspector'dan atanmadýysa, sahnede ilk bulunaný al (yedek)
        if (collectibleManager == null)
        {
#if UNITY_2023_2_OR_NEWER
            collectibleManager = Object.FindFirstObjectByType<CollectibleManager>();
#else
            collectibleManager = FindObjectOfType<CollectibleManager>();
#endif
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Manager'a bildir (tek parametre)
        if (collectibleManager != null)
            collectibleManager.AddCollectible(typeName);
        else
            Debug.LogWarning("CollectibleManager bulunamadý!");

        // VFX
        if (pickupVFX != null)
            Instantiate(pickupVFX, transform.position, Quaternion.identity);

        // Ses çal
        if (pickupSound != null)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
                // objeyi kýsa bir süre sonra devre dýþý býrak (ses çalarken)
                if (disableDelay > 0f)
                    Invoke(nameof(DisableSelf), disableDelay);
                else
                    DisableSelf();
            }
            else
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                DisableSelf();
            }
        }
        else
        {
            // Seviyeye baðlý olarak hemen kapat (veya pool'a ver)
            DisableSelf();
        }
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
