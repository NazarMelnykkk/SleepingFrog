
public interface IDamageable 
{
    public ValuePool GetHealthPool();

    public void TakeDamage(int damageValue) { }

    public void Die() { }
}
