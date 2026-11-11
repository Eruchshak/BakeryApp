namespace BakeryApp.Models;


public partial class User
{
    
    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

   
    public override string ToString() => FullName;
}


public partial class Product
{
    public override string ToString() => Name;
}