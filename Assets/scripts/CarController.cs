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

    // UI tarafýndan çaðrýlacak fonksiyonlar
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

        // Butona basýldýðýnda ýþýklar da anýnda güncellensin
        carLights.isBackLightOn = state;
        carLights.OperateBackLights();
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }



    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

        carLights = GetComponent<CarLights>();
    }

    void Update()
    {
        GetInputs();
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
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
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
            wheel.wheelCollider.motorTorque = -moveInput *600* maxAcceleration * Time.deltaTime;
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

    //void Brake()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {

    //        foreach (var wheel in wheels)
    //        {
    //            wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
    //        }

    //        carLights.isBackLightOn = true;
    //        carLights.OperateBackLights();
    //    }
    //    else
    //    {

    //        foreach (var wheel in wheels)
    //        {
    //            wheel.wheelCollider.brakeTorque = 0f;
    //        }

    //        carLights.isBackLightOn = false;
    //        carLights.OperateBackLights();
    //    }
    //}

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
            isBraking = isBrakeButtonPressed; // UI tarafýndan ayarlanan deðiþken
        }

        isBraking = Input.GetKey(KeyCode.Space) || isReversing; // Reverse butonuna basýldýðýnda da true olacak

        if (isBraking)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
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

    //void WheelEffects()
    //{
    //    foreach (var wheel in wheels)
    //    {
    //        if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.linearVelocity.magnitude >= 10.0f)
    //        {
    //            wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
    //            wheel.smokeParticle.Emit(1);
    //        }
    //        else
    //        {
    //            wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
    //        }

    //    }
    //}

    //void WheelEffects()
    //{
    //    foreach (var wheel in wheels)
    //    {
    //        WheelHit hit;
    //        bool isGrounded = wheel.wheelCollider.GetGroundHit(out hit);

    //        if (isGrounded)
    //        {

    //            // Yanal ve ileri kayma miktarýný kontrol et
    //            float slipAmount = Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);

    //            // Eðer slip belirli eþiði geçerse drift izi çýkar
    //            if (slipAmount > 0.6f) // bu eþiði ayarlayabilirsin (0.2 - 0.6 arasý genelde iyi)
    //            {
    //                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
    //                wheel.smokeParticle.Emit(1);
    //            }
    //            else
    //            {
    //                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
    //            }
    //        }
    //        else
    //        {
    //            // Havadaysa iz çýkarma
    //            wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
    //        }
    //    }
    //}

    void WheelEffects()
    {
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
                // ileri (gaz) ve yan (drift) kaymalarý topla
                slipAmount = Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);
            }

            // Þart: eðer fren yapýyorsak VEYA çekiþ kaybý yaþanýyorsa
            bool shouldEmit = false;

            if (isGrounded && isRear)
            {
                // Fren yapýyorsa veya kayma eþiði aþýlmýþsa
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
