using Baasic.Client.Common.Configuration;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Common.Infrastructure.Security;
using Baasic.Client.Configuration;
using System;

/// <summary>
/// Dependency Injection Module containing Baasic Client bindings.
/// </summary>
public class AppClientConfiguration : ClientConfiguration
{

    #region Fields

    static public string apiKey { get; set; }

    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="AppClientConfiguration" /> class.
    /// </summary>
    /// <param name="tokenHandler">The token handler.</param>
    public AppClientConfiguration(ITokenHandler tokenHandler)
        : base(apiKey, tokenHandler)
    {
        
    }

    public AppClientConfiguration(string applicationIdentifier, ITokenHandler tokenHandler) 
        : base(applicationIdentifier, tokenHandler)
    {
    }
    #endregion Constructors
}

/// <summary>
/// Dependency Injection Module containing Baasic Client bindings.
/// </summary>
public partial class DIModule : IDIModule
{
    #region Methods

    /// <summary>
    /// Load dependency injection bindings.
    /// </summary>
    /// <param name="dependencyResolver"></param>
    public virtual void Load(IDependencyResolver dependencyResolver)
    {
        #region Configuration

        dependencyResolver.Register<IClientConfiguration, AppClientConfiguration>();

        #endregion Configuration
    }

    #endregion Methods
}