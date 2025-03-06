CREATE TABLE [TicketReservation] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Email] nvarchar(200) NULL,
    [PhoneNumber] nvarchar(200) NULL,
    [NumberOfTickets] int NOT NULL,
    [Comments] nvarchar(500) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [UpdatedAt] datetimeoffset NULL,
    CONSTRAINT [PK_TicketReservation] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250305193831_AddTicketReservationTable', N'9.0.2');
