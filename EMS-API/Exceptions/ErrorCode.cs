namespace EMS_API.Exceptions
{
    public enum ErrorCode
    {
        UNCATEGORIZED_EXCEPTION = 9999,
        INVALID_KEY = 1001,
        USER_EXISTED = 1002,
        USERNAME_INVALID = 1003,
        INVALID_PASSWORD = 1004,
        USER_NOT_EXISTED = 1005,
        UNAUTHENTICATED = 1006,
        DATABASE_ERROR = 1007,
        NOT_FOUND = 1008,
        NOT_ENOUGH_QUANTITY = 1009
    }

    public static class ErrorCodeExtensions
    {
        private static readonly Dictionary<ErrorCode, string> errorMessages = new Dictionary<ErrorCode, string>
        {
            { ErrorCode.UNCATEGORIZED_EXCEPTION, "Uncategorized error" },
            { ErrorCode.INVALID_KEY, "Uncategorized error" },
            { ErrorCode.USER_EXISTED, "User existed" },
            { ErrorCode.USERNAME_INVALID, "Username must be at least 3 characters" },
            { ErrorCode.INVALID_PASSWORD, "Password must be at least 8 characters" },
            { ErrorCode.USER_NOT_EXISTED, "User not existed" },
            { ErrorCode.UNAUTHENTICATED, "Unauthenticated" },
            { ErrorCode.DATABASE_ERROR, "Database get an error" },
            { ErrorCode.NOT_FOUND, "Not found" },
            { ErrorCode.NOT_ENOUGH_QUANTITY, "Not enough quantity" }
        };

        public static int GetCode(this ErrorCode errorCode)
        {
            return (int)errorCode;
        }

        public static string GetMessage(this ErrorCode errorCode)
        {
            return errorMessages[errorCode];
        }
    }

}

