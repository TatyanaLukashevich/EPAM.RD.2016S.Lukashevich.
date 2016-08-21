namespace UserStorage.Interface
{
    /// <summary>
    /// Criteria interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICriteria<T>
    {
        /// <summary>
        /// Check whether an user satisfies the search criteria
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsMatch(T entity);
    }
}
