using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableDataAccessLayer
{
    internal class ReturnData
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    internal class Status
    {
        public const bool Success = true;
        public const bool Failure = false;

    }

    internal class StatusMessage
    {
        public const string Success = "Success";
        public const string NoRecordsFound = "No Records Found";
        public const string InvalidInput = "Invalid user input found!";
        public const string InvalidSession = "Invalid session information!";
    }
}