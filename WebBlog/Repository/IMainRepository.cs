namespace WebBlog.Repository
{
    public interface IMainRepository<T> where T : class
    {
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(int id);
        public void DeleteAll();
        public ICollection<T> GetAll();
        public T GetById(int id);
        public bool Save();
    }
}
