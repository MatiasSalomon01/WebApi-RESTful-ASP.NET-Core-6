using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Validaciones
{
    public class TipoArchivoValidaciones: ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public TipoArchivoValidaciones(string[] tiposValidos)
        {
            this.tiposValidos = tiposValidos;
        }

        public TipoArchivoValidaciones(GrupoTipoArchivos grupoTipoArchivos)
        {
            if(grupoTipoArchivos == GrupoTipoArchivos.Imagen)
                tiposValidos = new string[] { "image/jpeg", "image/png", "image/gif" };
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile is null) return ValidationResult.Success;

            if (!tiposValidos.Contains(formFile.ContentType))
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes: {string.Join(", ", tiposValidos)}");
            
            return ValidationResult.Success;
        }
    }
}
