using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.NetStandard.IO
{
    public static class FileExtensions
    {
        /// <summary>
        /// The <see cref="DataFileExtension.Instance"/>.
        /// </summary>
        public static FileExtension Data => DataFileExtension.Instance;
        /// <summary>
        /// The <see cref="JsonFileExtension.Instance"/>.
        /// </summary>
        public static FileExtension Json => JsonFileExtension.Instance;
        /// <summary>
        /// The <see cref="TextFileExtension.Instance"/>.
        /// </summary>
        public static FileExtension Text => TextFileExtension.Instance;
    }
}
