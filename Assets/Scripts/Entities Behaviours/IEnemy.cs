public interface IEnemy
{
    public int damage { get; }
    public void TakeDamage(int damage);
    public void Freeze(float time);
}
