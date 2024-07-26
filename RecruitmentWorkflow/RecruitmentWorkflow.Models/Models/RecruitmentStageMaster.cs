using System;
using System.Collections.Generic;

namespace RecruitmentWorkflow.Models.Models;

public partial class RecruitmentStageMaster
{
    public int Id { get; set; }

    public string Stage { get; set; } = null!;
}
