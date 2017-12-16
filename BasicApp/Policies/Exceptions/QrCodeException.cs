using System;
namespace BasicApp.Policies.Exceptions
{
    /// <summary>
    /// Exception thrown when an error occurs during the generation of a Voucher's QrCode
    /// </summary>
    public class QrCodeException : Exception
    {
        public QrCodeException(string message) : base(message)
        {

        }
    }
}
