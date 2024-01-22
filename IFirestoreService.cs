namespace Firebase
{
    public interface IFirestoreService
    {
        public Task<List<Shoe>> GetAll();

        public Task Add(Shoe shoe);
        public Task Update(Shoe shoe);
        public Task Delete(string id);
        public Task<Shoe> GetById(string id);
        
    }
}