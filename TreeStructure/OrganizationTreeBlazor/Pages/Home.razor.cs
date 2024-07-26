using OrganizationTreeBlazor.Services;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.Common.Trees.Models;
using Telerik.SvgIcons;
using TreeDomainLibrary.Models;
using TreeDomainLibrary.Models.Enums;
using TreeDomainLibrary.Models.ModelsDTO;

namespace OrganizationTreeBlazor.Pages
{
    public partial class Home
    {
        public IEnumerable<object> SelectedItems { get; set; } = new List<object>();
        EmployeeFlatData? SelectedItem { get; set; }
        List<EmployeeFlatData> EmployeesData { get; set; } = [];
        IEnumerable<object> ExpandedData { get; set; } = [];
        
        protected override async Task<IEnumerable<EmployeeFlatData>> OnInitializedAsync()
        {
            EmployeesData = await GetEmployeeData();
            return EmployeesData;
        }

        public async Task<List<EmployeeFlatData>> GetEmployeeData()
        {
            EmployeesData = await _apiServices.GetApiData<EmployeeFlatData>("/Employee/GetFlatEmployeesData");
            ExpandedData = EmployeesData.Where(x => x.HasChildren == true).ToList();

            return EmployeesData;
        }

        void SelectedItemsHandler(IEnumerable<object> item)
        {
            SelectedItem = item.FirstOrDefault() as EmployeeFlatData;
        }
    }
}
