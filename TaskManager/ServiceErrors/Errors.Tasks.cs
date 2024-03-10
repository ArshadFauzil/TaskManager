using ErrorOr;
using TaskManager.Constants;

namespace TaskManager.ServiceErrors;

public static class Errors
{
    public static class Tasks
    {
        public static Error NotFound => Error.NotFound(
            code: "Task.NotFound",
            description: "Task not found");

        public static Error InvalidName => Error.Validation(
            code: "Task.InvalidName",
            description: $"Task name must be at least {UserTaskConstants.MinTitleLength}" +
                $" characters long and at most {UserTaskConstants.MaxTitleLength} characters long.");
    }
}