namespace BurgerPlace.Roles
{
    public static class UserRoles
    {
        public const string Root = "root"; 
        // Multiple roles due to fact that root should have same privileges as user
        public const string User = "user,root"; 
    }
}
