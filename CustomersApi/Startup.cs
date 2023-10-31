using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CustomersApi
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            // Otras configuraciones

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // Más configuraciones
        }

        public void Configure(IApplicationBuilder app)
        {
            // Configuración de la tubería de solicitud HTTP
            app.UseRouting();

            // Habilitar CORS
            app.UseCors("AllowSpecificOrigin");

            // Middleware adicional (por ejemplo, autenticación, autorización, etc.)
            // app.UseAuthentication();
            // app.UseAuthorization();

            // Enrutamiento
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
