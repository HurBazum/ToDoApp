using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Entities;
using ToDoApp.Interfaces;

namespace ToDoApp.DAL.Repositories
{
    internal class GoalRepository : IRepository<Goal>
    {
        readonly GoalContext _context;
        public readonly bool autoSaveChanges;
        public GoalRepository(GoalContext goalContext)
        {
            _context = goalContext;
            autoSaveChanges = true;
        }
        public async Task<ICollection<Goal>> GetAll() => await _context.Goals.ToArrayAsync().ConfigureAwait(false);

        public async Task<Goal> AddItem(Goal entity)
        {
            if(entity is null)
            {
                throw new Exception("Попытка добавить пустую запись");
            }
            else
            {
                _context.Entry(entity).State = EntityState.Added;
                if(autoSaveChanges == true)
                {
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return entity;
            }
        }

        public async Task<bool> DeleteItem(int id)
        {
            Goal entity;
            
            entity = await GetItemById(id);
            if(entity is null)
            {
                throw new Exception($"Элемент с id={id} не найден");
            }
            else
            {
                _context.Entry(entity).State = EntityState.Deleted;
                if(autoSaveChanges == true)
                {
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }

                return autoSaveChanges;
            }
        }

        public async Task<Goal> GetItemById(int id) => await _context.Goals.FirstOrDefaultAsync(g => g.Id == id).ConfigureAwait(false) 
            ?? throw new Exception($"Элемент с таким id={id} не найден");

        public async Task<Goal> UpdateItem(Goal entity)
        {

            if(entity is null)
            { 
                throw new Exception("Попытка добавить пустую запись");
            }
            else
            {
                
                _context.Entry(entity).State = EntityState.Modified;

                if(autoSaveChanges == true)
                {
                    await _context.SaveChangesAsync();
                }

                return entity;
            }
        }
    }
}