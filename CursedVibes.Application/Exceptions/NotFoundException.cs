
namespace CursedVibes.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, string additionalContext)
            : base($"{message} - {additionalContext}") { }
    }
}
