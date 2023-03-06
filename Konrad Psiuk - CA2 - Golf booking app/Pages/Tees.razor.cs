using Database;
using Microsoft.AspNetCore.Components;

namespace Konrad_Psiuk___CA2___Golf_booking_app.Pages;

public partial class Tees
{
    [Inject]
    public IGolfRepository GolfRepository { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    public List<Tee> AllTees { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        AllTees.AddRange(await GolfRepository.GetAllTees());
    }

    void OpenTee(int id)
    {
        NavigationManager.NavigateTo($"/teeview/{id}");
    }
}