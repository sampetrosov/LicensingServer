// Copyright 2018 Samvel Petrosov.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using LicensingServer.Engine;
using LicensingServer.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace LicensingServer.Attributes
{
    public class OrdinarUserAuthorizationAttribute : AuthorizeAttribute
    {
        public override bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            var tokenValue = actionContext.Request.Headers.Authorization.Parameter;
            using (LicensingServerDB dbContext = new LicensingServerDB())
            {
                var findedTokens = dbContext.AuthorizationTokens.Where(x => x.TokenValue == tokenValue);
                if (findedTokens.Any())
                {
                    AuthorizationToken authorizationToken = findedTokens.OrderByDescending(x => x.ExpirationDate).First();
                    if (authorizationToken.ExpirationDate < DateTime.Today)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                        return;
                    }
                    return;
                }
            }
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            TaskFactory taskFactory = new TaskFactory(cancellationToken);
            return taskFactory.StartNew(async () =>
            {
                await base.OnAuthorizationAsync(actionContext, cancellationToken);
                var tokenValue = actionContext.Request.Headers.Authorization.Parameter;
                using (LicensingServerDB dbContext = new LicensingServerDB())
                {
                    var findedTokens = dbContext.AuthorizationTokens.Where(x => x.TokenValue == tokenValue);
                    if (findedTokens.Any())
                    {
                        AuthorizationToken authorizationToken = findedTokens.OrderByDescending(x => x.ExpirationDate).First();
                        if (authorizationToken.ExpirationDate < DateTime.Today)
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                            return;
                        }
                        return;
                    }
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            });
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var tokenValue = actionContext.Request.Headers.Authorization.Parameter;
            using (LicensingServerDB dbContext = new LicensingServerDB())
            {
                var findedTokens = dbContext.AuthorizationTokens.Where(x => x.TokenValue == tokenValue);
                if (findedTokens.Any())
                {
                    AuthorizationToken authorizationToken = findedTokens.OrderByDescending(x => x.ExpirationDate).First();
                    if (authorizationToken.ExpirationDate < DateTime.Today)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}