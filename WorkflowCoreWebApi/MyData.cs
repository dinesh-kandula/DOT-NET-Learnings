namespace WorkflowCoreWebApi
{
    public class MyData
    {
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public string currentInterviewerName { get; set; }
        public string currentInterviewStage { get; set; }
        public string currentInterviewStatus { get; set; }
        public string candidateStatus { get; set; }
    }

    public class MyDataDTO
    {
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
    }

    public class TaskData
    {
        public string? Value1 { get; set; }
        public string? Value2 { get; set; }
        public int Counter { get; set; }
    }
}