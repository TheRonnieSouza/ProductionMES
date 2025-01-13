using MediatR;
using ProductionMES.Core.Interfaces;
using System.Text.RegularExpressions;

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
            string mask =  _repository.MaskValidate(request.traceabilityCode,request.currentModel);

            if (string.IsNullOrEmpty(mask) || string.IsNullOrEmpty(request.traceabilityCode))
                return false;

            string[] splitMask = mask.Split(",");
            int currentIndex = 0;

            foreach (var part in splitMask)
            {
                // Localiza o início do comprimento do campo
                int startBracket = part.IndexOf('[');
                int endBracket = part.IndexOf(']');

                if (startBracket == -1 || endBracket == -1 || endBracket <= startBracket)
                    return false; // Máscara inválida

                // Extrai o nome e o comprimento do campo
                string fieldName = part.Substring(0, startBracket).Trim();
                string lengthStr = part.Substring(startBracket + 1, endBracket - startBracket - 1).Trim();

                if (!int.TryParse(lengthStr, out int fieldLength) || fieldLength <= 0)
                    return false; // Comprimento inválido

                // Verifica se a string tem caracteres suficientes para validar este campo
                if (currentIndex + fieldLength > request.traceabilityCode.Length)
                    return false;

                string fieldValue = request.traceabilityCode.Substring(currentIndex, fieldLength);
                currentIndex += fieldLength;

                // Validação com switch-case
                switch (fieldName)
                {
                    case "SERIAL_NUMBER":
                    case "DIA_NUMBER":
                    case "ANO_NUMBER":
                        if (!IsNumeric(fieldValue)) // Verifica se é numérico
                            return false;
                        break;

                    case "CLIENT_TEXT":
                        if (!IsAlphabetic(fieldValue)) // Verifica se é apenas letras
                            return false;
                        break;

                    case "PRODUCT_ALFANUMERICO":
                        if (!IsAlphanumeric(fieldValue)) // Verifica se é alfanumérico
                            return false;
                        break;

                    default:
                        return false; // Campo desconhecido na máscara
                }
            }

            // Verifica se o comprimento total corresponde ao esperado (17)
            bool totalLength = currentIndex == request.traceabilityCode.Length;
            if (!totalLength)
                return false; 
            else
                return await next();
        }

        // Funções auxiliares
        private bool IsNumeric(string value) => value.All(char.IsDigit);

        private bool IsAlphabetic(string value) => value.All(char.IsLetter);

        private bool IsAlphanumeric(string value) => value.All(char.IsLetterOrDigit);
        

    }
}
