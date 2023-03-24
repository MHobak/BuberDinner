# Buber Dinner API

- [Buber Dinner Api](#buber-dinner-api)
    - [Auth](#auth)
        - [Register](#register)
            - [Register Request](#register-request)
            - [Register Response](#register-response)
        - [Login](#login)
            - [Login Request](#login-request)
            - [Login Response](#login-response)

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
    "firstName": "Miguel",
    "lastName": "Hobak",
    "email": "miguel@hobak.com",
    "password": "Miguel123!"
}
```

#### Register Response
```js
200 Created
```

```json
{
    "id": "f920b060-3ef0-49b4-86c9-24ce9b9119fa",
    "firstName": "Miguel",
    "lastName": "Hobak",
    "email": "miguel@hobak.com",
    "token": "e4fdsgf2g...cvn9nkjh8"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
    "email": "miguel@hobak.com",
    "password": "Miguel123!"
}
```

#### Login Response
```js
200 Created
```

```json
{
    "id": "f920b060-3ef0-49b4-86c9-24ce9b9119fa",
    "firstName": "Miguel",
    "lastName": "Hobak",
    "email": "miguel@hobak.com",
    "token": "e4fdsgf2g...cvn9nkjh8"
}
```