namespace PHDesktopUI.Librery.Models
{
    public class CreateProjectModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateProjectModel(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
}
