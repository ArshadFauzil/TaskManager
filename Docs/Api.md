# Task Manager API

- [Task Manager API](#task-manager-api)
  - [User Tasks](#user-tasks)
    - [Create Task](#create-task)
      - [Create Task Request](#create-task-request)
      - [Create Task Success Response](#create-task-success-response)
      - [Create Task Error Response](#create-task-error-response)
    - [Get Task](#get-task)
      - [Get Task Request](#get-task-request)
      - [Get Task Success Response](#get-task-success-response)
      - [Get Task Error Response](#get-task-error-response)
    - [Update Task](#update-task)
      - [Update Task Request](#update-task-request)
      - [Update Task Success Response](#update-task-success-response)
      - [Update Task Error Response](#update-task-error-response)
    - [Delete Task](#delete-task)
      - [Delete Task Request](#delete-task-request)
      - [Delete Task Success Response](#delete-task-success-response)
      - [Delete Task Error Response](#delete-task-error-response)
  - [User Task Comments](#user-task-comments)
    - [Create Comment](#create-comment)
      - [Create Comment Request](#create-comment-request)
      - [Create Comment Success Response](#create-comment-success-response)
      - [Create Comment Error Response](#create-comment-error-response)
    - [Get Comment](#get-comment)
      - [Get Comment Request](#get-comment-request)
      - [Get Comment Success Response](#get-comment-success-response)
      - [Get Comment Error Response](#get-comment-error-response)
    - [Update Comment](#update-comment)
      - [Update Comment Request](#update-comment-request)
      - [Update Comment Success Response](#update-comment-success-response)
      - [Update Comment Error Response](#update-comment-error-response)
    - [Delete Comment](#delete-comment)
      - [Delete Comment Request](#delete-comment-request)
      - [Delete Comment Success Response](#delete-comment-success-response)
      - [Delete Comment Error Response](#delete-comment-error-response)
  - [User Task Files](#user-task-files)
    - [Create File](#create-file)
      - [Create File Request](#create-file-request)
      - [Create File Success Response](#create-file-success-response)
      - [Create File Error Response](#create-file-error-response)

# User Tasks

## Create Task

### Create Task Request

```js
POST / tasks;
```

```json
{
  "title": "Dentist's Appintment",
  "description": "appintment with Dr. X",
  "dueDate": "2023-04-09T10:00:00"
}
```

### Create Task Success Response

```js
201 Created
```

Returns the database ID of the newly created Task.

```js
"c8570655-f049-4f45-8c81-d87580abbcd5";
```

### Create Task Error Response

```js
400 Bad Request
```

```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "ERROR ID 1": [
            "ERROR MESSAGE"
        ],
        "ERROR ID 2": [
            "ERROR MESSAGE"
        ],
        ...
    },
    "traceId": "00-4f46d2175f88f441b5414bf8c9252ace-3e9bd134807f6111-00"
}
```

## Get Task

### Get Task Request

```js
GET /tasks/{{id}};
```

### Get Task Success Response

```js
200 Ok
```

```json
{
  "id": "c8570655-f049-4f45-8c81-d87580abbcd5",
  "title": "Dentist's Appintment",
  "description": "appintment with Dr. X",
  "dueDate": "2023-03-09T10:00:00",
  "status": "INCOMPLETE",
  "lastUpdatedDate": "2024-03-18T16:13:53.398427",
  "numberOfPagesOfUserTasks": 3
}
```

### Get Task Error Response

```js
404 Not Found
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Task of given ID not found",
  "status": 404,
  "traceId": "00-320f6a2503c8d7ccfd6959f4000dbf47-6a909e8fa992ec40-00"
}
```

## Get All Task By Page Number

### Get All Tasks Request

```js
GET /tasks?pageNumber={{pageNumber}};
```

### Get Task Success Response

```js
200 Ok
```

```json
[
  {
    "id": "c8570655-f049-4f45-8c81-d87580abbcd5",
    "title": "Dentist's Appintment",
    "description": "appintment with Dr. X",
    "dueDate": "2023-03-09T10:00:00",
    "status": "COMPLETE",
    "lastUpdatedDate": "2024-03-18T16:13:53.398427",
    "numberOfPagesOfUserTasks": 3
  },
  {
  {
    "id": "a556b41d-342a-4796-a5cb-516d332214ac",
    "title": "File taxes",
    "description": "File taxes",
    "dueDate": "2023-03-29T10:00:00",
    "status": "INCOMPLETE",
    "lastUpdatedDate": "2024-03-18T16:13:53.398427",
    "numberOfPagesOfUserTasks": 3
  },
  ...
]
```

### Get All Tasks Error Response

```js
400 Bad Request
```

```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "ERROR ID 1": [
            "ERROR MESSAGE"
        ],
        "ERROR ID 2": [
            "ERROR MESSAGE"
        ],
        ...
    },
    "traceId": "00-41be0f27ec244ad6d1e25be489c21e56-a9f8c82c4c4e282f-00"
}
```

## Update Task

### Update Task Request

```js
PUT /tasks/{{id}}
```

```json
{
  "title": "Dentist's Appintment",
  "description": "appintment with Dr. X",
  "dueDate": "2023-04-11T11:30:00",
  "status": "COMPLETE"
}
```

### Update Task Success Response

```js
204 No Content
```

### Update Task Error Response

```js
400 Bad Request
```

```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "ERROR ID 1": [
            "ERROR MESSAGE"
        ],
        "ERROR ID 2": [
            "ERROR MESSAGE"
        ],
        ...
    },
    "traceId": "00-4f46d2175f88f441b5414bf8c9252ace-3e9bd134807f6111-00"
}
```

## Delete Task

### Delete Task Request

```js
DELETE /tasks/{{id}}
```

### Delete Task Success Response

```js
204 No Content
```

### Delete Task Error Response

```js
404 Not Found
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Task of given ID not found",
  "status": 404,
  "traceId": "00-320f6a2503c8d7ccfd6959f4000dbf47-6a909e8fa992ec40-00"
}
```

# User Task Comments

## Create Comment

### Create Comment Request

```js
POST / tasks / comments;
```

```json
{
  "taskId": "c8570655-f049-4f45-8c81-d87580abbcd5",
  "comment": "appintment with Dr. X"
}
```

### Create Comment Success Response

```js
201 Created
```

Returns the database ID of the newly created Comment.

```js
"8a765f0c-7769-4f30-9bac-cb7888e931a0";
```

### Create Comment Error Response

```js
400 Bad Request
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
        "ERROR ID 1": [
            "ERROR MESSAGE"
        ],
        "ERROR ID 2": [
            "ERROR MESSAGE"
        ],
        ...
  },
  "traceId": "00-36f8f7c191b21e31d5b7f803e7b38f9e-3f634f8d606ab110-00"
}
```

## Get Comment

### Get Comment Request

```js
GET /tasks/comments/{{id}};
```

### Get Comment Success Response

```js
200 Ok
```

```json
{
  "id": "f7f95a54-1104-407a-90ae-30d7af1db078",
  "taskId": "931531c0-d520-4ba4-8ed5-3eda45655f3a",
  "comment": "comment body",
  "lastUpdatedDate": "2024-03-18T17:11:59.55896"
}
```

### Get Comment Error Response

```js
404 Not Found
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Comment of given ID not found",
  "status": 404,
  "traceId": "00-8d7c4eec03f3d3f5b00b89934871224a-973a20ac7d9ecb06-00"
}
```

## Get Comments By Task ID

### Get Comments Request

```js
GET /tasks/{{taskId}}/comments;
```

### Get Comments Success Response

```js
200 Ok
```

```json
[
    {
        "id": "f7f95a54-1104-407a-90ae-30d7af1db078",
        "taskId": "931531c0-d520-4ba4-8ed5-3eda45655f3a",
        "comment": "sample comment",
        "lastUpdatedDate": "2024-03-18T17:11:59.55896"
    },
    {
        "id": "e0c584ea-1d9a-4ce8-a43c-21c4590357f2",
        "taskId": "931531c0-d520-4ba4-8ed5-3eda45655f3a",
        "comment": "random comment",
        "lastUpdatedDate": "2024-03-18T17:01:28.364576"
    },
    {
        "id": "aa634165-2db2-413c-b2d3-c7e3026c958e",
        "taskId": "931531c0-d520-4ba4-8ed5-3eda45655f3a",
        "comment": "deqeqfeq efeqfqefq fefqefqefqef",
        "lastUpdatedDate": "2024-03-18T17:06:38.341421"
    },
    {
        "id": "70ebd0ed-8321-4764-85f1-bf036abe43a4",
        "taskId": "931531c0-d520-4ba4-8ed5-3eda45655f3a",
        "comment": "qfqefqefeqfe",
        "lastUpdatedDate": "2024-03-17T20:50:20.739497"
    },
    ...
]
```

### Get Comments Error Response

```js
400 Bad Request
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "ERROR ID 1": [
            "ERROR MESSAGE"
        ],
        "ERROR ID 2": [
            "ERROR MESSAGE"
        ],
        ...
  },
  "traceId": "00-ae2cd7abb0a91fdd416c1ac3905cef46-0697175a5748fdfb-00"
}
```

## Update Comment

### Update Comment Request

```js
PUT /tasks/comments/{{id}}
```

```json
{
  "comment": "Updated comment"
}
```

### Update Comment Success Response

```js
204 No Content
```

### Update Comment Error Response

```js
400 Bad Request
```

```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "ERROR ID 1": [
            "ERROR MESSAGE"
        ],
        "ERROR ID 2": [
            "ERROR MESSAGE"
        ],
        ...
    },
    "traceId": "00-4f46d2175f88f441b5414bf8c9252ace-3e9bd134807f6111-00"
}
```

## Delete Comment

### Delete Comment Request

```js
DELETE /tasks/comments/{{id}}
```

### Delete Comment Success Response

```js
204 No Content
```

### Delete Comment Error Response

```js
404 Not Found
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Comment of given ID not found",
  "status": 404,
  "traceId": "00-320f6a2503c8d7ccfd6959f4000dbf47-6a909e8fa992ec40-00"
}
```

# User Task Files

## Create File

### Create File Request

```js
POST / tasks / files;
```

```json
FormData
{
  "taskId": "c8570655-f049-4f45-8c81-d87580abbcd5",
  "file": {BLOB},
  "fileType: "image/jpeg"
}
```

### Create File Success Response

```js
201 Created
```

Returns the database ID of the newly created File.

```js
"8a765f0c-7769-4f30-9bac-cb7888e931a0";
```

### Create File Error Response

```js
400 Bad Request
```

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
        "ERROR ID 1": [
            "ERROR MESSAGE"
        ],
        "ERROR ID 2": [
            "ERROR MESSAGE"
        ],
        ...
  },
  "traceId": "00-36f8f7c191b21e31d5b7f803e7b38f9e-3f634f8d606ab110-00"
}
```
