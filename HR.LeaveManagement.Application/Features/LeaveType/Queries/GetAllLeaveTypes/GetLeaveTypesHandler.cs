using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesHandler> _appLogger;

        public GetLeaveTypesHandler(IMapper mapper , ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypesHandler> appLogger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _appLogger = appLogger;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            //Query DB
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            // Convert Entity to DTO
            var leaveTypesDto = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            //Return Dto
            _appLogger.LogInformation("All Leave Types Retrieved");
            return leaveTypesDto;
        }
    }
}
