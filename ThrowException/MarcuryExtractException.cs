using System;

namespace marcury_ext.ThrowException
{
    internal class MarcuryExtractException : Exception
    {
        public int ErrorCode { get; }
        private string customMessage;

        // Constructor receives error code and error message
        public MarcuryExtractException(int errorCode, string message)
            : base(message) // Call the constructor of the base Exception class to store the error message.
        {
            ErrorCode = errorCode;
            customMessage = message;
        }

        // Override the Message property to return a custom error message
        public override string Message {
            get {
                // Combines both the error code and the message from the base to return the complete error message
                return $"Error code: {ErrorCode} - {base.Message}";
            }
        }
    }
}
