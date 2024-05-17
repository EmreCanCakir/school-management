namespace Contracts
{
    public record GetClassroomDetailRequest
    {
        public Guid ClassroomId { get; set; }
    }

    public record GetClassroomDetailResponse
    {
        public Guid ClassroomId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public Guid DepartmentId { get; set; }
    }

    public record GetClassroomDetailResponseError
    {
        public string Message { get; set; }
    }
}
