using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    CharacterMovement2D playerMovement;
    SpriteRenderer spriteRenderer;
    PlayerInput playerInput;

    public Sprite crouchedSprite;
    public Sprite idleSprite;

    [Header("Camera")]
    public Transform cameraTarget;
    [Range(0.0f, 5.0f)]
    public float cameraTargetOffsetX = 2.0f;
    [Range(0.5f, 50.0f)]
    public float cameraTargetFlipSpeed = 5.0f;
    [Range(0.0f, 5.0f)]
    public float characterSpeedInfluence = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<CharacterMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimentação
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(movementInput);

        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }

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

            //TODO: Remover quando adicionarmos animação
            spriteRenderer.sprite = crouchedSprite;
        }
        if (playerInput.IsCrouchButtonUp() == true)
        {
            playerMovement.UnCrouch();

            //TODO: Remover quando adicionarmos animação
            spriteRenderer.sprite = idleSprite;
        }
    }

    private void FixedUpdate()
    {
        bool isFacingRight = spriteRenderer.flipX == false;
        float targetOffsetX = isFacingRight ? cameraTargetOffsetX : -cameraTargetOffsetX;
        float currentOffsetX = Mathf.Lerp(cameraTarget.localPosition.x, targetOffsetX, Time.fixedDeltaTime * cameraTargetFlipSpeed);

        currentOffsetX += playerMovement.CurrentVelocity.x * Time.fixedDeltaTime * characterSpeedInfluence;
        cameraTarget.localPosition = new Vector3(currentOffsetX, cameraTarget.localPosition.y, cameraTarget.localPosition.z);
    }
}
