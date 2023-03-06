using Database;
using Microsoft.AspNetCore.Components;

namespace Konrad_Psiuk___CA2___Golf_booking_app.Pages;

public partial class GolferEditor
{
    [Inject]
    public IGolfRepository GolfRepository { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private Golfer EditedGolfer;
    
    [Parameter]
    public string Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Id is not null && int.TryParse(Id, out var _id))
        {
            EditedGolfer = await GolfRepository.GetGolfer(_id);
        }
        else
        {
            EditedGolfer = new Golfer();
        }
    }

    async Task SaveGolfer()
    {
        if (EditedGolfer.Id <= 0)
        {
            await GolfRepository.AddGolfer(EditedGolfer);
        }
        else
        {
            await GolfRepository.ChangeGolfersData(EditedGolfer);
        }

        NavigationManager.NavigateTo("/allgolfers");
    }
}