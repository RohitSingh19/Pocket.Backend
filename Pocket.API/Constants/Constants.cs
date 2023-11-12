namespace Pocket.API.Constants
{
    public static class Messages
    {
        public static string UserAlreadyRegistered = "User already registered";
        public static string EmailNotFound = "Email not found";
        public static string PasswordIncorrect = "Incorrect password";
        public static string UserRegistered = "User registered successfully";
        public static string UserNameAlreadyTaken = "Username is already taken";
        public static string UserNameCreated = "Username created successfully";
        /// <summary>
        /// User Profession Fetched Successfully
        /// </summary>
        public static string UserProfessions = "Professions fetched successfully";
        public static string UserAdditionalDetails = "Additional details updated successfully";
        public static string UserProfileFetched = "User profile fecthed successfully";
    }

    public static class Errors
    {
        public static string EmailNotPresentInToken = "Token does not contains an Email";
        public static string EmailNotPresentInArgument = "Email paramter is missing in argument";
        public static string EmailMismatch = "Token and email do not belong to the same user";
    }
}
