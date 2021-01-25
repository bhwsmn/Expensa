namespace Models.Constants
{
    public static class RegexPatterns
    {
        public const string NamePattern = @"^[\w]([\w -]*[\w])?$";
        public const string UsernamePattern = @"([\w_]+)";
        public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$";
    }
}