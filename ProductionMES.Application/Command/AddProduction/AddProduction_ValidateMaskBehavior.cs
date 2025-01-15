using MediatR;
using ProductionMES.Core.Interfaces;

namespace ProductionMES.Application.Command.AddProduction
{
    public class AddProduction_ValidateMaskBehavior : IPipelineBehavior<AddProductionCommand, bool>
    {
        private readonly IProductionRepository _repository;

        public AddProduction_ValidateMaskBehavior(IProductionRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(AddProductionCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            string mask =  _repository.MaskValidate(request.currentModel);

            if (string.IsNullOrEmpty(mask) || string.IsNullOrEmpty(request.traceabilityCode))
                return false;

            string[] splitMask = mask.Split(",");
            int currentIndex = 0;

            foreach (var part in splitMask)
            {
                int startBracket = part.IndexOf('[');
                int endBracket = part.IndexOf(']');

                if (startBracket == -1 || endBracket == -1 || endBracket <= startBracket)
                    return false; 

                string fieldName = part.Substring(0, startBracket).Trim();
                string lengthName = part.Substring(startBracket + 1, endBracket - startBracket - 1).Trim();

                if (!int.TryParse(lengthName, out int fieldLength) || fieldLength <= 0)
                    return false; 
                                
                if (currentIndex + fieldLength > request.traceabilityCode.Length)
                    return false;

                string fieldValue = request.traceabilityCode.Substring(currentIndex, fieldLength);
                currentIndex += fieldLength;

                switch (fieldName)
                {
                    case "SERIAL_NUMBER":
                    case "DIA_NUMBER":
                    case "ANO_NUMBER":
                        if (!IsNumeric(fieldValue)) 
                            return false;
                        break;

                    case "CLIENT_TEXT":
                        if (!IsAlphabetic(fieldValue)) 
                            return false;
                        break;

                    case "PRODUCT_ALFANUMERICO":
                        if (!IsAlphanumeric(fieldValue)) 
                            return false;
                        break;

                    default:
                        return false; 
                }
            }

            
            bool totalLength = currentIndex == request.traceabilityCode.Length;
            if (!totalLength)
                return false; 
            else
                return await next();
        }

        private bool IsNumeric(string value) => value.All(char.IsDigit);

        private bool IsAlphabetic(string value) => value.All(char.IsLetter);

        private bool IsAlphanumeric(string value) => value.All(char.IsLetterOrDigit);  
    }
}
