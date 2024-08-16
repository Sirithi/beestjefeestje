using System.Runtime.Serialization;

namespace BeestjeFeestje.Domain.Utils
{
    [Serializable]
    public class ValidationException : Exception
    {
        private readonly List<ValidationMessage> _validationResults;

        public List<ValidationMessage> ValidationResults
        {
            get { return _validationResults; }
        }

        public ValidationException()
        {
        }

        public ValidationException(List<ValidationMessage> validationResults)
        {
            this._validationResults = validationResults;
        }

        public ValidationException(string? message) : base(message)
        {
        }

        public ValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}