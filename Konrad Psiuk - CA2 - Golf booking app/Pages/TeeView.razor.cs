using Database;
using Microsoft.AspNetCore.Components;

namespace Konrad_Psiuk___CA2___Golf_booking_app.Pages;

public partial class TeeView
{
    [Inject]
    public IGolfRepository GolfRepository { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Parameter]
    public string Id { get; set; }

    private Tee ViewedTee;

    protected override async Task OnInitializedAsync()
    {
        if (int.TryParse(Id, out var _id))
        {
            ViewedTee = await GolfRepository.GetTee(_id);
        }
    }

    void OpenBooking(int id)
    {
        NavigationManager.NavigateTo($"/bookingeditor/{id}");
    }
    
    void NewBooking()
    {
        NavigationManager.NavigateTo($"/bookingeditor/new/{ViewedTee.Id}");
    }
}