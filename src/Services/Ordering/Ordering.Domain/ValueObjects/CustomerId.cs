

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }

        public CustomerId(Guid id) => Value = id;

        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new DomainException("Customer id cannot be empty!");
            }

            return new CustomerId(value);
        }
    }
}
