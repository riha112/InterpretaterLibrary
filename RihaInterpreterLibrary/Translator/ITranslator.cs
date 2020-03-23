namespace RihaInterpreterLibrary.Translator
{
    public interface ITranslator
    {
        public int PriorityId { get; set; }
        string Translate(string code);
    }
}