using Database;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Konrad_Psiuk___CA2___Golf_booking_app.Pages;

public partial class BookingEditor
{
    [Parameter] 
    public string Id { get; set; }
    [Parameter]
    public string teeId { get; set; }
    [Inject]
    public IGolfRepository GolfRepository { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public IJSRuntime JsRuntime { get; set; }

    private TeeBooking EditedBooking;

    private List<Golfer> Golfers = new();
    private List<Golfer> SelectableGolfers =>
        new (Golfers.Where(g=>EditedBooking.Golfers.Any(sg=>sg.Id==g.Id)==false));

    private Golfer SelectedGolfer;
    protected override async Task OnInitializedAsync()
    {
        if (Id is not null && int.TryParse(Id, out var _id))
        {
            EditedBooking = await GolfRepository.GetTeeBooking(_id);
            BookingDate = DateOnly.FromDateTime(EditedBooking.BookingTime);
            BookingTime = TimeOnly.FromDateTime(EditedBooking.BookingTime);
        }
        else
        {
            EditedBooking = new TeeBooking();
            EditedBooking.BookedTee = await GolfRepository.GetTee(int.Parse(teeId));
            EditedBooking.Golfers = new();
            EditedBooking.BookingTime = DateTime.Now.AddDays(1);
        }
        Golfers.AddRange(await GolfRepository.GetAllGolfers());
        
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (SelectableGolfers.Any())
        {
            SelectedGolfer = SelectableGolfers.First();
        }
    }

    async Task SaveBooking()
    {
        EditedBooking.BookingTime = new DateTime(BookingDate.Year, BookingDate.Month, BookingDate.Day, BookingTime.Hour,
            BookingTime.Minute, BookingTime.Second);
        await GolfRepository.SaveTeeBooking(EditedBooking);
        NavigationManager.NavigateTo($"/teeview/{EditedBooking.Id}");
    }
    DateOnly BookingDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(1)); 
    TimeOnly BookingTime { get; set; } = new (10, 0, 0);
    
    List<TimeOnly> GetTimeIntervals()
    {
        var intervals = new List<TimeOnly>();
        var startTime = new TimeOnly(8, 0); 

        for (int i = 0; i < 40; i++)
        {
            intervals.Add(startTime);
            startTime = startTime.AddMinutes(15);
        }
        return intervals;
    }

    async Task AddGolfer()
    {
        if (SelectedGolfer == null)
        {
            await JsRuntime.InvokeVoidAsync("showalert", "Select the golfer");
        }
        else if (EditedBooking.Golfers.Count >= 4)
        {
            await JsRuntime.InvokeVoidAsync("showalert", "Max number of golfers already selected");
        }
        else
        {
            EditedBooking.Golfers.Add(SelectedGolfer);
        }
        
    }

    void RemoveGolfer(Golfer golfer)
    {
        EditedBooking.Golfers.Remove(golfer);
        StateHasChanged();
    }
}