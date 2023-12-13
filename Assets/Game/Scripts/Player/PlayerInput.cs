using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    private struct PlayerInputConstants
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Jump = "Jump";
        public const string Attack = "Attack";
        public const string Fire = "Fire1";
        public const string Close = "Fire2";
    }

    public Vector2 GetMovementInput()
    {
        // Input teclado
        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);

        if (Mathf.Approximately(horizontalInput, 0.0f))
        {
            horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
        }
        return new Vector2(horizontalInput, 0);
    }

    public bool IsJumpButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.Space);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Jump);
        bool isControllerButtonDown = Input.GetButtonDown(PlayerInputConstants.Jump);

        return isKeyboardButtonDown || isMobileButtonDown || isControllerButtonDown;
    }

    public bool IsJumpButtonHeld()
    {
        bool isKeyboardButtonHeld = Input.GetKey(KeyCode.Space);
        bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Jump);
        bool isControllerButtonHeld = Input.GetButton(PlayerInputConstants.Jump);

        return isKeyboardButtonHeld || isMobileButtonHeld || isControllerButtonHeld;
    }

    public bool IsCrouchButtonDown()
    {
        //bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.S);
        //troquei a tecla de S para seta para baixo (controle)
        bool isKeyboardButtonDown = Input.GetAxisRaw(PlayerInputConstants.Vertical) < 0;
        bool isMobileButtonDown = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) < 0;

        return isKeyboardButtonDown || isMobileButtonDown;
    }

    public bool IsCrouchButtonUp()
    {
        //bool isKeyboardButtonUp = Input.GetKey(KeyCode.S) == false;
        //troquei a tecla de S para seta para baixo (controle)
        bool isKeyboardButtonUp = Input.GetAxisRaw(PlayerInputConstants.Vertical) >= 0;
        bool isMobileButtonUp = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) >= 0;

        return isKeyboardButtonUp && isMobileButtonUp;
    }

    public bool IsLookingUpButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetAxisRaw(PlayerInputConstants.Vertical) > 0;
        //somente quando controle está muito para cima
        bool isMobileButtonDown = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) > 0.9f;

        return isKeyboardButtonDown || isMobileButtonDown;
    }

    public bool IsAttackButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.K);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Attack);
        bool isControllerButtonDown = Input.GetButtonDown(PlayerInputConstants.Fire);

        return isKeyboardButtonDown || isMobileButtonDown || isControllerButtonDown;
    }

    public bool IsCloseButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.Escape);
//        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Back);
        bool isControllerButtonDown = Input.GetButtonDown(PlayerInputConstants.Close);

        return isKeyboardButtonDown || isControllerButtonDown;
    }
}
