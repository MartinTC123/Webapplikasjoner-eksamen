using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.Controllers
{
    [Route("[controller]/[action]")]
    
    public class LokasjonController : ControllerBase
    {
        private readonly ILokasjonRepository _db;
        private ILogger<LokasjonController> _logger;
        private const string _loggetInn = "loggetInn";

        public LokasjonController(ILokasjonRepository db, ILogger<LokasjonController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ActionResult> Registrer(Lokasjon lokasjon)
        {
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            bool okStedsnavn = Validation.Validering.GyldigStedsnavn(lokasjon.Stedsnavn);
            bool okGyldigStedsNummer = Validation.Validering.GyldigStedsNummer(lokasjon.StedsNummer);
            
            if (okStedsnavn && okGyldigStedsNummer)
            {
                bool returOK = await _db.RegistrerLokasjon(lokasjon);
                if (!returOK )
                {
                    _logger.LogInformation("Lokasjonen ble ikke lagret");
                    return BadRequest("Lokasjonen ble ikke lagret");
                }
                return Ok("Lokasjon lagret");
            }
            _logger.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<ActionResult> Slett(string id)
        {
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            
            bool returOK = await _db.SlettLokasjon(id);

            if (!returOK)
            {
                _logger.LogInformation("Lokasjonen ble ikke slettet");
                return NotFound("Lokasjonen ble ikke slettet");
            }

            return Ok("Lokasjonen ble slettet");
        }

        public async Task<ActionResult> HentAlle()
        {
            List<Lokasjon> alleLokasjoner = await _db.HentAlle();
            return Ok(alleLokasjoner);
        }

        public async Task<ActionResult> HentEn(string id)
        {

            Lokasjon lokasjon = await _db.HentEn(id);
            
            if (lokasjon == null)
            {
                _logger.LogInformation("Fant ikke lokasjon");
                return NotFound("Fant ikke lokasjonen");
            }
            return Ok(lokasjon);
        }
    }
}