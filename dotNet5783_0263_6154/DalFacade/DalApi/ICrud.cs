namespace DalApi
{
    public interface ICrud<T>
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
        /// <returns></returns>
        public IEnumerable<T> GetAll();
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
