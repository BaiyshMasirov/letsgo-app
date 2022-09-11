namespace Application.Models
{
    public record Result(bool Succeed, string[] Messages, ApiUserToken Token)
    {
        public static Result Success() => new(true, null, null);

        public static Result Success(string message) => new(true, new[] { message }, null);

        public static Result Success(ApiUserToken token) => new(true, null, token);

        public static Result Success(IList<string> messages) => new(true, messages.ToArray(), null);

        public static Result Failure(string error) => new(false, new[] { error }, null);

        public static Result Failure(IList<string> errors) => new(false, errors.ToArray(), null);
    }
}