using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Telerik.Blazor.Components;
using TreeDomainLibrary.ErrorHandlingHelper;
using TreeDomainLibrary.Models.ModelsDTO;
using TreeDomainLibrary.Models;

namespace OrganizationTreeBlazor.Pages
{
    public partial class DeleteEmployee
    {
        private bool Visible { get; set; } = false;
        private string Title { get; set; } = "Delete Form for Employee";
        private bool LoaderOnFlag { get; set; } = false;
        private bool PostMessageFlag { get; set; } = false;
        private ProblemDetails? ProblemDetails { get; set; }
        private Employee? EmployeeTreeNode { get; set; }

        private TelerikDialog? dialog;

        [Parameter]
        public EventCallback GetEmployeeData { get; set; }
        [Parameter]
        public EmployeeFlatData? SelectedEmployeeData { get; set; }

        public DeleteEmployeeDTO EmployeeModel { get; set; } = new DeleteEmployeeDTO();
        private EditContext? EmployeeEditContext { get; set; }

        void VisibleChangedHandler(bool currVisible)
        {
            Visible = currVisible; // If you don't do this, the Dialog won't close because of the user action
        }

        public async Task ToggleDialogAsync()
        {
            Visible = !Visible;

            if (SelectedEmployeeData != null)
            {
                EmployeeModel.EmployeeId = SelectedEmployeeData.EmployeeId;
                if (!SelectedEmployeeData.HasChildren) {
                    EmployeeModel.NewParentId = 1;
                    EmployeeEditContext = new EditContext(EmployeeModel);
                    await OnValidSubmitHandler(EmployeeEditContext);
                }else
                    EmployeeEditContext = new EditContext(EmployeeModel);
            }
            else
                EmployeeEditContext = new EditContext(EmployeeModel);
        }

        private async Task OnValidSubmitHandler(EditContext editContext)
        {
            LoaderOnFlag = true;
            UpdateDialogState();
            try
            {
                var responseBody = await DeleteEmployeeAsync();

                ProblemDetails = null;
                EmployeeTreeNode = null;
                if (responseBody.Contains("title") && responseBody.Contains("message") && responseBody.Contains("status"))
                {
                    ProblemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                    Console.WriteLine($"{ProblemDetails.Message}, {ProblemDetails.Title}, {ProblemDetails.Status}");
                }
                else
                {
                    EmployeeTreeNode = JsonSerializer.Deserialize<Employee>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                    Console.WriteLine($"{EmployeeTreeNode.Name}, {EmployeeTreeNode.Id}, {EmployeeTreeNode.ParentId}, {EmployeeTreeNode.JobTitle}");
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
            EmployeeModel = new DeleteEmployeeDTO();
            EmployeeEditContext = new EditContext(EmployeeModel);
            UpdateDialogState();
        }

        public async Task<string> DeleteEmployeeAsync()
        {
            string responseBody = await _apiServices.DeleteAsync<DeleteEmployeeDTO>("/Employee/DeleteEmployee", EmployeeModel);
            return responseBody;
        }
    }
}
