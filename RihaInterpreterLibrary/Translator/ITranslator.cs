namespace RihaInterpreterLibrary.Translator
{
    /// <summary>
    /// Manipulates code, used to implement short hand expression and
    /// functions that are not directly implemented inside interpreters core
    /// for example : for-loop is converted to while-loop
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Order in which translator is executed (smallest goes first)
        /// Used to ensure that some translators are executed before others, as
        /// some of the translators may be depended of others, or may change
        /// code in way that other translator can't correctly translate code
        /// </summary>
        public int PriorityId { get; set; }

        /// <summary>
        /// Does the translating
        /// </summary>
        /// <param name="code">Initial code</param>
        /// <returns>Translated code</returns>
        string Translate(string code);
    }
}