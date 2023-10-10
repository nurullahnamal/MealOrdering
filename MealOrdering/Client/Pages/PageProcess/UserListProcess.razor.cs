using System.Net.Http.Json;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;

namespace MealOrdering.Client.Pages.Users
{
    public class UserListProcess:ComponentBase
    {
        [Inject]
        public HttpClient Client { get; set; }


        protected  List<UserDTO> userlList = new List<UserDTO>();



        protected async override Task OnInitializedAsync()
        {
            await LoadList();
        }

        protected async Task LoadList()
        {
           var serviceResponse= await Client.GetFromJsonAsync<ServiceResponse<List<UserDTO>>>("api/User/Users");
           if (serviceResponse.Success)
           {
               userlList = serviceResponse.Value;
           }
        }
    }
}
