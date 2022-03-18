﻿namespace Muvids.Application.Responses;

public class BaseResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = null!;

    public List<string> ValidationErrors { get; set; } = null!;

    public BaseResponse()
    {
        Success = true;
    }

    public BaseResponse(string message = null!)
    {
        Success = true;
        Message = message;
    }

    public BaseResponse(string message, bool success)
    {
        Success = success;
        Message = message;
    }


}
