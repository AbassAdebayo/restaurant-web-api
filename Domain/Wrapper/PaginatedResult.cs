using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Wrapper
{
    public class PaginatedResult<T> : Result
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;
        public List<T> Data { get; set; }

        public PaginatedResult()
        {
        }

        public PaginatedResult(List<T> data)
        {
            Data = data;
        }

        public PaginatedResult(IQueryable<T> source, int count)
        {
            TotalCount = count;
            Data = source.AsQueryable().ToList();
        }

        public PaginatedResult(ICollection<T> source, int count)
        {
            TotalCount = count;
            Data = source.ToList();
        }

        public PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Messages = messages;
        }

        public PaginatedResult(List<T> data = default, int count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public static PaginatedResult<T> Failure(List<string> messages)
        {
            return new(false, default, messages);
        }

        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, null, count, page, pageSize);
        }

        public new static PaginatedResult<T> Fail()
        {
            return new() { Succeeded = false };
        }

        public new static PaginatedResult<T> Fail(string message)
        {
            return new() { Succeeded = false, Messages = new List<string> { message } };
        }

        public static ErrorResult<T> ReturnError(string message)
        {
            return new() { Succeeded = false, Messages = new List<string> { message }, StatusCode = 500 };
        }

        public new static PaginatedResult<T> Fail(List<string> messages)
        {
            return new() { Succeeded = false, Messages = messages };
        }

        public static ErrorResult<T> ReturnError(List<string> messages)
        {
            return new() { Succeeded = false, Messages = messages, StatusCode = 500 };
        }

        public new static Task<PaginatedResult<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public new static Task<PaginatedResult<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<ErrorResult<T>> ReturnErrorAsync(string message)
        {
            return Task.FromResult(ReturnError(message));
        }

        public new static Task<PaginatedResult<T>> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static Task<ErrorResult<T>> ReturnErrorAsync(List<string> messages)
        {
            return Task.FromResult(ReturnError(messages));
        }

        public new static PaginatedResult<T> Success()
        {
            return new() { Succeeded = true };
        }

        public new static PaginatedResult<T> Success(string message)
        {
            return new() { Succeeded = true, Messages = new List<string> { message } };
        }

        public new static PaginatedResult<T> Success(List<string> messages)
        {
            return new() { Succeeded = true, Messages = messages };
        }

        public static PaginatedResult<T> Success(List<T> data)
        {
            return new() { Succeeded = true, Data = data };
        }

        public static PaginatedResult<T> Success(List<T> data, string message)
        {
            return new() { Succeeded = true, Data = data, Messages = new List<string> { message } };
        }

        public static PaginatedResult<T> Success(List<T> data, List<string> messages)
        {
            return new() { Succeeded = true, Data = data, Messages = messages };
        }

        public new static Task<PaginatedResult<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public new static Task<PaginatedResult<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public new static Task<PaginatedResult<T>> SuccessAsync(List<string> messages)
        {
            return Task.FromResult(Success(messages));
        }

        public static Task<PaginatedResult<T>> SuccessAsync(List<T> data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<PaginatedResult<T>> SuccessAsync(List<T> data, string message)
        {
            return Task.FromResult(Success(data, message));
        }

        public static Task<PaginatedResult<T>> SuccessAsync(List<T> data, List<string> messages)
        {
            return Task.FromResult(Success(data, messages));
        }
    }
}
