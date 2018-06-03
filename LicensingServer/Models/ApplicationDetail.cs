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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicensingServer.Models
{
    public class ApplicationDetail
    {
        [ForeignKey("Application")]
        public int ApplicationID { get; set; }
        [Key]
        public int DetailID { get; set; }
        public string DetailName { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }

        public Application Application { get; set; }
    }
}