﻿using RssAgregator.Models.Enums;
using System;

namespace RssAgregator.Models.Services
{
    public class NewsItemModel
    {
        public string PostName { get; set; }

        public string PostTags { get; set; }

        public bool AdultContent { get; set; }

        public string PostContent { get; set; }

        public int UserId { get; set; }

        public DateTime ModificationDate { get; set; }

        public bool IsNewOne { get; set; }

        public string PostId { get; set; }

        public Locaion? Locaion { get; set; }
    }
}
