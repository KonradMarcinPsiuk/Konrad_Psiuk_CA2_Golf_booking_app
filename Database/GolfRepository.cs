using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public interface IGolfRepository
    {
        Task<IEnumerable<Golfer>> GetAllGolfers();
        Task<Golfer> AddGolfer(Golfer golfer);
        Task<Golfer> GetGolfer(int id);
        Task<Golfer> ChangeGolfersData(Golfer golfer);
        Task<IEnumerable<Tee>> GetAllTees();
        Task<Tee> GetTee(int id);
        Task<TeeBooking> GetTeeBooking(int id);
        Task<TeeBooking> SaveTeeBooking(TeeBooking booking);

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
            _repository.ChangeTracker.Clear();
        }

        public async Task<IEnumerable<Golfer>> GetAllGolfers()=>
            await _repository.Golfers.AsNoTracking().ToListAsync();
        

        public async Task<Golfer> AddGolfer(Golfer golfer)
        {
            _repository.Golfers.Add(golfer);
            await _repository.SaveChangesAsync();
            _repository.ChangeTracker.Clear();
            return golfer;
        }

        public async Task<Golfer> GetGolfer(int id)=>
            await _repository.Golfers.AsNoTracking().FirstAsync(x=>x.Id==id);
        

        public async Task<Golfer> ChangeGolfersData(Golfer golfer)
        {
            _repository.Entry(golfer).State = EntityState.Modified;
            await _repository.SaveChangesAsync();
            _repository.ChangeTracker.Clear();
            return golfer;
        }

        public async Task<IEnumerable<Tee>> GetAllTees()=>
            await _repository.Tees.AsNoTracking().ToListAsync();
        

        public async Task<Tee> GetTee(int id) => 
            await _repository.Tees.AsNoTracking()
                .Include(tee=>tee.Bookings)
                .ThenInclude(booking=>booking.Golfers)
                .FirstAsync(x=>x.Id==id);

        public async Task<TeeBooking> GetTeeBooking(int id) =>
            await _repository.TeeBookings.AsNoTracking().FirstAsync(x=>x.Id==id);

        public async Task<TeeBooking> SaveTeeBooking(TeeBooking booking)
        {
            if(booking.BookedTee!=null)
                _repository.Tees.Attach(booking.BookedTee);
            
            if(booking.Golfers!=null)
                booking.Golfers.Select(golfer => _repository.Golfers.Attach(golfer));

            if (booking.Id > 0)
            {
                _repository.TeeBookings.Update(booking);
            }
            else
            {
                _repository.TeeBookings.Add(booking);
            }
            
            await _repository.SaveChangesAsync();
            _repository.ChangeTracker.Clear();
            return booking;
        }
    }
}
