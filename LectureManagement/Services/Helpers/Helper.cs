using Infrastructure.Utilities.Results;
using LectureManagement.Model;
using IResult = Infrastructure.Utilities.Results.IResult;
using DayOfWeek = LectureManagement.Model.DayOfWeek;
namespace LectureManagement.Services.Helpers
{
    public static class Helper
    {
        public static IResult IsAcademicYearLatest(List<AcademicYear> academicYears, Guid targetAcademicYearId)
        {
            if (!academicYears.Any())
            {
                return new ErrorResult("Academic Years not found.");
            }

            academicYears.Sort((x, y) => x.StartDate.CompareTo(y.StartDate));
            var latestAcademicYear = academicYears.Last();

            if (latestAcademicYear.Id != targetAcademicYearId)
            {
                return new ErrorResult("The academic year the course wants to add is not the most recent academic year");
            }

            if (latestAcademicYear.Status != AcademicYearStatus.Active)
            {
                return new ErrorResult("The academic year the course wants to add is not active");
            }

            return new SuccessResult();
        }

        public static IResult IsSemesterSameWithLectureSemester(Lecture lecture, Semester? semester)
        {
            if (lecture != null && lecture.Semester == semester)
            {
                return new SuccessResult();
            }

            return new ErrorResult("Semester is not same with lecture semester");
        }

        public static IResult CompareSchedules(
            Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> schedule,
            List<KeyValuePair<DayOfWeek, Tuple<TimeSpan, TimeSpan>>> existingScheduleEntries,
            string errorMessage)
        {
            foreach (var scheduleEntry in schedule)
            {
                foreach (var existingScheduleEntry in existingScheduleEntries)
                {
                    if (scheduleEntry.Key == existingScheduleEntry.Key)
                    {
                        if ((scheduleEntry.Value.Item1 >= existingScheduleEntry.Value.Item1 && scheduleEntry.Value.Item1 < existingScheduleEntry.Value.Item2) ||
                            (scheduleEntry.Value.Item2 > existingScheduleEntry.Value.Item1 && scheduleEntry.Value.Item2 <= existingScheduleEntry.Value.Item2))
                        {
                            return new ErrorResult(errorMessage);
                        }
                    }
                }
            }

            return new SuccessResult();
        }
    }
}
