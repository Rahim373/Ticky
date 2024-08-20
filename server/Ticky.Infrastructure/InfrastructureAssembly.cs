using System.Reflection;

namespace Ticky.Infrastructure;

public static class InfrastructureAssembly
{
    public static Assembly Assembly => typeof(InfrastructureAssembly).Assembly;
}
