public interface IHealthProvider
{
    int CurrentHealth { get; }
    int MaxHealth { get; }
    event System.Action<int> OnHealthChanged;
}
