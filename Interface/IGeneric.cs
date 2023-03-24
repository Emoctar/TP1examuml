namespace TP1examuml.Interface
{
    public interface IGeneric<T> where T: class
    {
        Task<List<T>> GEtAllAsync();
        Task<T> GetBYId(int id);
        Task<bool> Add(T entity);
        Task<bool> Delete(int id);
        Task<bool> Update(T entity);


    }
}
