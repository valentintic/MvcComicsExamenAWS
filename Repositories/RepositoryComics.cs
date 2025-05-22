using Microsoft.EntityFrameworkCore;
using MvcComicsExamen.Data;
using MvcComicsExamen.Models;

namespace MvcComicsExamen.Repositories
{
    public class RepositoryComics
    {
        private ComicsContext context;
        public RepositoryComics(ComicsContext context)
        {
            this.context = context;
        }

        private async Task<int> GetMaxIdComicsAsync()
        {
            return await this.context.Comic.MaxAsync(x => x.ID) + 1;
        }

        public async Task<List<Comic>> GetComics()
        {
            return await this.context.Comic.ToListAsync();
        }

        public async Task<Comic> GetComic(int id)
        {
            return await this.context.Comic.FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task AddComic(Comic comic)
        {
            comic.ID = await GetMaxIdComicsAsync();
            this.context.Comic.Add(comic);
            await this.context.SaveChangesAsync();
        }
    }
}
