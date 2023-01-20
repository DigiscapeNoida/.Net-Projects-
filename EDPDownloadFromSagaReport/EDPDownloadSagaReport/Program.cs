using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDPDownloadSagaReport
{
    class Program
    {
        static void Main(string[] args)
        {
            SagaXMLDownload obj = new SagaXMLDownload();
            obj.Process_Initiate();

        }
    }
}
