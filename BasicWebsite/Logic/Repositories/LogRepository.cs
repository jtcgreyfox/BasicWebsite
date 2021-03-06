﻿using Data.NoSql.Interfaces;
using Logic.Interfaces;

namespace Logic.Repositories
{

    //this was made before LoggingHelper but more oriented towards request/response rather than any json document
    public class LogRepository : ILogRepository
    {
        private const string REPOSITORY_COLLECTION_NAME = "Website";

        private INoSQLDataProvider _database;

        public LogRepository(INoSQLDataProvider database)
        {
            _database = database;
        }

        public void Log(System.Web.Mvc.ActionExecutingContext sender)
        {
            var log = NewDynamicLog();

            log.Page = sender.HttpContext.Request.Url.AbsoluteUri;
            log.HttpMethod = sender.HttpContext.Request.HttpMethod;
            log.HttpDirection = "Request";
            log.ViewModel = sender.Controller.ViewData.Model;

            _database.WriteDocument(REPOSITORY_COLLECTION_NAME, log);
        }

        public void Log(System.Web.Mvc.ActionExecutedContext sender)
        {
            var log = NewDynamicLog();

            log.Page = sender.HttpContext.Request.Url.AbsoluteUri;
            log.HttpMethod = sender.HttpContext.Request.HttpMethod;
            log.HttpDirection = "Response";
            log.ViewModel = sender.Controller.ViewData.Model;

            _database.WriteDocument(REPOSITORY_COLLECTION_NAME, log);
        }

        public void Log(dynamic sender)
        {
            var log = NewDynamicLog();

            log.SenderData = sender;

            _database.WriteDocument(REPOSITORY_COLLECTION_NAME, log);
        }

        private dynamic NewDynamicLog()
        {
            dynamic log = new System.Dynamic.ExpandoObject();

            return log;
        }
    }
}
