using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Image staminaBarUI;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public float staminaSeconds = 5f;
    public float maxStaminaSeconds = 5f;
    public float staminaSecondsRechargeLag = 3f;

    private bool wasRunning = false;
    private bool rechargeStaminaCorutineRunning = true;

    private Camera _playerCamera;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector] public bool canMove = true;

    private void Awake()
    {
        _playerCamera = Camera.main;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        staminaBarUI.type = Image.Type.Filled;
        staminaBarUI.fillMethod = Image.FillMethod.Horizontal;
        staminaBarUI.fillAmount = 1;
        // Lock cursor

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        reduceStamina(isRunning, wasRunning);

        float curSpeedX = canMove
            ? (isRunning && hasStamina() ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical")
            : 0;
        float curSpeedY = canMove
            ? (isRunning && hasStamina() ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal")
            : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        wasRunning = isRunning;

        DoorsLogic();
        
        _lastPosition = _playerCamera.transform.rotation.eulerAngles.y;
    }

    private float _lastPosition;
    private bool _shouldReset;

    private void DoorsLogic()
    {
        if (!Input.GetMouseButton(0))
        {
            _shouldReset = true;
            return;
        }

        if (_shouldReset)
        {
            _lastPosition = _playerCamera.transform.rotation.eulerAngles.y;
            _shouldReset = false;
            return;
        }

        if (!Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out var hit,
            10f)) return;

        if (!hit.transform.CompareTag("Doot")) return;

        var diff = _playerCamera.transform.rotation.eulerAngles.y - _lastPosition;
        hit.transform.Rotate(Vector3.up, diff, Space.World);
    }

    private void reduceStamina(bool isRunning, bool wasRunning)
    {
        if (isRunning && wasRunning && staminaSeconds > 0)
        {
            // stop recharging if running
            if (rechargeStaminaCorutineRunning)
            {
                StopAllCoroutines();
                rechargeStaminaCorutineRunning = false;
            }

            // reduce stamina
            updateStamina(-Time.deltaTime);


            if (staminaSeconds <= 0)
            {
                staminaSeconds = 0;
            }
        }

        if (!isRunning && !wasRunning && !rechargeStaminaCorutineRunning)
        {
            StartCoroutine(rechargeStaminaCoroutine());
        }
    }

    private IEnumerator rechargeStaminaCoroutine()
    {
        rechargeStaminaCorutineRunning = true;
        yield return new WaitForSeconds(staminaSecondsRechargeLag);

        while (staminaSeconds != maxStaminaSeconds)
        {
            yield return new WaitForSeconds(0.2f);
            updateStamina(0.1f);

            if (staminaSeconds > maxStaminaSeconds)
            {
                staminaSeconds = maxStaminaSeconds;
            }
        }

        rechargeStaminaCorutineRunning = false;
    }

    private bool hasStamina() => staminaSeconds > 0;

    private void updateStamina(float amount)
    {
        staminaSeconds += amount;
        staminaBarUI.fillAmount = staminaSeconds / maxStaminaSeconds;
    }
}