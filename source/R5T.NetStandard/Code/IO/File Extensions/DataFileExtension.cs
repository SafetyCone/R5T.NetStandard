using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.NetStandard.IO
{
    public sealed class DataFileExtension : FileExtension
    {
        /// <summary>
        /// The Visual Studio solution file-extension.
        /// </summary>
        public const string DataFileExtensionString = "data";


        #region Static

        /// <summary>
        /// The <see cref="DataFileExtension.DataFileExtensionString"/> value.
        /// </summary>
        public static readonly DataFileExtension Instance = new DataFileExtension(DataFileExtension.DataFileExtensionString);

        #endregion


        private DataFileExtension(string value)
            : base(value)
        {
        }
    }
}
