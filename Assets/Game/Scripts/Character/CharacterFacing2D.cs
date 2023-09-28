using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterFacing2D : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    float positionX = 1.79f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFacing(Vector2 movementInput, GameObject weapon)
    {
        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (weapon != null)
        {
            weapon.transform.localPosition = new Vector3(spriteRenderer.flipX ? -positionX : positionX, weapon.transform.localPosition.y, 0);
        }
    }

    public bool IsFacingRight()
    {
        return spriteRenderer.flipX == false;
    }
}
