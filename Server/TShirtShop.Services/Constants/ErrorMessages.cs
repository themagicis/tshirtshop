using System.Collections.Generic;

namespace TShirtShop.Services.Constants
{
    public static class ErrorMessages
    {
        public const string NoSuchAttribute = "001";
        public const string NoSuchCategory = "002";
        public const string NoSuchDepartment = "003";

        public static readonly IReadOnlyDictionary<string, string> Errors = new Dictionary<string, string>()
        {
            [NoSuchAttribute] = "Attribute with provided Id doesn't exist.",
            [NoSuchCategory] = "Category with provided Id doesn't exist.",
            [NoSuchDepartment] = "Department with provided Id doesn't exist.",
        };
    }
}
