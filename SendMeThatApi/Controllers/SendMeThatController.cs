using SendMeThatApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SendMeThatApi.Controllers
{
    public class SendMeThatController : ApiController
    {
        private SendMeThatDbEntities DbEntity;

        public SendMeThatController()
        {
            this.DbEntity = new SendMeThatDbEntities();
        }

        // GET api/values
        public IEnumerable<SendMeThatTable> Get(string ReceiversEmail)
        {
            return DbEntity.SendMeThatTables.Where(row => row.ReceiversEmail == ReceiversEmail).OrderByDescending(row => row.SharedDate);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]SendMeThatTable table)
        {
            DbEntity.SendMeThatTables.Add(table);
            DbEntity.SaveChanges();
            return new HttpResponseMessage { Content = new StringContent("[{\"Success\":\"Success\"},{\"Message\":\"Code sent\"}],{\"Data\":"+table.ReceiversEmail+"}", System.Text.Encoding.UTF8, "application/json") };
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
