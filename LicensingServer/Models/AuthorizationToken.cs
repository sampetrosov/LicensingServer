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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Text;
using LicensingServer.Engine;

namespace LicensingServer.Models
{
    public class AuthorizationToken
    {
        [Key]
        public int TokenID { get; set; }
        public int ApplicationID { get; set; }
        public int UserID { get; set; }
        [Required]
        public string TokenValue { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }

        public void GenerateTokenValue()
        {
            TokenValue = RijndaelSimple.Encrypt(UserID + ExpirationDate.ToString("MMddyyyy hh:mm:ss"));
        }

    }
}