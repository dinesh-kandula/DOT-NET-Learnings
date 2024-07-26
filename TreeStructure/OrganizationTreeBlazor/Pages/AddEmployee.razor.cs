using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Text.Json;
using Telerik.Blazor.Components.Common.Trees.Models;
using TreeDomainLibrary.ErrorHandlingHelper;
using TreeDomainLibrary.Models;
using TreeDomainLibrary.Models.ModelsDTO;
using System.ComponentModel;
using Telerik.Blazor.Components;

namespace OrganizationTreeBlazor.Pages
{
    public partial class AddEmployee
    {
        private bool Visible { get; set; } = false;
        private string Title { get; set; } = "Recruiting Form of Employee";
        private bool LoaderOnFlag { get; set; } = false;
        private bool PostMessageFlag { get; set; } = false;
        private ProblemDetails? ProblemDetails {  get; set; }
        private EmployeeTreeNode? EmployeeTreeNode { get; set; }

        private TelerikDialog? dialog;

        [Parameter]
        public EventCallback GetEmployeeData { get; set; }
        [Parameter]
        public EmployeeFlatData? SelectedEmployeeData { get; set; }

        public EmployeeDTO EmployeeModel { get; set; } = new EmployeeDTO();
        private EditContext? EmployeeEditContext { get; set; }

        void VisibleChangedHandler(bool currVisible)
        {
            Visible = currVisible; // If you don't do this, the Dialog won't close because of the user action
        }

        public void ToggleDialog()
        {
            Visible = !Visible;

            if(SelectedEmployeeData != null)
            {
                EmployeeModel.ParentId = SelectedEmployeeData.Id;
            }
            EmployeeEditContext = new EditContext(EmployeeModel);
        }

        private async Task OnValidSubmitHandler(EditContext editContext)
        {
            LoaderOnFlag = true;
            UpdateDialogState();
            try
            {
                var responseBody = await AddEmployeeAsync();
                await Console.Out.WriteLineAsync(responseBody);
                ProblemDetails = null;
                EmployeeTreeNode = null;
                if (responseBody.ToLower().Contains("title") && responseBody.ToLower().Contains("message") && responseBody.ToLower().Contains("status"))
                {
                    ProblemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                }
                else
                {
                    EmployeeTreeNode = JsonSerializer.Deserialize<EmployeeTreeNode>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                LoaderOnFlag = false;
                PostMessageFlag = true;
                UpdateDialogState();

                await Task.Delay(2000);
                PostMessageFlag = false;

                if (ProblemDetails != null)
                {
                    UpdateDialogState();
                    Visible = true;
                }
                else
                {
                    Visible = false;
                    ClearButton();
                    await GetEmployeeData.InvokeAsync();
                    StateHasChanged();
                }
            }
        }

        public void UpdateDialogState()
        {
            dialog.Refresh();
        }

        private void ClearButton()
        {
            EmployeeModel = new EmployeeDTO();
            EmployeeEditContext = new EditContext(EmployeeModel);
            UpdateDialogState();
        }

        public async Task<string> AddEmployeeAsync()
        {
            string responseBody = await _apiServices.PostAsync<EmployeeDTO>("/Employee/AddEmployee", EmployeeModel);
            return responseBody;
        }
    }
}
