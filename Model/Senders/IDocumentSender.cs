using System.Collections.Generic;
using System.IO;

namespace EFx.Model.Senders
{
    public interface IDocumentSender
    {
        void Send(IList<FileInfo> scannedFiles);
    }
}