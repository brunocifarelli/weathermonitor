
namespace APIWeather.Domain.Validate
{
    /// <summary>
    /// Classe de validação genérica, utilizar para realizar validações em sequencia.
    /// </summary>
    public class Guard
    {
        private readonly List<GuardValidationResult> _validationResults = new List<GuardValidationResult>();

        private Guard()
        {

        }

        public static void Validate(Action<Guard> action)
        {
            var guard = new Guard();
            action.Invoke(guard);

            if (guard._validationResults.Any())
            {
                throw new GuardValidationException(guard._validationResults.AsReadOnly());
            }
        }
        
        public Guard IsNull(object obj, string name, string message)
        {
            if (obj == null)
            {
                _validationResults.Add(new GuardValidationResult(name, message));
            }

            return this;
        }

        public Guard IsNotNull(object obj, string name, string message)
        {
            if (obj != null)
            {
                _validationResults.Add(new GuardValidationResult(name, message));
            }

            return this;
        }


        public Guard IsEmptyGuid(Guid obj, string name, string message)
        {
            if (obj == Guid.Empty)
            {
                _validationResults.Add(new GuardValidationResult(name, message));
            }

            return this;
        }
        
        public Guard IsNullOrEmptyString(string str, string name, string message)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                _validationResults.Add(new GuardValidationResult(name, message));
            }

            return this;
        }
        
        public Guard IsFalse(bool obj, string name, string message)
        {
            if (!obj)
            {
                _validationResults.Add(new GuardValidationResult(name, message));
            }

            return this;
        }

        public Guard IsTrue(bool obj, string name, string message)
        {
            if (obj)
            {
                _validationResults.Add(new GuardValidationResult(name, message));
            }

            return this;
        }
    }

    public class GuardResult
    {
        public bool IsValid => !ValidationResults.Any();
        public IEnumerable<GuardValidationResult> ValidationResults { get; }

        public GuardResult(IEnumerable<GuardValidationResult> validationResults)
        {
            ValidationResults = validationResults;
        }
    }
}
