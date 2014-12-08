using System.Web;
using System.Web.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Touristix.Filters;
using WebMatrix.WebData;

[assembly: PreApplicationStartMethod(typeof(PreApplicationTasks), "Initializer")]

public static class PreApplicationTasks
{
    public static void Initializer()
    {
        DynamicModuleUtility
        .RegisterModule(typeof(UserInitializationModule));
    }
}

public class UserInitializationModule : IHttpModule
{
    private static bool m_Initialise;
    private static object m_ObjetVerrou = new object();

    private const string m_NomUtilisateur = "admin";
    private const string m_MotPasse = "admin123*";
    private const string m_Role = "admin";

    void IHttpModule.Init(HttpApplication context)
    {
        lock (m_ObjetVerrou)
        {
            if (!m_Initialise)
            {
                new InitializeSimpleMembershipAttribute().OnActionExecuting(null);

                if (!WebSecurity.UserExists(m_NomUtilisateur))
                {
                    WebSecurity.CreateUserAndAccount(m_NomUtilisateur, m_MotPasse);
                }

                if (!Roles.RoleExists(m_Role))
                {
                    Roles.CreateRole(m_Role);
                }

                if (!Roles.IsUserInRole(m_NomUtilisateur, m_Role))
                {
                    Roles.AddUserToRole(m_NomUtilisateur, m_Role);
                }
            }
            m_Initialise = true;
        }
    }

    void IHttpModule.Dispose() { }
}
