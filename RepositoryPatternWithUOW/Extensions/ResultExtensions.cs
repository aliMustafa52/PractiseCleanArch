﻿using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Api.Mappings;
using RepositoryPatternWithUOW.Domain.Abstractions;
using RepositoryPatternWithUOW.Domain.Errors;

namespace RepositoryPatternWithUOW.Api.Extensions;

public static class ResultExtensions
{
    public static ObjectResult ToProblem(this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert success result to a problem");


        int statusCode = ErrorCodeToHttpStatusMapper.Map(result.Error);

        var problem = Results.Problem(statusCode: statusCode);
        var problemDetails = problem.GetType()
            .GetProperty(nameof(ProblemDetails))
            !.GetValue(problem) as ProblemDetails;

        problemDetails!.Extensions = new Dictionary<string, object?>
        {
            {
                "errors", new[] {
                    new
                    {
                        result.Error.Code,
                        result.Error.Description
                    }
                }
            }
        };

        return new ObjectResult(problemDetails);
    }
}