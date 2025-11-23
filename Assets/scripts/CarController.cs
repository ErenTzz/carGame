using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]

    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public bool isReversing = false; // Geri viteste mi?

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;


    private Rigidbody carRb;
    private CarLights carLights;

    // UI Butonuyla kontrol edilecek
    private bool isBrakeButtonPressed = false;

    // YENİ EKLENDİ: Arabanın kontrol edilip edilemeyeceğini belirler
    private bool isControllable = true;
    // YENİ EKLENDİ: Oyun bittiğinde freni zorla kilitler
    private bool forceBrake = false;


    // UI tarafından çağrılacak fonksiyonlar
    public void OnBrakeButtonDown()
    {
        isBrakeButtonPressed = true;
    }

    public void OnBrakeButtonUp()
    {
        isBrakeButtonPressed = false;
    }

    public void SetReverse(bool state)
    {
        isReversing = state;

        // Butona basıldığında ışıklar da anında güncellensin
        carLights.isBackLightOn = state;
        carLights.OperateBackLights();
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // YENİ EKLENDİ: Oyunu bitirmek için dışarıdan çağrılacak fonksiyon
    public void DisableControlsAndBrake()
    {
        isControllable = false;
        forceBrake = true;
        moveInput = 0; // Hareketi anında kes
    }


    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

        carLights = GetComponent<CarLights>();
    }

    void Update()
    {
        // DEĞİŞTİRİLDİ: Sadece kontrol edilebiliyorsa input al
        if (isControllable)
        {
            GetInputs();
        }
        else
        {
            // Kontrol edilemiyorsa inputları sıfırla
            moveInput = 0;
            steerInput = 0;
        }

        AnimatedWheels();
        WheelEffects();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    public void MoveInput(float input)
    {
        // DEĞİŞTİRİLDİ: Kontrol dışıysa input alma
        if (!isControllable)
        {
            moveInput = 0;
            return;
        }
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        // DEĞİŞTİRİLDİ: Kontrol dışıysa input alma
        if (!isControllable)
        {
            steerInput = 0;
            return;
        }
        steerInput = input;
    }

    void GetInputs()
    {
        // DEĞİŞTİRİLDİ: Zaten isControllable ile korunduğu için
        // ekstra kontrol gerekmiyor, sadece inputları al.
        if (control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = -moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }

        }
    }

    void Brake()
    {
        bool isBraking = false;

        // --- KLAVYE kontrolü ---
        if (control == ControlMode.Keyboard)
        {
            isBraking = Input.GetKey(KeyCode.Space);
        }
        // --- BUTTONS kontrolü ---
        else if (control == ControlMode.Buttons)
        {
            isBraking = isBrakeButtonPressed; // UI tarafından ayarlanan değişken
        }

        // DEĞİŞTİRİLDİ: Geri vites, fren butonu VEYA zorla fren (forceBrake) durumunu kontrol et
        isBraking = Input.GetKey(KeyCode.Space) || isReversing || forceBrake;

        if (isBraking)
        {
            foreach (var wheel in wheels)
            {
                // DEĞİŞTİRİLDİ: Fren gücünü biraz artırdım ki araba zorla durdurulduğunda daha hızlı dursun.
                // Time.deltaTime'ı buradan kaldırıp FixedUpdate'e taşıyabilirsin, ama şimdilik böyle kalsın.
                wheel.wheelCollider.brakeTorque = 800 * brakeAcceleration * Time.deltaTime;
            }

            carLights.isBackLightOn = true;
            carLights.OperateBackLights();
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0f;
            }

            carLights.isBackLightOn = false;
            carLights.OperateBackLights();
        }
    }

    void AnimatedWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;

        }
    }

    void WheelEffects()
    {
        // DEĞİŞTİRİLDİ: Sadece kontrol edilebiliyorsa efektleri çalıştır
        if (!isControllable)
        {
            // Eğer araba artık kontrol edilemiyorsa tüm izleri (TrailRenderer) kapat
            foreach (var wheel in wheels)
            {
                var trail = wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>();
                if (trail != null)
                    trail.emitting = false;
            }
            return;
        }

        foreach (var wheel in wheels)
        {
            bool isGrounded = wheel.wheelCollider.isGrounded;
            bool isBraking = Input.GetKey(KeyCode.Space);
            bool isRear = wheel.axel == Axel.Rear;
            bool isMovingFast = carRb.linearVelocity.magnitude >= 10.0f;

            // WheelCollider verisini al
            WheelHit hit;
            bool hasHit = wheel.wheelCollider.GetGroundHit(out hit);

            float slipAmount = 0f;
            if (hasHit)
            {
                // ileri (gaz) ve yan (drift) kaymaları topla
                slipAmount = Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);
            }

            // Şart: eğer fren yapıyorsak VEYA çekiş kaybı yaşanıyorsa
            bool shouldEmit = false;

            if (isGrounded && isRear)
            {
                // Fren yapıyorsa veya kayma eşiği aşılmışsa
                if ((isBraking && isMovingFast) || slipAmount > 0.4f)
                {
                    shouldEmit = true;
                }
            }

            // Efektleri uygula
            var trail = wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>();
            if (trail != null)
                trail.emitting = shouldEmit;

            if (shouldEmit && wheel.smokeParticle != null)
            {
                wheel.smokeParticle.Emit(1);
            }
        }
    }
}