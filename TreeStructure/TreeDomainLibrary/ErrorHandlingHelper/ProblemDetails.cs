
using Newtonsoft.Json;

namespace TreeDomainLibrary.ErrorHandlingHelper
{

    public class ProblemDetails
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int Status {  get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
