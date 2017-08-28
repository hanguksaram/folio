namespace ERP.SqlServer.Migrations
{
    using Core.Models;
    using Core.HistoryStuff;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Core.Enums;
    using Omu.ValueInjecter;
    using Newtonsoft.Json;

    internal sealed class Configuration : DbMigrationsConfiguration<ERP.SqlServer.Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ERP.SqlServer.Db";
        }

        protected override void Seed(ERP.SqlServer.Db context)
        {
            #region Comment
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #endregion

            #region BaseEntity
            var baseEntity = new BaseEntity {
                CreateDate = new DateTime(2017, 1, 10),
                CreatedBy = "Unknown",
                IsDeleted = false,
                ModifyBy = "Unknown",
                ModifyDate = new DateTime(2017, 1, 10)
            };
            #endregion

            #region Properties
            var skypePropType = new PropertyType { PropertyTypeName = "Skype" };
            skypePropType.InjectFrom(baseEntity);
            var phonePropType = new PropertyType { PropertyTypeName = "Phone" };
            phonePropType.InjectFrom(baseEntity);
            var emailPropType = new PropertyType { PropertyTypeName = "E-mail" };
            emailPropType.InjectFrom(baseEntity);
            var addEmailPropType = new PropertyType { PropertyTypeName = "Additional e-mail" };
            addEmailPropType.InjectFrom(baseEntity);

            var prop5 = new Property {
                PropertyType = new PropertyType { PropertyTypeName = "Telegram" },
                PropertyValue = "+79998884455",
            };
            prop5.InjectFrom(baseEntity);
            prop5.PropertyType.InjectFrom(baseEntity);

            var prop6 = new Property {
                PropertyType = new PropertyType { PropertyTypeName = "Is student" },
                PropertyValue = "true",
            };
            prop6.InjectFrom(baseEntity);
            prop6.PropertyType.InjectFrom(baseEntity);

            var prop7 = new Property {
                PropertyType = new PropertyType { PropertyTypeName = "Has auto" },
                PropertyValue = "false",
            };
            prop7.InjectFrom(baseEntity);
            prop7.PropertyType.InjectFrom(baseEntity);

            var prop8 = new Property {
                PropertyType = new PropertyType { PropertyTypeName = "Has home net" },
                PropertyValue = "true",
            };
            prop8.InjectFrom(baseEntity);
            prop8.PropertyType.InjectFrom(baseEntity);

            var prop9 = new Property
            {
                PropertyType = new PropertyType { PropertyTypeName = "Address" },
                PropertyValue = "Academ city",
            };
            prop9.InjectFrom(baseEntity);
            prop9.PropertyType.InjectFrom(baseEntity);
            #endregion

            #region Skills
            var skills = new List<Skill> {
                new Skill { SkillName = "C#" },
                new Skill { SkillName = "HTML" },
                new Skill { SkillName = "CSS/CSS3" },
                new Skill { SkillName = "SQL" },
                new Skill { SkillName = "Java" },
                new Skill { SkillName = "ObjC" },
                new Skill { SkillName = "Swift" },
                new Skill { SkillName = "PHP" },
                new Skill { SkillName = "VB.NET" },
                new Skill { SkillName = "Perl" },
                new Skill { SkillName = "GoLang" },
                new Skill { SkillName = "Python" },
                new Skill { SkillName = "Ruby" },
                new Skill { SkillName = "TypeScript" },
                new Skill { SkillName = "ES6" },
                new Skill { SkillName = "VBA" },
                new Skill { SkillName = "Bash" },
                new Skill { SkillName = "C/C++" },
                new Skill { SkillName = "ASM" }, // Programming Languages
                new Skill { SkillName = "ASP.NET" }, 
                new Skill { SkillName = "MVC" }, 
                new Skill { SkillName = "WebForms" }, 
                new Skill { SkillName = "WinForms" }, 
                new Skill { SkillName = "Silverlight" }, 
                new Skill { SkillName = "NancyFx" }, 
                new Skill { SkillName = ".NET Core" }, 
                new Skill { SkillName = "EF" }, 
                new Skill { SkillName = "Linq2SQL" }, 
                new Skill { SkillName = "NHibernate" }, // Microsoft Technologies
                new Skill { SkillName = "XML" },
                new Skill { SkillName = "REST/WebAPI" },
                new Skill { SkillName = "NodeJS" },
                new Skill { SkillName = "Cordova" },
                new Skill { SkillName = "Django" },
                new Skill { SkillName = "WSGI" },
                new Skill { SkillName = "Flask" },
                new Skill { SkillName = "Yii" },
                new Skill { SkillName = "Alembic" },
                new Skill { SkillName = "Spring MVC" },
                new Skill { SkillName = "OAuth 2.0" },
                new Skill { SkillName = "ElasticSearch" },
                new Skill { SkillName = "SignalR" },
                new Skill { SkillName = "Hibernate" },
                new Skill { SkillName = "Docker" },
                new Skill { SkillName = "Fabric" },
                new Skill { SkillName = "Microservices" },
                new Skill { SkillName = "CQRS" },
                new Skill { SkillName = "XSLT" },
                new Skill { SkillName = "OpenCV" },
                new Skill { SkillName = "Google APIs" },
                new Skill { SkillName = "nginx" },
                new Skill { SkillName = "memcacheD" },
                new Skill { SkillName = "Redis" },
                new Skill { SkillName = "MSMQ" },
                new Skill { SkillName = "RabbitMQ" }, // Backend Technologies
                new Skill { SkillName = "AngularJS" },
                new Skill { SkillName = "Knockout" },
                new Skill { SkillName = "Jquery" },
                new Skill { SkillName = "Bootstrap" },
                new Skill { SkillName = "Redux" },
                new Skill { SkillName = "React" },
                new Skill { SkillName = "LESS" },
                new Skill { SkillName = "SASS" },
                new Skill { SkillName = "Gulp" },
                new Skill { SkillName = "Handlebars" },
                new Skill { SkillName = "Meteor" },
                new Skill { SkillName = "UIKit" },
                new Skill { SkillName = "Mustache" },
                new Skill { SkillName = "JSAnimation" }, // Frontend Technologies
                new Skill { SkillName = "Android SDK" }, 
                new Skill { SkillName = "xCode" }, 
                new Skill { SkillName = "CoreData" }, 
                new Skill { SkillName = "Titanuim" }, 
                new Skill { SkillName = "Xamarin" }, // Mobile Technologies
                new Skill { SkillName = "Win" },
                new Skill { SkillName = "Linux" },
                new Skill { SkillName = "iOS" },
                new Skill { SkillName = "Android" },
                new Skill { SkillName = "WinPhone" },
                new Skill { SkillName = "MacOS" },
                new Skill { SkillName = "AWS" },
                new Skill { SkillName = "Azure" },
                new Skill { SkillName = "Google Cloud" },
                new Skill { SkillName = "BPMonline" }, // Platforms
                new Skill { SkillName = "MS SQL" },
                new Skill { SkillName = "mySQL" },
                new Skill { SkillName = "Oracle" },
                new Skill { SkillName = "postgress" },
                new Skill { SkillName = "sqlite" },
                new Skill { SkillName = "mongoDB" }, // Databases
                new Skill { SkillName = "Testing" },
                new Skill { SkillName = "Design" },
                new Skill { SkillName = "Usability" },
                new Skill { SkillName = "BA" },
                new Skill { SkillName = "Balsamiq" },
                new Skill { SkillName = "Cacoo" },
                new Skill { SkillName = "Architecture" },
                new Skill { SkillName = "Unit Testing" },
                new Skill { SkillName = "Perf Testing" },
                new Skill { SkillName = "Cont Integr" },
                new Skill { SkillName = "Mach Learng" },
                new Skill { SkillName = "SEO" },
                new Skill { SkillName = "Embedded" },
                new Skill { SkillName = "Additional" }, // Other
                new Skill { SkillName = "English" },
                new Skill { SkillName = "German" },
                new Skill { SkillName = "French" },
                new Skill { SkillName = "Spanish" },
                new Skill { SkillName = "Chinese" },
                new Skill { SkillName = "Japanese" }, // Foreign Languages
            };
            foreach (var skill in skills)
                skill.InjectFrom(baseEntity);
            #endregion

            #region GroupOfSkills
            var groupsOfSkills = new List<GroupOfSkills>
            {
                new GroupOfSkills { GroupName = "Programming Languages", Skills = skills.GetRange(0, 19) },
                new GroupOfSkills { GroupName = "Microsoft Technologies", Skills = skills.GetRange(19, 10) },
                new GroupOfSkills { GroupName = "Backend Technologies", Skills = skills.GetRange(29, 26) },
                new GroupOfSkills { GroupName = "Frontend Technologies", Skills = skills.GetRange(55, 14) },
                new GroupOfSkills { GroupName = "Mobile Technologies", Skills = skills.GetRange(69, 5) },
                new GroupOfSkills { GroupName = "Platforms", Skills = skills.GetRange(74, 10) },
                new GroupOfSkills { GroupName = "Databases", Skills = skills.GetRange(84, 6) },
                new GroupOfSkills { GroupName = "Other", Skills = skills.GetRange(90, 14) },
                new GroupOfSkills { GroupName = "Foreign Languages", Skills = skills.GetRange(104, 6) },
            };
            foreach (var group in groupsOfSkills)
            {
                group.InjectFrom(baseEntity);
                context.GroupOfSkills.Add(group);
            }
            #endregion

            #region Certificates
            var certificate1 = new Certificate { CertificateName = "MCSD" };
            certificate1.InjectFrom(baseEntity);
            context.Certificates.Add(certificate1);

            var certificate2 = new Certificate { CertificateName = "bpm’online" };
            certificate2.InjectFrom(baseEntity);
            context.Certificates.Add(certificate2);
            #endregion

            #region Positions
            var developerPosition = new Position { PositionName = "Developer" }; developerPosition.InjectFrom(baseEntity);
            var designerPosition = new Position { PositionName = "Designer" }; designerPosition.InjectFrom(baseEntity);
            #endregion

            #region Employees
            var baseEmployee = new Employee
            {
                MiddleName = null,
                Position = developerPosition,
                BirthDate = new DateTime(1990, 5, 6),
                HiringDate = new DateTime(2015, 10, 11),
                FiredDate = null,
            };
            baseEmployee.InjectFrom(baseEntity);

            var names = new Dictionary<string, string> {
                { "Jack", "Nicholson" },
                { "Ralph", "Fiennes" },
                { "Daniel", "Day-Lewis" },
                { "Robert", "De Niro" },
                { "Al", "Pacino" },
                { "Dustin", "Hoffman" },
                { "Tom", "Hanks" },
                { "Brad", "Pitt" },
                { "Anthony", "Hopkins" },
                { "Marlon", "Brando" },
                { "Jeremy", "Irons" },
                { "Denzel", "Washington" },
                { "Gene", "Hackman" },
                { "Jeff", "Bridges" },
                { "Tim", "Robbins" },
                { "Henry", "Fonda" },
                { "William", "Hurt" },
                { "Kevin", "Costner" },
                { "Clint", "Eastwood" },
                { "Leonardo", "DiCaprio" },
                { "Mel", "Gibson" },
                { "Samuel", "L. Jackson" },
                { "Tommy", "Lee Jones" },
                { "Nicolas", "Cage" },
                { "Morgan", "Freeman" },
                { "Michael", "Caine" },
                { "Russell", "Crowe" },
                { "Bruce", "Willis" },
                { "Johnny", "Depp" },
                { "Ben", "Kingsley" },
                { "Steve", "McQueen" },
                { "Heath", "Ledger" },
                { "Philip Seymour", "Hoffman" },
                { "John", "Malkovich" },
                { "Christian", "Bale" },
                { "Jason", "Robards" },
                { "Colin", "Firth" },
                { "George", "Clooney" },
                { "Edward", "Norton" },
                { "Sean", "Connery" },
                { "Yves", "Montand" },
                { "Richard", "Gere" },
                { "Gary", "Oldman" },
                { "Harrison", "Ford" },
                { "Matt", "Damon" }
            };

            var employees = new List<Employee>(50);
            Random rnd = new Random();
            int countSkills = 15;
            foreach (var name in names)
            {
                var emp = new Employee().InjectFrom(baseEmployee) as Employee;
                emp.FirstName = name.Key;
                emp.LastName = name.Value;
                var rndList = new List<int>(countSkills);
                while (rndList.Count < countSkills)
                {
                    int i = rnd.Next(0, skills.Count);
                    if (rndList.Contains(i))
                        continue;
                    else
                        rndList.Add(i);
                }
                emp.Skills = new List<EmployeeToSkill>(3);
                foreach (var i in rndList)
                    emp.Skills.Add(new EmployeeToSkill { Level = (Level)rnd.Next(0, 6), Preference = (Preference)rnd.Next(0, 3), Skill = skills[i] });
                foreach (var s in emp.Skills)
                    s.InjectFrom(baseEntity);
                emp.Properties = new List<Property>
                {
                    new Property { PropertyValue = String.Format("{0}.{1}", emp.FirstName, emp.LastName), PropertyType = skypePropType },
                    new Property { PropertyValue = String.Format("{0}@gamial.com", emp.LastName), PropertyType = emailPropType },
                    new Property { PropertyValue = String.Format("{0}@live.com", emp.LastName), PropertyType = addEmailPropType },
                    new Property { PropertyValue = String.Format("555-55-{0,2}", rnd.Next(0, 100)), PropertyType = phonePropType },
                };
                foreach (var prop in emp.Properties)
                    prop.InjectFrom(baseEntity);
                employees.Add(emp);
            }

            employees[0].Properties.Add(prop5);
            employees[0].Properties.Add(prop6);
            employees[0].Properties.Add(prop7);
            employees[0].Properties.Add(prop8);
            employees[0].Properties.Add(prop9);

            employees[1].Position = designerPosition;

            foreach (var employee in employees)
            {
                var history = new HistoryEmployee {
                    SerializedEmployee = JsonConvert.SerializeObject(employee.MapToEmployeeHistory()),
                    Employee = employee
                };
                history.InjectFrom(baseEntity);
                context.History.Add(history);
                context.Employees.Add(employee);
            }
                
            #endregion

            #region Users
            var company = new Company
            {
                Address1 = "Address1",
                Address2 = "Address1",
                City = "City",
                Country = "Country",
                Domains = "Domains",
                Email = "Domains",
                EmailTemplate = "EmailTemplate",
                EmailTitle = "EmailTitle",
                Logo = "Logo",
                // Managers
                Name = "Name",
                Phone = "Phone",
                State = "State",
                // TimeZone
                WebSite = "WebSite",
                Zip = "Zip",
            };
            company.InjectFrom(baseEntity);
            var user = new User {
                Email = "Email",
                FirstName = "FirstName",
                IsSa = false,
                LastName = "LastName",
                Password = "Password",
                Salt = "Salt",
                Title = "Title",
                Companies = new HashSet<Company> { company }
            };
            user.InjectFrom(baseEntity);
            context.Users.AddOrUpdate(user);
            #endregion

            context.SaveChanges();
        }
    }
}
