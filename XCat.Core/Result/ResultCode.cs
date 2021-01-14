using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ResultCode
{
    OK = 0,
    SUCCESS = 200,

    INVALID_ARGUMENT = 400,
    FAILED_PRECONDITION = 400,
    OUT_OF_RANGE = 400,
    UNAUTHENTICATED = 401,
    PERMISSION_DENIED = 403,
    NOT_FOUND = 404,
    ABORTED = 409,
    ALREADY_EXISTS = 409,
    RESOURCE_EXHAUSTED = 429,
    CANCELLED = 499,

    UNKNOWN = 500
}