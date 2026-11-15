using BakeryApp.Models;
using BakeryApp.Views;
using System.Collections.Generic;

namespace BakeryApp.Services;

public static class Session
{
    public static User? CurrentUser { get; set; }

    public static bool IsAdmin => CurrentUser?.RoleId == 1;
    public static bool IsStaff => CurrentUser?.RoleId == 1 || CurrentUser?.RoleId == 2;

    public static List<CartItem> CurrentCart { get; set; } = new();
}