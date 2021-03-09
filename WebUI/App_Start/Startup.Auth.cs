using System;
using System.Data.Entity.Migrations;
using WebUI.Models;

namespace WebUI
    {


    internal sealed class Configuration : DbMigrationsConfiguration<Domain.Entities.AbzContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }


    public partial class Startup
    {
        public void ConfigureAuth()
        {

        }
    }
}