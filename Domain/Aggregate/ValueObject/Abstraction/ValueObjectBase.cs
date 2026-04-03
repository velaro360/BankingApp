
namespace Domain.Aggregate.ValueObject.Abstraction
{
    public abstract class ValueObjectBase
    {
        abstract public bool IsIdentical(ValueObjectBase other);
    }
}
