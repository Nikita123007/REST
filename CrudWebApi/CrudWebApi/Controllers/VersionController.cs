using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrudWebApi.Resources.Versions;

namespace CrudWebApi.Controllers
{
    public class VersionController : ApiController
    {
        // GET version
        public IHttpActionResult Get(int id)
        {
            return Ok(new VersionRepresentation()
            {
                Id = id
            });
        }

    }
}
