namespace Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetCallerMemberName([System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            return callerMemberName;
        }
    }
}