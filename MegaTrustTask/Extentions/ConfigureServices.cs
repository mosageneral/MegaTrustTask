using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.ApplicationContext;
using DomainLayer.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Abstraction.UserInterfaces;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Concrete.UserService;
using ServiceLayer.Abstraction.VoteFormInterfaces;
using ServiceLayer.Concrete.FormServices;

namespace PresentaionLayer.Extentions
{
    public static class DependencySetUp
    {
        public static IServiceCollection RegisterService(this IServiceCollection services,WebApplicationBuilder applicationBuilder)
        {
    
            //Mapping Registeration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfigration());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);



            //Configure Data Base
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(applicationBuilder.Configuration.GetConnectionString("ConnectionString"));
              
            });



            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Concrete Service Registration
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IVoteFormService, VoteFormService>();
            return services;
        }
    }
}
