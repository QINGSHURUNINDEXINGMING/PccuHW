using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HW04.Models;

namespace HW04.Controllers
{
    public class APIController : ApiController
    {
        #region
        private class ServiceResult
        {
            public string Status { get; set; }
            public string APIResult { get; set; }

            ServiceResult serviceObj;
            Service db = new Service();
        }
        #endregion

        #region
        public ServiceController()
        {
            try
            {
                string[] initialTypes = { "慢走", "慢跑", "快走", "快跑" };
                AddKind addkind = new AddKind();
                IEnumerator<AddKind> addKinds = db.AddKinds;
            }
            catch
            {


            }
        }
        #endregion










    }
}
