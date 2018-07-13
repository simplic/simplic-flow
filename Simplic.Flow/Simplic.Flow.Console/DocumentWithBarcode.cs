using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class DocumentWithBarcode
    {
        public string Barcode { get; set; }
        public byte[] Blob { get; set; }
        public string Extension { get; set; }
    }
}
