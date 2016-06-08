﻿using System;
using System.ComponentModel.DataAnnotations;
using Bit.Api.Utilities;
using Bit.Core.Domains;
using Newtonsoft.Json;

namespace Bit.Api.Models
{
    public class SiteRequestModel
    {
        [StringLength(36)]
        public string FolderId { get; set; }
        [Required]
        [EncryptedString]
        [StringLength(300)]
        public string Name { get; set; }
        [Required]
        [EncryptedString]
        [StringLength(5000)]
        public string Uri { get; set; }
        [EncryptedString]
        [StringLength(200)]
        public string Username { get; set; }
        [Required]
        [EncryptedString]
        [StringLength(300)]
        public string Password { get; set; }
        [EncryptedString]
        [StringLength(5000)]
        public string Notes { get; set; }

        public Cipher ToCipher(string userId = null)
        {
            return new Cipher
            {
                UserId = new Guid(userId),
                FolderId = string.IsNullOrWhiteSpace(FolderId) ? null : (Guid?)new Guid(FolderId),
                Data = JsonConvert.SerializeObject(new CipherDataModel(this), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                Type = Core.Enums.CipherType.Site
            };
        }

        public Cipher ToCipher(Cipher existingSite)
        {
            existingSite.FolderId = string.IsNullOrWhiteSpace(FolderId) ? null : (Guid?)new Guid(FolderId);
            existingSite.Data = JsonConvert.SerializeObject(new CipherDataModel(this), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            existingSite.Type = Core.Enums.CipherType.Site;

            return existingSite;
        }
    }
}