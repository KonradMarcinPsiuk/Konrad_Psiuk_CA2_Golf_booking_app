using Microsoft.EntityFrameworkCore;

namespace Database
{
    public  class GolfRepository
    {
        private DatabaseContext _repository;

        public string DatabaseFilepath => _repository.DatabaseFilepath;
        public GolfRepository()
        {
            _repository = new DatabaseContext();
        }

        public async Task<IEnumerable<Golfer>> GetAllGolfers()
        {
            return await _repository.Golfers.ToListAsync();
        }

        public async Task<bool> AddGolfer(Golfer golfer)
        {
            _repository.Golfers.Add(golfer);
            return await _repository.SaveChangesAsync() > 0;
        }

        public async Task<Golfer> GetGolfer(int id)
        {
            return await _repository.Golfers.FindAsync(id);
        }

        public async Task<bool> ChangeGolfersData(Golfer golfer)
        {
            _repository.Entry(golfer).State = EntityState.Modified;
            return await _repository.SaveChangesAsync() > 0;
        }
    }
}
