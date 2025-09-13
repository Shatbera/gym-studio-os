public static class AppRoles
{
    public const string Admin = "Admin";
    public const string Owner = "Owner";
    public const string Manager = "Manager";
    public const string Staff = "Staff";
    public const string Coach = "Coach";
    public const string Member = "Member";

    public const string OwnerOrAdmin = Admin + "," + Owner;
    public const string OwnerOrManager = Admin + "," + Owner + "," + Manager;
    public const string StaffOrAbove = Admin + "," + Owner + "," + Manager + "," + Staff;
    public const string CoachOrAbove = Admin + "," + Owner + "," + Manager + "," + Staff + "," + Coach;

    public static readonly string[] All = new[]
    {
        Admin, Owner, Manager, Staff, Coach, Member
    };
}