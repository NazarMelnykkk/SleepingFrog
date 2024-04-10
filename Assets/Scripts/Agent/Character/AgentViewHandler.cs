using UnityEngine;

public class AgentViewHandler : MonoBehaviour
{
    private static readonly Vector2 _flipRightScale = new Vector2(1, 1);
    private static readonly Vector2 _flipLeftScale = new Vector2(-1, 1);

    public void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.localScale = _flipRightScale;
        }
        else if (direction.x < 0)
        {
            transform.localScale = _flipLeftScale;
        }
    }
}
