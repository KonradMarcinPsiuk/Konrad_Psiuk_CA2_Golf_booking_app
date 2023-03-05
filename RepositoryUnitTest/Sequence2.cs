namespace RepositoryUnitTest;

[Collection("Sequence")]
public class Sequence2
{
    private DbRepositoryFixture _fixture;

    public Sequence2(DbRepositoryFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task GetGolferById_ReturnGolfer()
    {
        var golfer = await _fixture.GolfRepository.GetGolfer(1);
        Assert.NotNull(golfer);
        Assert.True(string.Equals(golfer.FirstName, "John"));
    }

    [Fact]
    public async Task UpdateGolfer_ReturnTrue()
    {
        var golfer = await _fixture.GolfRepository.GetGolfer(1);
        golfer.Handicap = 5;
        await _fixture.GolfRepository.ChangeGolfersData(golfer);

        var updatedGolfer =await _fixture.GolfRepository.GetGolfer(1);
        Assert.True(updatedGolfer.Handicap==5);
    }


    [Fact]
    public async Task GetAllGolfers()
    {
        var golfers = await _fixture.GolfRepository.GetAllGolfers();
        Assert.NotEmpty(golfers);
    }


}