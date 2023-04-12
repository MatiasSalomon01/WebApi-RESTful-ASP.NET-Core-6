using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace PeliculasApi.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveedorDeValores = bindingContext.ValueProvider.GetValue(nombrePropiedad);

            if(proveedorDeValores == ValueProviderResult.None) return Task.CompletedTask;

            try
            {
                var valorDeserializado = JsonConvert.DeserializeObject<T>(proveedorDeValores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);

            }catch(Exception ex)
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "Valor invalido para tipo List<int>");
            }
            return Task.CompletedTask;
        }
    }
}
