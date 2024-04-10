
public class AutoDestroyPoolableObject : PoolableObject
{
    public float AutoDestroyTime = 5f;
    protected const string DisableMetodName = "Disable";

    protected virtual void OnEnable()
    {
        CancelInvoke(DisableMetodName);
        Invoke(DisableMetodName, AutoDestroyTime);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        CancelInvoke(DisableMetodName);
        gameObject.SetActive(false);
    }

/*    public override void Deleted()
    {
        base.Deleted();
        CancelInvoke(DisableMetodName);
        gameObject.SetActive(false);
    }*/

}
