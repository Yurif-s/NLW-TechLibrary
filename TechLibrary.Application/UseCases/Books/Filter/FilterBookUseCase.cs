﻿using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Infrastructure.DataAccess;

namespace TechLibrary.Application.UseCases.Books.Filter;
public class FilterBookUseCase
{
    private const int PAGE_SIZE = 10;

    public ResponseBooksJson Execute(RequestFilterBooksJson request)
    {
        var dbContext = new TechLibraryDbContext();

        var query = dbContext.Books.AsQueryable();

        if (string.IsNullOrWhiteSpace(request.Title) == false)
            query = query.Where(book => book.Title.Contains(request.Title));

        var books = query
            .OrderBy(book => book.Title).ThenBy(book => book.Author)
            .Skip((request.PageNumber - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToList();

        var totalCount = 0;
        if(string.IsNullOrWhiteSpace(request.Title))
            totalCount = dbContext.Books.Count();
        else
            totalCount = dbContext.Books.Count(book => book.Title.Contains(request.Title));

        return new ResponseBooksJson
        {
            Pagination = new ResponsePaginationJson
            {
                PageNumber = request.PageNumber,
                TotalCount = totalCount
            },
            Books = books.Select(book => new ResponseBookJson
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
            }).ToList()
        };
    }
}
