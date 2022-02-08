using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Commands.DeleteColor
{
    public class DeleteColorCommand:IRequest<Color>
    {
        public int Id { get; set; }
        public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, Color>
        {
            IColorRepository _colorRepository;
            IMapper _mapper;
            ColorBusinessRules _colorBusinessRules;

            public DeleteColorCommandHandler(IColorRepository colorRepository, IMapper mapper, ColorBusinessRules colorBusinessRules)
            {
                _colorRepository = colorRepository;
                _mapper = mapper;
                _colorBusinessRules = colorBusinessRules;
            }

            public async Task<Color> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
            {
                var mappedColor = _mapper.Map<Color>(request);
                var deletedColor = await _colorRepository.DeleteAsync(mappedColor);
                return deletedColor;
            }
        }
    }
}
