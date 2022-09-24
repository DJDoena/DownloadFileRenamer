namespace DoenaSoft.DownloadRenamer
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    [XmlRoot]
    public sealed class CrypticNames
    {
        public CrypticName[] CrypticNameList;
    }

    [DebuggerDisplay("DownloadName: {DownloadName}, ShortName: {ShortName}")]
    public sealed class CrypticName
    {
        public string DownloadName;

        public string ShortName;
    }
}