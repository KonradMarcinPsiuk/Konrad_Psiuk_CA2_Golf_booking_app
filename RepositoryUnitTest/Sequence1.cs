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
            Assert.True(result);
        }

       
      
        
    }
}