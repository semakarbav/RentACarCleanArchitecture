using Application.Features.Maintenenaces.Commands.CreateMaintenance;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenances.Commands.CreateMaintenance
{
    public class CreateMaintenaceCommandValidator:AbstractValidator<CreateMaintenanceCommand>
    {
        public CreateMaintenaceCommandValidator()
        {

        }
    }
}
