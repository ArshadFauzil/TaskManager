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

        public static Error MissingFieldsTaskCreateRequest => Error.Validation(
            code: "Task.EmptyTaskCreateRequest",
            description: "Required fields not provided in create request. Required fields are" +
             $" Title, description and DueDate."
        );

        public static Error MissingFieldsTaskUpdateRequest => Error.Validation(
            code: "Task.EmptyTaskUpdateRequest",
            description: "Required fields not provided in update request. Required fields are" +
             $" Title, description, DueDate and Status"
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
        public static Error MissingFieldsCommentCreateRequest => Error.Validation(
            code: "Task.EmptyCommentCreateRequest",
            description: "Required fields not provided in Comment update request. Required fields are" +
             $" TaskId and Comment"
        );

        public static Error MissingFieldsCommentUpdateRequest => Error.Validation(
            code: "Task.EmptyCommentUpdateRequest",
            description: "Required fields not provided in Comment update request. Required fields are" +
             $" Comment"
        );

        public static Error NotFoundValidation => Error.Validation(
            code: "Task.NotFoundValidation",
            description: "Task Comment of given ID not found");

        public static Error InvalidComment => Error.Validation(
            code: "Task.InvalidComment",
            description: $"Comment must be at least {UserTaskCommentDataModel.MinCommentLength}" +
                $" characters long and at most {UserTaskCommentDataModel.MaxCommentLength} characters long.");
    }

    public static class Files
    {
        public static Error MissingFieldsFilesCreateRequest => Error.Validation(
            code: "Task.EmptyFilesCreateRequest",
            description: "Required fields not provided in Task Files Create request. Required fields are" +
             $" TaskId and Files."
        );

        public static Error MissingItemsInFilesList => Error.Validation(
            code: "Task.MissingItemsInFilesList",
            description: "One or more items in the Files list are null. List cannot contain null values."
        );

        public static Error MissingFieldsFilesCreateRequestFileData => Error.Validation(
            code: "Task.MissingFiledsFileDataCreateRequest",
            description: "One or more fields in the File Data List members in the User task File Creation request" +
                " not provided. Required fields are File and FileType."
        );

        public static Error NotFoundValidation => Error.Validation(
            code: "Task.NotFoundValidation",
            description: "Task File of given ID not found");
    }
}