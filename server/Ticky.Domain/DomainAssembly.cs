using System.Reflection;

namespace Ticky.Domain;

public static class DomainAssembly
{
    public static Assembly Assembly => typeof(DomainAssembly).Assembly;
}
