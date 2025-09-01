using System.Reflection;

namespace OnionEcommerceAPI.Infrastructure.Persistence
{
    // The purpose of this class is to provide a reference to the Infrastructure.Persistence assembly for applying configurations
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
