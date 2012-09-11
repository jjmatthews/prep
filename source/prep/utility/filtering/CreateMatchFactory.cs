namespace prep.utility.filtering
{
    public class CreateMatchFactory<Item>
    {
        public static AnonymousMatch<Item> CreateAnonymousMatch(Condition<Item> condition)
        {
            return new AnonymousMatch<Item>(condition);
        }
    }
}