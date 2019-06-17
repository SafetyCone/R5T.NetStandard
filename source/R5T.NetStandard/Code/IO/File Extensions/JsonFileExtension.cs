using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.NetStandard.IO
{
    public sealed class JsonFileExtension : FileExtension
    {
        /// <summary>
        /// The "json" (JavaScript serialization object notation) file-extension.
        /// </summary>
        public const string JsonFileExtensionString = "json";


        #region Static

        /// <summary>
        /// The <see cref="JsonFileExtension.JsonFileExtensionString"/> value.
        /// </summary>
        public static readonly JsonFileExtension Instance = new JsonFileExtension(JsonFileExtension.JsonFileExtensionString);

        #endregion


        private JsonFileExtension(string value)
            : base(value)
        {
        }
    }
}
