using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.NetStandard.IO
{
    public sealed class TextFileExtension : FileExtension
    {
        /// <summary>
        /// The "txt" text file-extension.
        /// </summary>
        public const string TextFileExtensionString = "txt";


        #region Static

        /// <summary>
        /// The <see cref="TextFileExtension.TextFileExtensionString"/> value.
        /// </summary>
        public static readonly TextFileExtension Instance = new TextFileExtension(TextFileExtension.TextFileExtensionString);

        #endregion


        private TextFileExtension(string value)
            : base(value)
        {
        }
    }
}
