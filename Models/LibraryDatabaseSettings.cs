﻿namespace Library.Models;

public class LibraryDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string BooksCollectionName { get; set; } = null!;

    public string RateBookCollectionName { get; set; } = null!;
}
