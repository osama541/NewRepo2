using EmvQrCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DDL_integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EMVCoQRController : ControllerBase
    {
        [HttpPost("generate")]
        public IActionResult GenerateQr([FromBody] JsonElement request)
        {
            try
            {
                var tags = request.GetProperty("tags").Deserialize<Dictionary<string, object>>();
                var isImageRequired = request.GetProperty("isImageRequired").GetBoolean();

                var result = EmvQrUtility.Generate(tags, isImageRequired);

                return Ok(new
                {
                    QrString = result.qrString,
                    Base64Png = result.base64Png,
                    Base64Svg = result.base64Svg
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}