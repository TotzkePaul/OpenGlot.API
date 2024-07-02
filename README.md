# Language Learning Application

## Overview

The Language Learning Application is a comprehensive platform designed to facilitate language learning through interactive lessons, multimedia content, AI-powered chatbots, and user-generated content. The application supports various user roles, including Users, Reviewers, Creators, Admins, and SuperAdmins, providing a robust framework for both learners and content creators. The app uses AWS Cognito for Authentication and runs seamlessly in a Docker containerized environment.

## Table of Contents

- [Features](#features)
  - [User Profiles](#user-profiles)
  - [Learning Content](#learning-content)
  - [Question Types](#question-types)
  - [Multimedia Content](#multimedia-content)
  - [AI Chatbots](#ai-chatbots)
  - [User Interaction](#user-interaction)
  - [Achievements and Rewards](#achievements-and-rewards)
  - [User-Generated Content](#user-generated-content)
  - [Subscription Plans](#subscription-plans)
  - [Interactive Storybooks](#interactive-storybooks)
- [Technical Details](#technical-details)
  - [Technologies Used](#technologies-used)
  - [API Endpoints](#api-endpoints)
- [Setup](#setup)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [Usage](#usage)
  - [Uploading CSV Files](#uploading-csv-files)
  - [Example Requests](#example-requests)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Features

### User Profiles

- **User Registration and Authentication**: Users can register and log in using their credentials.
- **User Roles**: Users can have multiple roles such as User, Reviewer, Creator, Admin, and SuperAdmin.
- **User Dashboard**: A comprehensive dashboard that displays learning progress, strengths, and areas for improvement.

### Learning Content

- **Languages**: Users can select and learn multiple languages.
- **Courses**: Each language contains multiple courses tailored to different levels and topics.
- **Modules**: Courses are divided into modules, each focusing on specific aspects of the language.
- **Lessons**: Modules contain lessons that include various types of questions and multimedia content.

### Question Types

The application supports several types of questions, including:
- Listen to the Sentence
- Translate the Sentence
- Fill in the Blank
- True or False
- Multiple Choice
- Reorder the Sentence (Unjumble)

### Multimedia Content

- **Audio and Video Integration**: High-quality audio and video content are integrated to improve listening and comprehension skills.
- **Images**: Visual aids are used to reinforce learning.

### AI Chatbots

- **Conversational Practice**: AI-powered chatbots provide conversational practice and instant feedback.
- **Multiple AI Options**: Integration with APIs such as OpenAI's ChatGPT and Llama.

### User Interaction

- **Content Rating**: Users can rate lessons and other content with thumbs up or thumbs down.
- **Spaced Repetition System**: Vocabulary learning through flashcards using spaced repetition.
- **Notifications**: Users receive notifications for progress updates, upcoming lessons, and goals.

### Achievements and Rewards

- **Achievement Badges**: Users earn badges for reaching milestones and achievements.
- **Progress Tracking**: Detailed tracking of user progress across courses, modules, and lessons.

### User-Generated Content

- **Content Creation Tools**: Users can create and share lessons, exercises, and quizzes.
- **Community Sharing**: Content can be shared with the community for collaborative learning.

### Subscription Plans

- **Free and Premium Plans**: Various subscription plans with different access levels and features, including additional AI tools and an ad-free experience.

### Interactive Storybooks

- **Engaging Storybooks**: Interactive storybooks where learners make choices that affect the story's outcome, practicing the target language in context.

## Technical Details

### Technologies Used

- **Backend**: C#, .NET 8, Entity Framework Core
- **Frontend**: JavaScript, HTML, CSS
- **Database**: PostgreSQL
- **Authentication**: Amazon Cognito
- **Cloud Services**: Azure for media services and scalability

### API Endpoints

#### User Controller

- `GET /api/Users` - Retrieve all users
- `GET /api/Users/{id}` - Retrieve a user by ID
- `POST /api/Users` - Add a new user
- `PUT /api/Users/{id}` - Update a user
- `DELETE /api/Users/{id}` - Delete a user
- ... (other endpoints)

#### Files Controller

- `POST /api/Files/upload-images` - Upload a CSV file to add images
- `POST /api/Files/upload-audio` - Upload a CSV file to add audio

For more details on API endpoints, please refer to the provided documentation or Swagger UI.

## Setup

### Prerequisites

- Docker
- .NET 8 SDK
- PostgreSQL
- AWS Cognito setup for user authentication

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/OpenGlot/OpenGlot.API.git
    cd OpenGlot.API
    ```

2. Ensure PostgreSQL is running and update the connection string in `appsettings.json`:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword"
    }
    ```

### Running the Application

1. Build and run the Docker container:

    ```bash
    docker build -t language-learning-app .
    docker run -p 8080:8080 -p 8081:8081 --name language-learning-app -v $(pwd)/data:/app/data language-learning-app
    ```

2. Navigate to `http://localhost:8080` in your browser to access the application.

## Usage

### Uploading CSV Files

To upload images or audio files through CSV:

1. **Images CSV Format**:
    - Columns: `image_id`, `context`, `original_description`, `enhanced_description`, `file_name`

    Example:

    ```csv
    image_id,context,original_description,enhanced_description,file_name
    1,Scene,Original desc 1,Enhanced desc 1,image1.jpg
    2,Scene,Original desc 2,Enhanced desc 2,image2.jpg
    ```

2. **Audio CSV Format**:
    - Columns: `sentence_id`, `language`, `sentence`, `file_name`

    Example:

    ```csv
    sentence_id,language,sentence,file_name
    101,en,Hello world,audio1.mp3
    102,es,Hola mundo,audio2.mp3
    ```

Upload the CSV file using the corresponding API endpoint through Swagger UI or a REST client:
- `POST /api/Files/upload-images`
- `POST /api/Files/upload-audio`

### Example Requests

**Upload Images Example**:

```http
POST /api/Files/upload-images
Content-Type: multipart/form-data
```

**Upload Audio Example**:

```http
POST /api/Files/upload-audio
Content-Type: multipart/form-data
```

## Contributing

We welcome contributions from the community! Please check our [contributing guidelines](CONTRIBUTING.md) for more information on how to get started.
