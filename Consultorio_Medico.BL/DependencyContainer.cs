﻿using Consultorio_Medico.BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBLDependecies(this IServiceCollection services)
        {
            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<IScheduleBL, ScheduleBL>();
            services.AddTransient<IRolBL, RolBL>();
            services.AddTransient<IWorkPlaceBL, WorkPlaceBL>();
            services.AddTransient<IUserSchedulesBL, UserSchedulesBL>();
            services.AddTransient<ISecurityBL, SecurityBL>();
            services.AddTransient<ISpecialtieBL, SpecialtiesBL>();
            services.AddTransient<IPatientBL, PatientBL>();
            services.AddTransient<IAppointmentBL, AppointmentBL>();
            return services;
        }
    }
}
