using System.Web.Http;
using Swashbuckle.Application;
using Swashbuckle.Swagger;

namespace Routes.Api
{
    /// <summary>
    /// SwaggerConfig
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger("docs/{apiVersion}", c =>
                {
                    c.MapType<decimal>(() => new Schema { type = "decimal", format = "decimal" });
                    c.MapType<decimal?>(() => new Schema { type = "decimal?", format = "decimal nullable" });
                    c.MapType<short>(() => new Schema { type = "short", format = "Int16" });
                    c.MapType<byte>(() => new Schema { type = "byte", format = "byte" });
                    c.MapType<byte[]>(() => new Schema { type = "array de byte", format = "byte[]" });
                    c.MapType<int>(() => new Schema { type = "int", format = "Int32" });
                    c.MapType<int?>(() => new Schema { type = "int?", format = "Int32 nullable" });
                    c.MapType<long>(() => new Schema { type = "long", format = "Int64" });
                    c.MapType<bool?>(() => new Schema { type = "boolean?", format = "booelan nullable" });
                    c.UseFullTypeNameInSchemaIds();
                    c.IncludeXmlComments(string.Format(@"{0}\bin\Routes.Api.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                    c.SingleApiVersion("v1", "Api");
                })
                .EnableSwaggerUi("sandbox/{*assetPath}", c =>
                {
                    c.DisableValidator();
                });
        }
    }
}
