using DataAccess.AutoMapper;
using DataAccess.DataAccess;
using DataAccess.Repository.Interfaces;
using DataAccess.Repository.SQLServerServices;
using Microsoft.EntityFrameworkCore;

namespace SystemAPI
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", build => build.AllowAnyMethod()
                    .AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(hostName => true).Build());
            });

            services.AddAutoMapper(typeof(ApplicationMapper));

            InjectServices(services);
        }

        // add servides
        private void InjectServices(IServiceCollection services)
        {
            services.AddDbContext<prn231Context>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("DB"));
            });

            //add services repository pattern
            services.AddTransient<IClassRepository, ClassService>();
            services.AddTransient<IGradeTypeRepository, GradeTypeService>();
            services.AddTransient<IUserRepository, UserService>();
            //services.AddTransient<IUserRepository, UserFromFileManager>();
            services.AddTransient<IPassConditionRepository, PassConditionService>();
            services.AddTransient<IRoleRepository, RoleService>();
            services.AddTransient<IComparisonTypeRepositorycs, ComparisonTypeService>();
            services.AddTransient<IGradeRepository, GradeService>();
            services.AddTransient<ICourseRepository, CourseService>();
            services.AddTransient<ISessionRepository, SessionService>();
            services.AddTransient<ISessionStudentRepository, SessionStudentService>();
            services.AddTransient<IStudentGradeRepository, StudentGradeService>();
            services.AddTransient<ISemesterRepository, SemesterService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();



            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var scope = app.Services.CreateScope();

            app.Run();
        }
    }
}
