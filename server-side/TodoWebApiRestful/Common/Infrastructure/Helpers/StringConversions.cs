namespace TodoWebApiRestful.Common.InfrastructureLayer.Helpers
{
    public class StringConversions
    {
        public static string? ReplaceEmptyWithNull(string? value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
