using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FilterIT.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public byte[] Data { get; set; }
    }

    public class ImageDBContext : DbContext
    {
        public DbSet<Image> Images { get; set; }
    }
}