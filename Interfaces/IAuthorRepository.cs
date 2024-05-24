namespace BookEntityFramework.Services
{
    public interface IAuthorRepository
    {

        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> CreateAuthor(AuthorDto authorDto);
        Task<Author> UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto);
        Task<bool> DeleteAuthor(int id);
    }
}
