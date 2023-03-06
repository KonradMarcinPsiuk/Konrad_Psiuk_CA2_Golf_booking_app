using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public interface IGolfRepository
    {
        Task<IEnumerable<Golfer>> GetAllGolfers();
        Task<bool> AddGolfer(Golfer golfer);
        Task<Golfer> GetGolfer(int id);
        Task<bool> ChangeGolfersData(Golfer golfer);
        Task<IEnumerable<Tee>> GetAllTees();
        Task<Tee> GetTee(int id);

    }

    public  class GolfRepository : IGolfRepository
    {
        private DatabaseContext _repository;

        public string DatabaseFilepath => _repository.DatabaseFilepath;
        
        public GolfRepository()
        {
            _repository = new DatabaseContext();
            if (_repository.Database.CanConnect()) return;
            
            _repository.Database.EnsureCreated();
            CreateTees();
        }

        private void CreateTees()
        {
            _repository.Tees.Add(new Tee { Name = "Tee area 1" });
            _repository.Tees.Add(new Tee { Name = "Tee area 2" });
            _repository.Tees.Add(new Tee { Name = "Tee area 3" });
            _repository.Tees.Add(new Tee { Name = "Tee area 4" });
            _repository.Tees.Add(new Tee { Name = "Tee area 5" });
            _repository.SaveChanges();
        }

        public async Task<IEnumerable<Golfer>> GetAllGolfers()=>
            await _repository.Golfers.ToListAsync();
        

        public async Task<bool> AddGolfer(Golfer golfer)
        {
            _repository.Golfers.Add(golfer);
            return await _repository.SaveChangesAsync() > 0;
        }

        public async Task<Golfer> GetGolfer(int id)=>
            await _repository.Golfers.FindAsync(id);
        

        public async Task<bool> ChangeGolfersData(Golfer golfer)
        {
            _repository.Entry(golfer).State = EntityState.Modified;
            return await _repository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Tee>> GetAllTees()=>
            await _repository.Tees.ToListAsync();
        

        public async Task<Tee> GetTee(int id) => 
            await _repository.Tees.FindAsync(id);
    }
}
