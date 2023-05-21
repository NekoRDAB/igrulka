public interface IEnemy
{
    public int damage { get; }
    public int health { get; }
    public void TakeDamage(int damage);
    public void Freeze(float time);
}
