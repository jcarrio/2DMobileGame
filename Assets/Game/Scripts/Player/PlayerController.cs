using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(GameController))]
[RequireComponent(typeof(IDamageable))]
public class PlayerController : MonoBehaviour
{
    CharacterMovement2D playerMovement;
    CharacterFacing2D playerFacing;
    PlayerInput playerInput;
    GameController gameController;
    IDamageable damageable;

    private bool isAlreadyLookingAbove = false;
    private bool isAlreadyLookingBelow = false;

    [Header("Camera")]

    [SerializeField]
    private Transform cameraTarget;

    [Range(0.0f, 5.0f)]
    [SerializeField]
    private float cameraTargetOffsetX = 2.0f;

    [Range(0.5f, 50.0f)]
    [SerializeField]
    private float cameraTargetFlipSpeed = 5.0f;

    [Range(0.0f, 5.0f)]
    [SerializeField]
    private float characterSpeedInfluence = 0.5f;

    [SerializeField]
    private float amountCameraShiftAbove = 2.0f;

    [SerializeField]
    private float AmountCameraShiftBelow = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<CharacterMovement2D>();
        playerFacing = GetComponent<CharacterFacing2D>();
        playerInput = GetComponent<PlayerInput>();
        gameController = GetComponent<GameController>();
        damageable = GetComponent<IDamageable>();

        damageable.DeathEvent += OnDeath;
    }

    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DeathEvent -= OnDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movimentação
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(movementInput);

        playerFacing.UpdateFacing(movementInput);

        // Pulo
        if (playerInput.IsJumpButtonDown())
        {
            playerMovement.Jump();
        }
        if (playerInput.IsJumpButtonHeld() == false)
        {
            playerMovement.AbortJump();
        }

        // Agachar
        if (playerInput.IsCrouchButtonDown())
        {
            playerMovement.Crouch();
        }
        if (playerInput.IsCrouchButtonUp() == true)
        {
            playerMovement.UnCrouch();
        }
    }

    private void FixedUpdate()
    {
        float targetOffsetX = playerFacing.IsFacingRight() ? cameraTargetOffsetX : -cameraTargetOffsetX;
        float currentOffsetX = Mathf.Lerp(cameraTarget.localPosition.x, targetOffsetX, Time.fixedDeltaTime * cameraTargetFlipSpeed);

        currentOffsetX += playerMovement.CurrentVelocity.x * Time.fixedDeltaTime * characterSpeedInfluence;

        float localPositionY = CameraShift(cameraTarget.localPosition.y);
//        cameraTarget.localPosition = new Vector3(currentOffsetX, cameraTarget.localPosition.y, cameraTarget.localPosition.z);
        cameraTarget.localPosition = new Vector3(currentOffsetX, localPositionY, cameraTarget.localPosition.z);
    }

    private void OnDeath()
    {
        playerMovement.StopImmediately();
        enabled = false;
        gameController.GameOver();
    }

    private float CameraShift(float currentY)
    {
        if (isAlreadyLookingAbove)
        {
            // deixou de olhar pra cima? desfaz soma feita quando estava olhando
            if (!playerInput.IsLookingUpButtonDown())
            {
                currentY -= amountCameraShiftAbove;
                isAlreadyLookingAbove = false;
            }
        }
        else
        {
            // passou a olhar pra cima? soma valor para deslocar camera para cima
            if (playerInput.IsLookingUpButtonDown())
            {
                currentY += amountCameraShiftAbove;
                isAlreadyLookingAbove = true;
            }
        }
        if (isAlreadyLookingBelow)
        {
            // deixou de ficar agachado? desfaz subtração feita quando estava agachado
            if (!playerInput.IsCrouchButtonDown())
            {
                currentY += AmountCameraShiftBelow;
                isAlreadyLookingBelow = false;
            }
        }
        else
        {
            // passou a ficar agachado? subtrai valor para deslocar camera para baixo
            if (playerInput.IsCrouchButtonDown())
            {
                currentY -= AmountCameraShiftBelow;
                isAlreadyLookingBelow = true;
            }
        }
        return currentY;
    }
}
