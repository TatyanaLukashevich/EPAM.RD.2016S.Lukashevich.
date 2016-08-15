namespace UserStorage.Interface
{
    public interface ICriteria<T>
    {
        bool IsMatch(T entity);
    }
}
