-- Kreiranje baza
CREATE DATABASE QuizHub_UserDB;
CREATE DATABASE QuizHub_QuizDB;
CREATE DATABASE QuizHub_ResultDB;
GO

USE QuizHub_UserDB;
GO

-- ==========================
-- UserService tabele
-- ==========================
CREATE TABLE [dbo].[Users] (
    [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [Username] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    [Role] INT NOT NULL,
    [ProfileImage] NVARCHAR(500) NOT NULL
);
GO


USE QuizHub_QuizDB;
GO

-- ==========================
-- QuizService tabele
-- ==========================

CREATE TABLE [dbo].[Quizzes] (
    [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Category] NVARCHAR(100) NOT NULL,
    [Difficulty] NVARCHAR(50) NOT NULL,
    [TimeLimit] INT NOT NULL
);

CREATE TABLE [dbo].[Questions] (
    [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [Text] NVARCHAR(MAX) NOT NULL,
    [QuestionType] NVARCHAR(50) NOT NULL,
    [QuizId] BIGINT NOT NULL,
    [Weight] INT NOT NULL DEFAULT 1,
    FOREIGN KEY (QuizId) REFERENCES [dbo].[Quizzes](Id) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AnswerOptions] (
    [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [Text] NVARCHAR(MAX) NOT NULL,
    [IsCorrect] BIT NOT NULL,
    [QuestionId] BIGINT NOT NULL,
    FOREIGN KEY (QuestionId) REFERENCES [dbo].[Questions](Id) ON DELETE CASCADE
);
GO


USE QuizHub_ResultDB;
GO

-- ==========================
-- ResultsService tabele
-- ==========================

CREATE TABLE [dbo].[UserQuizResults] (
    [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [Score] INT NOT NULL,
    [TimeTaken] INT NOT NULL,
    [DatePlayed] DATETIME2 NOT NULL,
    [UserId] BIGINT NOT NULL,
    [QuizId] BIGINT NOT NULL
);

CREATE TABLE [dbo].[UserAnswers] (
    [Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [ResultId] BIGINT NOT NULL,
    [QuestionId] BIGINT NOT NULL,
    [SelectedAnswer] NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (ResultId) REFERENCES [dbo].[UserQuizResults](Id) ON DELETE CASCADE
);
GO
