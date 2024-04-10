using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewHandler : MonoBehaviour
{
    private bool _isFacingRight = true;

    private static readonly Vector2 _flipRightScale = new Vector2(1, 1);
    private static readonly Vector2 _flipLeftScale = new Vector2(-1, 1);

    public void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            _isFacingRight = true;
            transform.localScale = _flipRightScale;
        }
        else if (direction.x < 0)
        {
            _isFacingRight = false;
            transform.localScale = _flipLeftScale;
        }
    }
}
