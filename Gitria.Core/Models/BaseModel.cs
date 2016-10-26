namespace Gitria.Core.Models
{
    public class BaseModel
    {
        public string Error { get; set; }
        public bool HasError { get; set; }

        public void AddError(string message)
        {
            Error = message;
            HasError = true;
        }
    }
}
