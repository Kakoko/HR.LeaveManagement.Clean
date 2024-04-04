using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {

        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) => _leaveTypeRepository = leaveTypeRepository;
       

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // retrieve domain 
            var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

            // Verify records exists
            if (leaveTypeToDelete == null)
            {
                throw new ApplicationException($"Leave Type with Id = {request.Id} not found");
            }

            //Delete from DB
            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

            //return Unit Value
            return Unit.Value;
        }
    }
}
