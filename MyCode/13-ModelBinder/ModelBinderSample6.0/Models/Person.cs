using Microsoft.AspNetCore.Mvc;

namespace ModelBinderSample.Models;

[ModelBinder(BinderType = typeof(PersonsCsvBinder))]
public class Person
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? EmailAddress { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
}
