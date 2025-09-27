public static class AppRoles
{
    public const string Admin = "Admin";
    public const string Owner = "Owner";
    public const string User = "User";

    public const string OwnerOrAdmin = Admin + "," + Owner;
    
    public static readonly string[] All = new[]
    {
        Admin, Owner, User
    };
}