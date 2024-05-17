using Contracts;
using Infrastructure.Utilities.Results;
using MassTransit;
using OrganisationManagement.Services.Abstracts;

namespace OrganisationManagement.Consumers
{
    public sealed class GetClassroomDetailConsumer : IConsumer<GetClassroomDetailRequest>
    {
        private readonly IClassroomService _classroomService;

        public GetClassroomDetailConsumer(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        public async Task Consume(ConsumeContext<GetClassroomDetailRequest> context)
        {
            var classroom = _classroomService.GetById(context.Message.ClassroomId);

            if (!classroom.Success)
            {
                await context.RespondAsync(new GetClassroomDetailResponseError { Message = classroom.Message });
                return;
            }

            var response = new GetClassroomDetailResponse
            {
                Capacity = classroom.Data.Capacity,
                ClassroomId = classroom.Data.Id,
                Name = classroom.Data.Name,
                Code = classroom.Data.Code,
                DepartmentId = classroom.Data.DepartmentId
            };

            await context.RespondAsync(response);
        }
    }
}
