using ErrorOr;
using TaskManager.DataModels;
using TaskManager.Enumerations;

namespace TaskManager.ServiceErrors;

public static class Errors
{
    public static class Tasks
    {
        public static Error NotFound => Error.NotFound(
            code: "Task.NotFound",
            description: "Task of given ID not found");

        public static Error NotFoundValidation => Error.Validation(
            code: "Task.NotFoundValidation",
            description: "Task of given ID not found");

        public static Error MissingFieldsCreateUpdateRequest => Error.Validation(
            code: "Task.EmptyRequest",
            description: "Required fields not provided in request. Required fields are" +
             $" Title, description and DueDate."
        );

        public static Error InvalidTitle => Error.Validation(
            code: "Task.InvalidTitle",
            description: $"Task Title must be at least {UserTaskDataModel.MinTitleLength}" +
                $" characters long and at most {UserTaskDataModel.MaxTitleLength} characters long.");

        public static Error InvalidDescription => Error.Validation(
            code: "Task.InvalidDescription",
            description: $"Task Description must be at least {UserTaskDataModel.MinDescriptionLength}" +
                $" characters long and at most {UserTaskDataModel.MaxDescriptionLength} characters long.");

        public static Error InvalidDueDate => Error.Validation(
            code: "Task.InvalidDueDate",
            description: "DueDate must be a future date"
        );

        public static Error InvalidStatusValue => Error.Validation(
            code: "Task.InvalidStatusValue",
            description: "Valid Status value must be provided. Valid Statuses are [ " +
                string.Join(", ", Enum.GetNames(typeof(StatusEnum))) + " ]"
        );
    }

    public static class Comments
    {
        public static Error NotFoundValidation => Error.Validation(
            code: "Task.NotFoundValidation",
            description: "Task Comment of given ID not found");

        public static Error InvalidComment => Error.Validation(
            code: "Task.InvalidComment",
            description: $"Comment must be at least {UserTaskCommentDataModel.MinCommentLength}" +
                $" characters long and at most {UserTaskCommentDataModel.MaxCommentLength} characters long.");
    }
}