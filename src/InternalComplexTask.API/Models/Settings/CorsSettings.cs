namespace InternalComplexTask.API.Models.Settings
{
    public class CorsSettings
    {
        public Rule[] Rules { get; set; }
    }

    public class Rule
    {
        public bool Allow { get; set; }
        public string Origin { get; set; }
    }
}
