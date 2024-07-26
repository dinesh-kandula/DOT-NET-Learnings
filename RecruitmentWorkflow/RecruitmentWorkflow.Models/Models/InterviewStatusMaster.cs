using System;
using System.Collections.Generic;

namespace RecruitmentWorkflow.Models.Models;

public partial class InterviewStatusMaster
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;
}
