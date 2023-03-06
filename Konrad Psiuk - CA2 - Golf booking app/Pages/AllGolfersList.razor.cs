using Database;
using Microsoft.AspNetCore.Components;

namespace Konrad_Psiuk___CA2___Golf_booking_app.Pages;

public partial class AllGolfersList
{
    [Inject]
    public IGolfRepository GolfRepository { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public List<Golfer> AllGolfers { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        AllGolfers.AddRange(await GolfRepository.GetAllGolfers());
    }

    private void CreateNewGolfer ()=> NavigationManager.NavigateTo("/editgolfer");
    private void EditGolfer(int id) => NavigationManager.NavigateTo($"/editgolfer/{id}");
}