namespace DoenaSoft.DownloadRenamer
{
    using System;

    public sealed class BuilderException : Exception
    {
        public BuilderException(string message) : base(message)
        {
        }
    }
}