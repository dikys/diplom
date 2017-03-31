namespace Navigation.App.Dialogs.Elements
{
    public struct DialogElement
    {
        public DialogElement(string name, DialogTypes type, int id = 0, string value = "")
        {
            Name = name;
            Type = type;
            Value = value;
            Id = id;
        }

        public DialogTypes Type { get; }

        public string Value { get; set; }

        public string Name { get; }

        public int Id { get; }
    }
}
