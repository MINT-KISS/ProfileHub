# ProfileHub

ProfileHub is a RESTful API developed on ASP.NET Core that allows you to manage users and upload their photos to AWS S3.

## Table of Contents

- [Technology](#technology)
- [Installation](#installation)
- [Setup](#setup)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [Development](#development)
- [License](#license)

## Technologies

- **ASP.NET Core 6**
- **Entity Framework Core**
- **SQLite**
- **AWS SDK for .NET**
- **AWS S3**
- **Swagger (OpenAPI)**

## Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/MINT-KISS/ProfileHub.git
   cd ProfileHub

2. **Install dependencies:**

   ````bash
   dotnet restore

3. **Create the database and apply the migrations:** ```bash dotnet restore 3.

   ```bash
   dotnet ef database update

## Customization

Before running the project, you must configure AWS S3 and update the `appsettings.json` file.

1. **Customize AWS S3:**

    - Create an account on AWS if you don't already have one.
    - Create an S3 bucket to store your photos.
    - Get your access keys (Access Key ID and Secret Access Key).

2. **Update `appsettings.json`:**

   ```json
   {
     { “ConnectionStrings”: {
       { “DefaultConnection”: “Data Source=Database/ProfileHub.db”.
     },
     { “AWS”: {
       “AccessKey": ‘your-access-key’,
       { “SecretKey”: “your-secret-key”,
       “Region": { “your-region”,
       “BucketName": ”your-bucket-name”
     },
     “Logging": {
       { “LogLevel”: {
         { “default”: { “information”,
         { “Microsoft.AspNetCore”: “Warning”.
       }
     },
     “AllowedHosts": “*”
   }

## Usage

1. **Start the project:**

   ```bash
   dotnet run

2. **Open the Swagger UI:**

   Go to https://localhost:5203/swagger in your browser to see the documentation and test the API.

## API Endpoints

### Users

- **GET** `/api/users` - Get all users.
- **GET** `/api/users/{id}` - Get a user by ID.
- **POST** `/api/users` - Create a new user.
- **PUT** `/api/users/{id}` - Update a user.
- **DELETE** `/api/users/{id}` - Delete user.

### Photos

- **POST** `/api/users/{id}/photo` - Upload a photo for the user.

### Testing

You can use Swagger UI or Postman to test the API.

### Swagger UI

1. Navigate to `https://localhost:5203/swagger`.
2. Select the desired endpoint and click `Try it out'.
3. Enter the required parameters and click “Execute”.

### Postman

1. Import the Postman collection from the `ProfileHub.postman_collection.json` file.
2. Select the desired request and click “Send”.

### Project Structure

- **Controllers**: Contains controllers that handle HTTP requests.
- **Models**: Contains the data models.
- **Data**: Contains database context and repositories.
- **Repositories**: Contains repositories for working with data.
- **Services**: Contains services for working with AWS S3.
- **Interfaces**: Contains interfaces for repositories and services.

## License

This project is licensed under the [MIT License](LICENSE).