using Infrastructure.Utilities.Results;

namespace Infrastructure.Utilities.Business
{
    public  static class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            var errorMessages = "";
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    errorMessages += logic.Message + "\n";
                }
            }

            if (errorMessages.Length > 0)
            {
                return new ErrorResult(errorMessages);
            }

            return new SuccessResult();
        }
    }
}
