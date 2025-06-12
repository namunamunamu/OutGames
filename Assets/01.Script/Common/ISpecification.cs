using UnityEngine;

public interface ISpecification<T>
{
    public bool IsSatisfiedBy(T value);
    public string ErrorMassage { get; }
}
