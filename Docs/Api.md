# Task Manager API

- [Task Manager API](#task-manager-api)
  - [Create Task](#create-task)
    - [Create Task Request](#create-task-request)
    - [Create Task Response](#create-task-response)
  - [Get Task](#get-task)
    - [Get Task Request](#get-task-request)
    - [Get Task Response](#get-task-response)
  - [Update Task](#update-task)
    - [Update Task Request](#update-task-request)
    - [Update Task Response](#update-task-response)
  - [Delete Task](#delete-task)
    - [Delete Task Request](#delete-task-request)
    - [Delete Task Response](#delete-task-response)

## Create Task

### Create Task Request

```js
POST / tasks;
```

```json
{
  "title": "Dentist's Appintment",
  "description": "appintment with Dr. X",
  "dueDate": "2023-03-09T10:00:00"
}
```

### Create Task Response

```js
201 Created
```

```yml
Location: {{host}}/tasks/{{id}}
```

```json
{
  "title": "Dentist's Appintment",
  "description": "appintment with Dr. X",
  "dueDate": "2023-03-09T10:00:00",
  "status": "Incomplete"
}
```

## Get Task

### Get Task Request

```js
GET /tasks/{{id}}
```

### Get Task Response

```js
200 Ok
```

```json
{
  "title": "Dentist's Appintment",
  "description": "appintment with Dr. X",
  "dueDate": "2023-03-09T10:00:00",
  "status": "Incomplete"
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
  "dueDate": "2023-03-11T11:30:00",
  "status": "Incomplete"
}
```

### Update Task Response

```js
204 No Content
```

## Delete Task

### Delete Task Request

```js
DELETE /tasks/{{id}}
```

### Delete Task Response

```js
204 No Content
```
