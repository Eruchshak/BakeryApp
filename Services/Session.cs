using BakeryApp.Models;

namespace BakeryApp.Services;

public static class Session
{
    public static User? CurrentUser { get; set; }

    public static bool IsAdmin => CurrentUser?.RoleId == 1;
    public static bool IsStaff => CurrentUser?.RoleId == 1 || CurrentUser?.RoleId == 2;
}