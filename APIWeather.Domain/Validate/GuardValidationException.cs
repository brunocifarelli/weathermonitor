using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace APIWeather.Domain.Validate
{
    [Serializable]
    public class GuardValidationException : Exception
    {
        public ReadOnlyCollection<GuardValidationResult> Errors { get; }

        public GuardValidationException()
        {
        }

        public GuardValidationException(ReadOnlyCollection<GuardValidationResult> readOnlyCollection)
        {
            Errors = readOnlyCollection;
        }

        public GuardValidationException(string message) : base(message)
        {
        }

        public GuardValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GuardValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
