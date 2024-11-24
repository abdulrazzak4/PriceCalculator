IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022014833_initalCreate'
)
BEGIN
    CREATE TABLE [MSPTiers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Percentage] decimal(18,2) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_MSPTiers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022014833_initalCreate'
)
BEGIN
    CREATE TABLE [Resources] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [BasePrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Resources] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022014833_initalCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241022014833_initalCreate', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241106065520_QuestionAndAnswerOptionAdded'
)
BEGIN
    CREATE TABLE [Questions] (
        [Id] int NOT NULL IDENTITY,
        [Text] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Questions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241106065520_QuestionAndAnswerOptionAdded'
)
BEGIN
    CREATE TABLE [AnswerOptions] (
        [Id] int NOT NULL IDENTITY,
        [Text] nvarchar(max) NOT NULL,
        [PriceModifier] decimal(18,2) NOT NULL,
        [QuestionId] int NULL,
        CONSTRAINT [PK_AnswerOptions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AnswerOptions_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241106065520_QuestionAndAnswerOptionAdded'
)
BEGIN
    CREATE INDEX [IX_AnswerOptions_QuestionId] ON [AnswerOptions] ([QuestionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241106065520_QuestionAndAnswerOptionAdded'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241106065520_QuestionAndAnswerOptionAdded', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241106123255_FloorPriceAddedToMSPTier'
)
BEGIN
    ALTER TABLE [MSPTiers] ADD [FloorPrice] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241106123255_FloorPriceAddedToMSPTier'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241106123255_FloorPriceAddedToMSPTier', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112214859_QuestionsAddedToResource'
)
BEGIN
    ALTER TABLE [AnswerOptions] DROP CONSTRAINT [FK_AnswerOptions_Questions_QuestionId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112214859_QuestionsAddedToResource'
)
BEGIN
    ALTER TABLE [Questions] ADD [ResourceId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112214859_QuestionsAddedToResource'
)
BEGIN
    DROP INDEX [IX_AnswerOptions_QuestionId] ON [AnswerOptions];
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AnswerOptions]') AND [c].[name] = N'QuestionId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AnswerOptions] DROP CONSTRAINT [' + @var0 + '];');
    EXEC(N'UPDATE [AnswerOptions] SET [QuestionId] = 0 WHERE [QuestionId] IS NULL');
    ALTER TABLE [AnswerOptions] ALTER COLUMN [QuestionId] int NOT NULL;
    ALTER TABLE [AnswerOptions] ADD DEFAULT 0 FOR [QuestionId];
    CREATE INDEX [IX_AnswerOptions_QuestionId] ON [AnswerOptions] ([QuestionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112214859_QuestionsAddedToResource'
)
BEGIN
    CREATE INDEX [IX_Questions_ResourceId] ON [Questions] ([ResourceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112214859_QuestionsAddedToResource'
)
BEGIN
    ALTER TABLE [AnswerOptions] ADD CONSTRAINT [FK_AnswerOptions_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112214859_QuestionsAddedToResource'
)
BEGIN
    ALTER TABLE [Questions] ADD CONSTRAINT [FK_Questions_Resources_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112214859_QuestionsAddedToResource'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241112214859_QuestionsAddedToResource', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241114162700_ReplaceQuestionToScope'
)
BEGIN
    DROP TABLE [AnswerOptions];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241114162700_ReplaceQuestionToScope'
)
BEGIN
    DROP TABLE [Questions];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241114162700_ReplaceQuestionToScope'
)
BEGIN
    CREATE TABLE [Scopes] (
        [Id] int NOT NULL IDENTITY,
        [Text] nvarchar(max) NOT NULL,
        [PriceModifier] decimal(18,2) NOT NULL,
        [ResourceId] int NOT NULL,
        CONSTRAINT [PK_Scopes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Scopes_Resources_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241114162700_ReplaceQuestionToScope'
)
BEGIN
    CREATE INDEX [IX_Scopes_ResourceId] ON [Scopes] ([ResourceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241114162700_ReplaceQuestionToScope'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241114162700_ReplaceQuestionToScope', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120114954_addDiscount'
)
BEGIN
    CREATE TABLE [Discounts] (
        [Id] int NOT NULL IDENTITY,
        [MinQuantity] int NOT NULL,
        [MaxQuantity] int NOT NULL,
        [DiscountPercentage] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Discounts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120114954_addDiscount'
)
BEGIN
    CREATE TABLE [DiscountResources] (
        [DiscountId] int NOT NULL,
        [ResourceId] int NOT NULL,
        CONSTRAINT [PK_DiscountResources] PRIMARY KEY ([DiscountId], [ResourceId]),
        CONSTRAINT [FK_DiscountResources_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [Discounts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DiscountResources_Resources_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120114954_addDiscount'
)
BEGIN
    CREATE INDEX [IX_DiscountResources_ResourceId] ON [DiscountResources] ([ResourceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241120114954_addDiscount'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241120114954_addDiscount', N'8.0.10');
END;
GO

COMMIT;
GO

