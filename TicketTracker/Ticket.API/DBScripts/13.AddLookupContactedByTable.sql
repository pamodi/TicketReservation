ALTER TABLE [TicketReservation] ADD [ContactedBy] nvarchar(200) NULL;

ALTER TABLE [TicketReservation] ADD [IsStudent] bit NOT NULL DEFAULT CAST(0 AS bit);

CREATE TABLE [LookupContactedBy] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_LookupContactedBy] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250306152216_AddLookupContactedByTable', N'9.0.2');