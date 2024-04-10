using UnityEngine;

public class AgentAnimationEventController : MonoBehaviour
{

    [SerializeField] private AgentAttackHandler _attackHandler;

    public void OnDealDamageAttackAnimationEvent()
    {
        _attackHandler.DealDamage();
    }

    public void OnEndAttackAnimationEvent()
    {
        _attackHandler.EndOfAttack();
    }

}
