using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Infrastructur.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;

namespace HR.LeaveManagement.Infrastructur
{
  
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings").Bind);
            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}
