using Contracts;
using Infrastructure.Utilities.Results;
using MassTransit;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace LectureManagement.Services.Concretes
{
    public class ClassroomDetailResponseService
    {
        private readonly IRequestClient<GetClassroomDetailRequest> _client;

        public ClassroomDetailResponseService(IRequestClient<GetClassroomDetailRequest> client)
        {
            _client = client;
        }

        public async Task<IDataResult<GetClassroomDetailResponse>> GetClassroomDetailAsync(Guid classroomId)
        {
            var response = await _client.GetResponse<GetClassroomDetailResponse, GetClassroomDetailResponseError>
                (new { ClassroomId = classroomId });

            if (response.Is(out Response<GetClassroomDetailResponse> successResponse))
            {
                return new SuccessDataResult<GetClassroomDetailResponse>(successResponse.Message);
            }

            if (response.Is(out Response<GetClassroomDetailResponseError> errorResponse))
            {
                return new ErrorDataResult<GetClassroomDetailResponse>(errorResponse.Message.Message);
            }

            return new ErrorDataResult<GetClassroomDetailResponse>("Something went wrong while fetching classroom info");
        }
    }
}
