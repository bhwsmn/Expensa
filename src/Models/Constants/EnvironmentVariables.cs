using System;

namespace Models.Constants
{
     public static class EnvironmentVariables
    {
        public static readonly string PostgresqlHost = 
            Environment.GetEnvironmentVariable("POSTGRES_HOST");
        public static readonly string PostgresqlPort = 
            Environment.GetEnvironmentVariable("POSTGRES_PORT");
        public static readonly string PostgresqlUsername = 
            Environment.GetEnvironmentVariable("POSTGRES_USERNAME");
        public static readonly string PostgresqlPassword = 
            Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        public static readonly string PostgresqlDatabaseName = 
            Environment.GetEnvironmentVariable("POSTGRES_DB");
        
        public static readonly bool RegistrationEnabled = 
            Environment.GetEnvironmentVariable("REGISTRATION_ENABLED")?.ToLower() == "true";
    }
}