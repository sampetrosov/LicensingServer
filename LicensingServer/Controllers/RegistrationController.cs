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
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace LicensingServer.Controllers
{
    public class RegistrationController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> RegisterAsync([FromBody]User inputData)
        {
            AuthorizationToken result = await Task<AuthorizationToken>.Factory.StartNew(
                (a) =>
                {
                    User user = a as User;
                    try
                    {
                        using (LicensingServerDB dbContext = new LicensingServerDB())
                        {
                            user = dbContext.Users.Add(user);
                            dbContext.SaveChanges();
                            AuthorizationToken authorizationToken = new AuthorizationToken
                            {
                                UserID = user.UserID,
                                ExpirationDate = DateTime.Today.AddDays(1)
                            };
                            authorizationToken.GenerateTokenValue();
                            authorizationToken = dbContext.AuthorizationTokens.Add(authorizationToken);
                            dbContext.SaveChanges();
                            return authorizationToken;
                        }
                    }
                    catch
                    {

                    }
                    return null;
                },
                inputData);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IHttpActionResult> GetTokenAsync([FromBody]User inputData)
        {
            AuthorizationToken result = await Task<AuthorizationToken>.Factory.StartNew(
                (a) =>
                {
                    User user = a as User;
                    try
                    {
                        using (LicensingServerDB dbContext = new LicensingServerDB())
                        {
                            user = dbContext.Users.FindAsync(user.UserID).Result;
                            AuthorizationToken authorizationToken = new AuthorizationToken
                            {
                                UserID = user.UserID,
                                ExpirationDate = DateTime.Today.AddDays(1)
                            };
                            authorizationToken.GenerateTokenValue();
                            authorizationToken = dbContext.AuthorizationTokens.Add(authorizationToken);
                            dbContext.SaveChanges();
                            return authorizationToken;
                        }
                    }
                    catch
                    {

                    }
                    return null;
                },
                inputData);
            return Ok(result);
        }
    }
}