using Escola.BO;
using Escola.BO.Interfaces;
using Escola.DAO;
using Escola.DAO.Interfaces;
using Escola.Dominio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Escola.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddDbContext<DbContextConfiguration>(options => options.UseMySql(Configuration.GetConnectionString("DBPadraoMySql")));

            services.AddScoped<IAlunoDAO, AlunoDAO>();
            services.AddScoped<IAlunoProvaDAO, AlunoProvaDAO>();
            services.AddScoped<IGabaritoDAO, GabaritoDAO>();
            services.AddScoped<IProvaDAO, ProvaDAO>();
            services.AddScoped<IRespostaAlunoDAO, RespostaAlunoDAO>();

            services.AddScoped<IAlunoBO, AlunoBO>();
            services.AddScoped<IProvaBO, ProvaBO>();

            services.AddCors(x => x.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
