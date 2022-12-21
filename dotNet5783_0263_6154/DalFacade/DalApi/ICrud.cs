using DO;

namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        /// <summary>
        /// this function adds a new object to the list
        /// </summary>
        /// <param name="entity"></param>
        public int Add(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public T GetByPredicat(Func<T?, bool> func);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<T?> GetAll(Func<T?, bool>? func = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity);

    }
}
