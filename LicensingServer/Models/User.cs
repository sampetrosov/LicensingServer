﻿// Copyright 2018 Samvel Petrosov.
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
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LicensingServer.Models
{
    public class User
    {
        private string _Password;
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password {
            get { return _Password; }
            set {
                _Password = SHA512.Create().ComputeHash(Encoding.ASCII.GetBytes(value)).Select(x=> Convert.ToChar(x).ToString()).Aggregate((x,y)=> x+y);
            }
        }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public bool IsAdmin { get; set; } = false;       
    }
}