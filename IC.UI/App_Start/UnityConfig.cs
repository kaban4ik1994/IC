using System;
using IC.Entities;
using IC.Entities.Models;
using IC.Services;
using IC.Services.Interfaces;
using Microsoft.Practices.Unity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace IC.UI.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container
                .RegisterType<IDataContextAsync, IcContext>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IRepositoryAsync<User>, Repository<User>>()
                .RegisterType<IRepositoryAsync<Computer>, Repository<Computer>>()
                .RegisterType<IRepositoryAsync<Course>, Repository<Course>>()
                .RegisterType<IRepositoryAsync<Group>, Repository<Group>>()
                .RegisterType<IRepositoryAsync<Message>, Repository<Message>>()
                .RegisterType<IRepositoryAsync<Role>, Repository<Role>>()
                .RegisterType<IRepositoryAsync<Specialty>, Repository<Specialty>>()
                .RegisterType<IRepositoryAsync<Student>, Repository<Student>>()
                .RegisterType<IRepositoryAsync<UserRole>, Repository<UserRole>>()
                .RegisterType<IRepositoryAsync<History>, Repository<History>>()
                .RegisterType<IRepositoryAsync<UserRole>, Repository<UserRole>>()
                .RegisterType<ICourseService, CourseService>(new PerRequestLifetimeManager())
                .RegisterType<ISpecialtyService, SpecialtyService>(new PerRequestLifetimeManager())
                .RegisterType<IStudentService, StudentService>(new PerRequestLifetimeManager())
                .RegisterType<IGroupService, GroupService>(new PerRequestLifetimeManager())
                .RegisterType<IUserService, UserService>(new PerRequestLifetimeManager())
                .RegisterType<IMessageService, MessageService>(new PerRequestLifetimeManager())
                .RegisterType<IHistoryService, HistoryService>(new PerRequestLifetimeManager())
                .RegisterType<IComputerService, ComputerService>(new PerRequestLifetimeManager())
                .RegisterType<IUserRoleService, UserRoleService>(new PerRequestLifetimeManager());
        }
    }
}
