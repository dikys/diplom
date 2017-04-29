namespace Navigation.App.Dialogs.Elements
{
    public struct DialogElement
    {
        public DialogElement(string name, DialogElementTypes elementType, string value = "")
        {
            Name = name;
            ElementType = elementType;
            Value = value;
        }

        public DialogElementTypes ElementType { get; }

        public string Value { get; set; }

        public string Name { get; }
    }
}
