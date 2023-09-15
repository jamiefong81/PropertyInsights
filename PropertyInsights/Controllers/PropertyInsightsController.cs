using Microsoft.AspNetCore.Mvc;
using PropertyInsights.Services;

namespace PropertyInsights.Controllers
{
    [ApiController]
    [Route("propertyInsights")]
    public class PropertyInsightsController : ControllerBase
    {
        [HttpGet]
        public async Task<double> GetPropertyInsights()
        {
            var safetyScoreService = new SafetyScoreService();
            var score = await safetyScoreService.GetSafetyScore(41.89311325766901, -87.76184733149714);
            return score;
        }
    }
}
