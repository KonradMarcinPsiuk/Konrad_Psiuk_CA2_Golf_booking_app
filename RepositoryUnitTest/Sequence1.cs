using Database;
using Microsoft.EntityFrameworkCore;

namespace RepositoryUnitTest
{
    [Collection("Sequence")]
    public class Sequence1
    {
        private DbRepositoryFixture _fixture;

        public Sequence1(DbRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AddGolfer_ReturnTrue()
        {
            var newGolfer = new Golfer();
            newGolfer.FirstName = "John";
            newGolfer.LastName = "Doe";
            newGolfer.EmailAddress = "jg@gmail.com";
            newGolfer.Sex = "Male";
            newGolfer.Handicap = 10;
            var result = await _fixture.GolfRepository.AddGolfer(newGolfer);
            Assert.True(result.Id>0);
        }

        [Fact]
        public async Task GetAllTees()
        {
            var tees = await _fixture.GolfRepository.GetAllTees();
            Assert.NotEmpty(tees);
        }

        [Fact]
        public async Task AddTeeBooking_SaveBooking_ReturnBookingWithId()
        {
            var booking = new TeeBooking();
            booking.BookingTime = DateTime.Now;
            var tees = await _fixture.GolfRepository.GetAllTees();
            booking.BookedTee = tees.First();
            var savedBooking = await _fixture.GolfRepository.SaveTeeBooking(booking);
            Assert.True(savedBooking.Id>0);

            var getBooking = await _fixture.GolfRepository.GetTeeBooking(savedBooking.Id);
            Assert.NotNull(getBooking);
            Assert.True(getBooking.Id==savedBooking.Id);

            getBooking.BookingTime = new DateTime(2023, 1, 1, 12, 0, 0);
            var updatedBooking = await _fixture.GolfRepository.SaveTeeBooking(getBooking);
            Assert.True(updatedBooking.BookingTime.Year == 2023 && updatedBooking.BookingTime.Hour == 12);
        }
    }
}