using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregate.ValueObject.Abstraction
{
    public abstract class ValueObjectBase
    {
        abstract public bool IsIdentical(ValueObjectBase other);
    }
}
