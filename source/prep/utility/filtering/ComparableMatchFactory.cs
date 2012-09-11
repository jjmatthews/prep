using System;
using prep.collections;

namespace prep.utility.filtering
{
    public class ComparableMatchFactory<Item, PropertyType>
      : ICreateMatchers<Item, PropertyType> where PropertyType : IComparable<PropertyType>
    {
        PropertyAccessor<Item, PropertyType> accessor;
        ICreateMatchers<Item, PropertyType> match_factory;

        public ComparableMatchFactory(PropertyAccessor<Item, PropertyType> accessor,
                                      ICreateMatchers<Item, PropertyType> original_factory)
        {
            this.accessor = accessor;
            this.match_factory = original_factory;
        }

        public IMatchAn<Item> greater_than(PropertyType value)
        {
            return CreateMatchFactory<Item>.CreateAnonymousMatch(x => accessor(x).CompareTo(value) > 0);
        }

        public IMatchAn<Item> equal_to(PropertyType value)
        {
            return match_factory.equal_to(value);
        }

        public IMatchAn<Item> equal_to_any(params PropertyType[] values)
        {
            return match_factory.equal_to_any(values);
        }

        public IMatchAn<Item> not_equal_to(PropertyType value)
        {
            return match_factory.not_equal_to(value);
        }

        public IMatchAn<Item> between(PropertyType start, PropertyType end)
        {
            return
                CreateMatchFactory<Item>.CreateAnonymousMatch(
                    x => accessor(x).CompareTo(start) >= 0 && accessor(x).CompareTo(end) <= 0);
        }
    }
}